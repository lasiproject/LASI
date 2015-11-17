'use strict';
import { DocumentListItemModel } from 'app/models';
import { DocumentListService } from './document-list-service-provider';
import { Task, TasksListService } from './tasks-list-service-provider';
import { DocumentsService } from './documents-service';
import { DocumentModelService } from 'app/document-viewer/document-model-service';
export class ListController {
    expanded = false;
    documents: DocumentListItemModel[] = [];
    tasks: Task[] = [];
    static $inject = ['$q', 'documentListService', 'tasksListService', 'documentsService', 'documentModelService'];
    constructor(
        private $q: angular.IQService,
        private documentListService: DocumentListService,
        private tasksListService: TasksListService,
        private documentsService: DocumentsService,
        private documentModelService: DocumentModelService) {
        this.activate();
    }

    toggleExpanded() {
        this.expanded = !this.expanded;
    }

    deleteById(id: string) {
        var deleteResult = this.documentsService.deleteById(id);
        console.log(deleteResult);
        this.documentListService.get().then(documents=> this.documents = documents);
    }
    get documentCount() {
        return this.documents.length;
    }

    processDocument(document: DocumentListItemModel) {
        if (!this.documents.some(d => d.raeification && d.id === document.id)) {
            this.documentModelService.processDocument(document.id).then(processed => document.raeification = processed,
                reason => console.error(reason)
            );
        }
    }

    activate() {
        return this.$q.all([
            this.documentListService.get(),
            this.$q.when(this.tasksListService.getActiveTasks().map(task => {
                this.tasks[task.id] = task;
                var t = this.documents.first(d => d.name === task.name);
                if (t) {
                    (<any>t).task = task;
                }
                return t;
            }))
        ]).then(data => {
            let [documents, tasks] = <[DocumentListItemModel[], Task[]]>data;
            let associated = documents.correlate(tasks.filter(t => !!t), document => document.id, task => task.id,
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