module LASI.widgets {
    'use strict';

    processingTaskProgressBar.$inject = [];
    function processingTaskProgressBar(): ng.IDirective {
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