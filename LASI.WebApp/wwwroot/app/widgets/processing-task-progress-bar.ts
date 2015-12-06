'use strict';
import template from 'app/widgets/processing-task-progress-bar.html';
processingTaskProgressBar.$inject = [];
export function processingTaskProgressBar(): angular.IDirective {
    return {
        restrict: 'E',
        scope: {
            task: '='
        },
        template 
    };
}
