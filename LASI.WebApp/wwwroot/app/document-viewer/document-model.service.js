'use strict';
documentModelService.$inject = ['$http'];
function documentModelService($http) {
    return {
        processDocument: function (documentId) {
            return $http.get("Analysis/" + documentId, { cache: false });
        }
    };
}
exports.documentModelService = documentModelService;
