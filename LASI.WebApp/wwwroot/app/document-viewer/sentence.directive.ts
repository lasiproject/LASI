namespace LASI.documentViewer {
    'use strict';

    function sentence(): angular.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/sentence.directive.html',
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }

    angular
        .module('documentViewer')
        .directive({ sentence });
}