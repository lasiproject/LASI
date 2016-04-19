documentsService.$inject = ['$http'];
export function documentsService($http: ng.IHttpService): DocumentsService {
    function getbyId(documentId) {
        return $http.get<DocumentListItem>();
    }
    function deleteById(documentId) {
        return $http.delete(`/api/UserDocuments/${documentId}`);
    }
    return {
        deleteById,
        getbyId
    };
} 
 