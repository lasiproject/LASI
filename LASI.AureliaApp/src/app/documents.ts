import { autoinject, bindable } from 'aurelia-framework';
import DocumentModelService from './services/document-model-service';
import { DocumentModel } from 'app/models';

@autoinject export class Documents {
  constructor(readonly documentModelService: DocumentModelService) { }

  async activate() {
    this.document = await this.documentModelService.processDocument(4);
  }

  @bindable document: DocumentModel;
}