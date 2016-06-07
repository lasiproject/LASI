import '../../styles/lexical.css!css';
import {bindable, autoinject, subscriberCollection} from 'aurelia-framework';
import DocumentModelService from './document-model-service';
import {DocumentModel} from 'src/models';
@autoinject export class DocumentViewer {
  //constructor(private documentModelService: DocumentModelService) { }

  @bindable document: DocumentModel;
  bind() {
    this.typeAheadSource = document && this.document && this.document.paragraphs
      .flatMap(p => p.sentences)
      .flatMap(s => s.phrases)
      .flatMap(s => s.words)
      .map(w => w.text) || [];
  }
  @bindable
  typeAheadSource: any[];
  @bindable
  searchTerm: string;
  queryTypeAhead(query, callback) {
    console.log(query);
    console.log(callback);
  }
}