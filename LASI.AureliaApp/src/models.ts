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

