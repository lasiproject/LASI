'use strict';
processingTaskProgressBar.$inject = [];
export function processingTaskProgressBar(): angular.IDirective {
    return {
        restrict: 'E',
        scope: {
            task: '='
        },
        templateUrl: './app/widgets/processing-task-progress-bar.html'
    };
}
