import { Injectable } from 'angular2/core';
import { Http } from 'angular2/http';
import 'rxjs/Rx';

@Injectable()
export class ListService {
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