import {autoinject, bindable} from 'aurelia-framework';
import LexicalMenuBuilder from '../lexical-menu-builder';

@autoinject
export class Phrase {
    constructor(private lexicalMenuBuilder: LexicalMenuBuilder) { }
    
    activate() {
        var contextmenu = this.lexicalMenuBuilder.buildAngularMenu(this.phrase.contextmenu);
        this.phrase.hasContextmenuData = !!contextmenu;
        if (this.phrase.hasContextmenuData) {
            this.phrase.contextmenuDataSource = this.phrase.contextmenu;
            this.phrase.contextmenu = contextmenu;
        }

    }
    @bindable phrase: models.PhraseModel;
    @bindable parentId: string;
}