module LASI.documentViewer {
    'use strict';

    paragraph.$inject = ['$window'];
    function paragraph($window: ng.IWindowService): ng.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/paragraph.html',
            scope: {
                paragraph: '=',
                parentId: '='
            },
            link(scope, element, attrs) {
                console.log(scope);
            }
        };

    }
    angular
        .module('documentViewer')
        .directive('paragraph', paragraph);
}