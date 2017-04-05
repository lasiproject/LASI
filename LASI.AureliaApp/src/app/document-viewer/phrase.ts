import { autoinject, bindable } from 'aurelia-framework';
import LexicalMenuBuilder from 'src/app/services/lexical-menu-builder';
import { Contextmenu, ContextmenuOptions } from 'shared/context-menu';
import { PhraseModel } from 'models';

@autoinject export class Phrase {

  @bindable phrase: PhraseModel;
  @bindable parentId: string;

  constructor(private lexicalMenuBuilder: LexicalMenuBuilder) { }

  bind(bindingContext: Object, overrideContext: Object) {
    const contextmenu = this.lexicalMenuBuilder.buildAngularMenu(this.phrase.contextmenu);
    this.phrase.hasContextmenuData = !!contextmenu;
    if (this.phrase.hasContextmenuData) {
      this.phrase.contextmenuDataSource = this.phrase.contextmenu;
      this.phrase.contextmenu = contextmenu;

    }

  }
}