// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
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
                templateUrl: '/app/document-viewer/lexical/phrase.html',
                link: link,
                scope: {
                    phrase: '=',
                    parentId: '='
                }
            };
            function link(scope, element, attrs) {
                var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
                scope.phrase.hasContextmenu = !!contextmenu;
                if (scope.phrase.hasContextmenu) {
                    scope.phrase.contextmenu = contextmenu;
                }
            }
        }
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
