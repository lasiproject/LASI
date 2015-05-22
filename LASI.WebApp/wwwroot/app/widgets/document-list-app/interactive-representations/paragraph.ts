// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    'use strict';

    interface IParagraph extends ng.IDirective {
    }
    class Paragraph extends TextualDirective implements IParagraph {
        constructor(templateUrl: string, link: ng.IDirectiveLinkFn, public scope: ITextualDirectiveScope) {
            super(templateUrl, link, undefined, scope);
        }
    }

    interface IParagraphScope extends ng.IScope {
        paragraph: IParagraphModel
        parentId: string|number
    }

    interface IParagraphAttributes extends ng.IAttributes {
        paragraph: IParagraphModel
        parentId: string|number
    }

    paragraph.$inject = ['$window'];
    function paragraph($window: ng.IWindowService): Paragraph {
        var scope = {
            paragraph: '=',
            parentId: '='
        };

        var link: ng.IDirectiveLinkFn = function (scope: IParagraphScope, element: ng.IAugmentedJQuery, attrs: IParagraphAttributes) {

        };

        return new Paragraph('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);

    }


    angular.module('app').directive('paragraph', paragraph);
}