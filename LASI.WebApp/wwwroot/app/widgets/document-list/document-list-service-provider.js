/// <reference path = "IDocumentListItem.d.ts" />
var DocumentListServiceProvider = (function () {
    function DocumentListServiceProvider() {
        this.$inject = ['$resource'];
        this.$get.$inject = ['$resource'];
    }
    DocumentListServiceProvider.prototype.setDocumentListUrl = function (url) {
        this.documentListUrl = url;
        return this;
    };
    DocumentListServiceProvider.prototype.setRecentDocumentCount = function (count) {
        this.recentDocumentCount = count;
        return this;
    };
    /**
     * @param $resource an instance of the Resource Service supplied by the angular-resource module.
     */
    DocumentListServiceProvider.prototype.$get = function ($resource) {
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
            deleteDocument: function (documentId) {
                return resource.delete({ documentId: documentId });
            }
        };
    };
    return DocumentListServiceProvider;
})();
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('documentListService', DocumentListServiceProvider);
})();
