var App;
(function (App) {
    'use strict';
    paragraph.$inject = ['$window'];
    function paragraph($window) {
        var link = function (scope, element, attrs) {
            //console.log(scope.parentId);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/paragraph.html',
            link: link,
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };
    }
    angular.module('interactiveRepresentations').directive('paragraph', paragraph);
})(App || (App = {}));
