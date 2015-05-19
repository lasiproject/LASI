/// <reference path="../../../typings/angularjs/angular.d.ts" />
/// <reference path="../../../typings/angularjs/angular-resource.d.ts" />

module DocumentList {
    class DocumentListServiceProvider implements ng.IServiceProvider {
        private documentListUrl: string
        private recentDocumentCount: number
        setDocumentListUrl(url: string) {
            this.documentListUrl = url;
            return this;
        }
        setRecentDocumentCount(count: number) {
            this.recentDocumentCount = count;
            return this;
        }


        $inject: string[] = ['$resource'];
        $get($resource) {
            var documentList;
            var DocumentList;
            return {
                getDocumentList: function () {
                    DocumentList = $resource(
                        this.documentListUrl + '/' + this.recentDocumentCount,
                        {},
                        {
                            get: {
                                method: 'GET',
                                isArray: true
                            }
                        });
                    documentList = DocumentList.get();
                    return documentList;
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
}
