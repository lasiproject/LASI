module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('document', document);


    document.$inject = [];

    function document(): IDocument {

        var link: ng.IDirectiveLinkFn = function (scope: IDocumentScope, element: ng.IAugmentedJQuery, attrs: IDocumentAttributes) {
            log(scope);
            log(element);
            log(attrs);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/document.html',
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
}