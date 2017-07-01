import {ContextMenuDataSource, LexicalMenu} from 'app/models';

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