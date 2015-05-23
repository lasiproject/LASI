module App {
    'use strict';

    interface IDocument extends ng.IDirective {
    }


    interface IDocumentScope extends ng.IScope {
    }

    interface IDocumentAttributes extends ng.IAttributes {
    }

    document.$inject = ['$window'];
    function document($window: ng.IWindowService): IDocument {

        var link: ng.IDirectiveLinkFn = function (scope: IDocumentScope, element: ng.IAugmentedJQuery, attrs: IDocumentAttributes) {
            console.log(scope); 
        }
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/document.html',
            replace: true,
            scope: {
                document: '='
            },
            link: link
        };
       
        //return new Document('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);

    }

    angular
        .module('interactiveRepresentations')
        .directive('document', document);
}