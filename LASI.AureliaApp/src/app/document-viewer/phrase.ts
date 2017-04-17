import { autoinject, bindable } from 'aurelia-framework';
import LexicalMenuBuilder from 'src/app/services/lexical-menu-builder';
import Contextmenu, { ContextMenuOptions } from 'shared/context-menu/context-menu';
import { PhraseModel } from 'models';

@autoinject export class Phrase {

  @bindable phrase: PhraseModel;
  @bindable parentId: string;

  constructor(readonly lexicalMenuBuilder: LexicalMenuBuilder) { }

  bind(bindingContext: {}, overrideContext: {}) {
    const contextmenu = this.lexicalMenuBuilder.buildMenu(this.phrase.contextmenu);
    this.phrase.hasContextmenuData = !!contextmenu;
    if (this.phrase.hasContextmenuData) {
      this.phrase.contextmenuDataSource = this.phrase.contextmenu;
      this.phrase.contextmenu = contextmenu!;

    }

  }
}
