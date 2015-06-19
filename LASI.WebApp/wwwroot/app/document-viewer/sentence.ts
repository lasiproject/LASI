// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module LASI.documentViewer  {
    'use strict';

    interface ISentence extends ng.IDirective {
    }

    interface ISentenceScope extends ng.IScope {
        sentence: ISentenceModel;
        parentId: string|number;
    }

    interface ISentenceAttributes extends ng.IAttributes {
    }

    function sentence(): ISentence {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.html',
            link: function (scope: ISentenceScope, element: ng.IAugmentedJQuery, attrs: ISentenceAttributes) {
                //console.log(attrs);
            },
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }

    angular.module(LASI.documentViewer.moduleName).directive('sentence', sentence);
}