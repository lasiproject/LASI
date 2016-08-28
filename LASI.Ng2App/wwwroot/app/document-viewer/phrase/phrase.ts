import { OnInit } from '@angular/core';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import { component, input } from 'ng2-conventions-decorators';
import template from './phrase.html';

@component(template) export class PhraseComponent implements OnInit {
    constructor(readonly lexicalMenuBuilder: LexicalMenuBuilder) {}
    ngOnInit() {
        var contextmenu = this.lexicalMenuBuilder.buildAngularMenu(this.phrase.contextmenu);
        this.phrase.hasContextmenuData = !!contextmenu;
        if (this.phrase.hasContextmenuData) {
            this.phrase.contextmenuDataSource = this.phrase.contextmenu;
            this.phrase.contextmenu = contextmenu;
        }

    }
    @input phrase: models.PhraseModel;
    @input parentId: string;
}