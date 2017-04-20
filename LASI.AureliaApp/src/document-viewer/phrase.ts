import { autoinject, bindable } from 'aurelia-framework';
import Contextmenu, { ContextMenuOptions } from 'shared/context-menu/context-menu';
import { PhraseModel } from 'models';
import LexicalMenuBuilder from 'app/services/lexical-menu-builder';

@autoinject export class Phrase {

  @bindable phrase: PhraseModel;
  @bindable parentId: string;

  constructor(readonly lexicalMenuBuilder: LexicalMenuBuilder) { }

  bind(bindingContext: {}, overrideContext: {}) {
    const contextmenu = this.lexicalMenuBuilder.build(this.phrase.contextmenu)!;
    this.phrase.hasContextmenuData = !!contextmenu;
    if (this.phrase.hasContextmenuData) {
      this.phrase.contextmenuDataSource = this.phrase.contextmenu;
      this.phrase.contextmenu = contextmenu;
    }

  }
}
