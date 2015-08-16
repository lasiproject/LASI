namespace LASI.widgets {
    'use strict';

    processingTaskProgressBar.$inject = [];
    function processingTaskProgressBar(): angular.IDirective {
        return {
            restrict: 'E',
            scope: {
                task: '='
            },
            templateUrl: '/app/widgets/processing-task-progress-bar.html'
        };
    }

    angular
        .module('widgets')
        .directive('processingTaskProgressBar', processingTaskProgressBar);
}