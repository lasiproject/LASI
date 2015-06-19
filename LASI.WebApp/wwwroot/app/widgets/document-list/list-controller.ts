module LASI.documentList {
    'use strict';

    angular
        .module(LASI.documentList.moduleName)
        .controller('ListController', ListController);

    ListController.$inject = [
        '$q', '$log', '$rootScope', 'documentListService',
        'tasksListService', 'documentsService', 'DocumentModelService'
    ];

    function ListController(
        $q: ng.IQService,
        $log: ng.ILogService,
        $rootScope: ng.IRootScopeService,
        documentListService: IDocumentListService,
        tasksListService: ITasksListService,
        documentsService: IDocumentsService,
        documentModelService: LASI.documentViewer.IDocumentModelService) {
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

            vm.deleteById = (id: string) => {
                var deleteResult = documentsService.deleteById(id);
                $log.info(deleteResult);
                vm.documents = documentListService.getDocumentList();
            };

            vm.processDocument = (documentId: string) => {
                var deferred = $q.defer();
                $q.when(documentModelService.processDocument(documentId)).then((data) => {
                    $q.when(data).then(d => {
                        vm.documents.filter(d => d.id === documentId)[0].documentModel = d;
                        if (!$rootScope.$$phase) {
                            $rootScope.$apply();
                        }
                    });
                });

            };

            $q.all([
                $q.when(documentListService.getDocumentList()),
                $q.when(tasksListService.getActiveTasks(tasks => tasks.forEach(task => {
                    vm.tasks[task.id] = task;
                    vm.documents.filter(d => d.name === task.name)[0].task = task;
                })))
            ]).then(function (promises) {
                vm.documents = promises[0];
                vm.documents.correlate(promises[1], document => document.id, task => task.id)
                    .forEach(documentWithTask => {
                    var document = documentWithTask.first,
                        task = documentWithTask.second;
                    document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });
                vm.tasks = {};
                promises[1].forEach(task => { vm.tasks[task.id] = task; });
            });
        }
    }
}