module App {
    'use strict';

    interface IDocument extends ng.IDirective {
    }

    interface IDocumentScope extends ng.IScope {
        document: IDocumentModel
    }

    interface IDocumentAttributes extends ng.IAttributes {
        document: IDocumentModel
    }

    document.$inject = ['$window'];
    function document($window: ng.IWindowService): IDocument {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/document.html',
            link: link,
            replace: true,
            scope: {
                document: '='
            }
        };

        function link(scope: IDocumentScope, element: ng.IAugmentedJQuery, attrs: IDocumentAttributes) {

        }
    }

    angular
        .module(LASI.documentList.ngName)
        .directive('document', document);
}