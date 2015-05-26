// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    function sentence() {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.html',
            link: function (scope, element, attrs) {
                console.log(attrs);
            },
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
    angular.module(LASI.documentViewer.ngName).directive('sentence', sentence);
})(App || (App = {}));
