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
            'tasksListService', 'documentsService', 'documentModelService'
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
                vm.processDocument = function (document) {
                    documentModelService.processDocument(document.id)
                        .success(function (data) { return document.raeification = data; })
                        .error(function (message) { return message; });
                };
                vm.documents = documentListService.getDocumentList();
                vm.tasks = tasksListService.getActiveTasks(function (tasks) { return tasks.map(function (task) {
                    vm.tasks[task.id] = task;
                    return (vm.documents.filter(function (d) { return d.name === task.name; })[0] || {}).task = task;
                }); });
                $q.all([vm.documents, vm.tasks]).then(function (data) {
                    var _a = data, documents = _a[0], tasks = _a[1];
                    var associated = documents.correlate(tasks, function (document) { return document.id; }, function (task) { return task.id; }, function (document, task) {
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });
                    tasks.forEach(function (task) { vm.tasks[task.id] = task; });
                });
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
