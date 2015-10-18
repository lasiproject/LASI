var LASI;
(function (LASI) {
    var widgets;
    (function (widgets) {
        'use strict';
        processingTaskProgressBar.$inject = [];
        function processingTaskProgressBar() {
            return {
                restrict: 'E',
                scope: {
                    task: '='
                },
                templateUrl: '/app/widgets/processing-task-progress-bar.directive.html'
            };
        }
        angular
            .module('widgets')
            .directive({ processingTaskProgressBar: processingTaskProgressBar });
    })(widgets = LASI.widgets || (LASI.widgets = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=processing-task-progress-bar.directive.js.map