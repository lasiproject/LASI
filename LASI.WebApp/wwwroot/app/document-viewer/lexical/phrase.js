// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        phrase.$inject = ['lexicalMenuBuilder'];
        function phrase(lexicalMenuBuilder) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/lexical/phrase.html',
                link: function (scope, element, attrs) {
                    var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
                    scope.phrase.hasContextmenu = !!contextmenu;
                    if (scope.phrase.hasContextmenu) {
                        scope.phrase.contextmenu = contextmenu;
                    }
                },
                scope: {
                    phrase: '=',
                    parentId: '='
                }
            };
        }
        angular.module(LASI.documentViewer.ngName).directive('phrase', phrase);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
