'use strict';

documentsService.$inject = ['$q', '$http'];
export interface DocumentsService {
    getbyId(documentId: string): ng.IPromise<DocumentListItem>;
    deleteById(documentId: string): ng.IPromise<any>;
}
export function documentsService($q: ng.IQService, $http: ng.IHttpService): DocumentsService {
    function getbyId(documentId) {
        return $http.get<DocumentListItem>(`/api/UserDocuments/${documentId}`);
    }
    function deleteById(documentId) {
        return $http.delete(`/api/UserDocuments/${documentId}`);
    }
    return {
        deleteById,
        getbyId
    };
    function wrapWithQ(httpPromise: ng.IHttpPromise<DocumentListItem>): PromiseLike<DocumentListItem> {
        var deferred = $q.defer<DocumentListItem>();
        httpPromise.then(response => deferred.resolve(response.data), error => deferred.reject(error));
        return deferred.promise;
    }
} 
 