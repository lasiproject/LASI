(function() {
    'use strict';

    angular
        .module(LASI.taskList.ngName)
        .directive('TaskListDirective', TaskListDirective);

    TaskListDirective.$inject = ['$window'];
    
    function TaskListDirective ($window) {
        // Usage:
        //     <TaskListDirective></TaskListDirective>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();