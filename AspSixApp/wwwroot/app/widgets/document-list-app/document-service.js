(function () {
    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .factory('documentService', documentService);

    documentService.$inject = ['$http', '$q', '$log'];

    function documentService($http, $q, $log) {
        return {
            getData: getData,
            uploadDocuments: uploadDocuments
        };

        function getData() {
            var deferred = $q.defer();
            $http.get('api/UserDocuments/list')
                .success(function (data) {
                    $log.info(data);
                    deferred.resolve(data);
                })
                .error(function (status) {
                    deferred.reject(status);
                });
            return deferred.promise;
        }
        function uploadDocuments(fileInputs) {
            var data = $(fileInputs);
            var deferred = $q.defer(),
                succuss = function (data) {
                    $log.info(data);
                },
                error = function (status) {
                    $log.error(status);
                    deferred.reject(status);
                };
            $http.post({
                url: 'api/UserDocuments',
                contentType: 'xxx-multipart-form-encoded'
            });
            return deferred.promise;
        }
    }
})();