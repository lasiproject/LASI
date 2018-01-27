import VerbalMenu from 'models/verbal-menu';

export interface LexicalMenuBuilder {
  build: (source: LexicalMenu) => LexicalMenu;
}
export namespace LexicalMenuBuilder {}

export type LexicalMenuCore = {
  /**
  * The id of the lexical element for which the menu is defined.
  */
  lexicalId: string | number;
};
export type LexicalMenu = VerbalMenu | ReferencerMenu;
export namespace LexicalMenu {}

export interface ReferencerMenu extends LexicalMenuCore {
  kind: 'referencer';
  /**
    * The id of the referencer for which the menu is defined.
    */
  lexicalId: number;
  /**
    * The ids of any entities the referred to.
    */
  refersToIds: number[];
}
export namespace ReferencerMenu {}

export type ContextMenuDataSource = VerbalMenu | ReferencerMenu;
export namespace ContextMenuDataSource {}
