var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .directive('phrase', phrase);
        phrase.$inject = ['lexicalMenuBuilder'];
        function phrase(lexicalMenuBuilder) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/phrase.directive.html',
                scope: {
                    phrase: '=',
                    parentId: '='
                },
                link: link
            };
            function link(scope, element, attrs) {
                var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
                scope.phrase.hasContextmenuData = !!contextmenu;
                if (scope.phrase.hasContextmenuData) {
                    scope.phrase.contextmenu = contextmenu;
                }
            }
        }
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
