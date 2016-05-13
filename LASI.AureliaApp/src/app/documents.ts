import {autoinject, bindable} from 'aurelia-framework';
import DocumentModelService from './document-viewer/document-model-service';

@autoinject
export class Documents {
    constructor(private documentModelService: DocumentModelService) { }

    async activate() {
        this.document = await this.documentModelService.processDocument(4);
    }

    document: models.DocumentModel;
}