module LASI.documentViewer {
    'use strict';


    interface IParagraph extends ng.IDirective {
    }

    interface IParagraphScope extends ng.IScope {
        paragraph: models.IParagraphModel;
        parentId: string|number;
    }

    interface IParagraphAttributes extends ng.IAttributes {
        paragraph: models.IParagraphModel;
        parentId: string|number;
    }

    paragraph.$inject = ['$window'];
    function paragraph($window: ng.IWindowService): IParagraph {

        var link: ng.IDirectiveLinkFn = function (scope: IParagraphScope, element: ng.IAugmentedJQuery, attrs: IParagraphAttributes) {
            //console.log(scope.parentId);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/paragraph.html',
            link: link,
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };

    }
    angular
        .module('documentViewer')
        .directive('paragraph', paragraph);
}