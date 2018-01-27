import { ContextMenuDataSource, LexicalMenu } from 'models/lexical-menu';
import Phrase from 'models/phrase-model';
import Word from 'models/word-model';
import Clause from 'models/clause-model';

export default interface LexicalModelInternal {
  text: string;
  detailText: string;
  id: number;
  style: {
    cssClass: string;
  };
  hasContextmenuData: boolean;
  contextmenuDataSource: ContextMenuDataSource;
  contextmenu: LexicalMenu;
}

export type LexicalModel = Phrase | Word | Clause;
export namespace LexicalModel { }
