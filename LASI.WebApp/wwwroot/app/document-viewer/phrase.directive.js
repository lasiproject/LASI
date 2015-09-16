var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
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
        angular
            .module('documentViewer')
            .directive({ phrase: phrase });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
