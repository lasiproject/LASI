'use strict';
import template from './document-list-menu-item.html';

export function documentListMenuItem(): angular.IDirective {
    return {
        replace: true,
        restrict: 'E',
        template,
        scope: true,
        controller: Controller,
        controllerAs: 'menuItem',
        bindToController: {
            name: '=',
            documentId: '='
        },
    };
}
class Controller {
    static $inject = ['resultsService'];
   
    constructor(private resultsService: ResultsService) {
        this.activate();
    }

    activate() {
        return this.resultsService.getTasksForDocument(this.documentId)
            .then(tasks => this.tasks = tasks);
    }
    click(event: ng.IAngularEvent) {
        event.preventDefault();
        event.stopPropagation();
        this.showProgress = true;

        var promise = this.resultsService.processDocument(this.documentId, this.name)
            .then(() => this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete);
    }

    analysisProgress: number;
    showProgress: boolean;
    documentId: string;
    name: string;
    tasks: Task[];
}