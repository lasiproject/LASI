'use strict';
define(["require", "exports"], function (require, exports) {
    documentModelService.$inject = ['$http'];
    function documentModelService($http) {
        return {
            processDocument: function (documentId) {
                return $http.get("Analysis/" + documentId, { cache: false });
            }
        };
    }
    exports.documentModelService = documentModelService;
});
//# sourceMappingURL=document-model.service.js.map