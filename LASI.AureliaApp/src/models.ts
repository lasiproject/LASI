import DocumentListItem from './models/document-list-item';
import VerbalMenu from './models/verbal-menu';
import LexicalModelCore from './models/lexical-model-core';
import TextFragmentModel from './models/text-fragment-model';

export interface TasksListService {
  getActiveTasks(): Promise<Task[]>;
  tasks: Task[];
}
export namespace TasksListService {}

export interface DocumentListService {
  get(): Promise<DocumentListItem[]>;
  deleteDocument(documentId: string): Promise<DocumentListItem>;
}
export namespace DocumentListService {}

export interface DocumentService {
  getbyId(documentId: string): Promise<DocumentListItem>;
  deleteById(documentId: string): Promise<Response>;
}
export namespace DocumentService {}

export interface DocumentListServiceConfig {
  setRecentDocumentCount(count: number): DocumentListServiceConfig;
  setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export namespace DocumentListServiceConfig {}

export interface Task {
  id: string;
  name: string;
  percentComplete: number;
  state?: string;
  statusMessage?: string;
}
export namespace Task {}

export interface PageModel extends TextFragmentModel {
  kind: 'page';
  pageNumber: number;
}
export namespace PageModel {}

export interface SentenceModel {
  kind: 'sentence';
  phrases: PhraseModel[];
}
export namespace SentenceModel {}

export interface PhraseModel extends LexicalModelCore {
  kind: 'phrase';
  words: WordModel[];
}
export namespace PhraseModel {}

export interface WordModel extends LexicalModelCore {
  kind: 'word';
}
export namespace WordModel {}

export interface ClauseModel extends LexicalModelCore {
  kind: 'clause';
  phrases: PhraseModel[];
}
export namespace ClauseModel {}

export type LexicalModel = PhraseModel | WordModel | ClauseModel;
export namespace LexicalModel {}

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
