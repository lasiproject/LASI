import { Http } from '@angular/http';
import { injectable } from 'ng2-conventions-decorators';

@injectable export class ListService {
    constructor(readonly http: Http) { }

    getById(documentId: string) {
        return this.http
            .get(`/api/UserDocuments/${documentId}`)
            .map(response => response.json() as models.DocumentListItem);
    }

    deleteById(documentId: string) {
        return this.http.delete(`/api/UserDocuments/${documentId}`);
    }
}