/// <reference path="./../../typings/tsd.d.ts" />

declare module 'modules' {
    export default '';
}
declare interface DocumentModelService {
    processDocument(documentId: string): ng.IPromise<DocumentModel>;
}
declare interface TasksListService {
    getActiveTasks(): ng.IPromise<Task[]>;
    tasks: Task[];
}
declare interface TasksListServiceProvider {
    $get: ($q: ng.IQService, http: ng.IHttpService, $interval: ng.IIntervalService, userService: any) => TasksListService;
    setTasksListUrl: (url: string) => TasksListServiceProvider;
    setUpdateInterval: (milliconds: number) => TasksListServiceProvider;
}

interface Credentials {
    email: string;
    password: string;
    antiforgeryToken: string;
    rememberMe?: boolean;
}

declare interface Task {
    id: string;
    name: string;
    percentComplete: number;
    state?: string;
    statusMessage?: string;
}
declare interface DocumentListItemModel {
    id: string;
    name: string;
    progress: number;
    percentComplete: number;
    showProgress: boolean;
    statusMessage: string;
    raeification: DocumentModel;
    task: Task;
}

declare interface DocumentListItem {
    /**
     * The name of the document
     */
    name: string;
    /**
     * The id of the document
     */
    id: string;
    /**
     * The content is optional as the list item may just be a placeholder for the document.
     */
    content?: string;
}
declare interface TextFragmentModel {
    paragraphs: ParagraphModel[];
}

declare interface DocumentModel extends TextFragmentModel {
    title: string;
    id: string;
    progress: number | string;
    percentComplete: number | string;
}

declare interface PageModel extends TextFragmentModel {
    pageNumber: number;
}

declare interface ParagraphModel {
    sentences: SentenceModel[];
}

declare interface SentenceModel {
    phrases: PhraseModel[];
}
declare interface LexicalModel {
    text: string;
    detailText: string;
    id: number;
    style: { cssClass: string };
    hasContextmenuData: boolean;
    contextmenu: LexicalContextmenuData |
    VerbalContextmenuData | ReferencerContextmenuData;
}

declare interface PhraseModel extends LexicalModel {
    words: WordModel[];
}

declare interface WordModel extends LexicalModel { }
declare interface LexicalMenuBuilderFactory {
    buildAngularMenu: (source: LexicalContextmenuData) => angular.ui.bootstrap.contextMenu.ContextMenu;
}

declare interface LexicalContextmenuData {
    /**
    * The id of the lexical element for which the menu is defined.
    */
    lexicalId: string | number;
}
declare interface VerbalContextmenuData extends LexicalContextmenuData {
       
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

declare interface ReferencerContextmenuData extends LexicalContextmenuData {
    /**
     * The id of the referencer for which the menu is defined.
     */
    lexicalId: number;
    /**
     * The ids of any entities the referred to.
     */
    refersToIds: number[];
}
declare interface User extends Credentials {
    loggedIn?: boolean;
    email: string;
    password: string;
    documents: any[];
    token?: string;
}
 
/**
 @function A trait which decsribes a functions whose $inject property is required.
*/
declare type FunctionMap = { [name: string]: Function };
declare type ConstructorMap = { [name: string]: new (...args) => any };
declare type FunctionOrConstructorMap = { [name: string]: Function | (new (...args) => any) };

/**
 Describes the shape of angular module which will be registered via angular.module('name', ...deps).
 The requires array may contain both the names angular modules and other NgModuleConfig objects as dependencies.
 @see angular.module
*/
declare type NgModuleConfig = {
    name: string;
    requires: (string | NgModuleConfig)[];
    configs?: Function[];
    runs?: Function[];
    values?: FunctionMap;
    constants?: FunctionMap;
    filters?: FunctionMap;
    controllers?: ConstructorMap;
    directives?: FunctionMap;
    factories?: FunctionMap;
    services?: ConstructorMap;
    providers?: FunctionOrConstructorMap;
}
declare interface Array<T> {
    flatMap<TArray, TResult>(arraySelector: (element: T) => TArray[], elementSelector: (element: TArray) => TResult): TResult[];
    flatMap<TArray>(arraySelector: (element: T) => TArray[]): TArray[];
    //flatMap<U>(arraySelector: (element: T) => U[], elementSelector?: (element: T) => U): U[];
    //flatMap<U extends T[]>(): T[];
    flatMap(): any[];
    correlate<TInner, TKey>(inner: TInner[], outerKeySelector: (e: T) => TKey, innerKeySelector: (e: TInner) => TKey): { first: T, second: TInner }[];
    /**
    * @function correlate Correlates the elements of the array with the elements of another 
    * based on the strict (===) equality of the keys selected from each element
    * by the provided selector functions. Models the semantics of a relational inner join.
    * Returns an array of elements produced by invoking the specified resultSelector function on each pair of correlated elements.
    * @param inner An array whose elements will be correlated with the source array.
    * @param outerKeySelector A function to produce a key value from each element in the outer array.
    * @param innerKeySelector A function to produce a key value from each element in the inner array.
    * @param resultSelector A function to transform each pair of correlated elements.
    * @returns An array of elements produced by invoking the specified resultSelector function on each pair of correlated elements. 
    */
    correlate<TInner, TKey, TResult>(
        inner: TInner[],
        outerKeySelector: (e: T) => TKey,
        innerKeySelector: (e: TInner) => TKey,
        resultSelector: (first: T, second: TInner) => TResult
    ): TResult[];
    sum(valueSelector?: (any) => number): number;
    average(valueSelector?: (any) => number): number;

    first(predicate?: (element: T) => boolean): T;
    last(predicate?: (element: T) => boolean): T;

} 