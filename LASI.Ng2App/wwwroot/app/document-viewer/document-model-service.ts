import { injectable } from 'ng2-conventions-decorators';
import { Http } from '@angular/http';

@injectable export class DocumentModelService {
    constructor(readonly http: Http) { }

    processDocument(documentId?: number | string) {
        return this.http.get('/test/doc.json').map(response => response.json() as models.DocumentModel);
        // return this.http.get(`Analysis/${documentId}`).map(response => response.json() as models.DocumentModel);
    }
}