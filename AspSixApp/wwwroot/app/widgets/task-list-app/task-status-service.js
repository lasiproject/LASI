(function () {
    'use strict';

    angular
        .module(LASI.taskList.ngName)
        .factory('taskStatusService', taskStatusService);

    taskStatusService.$inject = ['$http', '$q'];

    function taskStatusService($http, $q) {
        var taskList = [];
        return {
            getList: function (reload) {
                var request;
                var deferred = $q.defer();
                if (reload) {
                    request = $http.get('api/Tasks')
                        .success(function (data) {
                            console.log(data);
                            taskList = data;
                            deferred.resolve(taskList);
                        })
                        .error(function (status) {
                            deferred.reject(status);
                        });
                } else {
                    deferred.resolve(taskList);
                }
                return deferred.promise;
            }
        };
    }
})();