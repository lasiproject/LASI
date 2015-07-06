module LASI.documentList {
    'use strict';

    export interface IDocumentListServiceConfig {
        setRecentDocumentCount(count: number): IDocumentListServiceConfig;
        setDocumentListUrl(url: string): IDocumentListServiceConfig;
    }
    export interface IDocumentListService {
        getDocumentList(): IDocumentListItemModel[];
        deleteDocument(documentId: string): IDocumentListItemModel;
    }
    export interface IDocumentListItemModel {
        id: string;
        name: string;
        progress: number|string;
        percentComplete: number|string;
        showProgress: boolean;
        statusMessage: string;
        raeification: models.IDocumentModel;
    }
    class DocumentListServiceProvider implements IDocumentListServiceConfig, ng.IServiceProvider {
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
        /**
         * @param $resource an instance of the Resource Service supplied by the angular-resource module.
         */
        $get($resource: ng.resource.IResourceService): IDocumentListService {
            var resource = $resource<IDocumentListItemModel[]>(this.documentListUrl + '/' + this.recentDocumentCount, {}, {
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
                getDocumentList: resource.get
            };
        };
    }



    angular
        .module('documentList')
        .provider('documentListService', DocumentListServiceProvider);


}