namespace LASI.documentList {
    'use strict';

    class ListController {
        expanded = false;
        documents: DocumentListItemModel[] = [];
        tasks: Task[];
        static $inject = ['$q', 'documentListService', 'tasksListService', 'documentsService', 'documentModelService'];
        constructor(
            private $q: angular.IQService,
            private documentListService: DocumentListService,
            private tasksListService: TasksListService,
            private documentsService: DocumentsService,
            private documentModelService: LASI.documentViewer.DocumentModelService) {
            this.activate();
        }


        deleteById(id: string) {
            var deleteResult = this.documentsService.deleteById(id);
            log(deleteResult);
            this.documents = this.documentListService.get();
        }
        get documentCount() { return this.documents.length; }

        processDocument(document: DocumentListItemModel) {
            if (!this.documents.some(d => d.raeification && d.id === document.id)) {
                let data = this.documentModelService.processDocument(document.id)
                    .success(processed => document.raeification = processed)
                    .error(error => console.log(error));
            }
        }




        activate() {
            return this.$q.all([
                this.$q.when(this.documentListService.get()),
                this.tasksListService.getActiveTasks().then(tasks => tasks.map(task => {
                    this.tasks[task.id] = task;
                    var t = this.documents.first(d => d.name === task.name);
                    (t && (<any>t)).task = task;
                    return t;
                }))
            ]).then(data => {
                let [documents, tasks] = <[DocumentListItemModel[], Task[]]>data;
                let associated = documents.correlate(tasks, document => document.id, task => task.id,
                    (document, task) => {
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });

                tasks.forEach(task => { this.tasks[task.id] = task; });
                [this.documents, this.tasks] = [documents, tasks];
            });

        }
    }


    angular.module('documentList').controller({ ListController });

}



