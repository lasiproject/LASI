module LASI.documentViewer {
    'use strict';

    documentViewerDirective.$inject = [];

    function documentViewerDirective(): IDocument {

        var link: ng.IDirectiveLinkFn = function (scope: IDocumentScope, element: ng.IAugmentedJQuery, attrs: IDocumentAttributes) {
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
    interface IDocument extends ng.IDirective {
    }

    interface IDocumentScope extends ng.IScope {
    }

    interface IDocumentAttributes extends ng.IAttributes {
    }
    angular
        .module('documentViewer')
        .directive('documentViewerDirective', documentViewerDirective);


}