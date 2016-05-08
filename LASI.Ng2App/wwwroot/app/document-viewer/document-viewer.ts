import { Component, OnInit } from 'angular2/core';
import { Http } from 'angular2/http';
import { RouteParams } from 'angular2/router';
import { Input } from 'app/ng2-utils';
import DocumentModelService from './document-model-service';
import { ParagraphComponent } from './components';
import template from './document-viewer.html';
import 'rxjs/add/operator/map';
@Component({
    selector: 'document-viewer',
    directives: [ParagraphComponent],
    template
})
export class DocumentViewerComponent implements OnInit {
    constructor(private documentModelService: DocumentModelService, private routeParams: RouteParams) {

    }

    @Input document: models.DocumentModel;

    ngOnInit() {
        this.documentModelService.processDocument(4).subscribe(document => this.document = document);
    }
}