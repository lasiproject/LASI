// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    function phrase() {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/lexical/phrase.html',
            link: function (scope, element, attrs) {
                console.log(attrs);
                var menu = scope.phrase.contextmenu;
            },
            scope: {
                phrase: '=',
                parentId: '='
            }
        };
    }
    angular.module('interactiveRepresentations').directive('phrase', phrase);
})(App || (App = {}));
