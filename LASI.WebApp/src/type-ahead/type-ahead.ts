//import { ViewCompiler, useView, customElement, bindable } from 'aurelia-framework';
//import { TemplateRegistryEntry, Loader } from 'aurelia-loader';
//import { HttpClient } from 'aurelia-fetch-client';
//import { Observable } from 'rxjs/Observable';
//import 'rxjs/add/operator/distinctUntilChanged';
//import 'rxjs/add/operator/debounce';
//import 'rxjs/add/observable/from';
//import 'rxjs/add/observable/of';
//import 'rxjs/add/operator/map';
//import 'rxjs/add/operator/mergeMap';
//import 'rxjs/add/operator/switchMap';
//import 'rxjs/add/operator/throttle';
//import 'rxjs/add/operator/debounceTime';
//import 'rxjs/add/operator/buffer';
//import 'rxjs/add/operator/bufferWhen';
//import 'typeahead';
//import $ from 'jquery';

//@customElement('type-ahead') export default class TypeAhead {
//  constructor(readonly element: Element, readonly loader: Loader) { }

//  @bindable value = '';
//  @bindable sourceArray: { text: string }[];
//  @bindable options: TypeAheadOptions<{ text: string }>;

//  async searchTerm(term: {}) {
//    Observable.of(1, 2, 3).throttle
//    const results = this.sourceArray.filter(x => x.text === term);
//    return results;
//  }
//  bind(bindingContext: object, overrideContext: object) {
//    console.log(bindingContext);
//    console.log(overrideContext);
//  }

//  async getSuggestions() {
//    //var keyup = Rx.Observable.fromEvent($(this.element).find('input'), 'keyup')
//    const keyup = Observable.from(this.sourceArray)
//      .map(e => {
//        return e.text; // Project the text from the input
//      })
//      .debounce(async () => await 750)
//      .distinctUntilChanged(); // Only if the value has changed

//    const searcher = keyup
//      .switchMap(term => this.searchTerm(term))
//      .flatMap(searches => searches);

//    this.searcher = searcher;

//    return (query: {}, syncResults: ({ }) => {}, asyncResults: ({ }) => {}) => {
//      const subscription = this.searcher.subscribe(
//        (data) => {
//          return asyncResults(data);
//        },
//        function (error) {
//          return asyncResults([]);
//        }
//      );
//    };
//  }

//  async attached() {
//    //const options: Options = {
//    //  hint: true,
//    //  async: true,
//    //  highlight: true,
//    //  minLength: 2,
//    //  display: 'text'
//    //};

//    let templateRegistryEntry;
//    try {
//      templateRegistryEntry = await this.loader.loadTemplate('./type-ahead.html');
//    } catch (e) {
//      templateRegistryEntry = await this.loader.loadTemplate('./type-ahead.html');
//    }

//    //this.typeAhead = $(this.element).find('input').typeahead(this.options);

//    //this.typeAhead.on('typeahead:selected', (event: Event, datum, name) => {
//    //  event = new CustomEvent('selected', {
//    //    detail: {
//    //      value: datum
//    //    },
//    //    bubbles: true
//    //  });
//    //  this.element.dispatchEvent(event);
//    //});
//    //this.typeAhead.on('typeahead:autocomplete', (event: Event, datum, name) => {
//    //  event = new CustomEvent('autocomplete', {
//    //    detail: {
//    //      value: datum
//    //    },
//    //    bubbles: true
//    //  });
//    //  this.element.dispatchEvent(event);
//    //});
//    // Typeahead will trigger an 'open' if input gains focus to display suggestions
//    // This in turn triggers antoher async call
//    //this.typeAhead.on('typeahead:asyncrequest', (event, query, name) => {
//    //  this.isSearching = true;
//    //});
//    //this.typeAhead.on('typeahead:asyncreceive', (event, query, name) => {
//    //  this.isSearching = false;
//    //});
//    //this.typeAhead.on('typeahead:asynccancel', (event: Event, query, name) => {
//    //  this.isSearching = false;
//    //});

//  }

//  async valueChanged(newValue: string, oldValue: {}) {
//    console.log('valueChanged', newValue);
//    const results = this.options(newValue, () => this.sourceArray, () => []);
//    console.log(results);
//  }

//  optionsChanged(newValue: {}, oldValue: {}) {
//    console.log('optionsChanged', newValue);
//  }

//  detached() {
//    //$(this.element).typeahead('destroy');
//  }
//  searcher: Observable<{ text: string }>;

//  // (event: string, handler: (event: JQueryEventObject, query, name: string) => void, ...rest) => void | Promise<void>;
//  typeAhead: JQuery;

//  isSearching = false;
//}
//function scoreMatch(term: {}, match: {}): number {
//  return 1; // Stub
//}

//export type TypeAheadOptions<Source> = (query: string, syncResults: (...args: {}[]) => Source[], asyncResults: (...args: {}[]) => Source[]) => {};

////type Options = Twitter.Typeahead.Options & { async: boolean, minLength: number, display: string, options?: Options, sourceArray?: {}[] };
