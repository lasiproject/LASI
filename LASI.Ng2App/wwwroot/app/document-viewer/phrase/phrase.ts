import { Component, OnInit } from 'angular2/core';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import {Input, Injectable} from 'app/ng2-utils';
import template from './phrase.html';

@Component({
    selector: 'phrase',
    template
})
@Injectable
export class PhraseComponent implements OnInit {
    constructor(private lexicalMenuBuilder: LexicalMenuBuilder) {

    }
    ngOnInit() {
        var contextmenu = this.lexicalMenuBuilder.buildAngularMenu(this.phrase.contextmenu);
        this.phrase.hasContextmenuData = !!contextmenu;
        if (this.phrase.hasContextmenuData) {
            this.phrase.contextmenuDataSource = this.phrase.contextmenu;
            this.phrase.contextmenu = contextmenu;
        }

    }
    @Input phrase: models.PhraseModel;
    @Input parentId: string;
}