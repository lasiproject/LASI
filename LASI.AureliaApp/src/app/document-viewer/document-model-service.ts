import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {DocumentModel} from 'src/models';

@autoinject export default class DocumentModelService {
    constructor(private http: HttpClient) { }

    async processDocument(documentId) {
        const response = await this.http.configure(x => x.withBaseUrl('/')).fetch('src/app/test/doc.json');
        const json = await response.json();
        return json as DocumentModel;
        // return this.http.get(`Analysis/${documentId}`).map(response => response.json() as models.DocumentModel);
    }
}