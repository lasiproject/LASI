export interface TasksListService {
  getActiveTasks(): Promise<Task[]>;
  tasks: Task[];
}
export namespace TasksListService { }

export interface DocumentListService {
  get(): Promise<DocumentListItem[]>;
  deleteDocument(documentId: string): Promise<DocumentListItem>;
}
export namespace DocumentListService { }

export interface DocumentService {
  getbyId(documentId: string): Promise<DocumentListItem>;
  deleteById(documentId: string): Promise<Response>;
}
export namespace DocumentService { }

export interface DocumentListServiceConfig {
  setRecentDocumentCount(count: number): DocumentListServiceConfig;
  setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export namespace DocumentListServiceConfig { }

export interface Task {
  id: string;
  name: string;
  percentComplete: number;
  state?: string;
  statusMessage?: string;
}
export namespace Task { }

export interface DocumentListItem {
  id: string;
  name: string;
  progress: number;
  percentComplete: number;
  showProgress: boolean;
  statusMessage: string;
  raeification: DocumentModel;
  task: Task;
  /**
  * The content is optional as the list item may just be a placeholder for the document.
  */
  content?: string;
}
export namespace DocumentListItem { }

export interface TextFragmentModel {
  paragraphs: ParagraphModel[];
}
export namespace TextFragmentModel { }

export interface DocumentModel extends TextFragmentModel {
  kind: 'document';
  title: string;
  id: string;
  progress: number | string;
  percentComplete: number | string;
}
export namespace DocumentModel { }

export interface PageModel extends TextFragmentModel {
  kind: 'page';
  pageNumber: number;
}
export namespace PageModel { }

export interface ParagraphModel {
  kind: 'paragraph';
  sentences: SentenceModel[];
}
export namespace ParagraphModel { }

export interface SentenceModel {
  kind: 'sentence';
  phrases: PhraseModel[];
}
export namespace SentenceModel { }

interface LexicalModelInternal {
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

export interface PhraseModel extends LexicalModelInternal {
  kind: 'phrase';
  words: WordModel[];
}
export namespace PhraseModel { }

export interface WordModel extends LexicalModelInternal {
  kind: 'word';
}
export namespace WordModel { }

export interface ClauseModel extends LexicalModelInternal {
  kind: 'clause';
  phrases: PhraseModel[];
}
export namespace ClauseModel { }

export type LexicalModel = PhraseModel | WordModel | ClauseModel;
export namespace LexicalModel { }

export interface LexicalMenuBuilder {
  build: (source: LexicalMenu) => LexicalMenu;
}
export namespace LexicalMenuBuilder { }

type LexicalMenuCore = {
  /**
  * The id of the lexical element for which the menu is defined.
  */
  lexicalId: string | number;
};
export type LexicalMenu = VerbalMenu | ReferencerMenu;
export namespace LexicalMenu { }

export interface VerbalMenu extends LexicalMenuCore {
  kind: 'verbal';
  /**
    * The ids of any subjects.
    */
  subjectIds: number[];
  /**
    * The ids of any direct objects.
    */
  directObjectIds: number[];
  /**
    * The ids of any direct objects.
    */
  indirectObjectIds: number[];
}
export namespace VerbalMenu { }

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
export namespace ReferencerMenu { }

export type ContextMenuDataSource = VerbalMenu | ReferencerMenu;
export namespace ContextMenuDataSource { }

export interface Credentials {
  email: string;
  password: string;
  rememberMe?: boolean;
}
export namespace Credentials { }

export interface AuthenticationResult {
  user?: User;
  autenticated?: boolean;
  token?: string;
}
export namespace AuthenticationResult { }

export interface User extends Credentials {
  loggedIn?: boolean;
  email: string;
  password: string;
  documents: DocumentModel[];
  id: string;
}
export namespace User { }