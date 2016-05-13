import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@autoinject export default class DocumentModelService implements models.DocumentModelService {
    constructor(private http: HttpClient) { }

    async processDocument(documentId) {
        var response = await this.http.fetch('src/app/test/doc.json');
        return await response.json() as models.DocumentModel;
        // return this.http.get(`Analysis/${documentId}`).map(response => response.json() as models.DocumentModel);
    }
}