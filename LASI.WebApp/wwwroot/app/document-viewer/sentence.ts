module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('sentence', sentence);

    function sentence(): ISentence {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.html',
            link: (scope: ISentenceScope, element: ng.IAugmentedJQuery, attrs: ISentenceAttributes) => { },
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
    interface ISentence extends ng.IDirective {
    }

    interface ISentenceScope extends ng.IScope {
        sentence: models.ISentenceModel;
        parentId: string | number;
    }

    interface ISentenceAttributes extends ng.IAttributes {
    }

}