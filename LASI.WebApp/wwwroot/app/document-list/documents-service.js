System.register([], function(exports_1) {
    'use strict';
    function documentsService($q, $http) {
        function getbyId(documentId) {
            return $http.get("api/UserDocuments/" + documentId);
        }
        function deleteById(documentId) {
            return $http.delete("api/UserDocuments/" + documentId);
        }
        return {
            deleteById: deleteById,
            getbyId: getbyId
        };
        function wrapWithQ(httpPromise) {
            var deferred = $q.defer();
            httpPromise.then(function (response) { return deferred.resolve(response.data); }, function (error) { return deferred.reject(error); });
            return deferred.promise;
        }
    }
    exports_1("documentsService", documentsService);
    return {
        setters:[],
        execute: function() {
            documentsService.$inject = ['$q', '$http'];
        }
    }
});
