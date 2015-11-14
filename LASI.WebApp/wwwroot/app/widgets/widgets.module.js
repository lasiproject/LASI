System.register(['./document-list', './processing-task-progress-bar'], function(exports_1) {
    'use strict';
    var processing_task_progress_bar_1;
    return {
        setters:[
            function (_1) {},
            function (processing_task_progress_bar_1_1) {
                processing_task_progress_bar_1 = processing_task_progress_bar_1_1;
            }],
        execute: function() {
            exports_1("default",{
                name: 'widgets',
                requires: ['ui.bootstrap'],
                directives: { processingTaskProgressBar: processing_task_progress_bar_1.processingTaskProgressBar }
            });
        }
    }
});
