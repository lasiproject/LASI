import { Injectable } from 'app/ng2-utils';
import { Http } from 'angular2/http';

@Injectable export default class DocumentModelService implements models.DocumentModelService {
    constructor(private http: Http) { }

    processDocument(documentId) {
        return this.http.get('/test/doc.json').map(response => response.json() as models.DocumentModel);
        // return this.http.get(`Analysis/${documentId}`).map(response => response.json() as models.DocumentModel);
    }
}