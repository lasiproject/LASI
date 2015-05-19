/// <reference path = "IDocumentListItem.d.ts" />
interface IDocumentListServiceConfig {
    setRecentDocumentCount(count: number): IDocumentListServiceConfig
    setDocumentListUrl(url: string): IDocumentListServiceConfig
}

class DocumentListServiceProvider implements IDocumentListServiceConfig, ng.IServiceProvider {
    private documentListUrl: string
    private recentDocumentCount: number
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


    $inject = ['$resource']
    /**
     * @param $resource an instance of the Resource Service supplied by the angular-resource module.
     */
    $get($resource: ng.resource.IResourceService) {
        var resource = $resource(this.documentListUrl + '/' + this.recentDocumentCount, {}, {
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
            getDocumentList: function () {
                return resource.get();
            },
            deleteDocument: function (documentId: string) {
                return resource.delete({ documentId });
            }
        };
    }
}
(function () {

    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .provider('documentListService', DocumentListServiceProvider);

})();