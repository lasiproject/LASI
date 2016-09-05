import { bindable, autoinject } from 'aurelia-framework';
import { DocumentModel, LexicalModel } from 'src/models';
export class DocumentViewer {

  @bindable document: DocumentModel;
  @bindable typeAheadSource: LexicalModel[];
  @bindable searchTerm = '';

  typeaheadOptions = {
    hint: true,
    async: true,
    highlight: true,
    minLength: 2,
    display: 'text', datasets: {
      name: 'my-dataset',
      source: this.typeAheadSource
    }
  };

  bind() {
    this.typeAheadSource = this.document && this.document.paragraphs
      .flatMap(p => p.sentences)
      .flatMap(s => s.phrases)
      .flatMap(s => s.words)
      .map(w => w) || [];
  }
  queryTypeAhead(query, callback) {
    console.log(query);
    console.log(callback);
    return callback(query.target.value);
  }

  search = (query) => {
    return this.typeAheadSource.filter(x => x.text === query);
  }
}