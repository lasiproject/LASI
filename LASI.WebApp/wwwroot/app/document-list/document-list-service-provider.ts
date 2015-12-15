'use strict';

export interface DocumentListServiceConfig {
    setRecentDocumentCount(count: number): DocumentListServiceConfig;
    setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export interface DocumentListService {
    get(): PromiseLike<DocumentListItemModel[]>;
    deleteDocument(documentId: string): PromiseLike<DocumentListItemModel>;
}
export class DocumentListServiceProvider implements DocumentListServiceConfig, ng.IServiceProvider {
    private documentListUrl: string;
    private recentDocumentCount: number;
    constructor() {
        this.$get.$inject = ['$q', '$http'];
    }
    setDocumentListUrl(url: string) {
        this.documentListUrl = url;
        return this;
    }
    setRecentDocumentCount(count: number) {
        this.recentDocumentCount = count;
        return this;
    }

    $get($q: ng.IQService, $http: ng.IHttpService): DocumentListService {
        let [limit, listUrl] = [this.recentDocumentCount, this.documentListUrl];
        return {
            deleteDocument(documentId: string) {
                return $http.delete(listUrl + '?documentId=' + documentId);
            },
            get() {
                let deferred = $q.defer<DocumentListItemModel[]>();
                $http.get<DocumentListItemModel[]>(`${listUrl}?limit=${limit}`)
                    .then(response=> deferred.resolve(response.data))
                    .catch(error=> {
                        console.error.bind(console);
                        deferred.resolve([]);
                    });
                return deferred.promise;
            }
        };
    };
}