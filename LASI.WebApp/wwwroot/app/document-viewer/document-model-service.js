System.register([], function(exports_1) {
    'use strict';
    function documentModelService($q, $http) {
        return {
            processDocument: function (documentId) {
                var deferred = $q.defer();
                $http.get("Analysis/" + documentId, { cache: false }).then(function (d) { return deferred.resolve(d.data); });
                return deferred.promise;
            }
        };
    }
    exports_1("documentModelService", documentModelService);
    return {
        setters:[],
        execute: function() {
            documentModelService.$inject = ['$q', '$http'];
        }
    }
});
