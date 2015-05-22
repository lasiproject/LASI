// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    "use strict";

    interface ISentence extends ng.IDirective {
    }
    class Sentence extends TextualDirective implements ISentence {
        constructor(templateUrl: string, link: ng.IDirectiveLinkFn, public scope: ITextualDirectiveScope) {
            super(templateUrl, link, undefined, scope);
        }
    }
    interface ISentenceScope extends ng.IScope {
        sentence: ISentenceModel
        parentId: string|number
    }

    interface ISentenceAttributes extends ng.IAttributes {
    }

    sentence.$inject = ["$window"];
    function sentence($window: ng.IWindowService): Sentence {
        var scope = {
            sentence: '=',
            parentId: '='
        };

        var link: ng.IDirectiveLinkFn = function (scope: ISentenceScope, element: ng.IAugmentedJQuery, attrs: ISentenceAttributes) {

        };
        return new Sentence('/app/widgets/document-list-app/interactive-representations/sentence.html', link, scope);
    }

    angular.module("app").directive("sentence", sentence);
}