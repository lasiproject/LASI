module LASI.documentViewer {
    'use strict';

    documentViewerDirective.$inject = [];

    function documentViewerDirective(): ng.IDirective {

        var link: ng.IDirectiveLinkFn = function (scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
            log(scope);
            log(element);
            log(attrs);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/document-viewer-directive.html',
            replace: true,
            scope: {
                document: '='
            },
            link
        };
    }

    angular
        .module('documentViewer')
        .directive('documentViewerDirective', documentViewerDirective);


}