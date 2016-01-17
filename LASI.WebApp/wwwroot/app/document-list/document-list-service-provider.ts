'use strict';
import { UserService } from 'app/user-service';
export interface DocumentListServiceConfig {
    setRecentDocumentCount(count: number): DocumentListServiceConfig;
    setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export interface DocumentListService {
    get(): ng.IPromise<DocumentListItemModel[]>;
    deleteDocument(documentId: string): PromiseLike<DocumentListItemModel>;
}
export class DocumentListServiceProvider implements DocumentListServiceConfig, ng.IServiceProvider {
    private documentListUrl: string;
    private recentDocumentCount: number;
    constructor() {
        this.$get.$inject = ['$q', '$http', 'UserService'];
    }
    setDocumentListUrl(url: string) {
        this.documentListUrl = url;
        return this;
    }
    setRecentDocumentCount(count: number) {
        this.recentDocumentCount = count;
        return this;
    }

    $get($q: ng.IQService, $http: ng.IHttpService, userService: UserService): DocumentListService {
        let [limit, listUrl] = [this.recentDocumentCount, this.documentListUrl];
        return {
            deleteDocument(documentId: string) {
                if (userService.loggedIn) {
                    return $http.delete(`${listUrl}?documentId=${documentId}`);
                } else {
                    return {} as any;
                }
            },
            get() {
                return userService.loggedIn
                    ? $http.get<DocumentListItemModel[]>(`${listUrl}?limit=${limit}`)
                        .then(response => response.data)
                        .catch(error => {
                            return $q.resolve([]);
                        })
                    : $q.resolve([]);
            }
        };
    };
}