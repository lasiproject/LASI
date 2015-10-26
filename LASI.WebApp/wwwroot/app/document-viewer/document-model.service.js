'use strict';
System.register([], function(exports_1) {
    function documentModelService($http) {
        return {
            processDocument: function (documentId) {
                return $http.get("Analysis/" + documentId, { cache: false });
            }
        };
    }
    exports_1("documentModelService", documentModelService);
    return {
        setters:[],
        execute: function() {
            documentModelService.$inject = ['$http'];
        }
    }
});
