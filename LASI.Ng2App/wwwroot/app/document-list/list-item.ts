import { Component, OnInit } from 'angular2/core';
import { Injectable, Input} from 'app/ng2-utils';
import ListService from './list-service';
import TaskService from './task-service';
import ResultService from 'app/document-viewer/results-service';
import template from "./list-item.html";
@Component({
    selector: 'list-item',
    template
})
export class ListItemComponent implements OnInit {
    constructor(private listService: ListService, private taskService: TaskService, private resultsService: ResultService) { }

    @Input documentId: string;

    @Input documentName: string;

    @Input showProgress = true;

    @Input analysisProgress = 0;

    @Input item: models.DocumentListItem;

    analysisPhase = 'Pending';

    ngOnInit() {
        this.listService.getById(this.documentId)
            .zip(this.taskService.getTasksForDocumentById(this.documentId))
        // .last(item => this.item = item);
    }
    click(event: Event) {
        event.preventDefault();
        event.stopPropagation();
        this.showProgress = true;

        this.resultsService.processDocument(this.documentId, this.documentName)
            .then(() => this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete);
    }
}