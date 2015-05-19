(function () {
    'use strict';

    angular
        .module(LASI.results.ngName)
        .factory('contextMenuService', contextMenuService);

    contextMenuService.$inject = ['$http', '$q', '$log'];

    function contextMenuService($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() {
            var deferred = $q.defer();
            $http.get('api/UserDocuments/List')
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