'use strict';
import { DocumentModelService } from './../document-viewer/document-model.service';
import { DocumentModel } from 'app/models';
export interface DocumentListServiceConfig {
    setRecentDocumentCount(count: number): DocumentListServiceConfig;
    setDocumentListUrl(url: string): DocumentListServiceConfig;
}
export interface DocumentListService {
    get(): DocumentListItemModel[];
    deleteDocument(documentId: string): DocumentListItemModel;
}
export interface DocumentListItemModel {
    id: string;
    name: string;
    progress: number;
    percentComplete: number;
    showProgress: boolean;
    statusMessage: string;
    raeification: DocumentModel;
}
export class DocumentListServiceProvider implements DocumentListServiceConfig, angular.IServiceProvider {
    private documentListUrl: string;
    private recentDocumentCount: number;
    constructor() {
        this.$get.$inject = ['$resource'];
    }
    setDocumentListUrl(url: string) {
        this.documentListUrl = url;
        return this;
    }
    setRecentDocumentCount(count: number) {
        this.recentDocumentCount = count;
        return this;
    }

    $inject = ['$resource'];

    $get($resource: angular.resource.IResourceService): DocumentListService {
        var resource = $resource<DocumentListItemModel[]>(this.documentListUrl + '?limit=' + this.recentDocumentCount, {}, {
            get: {
                method: 'GET',
                isArray: true
            },
            delete: {
                method: 'DELETE',
                isArray: false
            }
        });

        return {
            deleteDocument: function (documentId: string) {
                return resource.delete({ documentId })[0];
            },
            get: resource.get
        };
    };
}