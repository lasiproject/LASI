import template from './processing-task-progress-bar.html';

export function processingTaskProgressBar(): angular.IDirective {
    return {
        restrict: 'E',
        scope: {
            task: '='
        },
        template 
    };
}
