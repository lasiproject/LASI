namespace LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('sentence', sentence);

    function sentence(): angular.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.html',
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
}