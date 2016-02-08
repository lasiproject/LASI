'use strict';
export default class ListController {
    static $inject = ['documentListService', 'documentsService', 'documentModelService', 'tasksListService'];

    private documents: DocumentListItem[] = [];
    constructor(
        private documentListService: DocumentListService,
        private documentsService: DocumentsService,
        private documentModelService: DocumentModelService,
        private taskListService: TasksListService) {
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

    processDocument(document: DocumentListItem) {
        if (!this.documents.some(d => d.raeification && d.id === document.id)) {
            this.documentModelService.processDocument(document.id).then(processed => document.raeification = processed,
                reason => console.error(reason)
            );
        }
    }

    activate() {
        var documentPromise = this.documentListService.get()
            .then(documents => this.documents = documents)
            .then(documents => this.taskListService.getActiveTasks()).then(tasks => this.tasks = tasks)
            .then(tasks => ({ documents: this.documents, tasks }))
            .then(({ documents, tasks }) => tasks.map(task => {
                this.taskListService[task.id] = task;
                var document = documents.first(d => d.name === task.name);
                if (document) {
                    document.task = task;
                }
                return document;
            }));
        return documentPromise.then(data => {

            let associated = this.documents.correlate(this.tasks.filter(t => !!t), document => document.id, task => task.id,
                (document, task) => {
                    document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });

            this.tasks.forEach(task => { this.taskListService[task.id] = task; });
        });

    }

    tasks: Task[];
    expanded = false;
}