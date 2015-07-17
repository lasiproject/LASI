module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('sentence', sentence);

    function sentence(): ng.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.html',
            link: (scope, element, attrs) => {
                log(scope);
                log(element);
                log(attrs);
            },
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
}