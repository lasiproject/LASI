import { OnInit } from '@angular/core';
import { component, input } from 'ng2-conventions-decorators';
import { DocumentModelService } from './document-model-service';
import template from './document-viewer.html';

@component(template) export class DocumentViewerComponent implements OnInit {
    constructor(readonly documentModelService: DocumentModelService) { }
    @input document: models.DocumentModel;

    ngOnInit() {
        this.documentModelService.processDocument(4).subscribe(document => this.document = document);
    }
}