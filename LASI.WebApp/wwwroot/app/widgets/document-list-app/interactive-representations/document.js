var App;
(function (App) {
    'use strict';
    document.$inject = ['$window'];
    function document($window) {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/document.html',
            link: link,
            replace: true,
            scope: { document: '=' }
        };
        function link(scope, element, attrs) {
        }
    }
    angular
        .module(LASI.documentList.ngName)
        .directive('document', document);
})(App || (App = {}));
