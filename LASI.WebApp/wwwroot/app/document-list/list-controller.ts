'use strict';
import { DocumentListService } from './document-list-service-provider';
import { DocumentsService } from './documents-service';

export class ListController {
    expanded = false;
    private documents: DocumentListItemModel[] = [];
    static $inject = ['$q', 'documentListService', 'documentsService', 'documentModelService', 'tasks'];
    constructor(
        private $q: angular.IQService,
        private documentListService: DocumentListService,
        private documentsService: DocumentsService,
        private documentModelService: DocumentModelService,
        private tasks: Task[]) {
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
        var documentPromise = this.documentListService.get()
            .then(documents => this.documents = documents)
            .then(documents => ({ documents: this.documents, tasks: this.tasks }))
            .then(xs=> xs.tasks.map(task => {
                this.tasks[task.id] = task;
                var doc = this.documents.first(d => d.name === task.name);
                if (doc) {
                    doc.task = task;
                }
                return doc;
            }));
        return documentPromise.then(data => {

            let associated = this.documents.correlate(this.tasks.filter(t => !!t), document => document.id, task => task.id,
                (document, task) => {
                    document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });

            this.tasks.forEach(task => { this.tasks[task.id] = task; });
        });

    }
}