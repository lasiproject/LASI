'use strict';

import template from 'app/document-list/document-list-menu-item.html';

export function documentListMenuItem(): angular.IDirective {
    return {
        replace: true,
        restrict: 'E',
        template,
        scope: true,
        bindToController: {
            name: '=',
            documentId: '='
        },
        controllerAs: 'menuItem',
        controller: Controller
    };
}
class Controller {
    static $inject = ['$q', 'resultsService'];
    analysisProgress: number;
    showProgress: boolean;
    documentId: string;
    name: string;
    task: Task;
    constructor(private $q: ng.IQService, private resultsService: ResultsService) {
        this.activate();
    }
    activate() {
        var deferred = this.$q.defer();
        this.task = this.resultsService.taskFor(this.documentId);

        deferred.resolve();
        return deferred.promise;
    }
    click(event: ng.IAngularEvent) {
        event.preventDefault();
        event.stopPropagation();

        var promise = this.resultsService.processDocument(this.documentId, this.name)
            .then(function () {
                this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete;
            });
        this.analysisProgress = this.resultsService.tasks[this.documentId].percentComplete;
        this.showProgress = true;
    }
}