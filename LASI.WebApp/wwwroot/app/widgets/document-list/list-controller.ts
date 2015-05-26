(function () {
    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .controller('ListController', ListController);

    ListController.$inject = ['$q', '$log', '$rootScope', 'Upload', 'documentListService', 'tasksListService', 'DocumentsService'];

    function ListController($q, $log, $rootScope, Upload: ng.angularFileUpload.IUploadService, documentListService, tasksListService, DocumentsService) {
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

            $rootScope.upload = function (files) {
                if (files && files.length) {
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];
                        Upload.upload({
                            url: 'api/UserDocuments',
                            file: file,
                            method: 'POST',
                            fileName:file.name
                        }).progress(progress)
                            .success(success);
                    }
                }
            };
            vm.deleteById = (id: number | string) => {
                var deleteResult = DocumentsService.deleteById(id);
                $log.info(deleteResult);
                vm.documents = documentListService.getDocumentList();
            };

            $q.all({
                deferredDocumentList: documentListService.getDocumentList(),
                deferredTaskStatusList: tasksListService.getActiveTasks()
            }).then(function (promises) {
                vm.documents = promises.deferredDocumentList;
                vm.documents.correlate(promises.deferredTaskStatusList, document => document.id, task=> task.id)
                    .forEach(documentWithTask => {
                    var document = documentWithTask.first,
                        task = documentWithTask.second;
                    document.showProgress = task.state === "Ongoing" || task.state === "Complete";
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });
                vm.tasks = {};
                promises.deferredTaskStatusList.forEach(task => { vm.tasks[task.id] = task; });
            });

            function progress(evt) {
                var progressPercentage = 100.0 * evt.loaded / evt.total;
                $rootScope.log = 'progress: ' + progressPercentage + '% ' + evt.config.file.name + '\n' + $rootScope.log;
            }
            function success(data, status, headers, config) {
                $rootScope.log = 'file ' + config.file.name + 'uploaded. Response: ' + JSON.stringify(data) + '\n' + $rootScope.log;
                $rootScope.$apply();
            }
        }
    }
})();
