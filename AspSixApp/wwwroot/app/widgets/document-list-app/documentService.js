(function () {
    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .factory('documentService', documentService);

    documentService.$inject = ['$http', '$q', '$log'];

    function documentService($http, $q, $log) {
        var service = {
            getData: getData
        };

        return service;

        function getData() {
            var deferred = $q.defer();
            $http.get('api/UserDocuments/list')
                .success(function (data) {
                    deferred.resolve(data);
                    $log.info(data);
                }).error(function (status) {
                    deferred.reject(status);
                });
            return deferred.promise;
        }
    }
})();