import '../../styles/lexical.css!css';
import {bindable, autoinject, subscriberCollection} from 'aurelia-framework';
import DocumentModelService from './document-model-service';
import {DocumentModel} from 'src/models';
@autoinject export class DocumentViewer {
  //constructor(private documentModelService: DocumentModelService) { }

  @bindable document: DocumentModel;
  bind() {
    this.words = document && this.document && this.document.paragraphs.flatMap(p => p.sentences).flatMap(s => s.phrases).flatMap(s => s.words) || [];
  }
  words: any[];
  @bindable searchTerm: string;
}