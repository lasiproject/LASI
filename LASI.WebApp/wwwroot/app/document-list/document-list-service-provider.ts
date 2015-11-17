'use strict';
import { DocumentModelService } from './../document-viewer/document-model-service';
import { DocumentModel, DocumentListItemModel } from 'app/models';

export interface DocumentListServiceConfig {
    setRecentDocumentCount(count: number): DocumentListServiceConfig;
    setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export interface DocumentListService {
    get(): ng.IPromise<DocumentListItemModel[]>;
    deleteDocument(documentId: string): ng.IPromise<DocumentListItemModel>;
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
                $http.get<DocumentListItemModel[]>(`${listUrl}?limit=${limit}`).then(response=> deferred.resolve(response.data));
                return deferred.promise;
            }
        };
    };
}