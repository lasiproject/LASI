import { autoinject, bindable } from 'aurelia-framework';
import DocumentModelService from 'src/app/services/document-model-service';
import { DocumentModel } from 'src/models';

@autoinject export class Documents {
  constructor(readonly documentModelService: DocumentModelService) { }

  async activate() {
    this.document = await this.documentModelService.processDocument(4);
  }

  @bindable document: DocumentModel;
}