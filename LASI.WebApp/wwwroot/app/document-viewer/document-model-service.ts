documentModelService.$inject = ['$q', '$http'];
export function documentModelService($q: ng.IQService, $http: angular.IHttpService): DocumentModelService {
    return {
        processDocument(documentId) {
            return $http.get<DocumentModel>(`Analysis/${documentId}`, { cache: false }).then(({ data }) => data);
        }
    };
}