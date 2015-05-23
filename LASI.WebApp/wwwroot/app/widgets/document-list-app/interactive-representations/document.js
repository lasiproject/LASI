var App;
(function (App) {
    'use strict';
    document.$inject = ['$window'];
    function document($window) {
        var link = function (scope, element, attrs) {
            console.log(scope);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/document.html',
            replace: true,
            scope: {
                document: '='
            },
            link: link
        };
        //return new Document('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);
    }
    angular
        .module('interactiveRepresentations')
        .directive('document', document);
})(App || (App = {}));
