export interface TasksListService {
  getActiveTasks(): Promise<Task[]>;
  tasks: Task[];
}
export declare namespace TasksListService { }

export interface DocumentListService {
  get(): Promise<DocumentListItem[]>;
  deleteDocument(documentId: string): Promise<DocumentListItem>;
}
export declare namespace DocumentListService { }

export interface DocumentService {
  getbyId(documentId: string): Promise<DocumentListItem>;
  deleteById(documentId: string): Promise<Response>;
}
export declare namespace DocumentService { }

export interface DocumentListServiceConfig {
  setRecentDocumentCount(count: number): DocumentListServiceConfig;
  setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export declare namespace DocumentListServiceConfig { }

export interface Task {
  id: string;
  name: string;
  percentComplete: number;
  state?: string;
  statusMessage?: string;
}
export declare namespace Task { }

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
export declare namespace DocumentListItem { }

export interface TextFragmentModel {
  paragraphs: ParagraphModel[];
}
export declare namespace TextFragmentModel { }

export interface DocumentModel extends TextFragmentModel {
  kind: 'document';
  title: string;
  id: string;
  progress: number | string;
  percentComplete: number | string;
}
export declare namespace DocumentModel { }

export interface PageModel extends TextFragmentModel {
  kind: 'page';
  pageNumber: number;
}
export declare namespace PageModel { }

export interface ParagraphModel {
  kind: 'paragraph';
  sentences: SentenceModel[];
}
export declare namespace ParagraphModel { }

export interface SentenceModel {
  kind: 'sentence';
  phrases: PhraseModel[];
}
export declare namespace SentenceModel { }

interface LexicalModelInternal {
  text: string;
  detailText: string;
  id: number;
  style: {
    cssClass: string;
  };
  hasContextmenuData: boolean;
  contextmenuDataSource: LexicalContextMenuData;
  contextmenu: ContextMenuDataSource;
}

export interface PhraseModel extends LexicalModelInternal {
  kind: 'phrase';
  words: WordModel[];
}
export declare namespace PhraseModel { }

export interface WordModel extends LexicalModelInternal {
  kind: 'word';
}
export declare namespace WordModel { }

export interface ClauseModel extends LexicalModelInternal {
  kind: 'clause';
  phrases: PhraseModel[];
}
export declare namespace ClauseModel { }

export type LexicalModel = PhraseModel | WordModel | ClauseModel;
export declare namespace LexicalModel { }

export interface LexicalMenuBuilder {
  buildAngularMenu: (source: LexicalContextMenuData) => LexicalMenuBuilder;
}
export declare namespace LexicalMenuBuilder { }

export interface LexicalContextMenuData {
  /**
  * The id of the lexical element for which the menu is defined.
  */
  lexicalId: string | number;
}
export declare namespace LexicalContextMenuData { }

export interface VerbalContextMenuData extends LexicalContextMenuData {
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
export namespace VerbalContextMenuData { }

export interface ReferencerContextmenuData extends LexicalContextMenuData {
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
export namespace ReferencerContextmenuData { }

export type ContextMenuDataSource = VerbalContextMenuData | ReferencerContextmenuData;
export namespace ContextMenuDataSource { }

export interface Credentials {
  email: string;
  password: string;
  rememberMe: boolean;
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