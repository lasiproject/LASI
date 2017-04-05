import { bindable, autoinject } from 'aurelia-framework';
import { DocumentModel, LexicalModel } from 'models';
export class DocumentViewer {

  @bindable document: DocumentModel;
  @bindable typeAheadSource: LexicalModel[];
  @bindable searchTerm = '';

  bind() {
    this.typeAheadSource = this.document && this.document.paragraphs
      .flatMap(p => p.sentences)
      .flatMap(s => s.phrases)
      .flatMap(s => s.words)
      .map(w => w) || [];
  }

  queryTypeAhead(query: any, callback: any) {
    console.log(query);
    console.log(callback);
    return callback(query.target.value);
  }

  search = (query: {}) => {
    return this.typeAheadSource.filter(x => x.text === query);
  }

  readonly typeaheadOptions = (viewer => new class {
    readonly hint = true;
    readonly async = true;
    readonly highlight = true;
    readonly minLength = 2;
    readonly display = 'text';
    readonly datasets = {
      name: 'my-dataset',
      source: viewer.typeAheadSource
    };
  })(this);
}