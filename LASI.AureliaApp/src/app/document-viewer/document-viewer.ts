import { bindable, autoinject } from 'aurelia-framework';
import { DocumentModel } from 'src/models';
export class DocumentViewer {

  @bindable document: DocumentModel;
  bind() {
    this.typeAheadSource = this.document && this.document.paragraphs
      .flatMap(p => p.sentences)
      .flatMap(s => s.phrases)
      .flatMap(s => s.words)
      .map(w => w.text) || [];
  }
  @bindable typeAheadSource: any[];
  @bindable searchTerm: string;
  queryTypeAhead(query, callback) {
    console.log(query);
    console.log(callback);
    return callback(query.target.value);
  }

  search = (query) => {
    return this.typeAheadSource.filter(x => x.text === query);
  }
}