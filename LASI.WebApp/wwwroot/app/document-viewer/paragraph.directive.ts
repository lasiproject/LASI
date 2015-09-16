namespace LASI.documentViewer {
    'use strict';

    paragraph.$inject = ['$window'];
    function paragraph($window: angular.IWindowService): angular.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/paragraph.directive.html',
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };

    }
    angular
        .module('documentViewer')
        .directive({ paragraph });
}