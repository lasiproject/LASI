'use strict';

documentModelService.$inject = ['$q', '$http'];

export function documentModelService($q: ng.IQService, $http: angular.IHttpService): DocumentModelService {
    return {
        processDocument(documentId) {
            return $http.get(`Analysis/${documentId}`, { cache: false });
        }
    };
}