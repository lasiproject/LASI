declare namespace models {
    export interface DocumentModelService {
        processDocument(documentId: string);
    }

    export interface TasksListService {
        getActiveTasks(): Promise<Task[]>;
        tasks: Task[];
    }

    export interface UserService {
        login({ email, password, rememberMe }: Credentials): Promise<User>;
        loginGet(): Promise<User>;
        loginPost(data: {}): Promise<User>;
        user: User;
        loggedIn: boolean;
    }

    // export interface TasksListServiceProvider {
    //     $get: ($q: ng.IQService, http: ng.IHttpService, $interval: ng.IIntervalService, userService: any) => TasksListService;
    //     setTasksListUrl: (url: string) => TasksListServiceProvider;
    //     setUpdateInterval: (milliconds: number) => TasksListServiceProvider;
    // }

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

    export interface ResultsService {
        tasks: Task[];
        processDocument(documentId: string, documentName: string);
        getTasksForDocument(id: string);
    }

    export interface Credentials {
        email: string;
        password: string;
        rememberMe?: boolean;
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
        title: string;
        id: string;
        progress: number | string;
        percentComplete: number | string;
    }

    export interface PageModel extends TextFragmentModel {
        pageNumber: number;
    }

    export interface ParagraphModel {
        sentences: SentenceModel[];
    }

    export interface SentenceModel {
        phrases: PhraseModel[];
    }

    export interface LexicalModel {
        text: string;
        detailText: string;
        id: number;
        style: { cssClass: string };
        hasContextmenuData: boolean;
        contextmenuDataSource: LexicalContextmenuData;
        contextmenu: LexicalContextmenuData |
        VerbalContextmenuData | ReferencerContextmenuData;
    }

    export interface PhraseModel extends LexicalModel {
        words: WordModel[];
    }

    export interface WordModel extends LexicalModel { }

    export interface LexicalMenuBuilder {
        buildAngularMenu: (source: LexicalContextmenuData) => any;
    }

    export interface LexicalContextmenuData {
        /**
        * The id of the lexical element for which the menu is defined.
        */
        lexicalId: string | number;
    }

    export interface VerbalContextmenuData extends LexicalContextmenuData {

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

    export interface ReferencerContextmenuData extends LexicalContextmenuData {
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


}
declare interface Array<T> {
    flatMap<TArray, TResult>(arraySelector: (element: T) => TArray[], elementSelector: (element: TArray) => TResult): TResult[];
    flatMap<TArray>(arraySelector: (element: T) => TArray[]): TArray[];
    //flatMap<U>(arraySelector: (element: T) => U[], elementSelector?: (element: T) => U): U[];
    //flatMap<U extends T[]>(): T[];

    flatMap<TArray extends T[]>(): T[];
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
interface JQuery { contextmenu: any; }