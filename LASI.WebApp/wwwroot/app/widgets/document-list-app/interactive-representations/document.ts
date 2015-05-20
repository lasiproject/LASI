module App {
    'use strict';

    interface IDocument extends ng.IDirective {
    }

    interface IDocumenScope extends ng.IScope {
        document: IDocumentModel
    }

    interface IDocumenAttributes extends ng.IAttributes {
        document: IDocumentModel
    }

    document.$inject = ['$window'];
    function document($window: ng.IWindowService): IDocument {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/document.html',
            link: link,
            replace: true,
            scope: { document: '=' }
        }

        function link(scope: IDocumenScope, element: ng.IAugmentedJQuery, attrs: IDocumenAttributes) {

        }
    }

    angular
        .module(LASI.documentList.ngName)
        .directive('document', document);
}