/// <reference path="../../../typings/angularjs/angular.d.ts" />
/// <reference path="../../../typings/angularjs/angular-resource.d.ts" />
var DocumentList;
(function (DocumentList_1) {
    var DocumentListServiceProvider = (function () {
        function DocumentListServiceProvider() {
            this.$inject = ['$resource'];
        }
        DocumentListServiceProvider.prototype.setDocumentListUrl = function (url) {
            this.documentListUrl = url;
            return this;
        };
        DocumentListServiceProvider.prototype.setRecentDocumentCount = function (count) {
            this.recentDocumentCount = count;
            return this;
        };
        DocumentListServiceProvider.prototype.$get = function ($resource) {
            var documentList;
            var DocumentList;
            return {
                getDocumentList: function () {
                    DocumentList = $resource(this.documentListUrl + '/' + this.recentDocumentCount, {}, {
                        get: {
                            method: 'GET',
                            isArray: true
                        }
                    });
                    documentList = DocumentList.get();
                    return documentList;
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
})(DocumentList || (DocumentList = {}));
