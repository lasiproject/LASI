'use strict';
import { UserService } from 'app/user-service';
export interface DocumentListServiceConfig {
    setRecentDocumentCount(count: number): DocumentListServiceConfig;
    setDocumentListUrl(url: string): DocumentListServiceConfig;
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
                if (userService.loggedIn) {
                    return $http.get<DocumentListItem[]>(`${listUrl}?limit=${limit}`)
                        .then(response => response.data)
                        .catch(error => []);
                } else {
                    return $q.resolve([]);
                }
            }
        };
    };
}