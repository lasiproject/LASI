module LASI.documentList {
    'use strict';

    angular
        .module('documentList')
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

            vm.processDocument = (document: IDocumentListItemModel) => {
                documentModelService.processDocument(document.id)
                    .success(data => document.raeification = data)
                    .error(message => message);
            };
            vm.documents = documentListService.getDocumentList();

            vm.tasks = tasksListService.getActiveTasks(tasks => tasks.map(task => {
                vm.tasks[task.id] = task;
                return (vm.documents.filter(d => d.name === task.name)[0] || {}).task = task;
            }));





            $q.all([vm.documents, vm.tasks]).then((data) => {
                let [documents, tasks] = <[IDocumentListItemModel[], ITask[]]>data;
                let associated = documents.correlate(tasks, document => document.id, task => task.id,
                    (document, task) => {
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });

                tasks.forEach(task => { vm.tasks[task.id] = task; });
            });
        }
    }
}



