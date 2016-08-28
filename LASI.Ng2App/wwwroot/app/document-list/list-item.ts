import { OnInit } from '@angular/core';
import { component, input } from 'ng2-conventions-decorators';
import { ListService } from './list-service';
import { TaskService } from './task-service';
import { ResultService } from 'app/document-viewer/results-service';
import template from './list-item.html';

@component(template)
export class ListItemComponent implements OnInit {
    constructor(private listService: ListService, private taskService: TaskService, private resultsService: ResultService) { }

    @input documentId: string;
    @input documentName: string;
    @input showProgress = true;
    @input analysisProgress = 0;
    @input item: models.DocumentListItem;
    analysisPhase = 'Pending';

    ngOnInit() {
        this.listService.getById(this.documentId)
            .zip(this.taskService.getTasksForDocumentById(this.documentId))
            .subscribe(([document, task]) => {
                console.log(document.id);
                console.log(document.percentComplete);
                console.log(task.id);
                console.log(task.percentComplete);
            });
        // .last(item => this.item = item);
    }
    click(event) {
        event.preventDefault();
        event.stopPropagation();
        this.showProgress = true;

        this.resultsService.processDocument(this.documentId, this.documentName)
            .subscribe(() => this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete);
    }
}