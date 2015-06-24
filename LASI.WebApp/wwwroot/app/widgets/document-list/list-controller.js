var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .controller('ListController', ListController);
        ListController.$inject = [
            '$q', '$log', '$rootScope', 'documentListService',
            'tasksListService', 'documentsService', 'DocumentModelService'
        ];
        function ListController($q, $log, $rootScope, documentListService, tasksListService, documentsService, documentModelService) {
            /* jshint validthis:true */
            var vm = this;
            vm.title = 'ListController';
            vm.documents = [];
            Object.defineProperty(vm, 'documentCount', {
                get: function () { return vm.documents.length; },
                enumerable: true,
                configurable: false
            });
            vm.expanded = false;
            activate();
            function activate() {
                vm.deleteById = function (id) {
                    var deleteResult = documentsService.deleteById(id);
                    $log.info(deleteResult);
                    vm.documents = documentListService.getDocumentList();
                };
                vm.processDocument = function (documentId) {
                    var deferred = $q.defer();
                    $q.when(documentModelService.processDocument(documentId)).then(function (data) {
                        $q.when(data).then(function (d) {
                            vm.documents.filter(function (d) { return d.id === documentId; })[0].documentModel = d;
                            if (!$rootScope.$$phase) {
                                $rootScope.$apply();
                            }
                        });
                    });
                };
                $q.all([
                    $q.when(documentListService.getDocumentList()),
                    $q.when(tasksListService.getActiveTasks(function (tasks) { return tasks.forEach(function (task) {
                        vm.tasks[task.id] = task;
                        vm.documents.filter(function (d) { return d.name === task.name; })[0].task = task;
                    }); }))
                ]).then(function (promises) {
                    vm.documents = promises[0];
                    vm.documents.correlate(promises[1], function (document) { return document.id; }, function (task) { return task.id; })
                        .forEach(function (documentWithTask) {
                        var document = documentWithTask.first, task = documentWithTask.second;
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });
                    vm.tasks = {};
                    promises[1].forEach(function (task) { vm.tasks[task.id] = task; });
                });
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
