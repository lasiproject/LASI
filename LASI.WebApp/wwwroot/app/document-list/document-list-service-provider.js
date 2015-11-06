System.register([], function(exports_1) {
    'use strict';
    var DocumentListServiceProvider;
    return {
        setters:[],
        execute: function() {
            DocumentListServiceProvider = (function () {
                function DocumentListServiceProvider() {
                    this.$get.$inject = ['$q', '$http'];
                }
                DocumentListServiceProvider.prototype.setDocumentListUrl = function (url) {
                    this.documentListUrl = url;
                    return this;
                };
                DocumentListServiceProvider.prototype.setRecentDocumentCount = function (count) {
                    this.recentDocumentCount = count;
                    return this;
                };
                DocumentListServiceProvider.prototype.$get = function ($q, $http) {
                    var _a = [this.recentDocumentCount, this.documentListUrl], limit = _a[0], listUrl = _a[1];
                    return {
                        deleteDocument: function (documentId) {
                            return $http.delete(listUrl + '?documentId=' + documentId);
                        },
                        get: function () {
                            var deferred = $q.defer();
                            $http.get(listUrl + "?limit=" + limit).then(function (response) { return deferred.resolve(response.data); });
                            return deferred.promise;
                        }
                    };
                };
                ;
                return DocumentListServiceProvider;
            })();
            exports_1("DocumentListServiceProvider", DocumentListServiceProvider);
        }
    }
});
