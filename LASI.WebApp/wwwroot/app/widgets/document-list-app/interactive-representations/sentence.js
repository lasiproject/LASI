// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    function sentence() {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/sentence.html',
            link: function (scope, element, attrs) {
                console.log(attrs);
            },
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
    angular.module('interactiveRepresentations').directive('sentence', sentence);
})(App || (App = {}));
