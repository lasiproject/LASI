import '../../styles/lexical.css!css';
import {bindable, autoinject} from 'aurelia-framework';
import DocumentModelService from './document-model-service';

@autoinject
export class DocumentViewer {
    //constructor(private documentModelService: DocumentModelService) { }

    //async activate() {
      //  this.document = await this.documentModelService.processDocument(4);
    //}

    @bindable document: models.DocumentModel;
}