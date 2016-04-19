import { Component, OnInit, Input, Output } from 'angular2/core';
import "rxjs/Rx";
import { ListService } from './list-service';
import { TaskService } from './task-service';
import template from "app/viewer/document.html";
@Component({
    selector: 'list-item',
    exportAs: 'listItem',
    template
})
export class ListItemComponent implements OnInit {
    constructor(private listService: ListService, private taskService: TaskService) { }

    @Input() documentId: string;

    @Input() documentName: string;

    @Input() showProgress = true;

    @Output() analysisProgress = 0;

    @Output() analysisPhase = 'Pending';

    item: models.DocumentListItem;

    ngOnInit() {
        this.listService.getById(this.documentId)
            .zip(this.taskService.getTasksForDocumentById(this.documentId))
            // .last(item => this.item = item);
    }
    click(event: Event) {
        event.preventDefault();
        event.stopPropagation();
        this.showProgress = true;

        this.resultsService.processDocument(this.documentId, this.name)
            .then(() => this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete);
    }
}