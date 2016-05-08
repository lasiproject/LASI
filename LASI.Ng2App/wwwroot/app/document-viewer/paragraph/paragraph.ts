import { Component } from 'angular2/core';
import {Input } from 'app/ng2-utils';
import template from './paragraph.html';
import { SentenceComponent } from 'app/document-viewer/components';

@Component({
    selector: 'paragraph',
    directives: [SentenceComponent],
    template
})
export class ParagraphComponent {
    @Input paragraph;
    @Input parentId;
} 