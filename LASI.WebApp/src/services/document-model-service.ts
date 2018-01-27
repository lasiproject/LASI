import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import DocumentModel from 'app/models/document-model';

@autoinject export default class DocumentService {
  constructor(readonly http: HttpClient) { }

  async processDocument(instructions: { documentId: number }) {
    const response = await this.http.configure(x => x.withBaseUrl('/')).fetch('doc.json');
    return await response.json() as DocumentModel;
  }
}
