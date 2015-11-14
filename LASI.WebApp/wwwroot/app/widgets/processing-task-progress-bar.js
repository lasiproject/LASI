System.register([], function(exports_1) {
    'use strict';
    function processingTaskProgressBar() {
        return {
            restrict: 'E',
            scope: {
                task: '='
            },
            templateUrl: './app/widgets/processing-task-progress-bar.html'
        };
    }
    exports_1("processingTaskProgressBar", processingTaskProgressBar);
    return {
        setters:[],
        execute: function() {
            processingTaskProgressBar.$inject = [];
        }
    }
});
