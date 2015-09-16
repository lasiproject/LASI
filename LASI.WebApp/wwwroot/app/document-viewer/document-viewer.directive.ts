namespace LASI.documentViewer {
    'use strict';

    documentViewerDirective.$inject = [];

    function documentViewerDirective(): angular.IDirective {

        var link: angular.IDirectiveLinkFn = function (scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes) {
            log(scope);
            log(element);
            log(attrs);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/document-viewer.directive.html',
            replace: true,
            scope: {
                document: '='
            },
            link
        };
    }

    angular
        .module('documentViewer')
        .directive({ documentViewerDirective });
}