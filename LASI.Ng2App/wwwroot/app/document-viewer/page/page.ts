import { Component, OnInit } from 'angular2/core';
import { Input } from 'app/ng2-utils';
import template from './page.html';
import { ParagraphComponent } from 'app/document-viewer/components';

@Component({
    selector: 'document-page',
    directives: [ParagraphComponent],
    template
})
export class DocumentPageComponent implements OnInit { 
    constructor() { } 
 
    ngOnInit() { } 
    @Input document;
    @Input pageData;
}