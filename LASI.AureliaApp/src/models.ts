export interface TasksListService {
  getActiveTasks(): Promise<Task[]>;
  tasks: Task[];
}

export interface DocumentListService {
  get(): Promise<DocumentListItem[]>;
  deleteDocument(documentId: string): Promise<DocumentListItem>;
}

export interface DocumentService {
  getbyId(documentId: string): Promise<DocumentListItem>;
  deleteById(documentId: string): Promise<any>;
}

export interface DocumentListServiceConfig {
  setRecentDocumentCount(count: number): DocumentListServiceConfig;
  setDocumentListUrl(url: string): DocumentListServiceConfig;
}

export interface Credentials {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface AuthenticationResult {
  user?: User;
  autenticated?: boolean;
  token?: string;
}

export interface Task {
  id: string;
  name: string;
  percentComplete: number;
  state?: string;
  statusMessage?: string;
}

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

export interface TextFragmentModel {
  paragraphs: ParagraphModel[];
}

export interface DocumentModel extends TextFragmentModel {
  kind: 'document';
  title: string;
  id: string;
  progress: number | string;
  percentComplete: number | string;
}

export interface PageModel extends TextFragmentModel {
  kind: 'page';
  pageNumber: number;
}

export interface ParagraphModel {
  kind: 'paragraph';
  sentences: SentenceModel[];
}

export interface SentenceModel {
  kind: 'sentence';
  phrases: PhraseModel[];
}

interface LexicalModelInternal {
  text: string;
  detailText: string;
  id: number;
  style: {
    cssClass: string;
  };
  hasContextmenuData: boolean;
  contextmenuDataSource: LexicalContextMenuData;
  contextmenu: LexicalContextMenuData |
  VerbalContextMenuData | ReferencerContextmenuData;
}

export interface PhraseModel extends LexicalModelInternal {
  kind: 'phrase';
  words: WordModel[];
}

export interface WordModel extends LexicalModelInternal {
  kind: 'word';
}

export interface ClauseModel extends LexicalModelInternal {
  kind: 'clause';
}

export type LexicalModel = PhraseModel | WordModel | ClauseModel;

export interface LexicalMenuBuilder {
  buildAngularMenu: (source: LexicalContextMenuData) => any;
}

export interface LexicalContextMenuData {
  /**
 * The id of the lexical element for which the menu is defined.
 */
  lexicalId: string | number;
}

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

export interface User extends Credentials {
  loggedIn?: boolean;
  email: string;
  password: string;
  documents: any[];
  id: string;
}
