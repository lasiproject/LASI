import { Http } from 'angular2/http';
import { Injectable } from './ng2-utils';

@Injectable
export default class ListService {
    constructor(private http: Http) { }

    getById(documentId: string) {
        return this.http
            .get(`/api/UserDocuments/${documentId}`)
            .map(response => response.json() as models.DocumentListItem);
    }

    deleteById(documentId: string) {
        return this.http.delete(`/api/UserDocuments/${documentId}`);
    }
}