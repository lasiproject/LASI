import $ from 'jquery';
import { ViewCompiler, useView, ViewResources, autoinject, customElement, Container, bindable } from 'aurelia-framework';
import { TemplateRegistryEntry, Loader } from 'aurelia-loader';
import { HttpClient } from 'aurelia-fetch-client';
import { Observable, Subject, Subscription, Observer } from 'rxjs';
import 'typeahead';

@autoinject @customElement('type-ahead') export class TypeAhead {
  constructor(
    readonly element: Element,
    readonly http: HttpClient,
    readonly loader: Loader,
    readonly viewResources: ViewResources,
    readonly viewCompiler: ViewCompiler,
    readonly container: Container) { }

  async searchTerm(term: any): Promise<any> {
    const results = this.sourceArray.filter(x => x.text === term);
    /*tslint:disable variable-name*/
    return results;
  }
  bind(bindingContext: Object, overrideContext: Object) {
    console.log(bindingContext);
    console.log(overrideContext);

  }

  async getSuggestions() {
    //var keyup = Rx.Observable.fromEvent($(this.element).find('input'), 'keyup')
    const keyup = Observable.of(...this.sourceArray)
      .map(e => {
        return e.text; // Project the text from the input
      })
      .debounce(async () => await 750)
      .distinctUntilChanged(); // Only if the value has changed

    const searcher = keyup.switchMap(this.searchTerm);

    this.searcher = searcher;

    return (query: {}, syncResults: ({ }) => any, asyncResults: ({ }) => any) => {
      const subscription = this.searcher.subscribe(
        (data) => {
          return asyncResults(data);
        },
        function (error) {
          return asyncResults([]);
        }
      );
    };
  }

  async attached() {

    let options: Options = {
      hint: true,
      async: true,
      highlight: true,
      minLength: 2,
      display: 'text'
    };

    let templateRegistryEntry;
    try {
      templateRegistryEntry = await this.loader.loadTemplate('shared/type-ahead/type-ahead.html');
    } catch (e) {
      templateRegistryEntry = await this.loader.loadTemplate('/type-ahead/type-ahead.html');
    }

    this.typeAhead = $(this.element).find('input').typeahead(this.options);

    this.typeAhead.on('typeahead:selected', (event: Event, datum, name) => {
      event = new CustomEvent('selected', {
        detail: {
          value: datum
        },
        bubbles: true
      });
      this.element.dispatchEvent(event);
    });
    this.typeAhead.on('typeahead:autocomplete', (event: Event, datum, name) => {
      event = new CustomEvent('autocomplete', {
        detail: {
          value: datum
        },
        bubbles: true
      });
      this.element.dispatchEvent(event);
    });
    // Typeahead will trigger an 'open' if input gains focus to display suggestions
    // This in turn triggers antoher async call
    this.typeAhead.on('typeahead:asyncrequest', (event, query, name) => {
      this.isSearching = true;
    });
    this.typeAhead.on('typeahead:asyncreceive', (event, query, name) => {
      this.isSearching = false;
    });
    this.typeAhead.on('typeahead:asynccancel', (event: Event, query, name) => {
      this.isSearching = false;
    });

  }

  async valueChanged(newValue, oldValue) {
    console.log('valueChanged', newValue);
    var results = this.options(newValue, () => this.sourceArray);
    console.log(results);
  }

  optionsChanged(newValue, oldValue) {
    console.log('optionsChanged', newValue);
  }

  detached() {
    $(this.element).typeahead('destroy');
  }
  searcher: Observable<{ text }>;

  // (event: string, handler: (event: JQueryEventObject, query, name: string) => void, ...rest) => void | Promise<void>;
  typeAhead: JQuery;

  isSearching = false;
  @bindable value;
  @bindable sourceArray: any[];
  @bindable options: TypeAheadOptions<{ text: string }>;
}
function scoreMatch(term, match): number {
  return 1; // Stub
}

declare module 'aurelia-templating' {
  export interface View {
    fragment: Element | DocumentFragment;
  }
}

export type TypeAheadOptions<Source> = (query: string, syncResults: (...args) => Source[], asyncResults: (...args) => Source[]) => any;

type Options = Twitter.Typeahead.Options & { async: boolean, minLength: number, display: string, options?: Options, sourceArray?: any[] };