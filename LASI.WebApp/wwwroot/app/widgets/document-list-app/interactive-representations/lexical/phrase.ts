// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    "use strict";

    interface IPhrase extends ng.IDirective {
    }
    class Phrase extends TextualDirective implements IPhrase {
        constructor(templateUrl: string, link: ng.IDirectiveLinkFn, public scope: ITextualDirectiveScope) {
            super(templateUrl, link, undefined, scope);
        }
    }

    interface IPhraseScope extends ng.IScope {
    }

    interface IPhraseAttributes extends ng.IAttributes {
    }

    phrase.$inject = ["$window"];
    function phrase($window: ng.IWindowService): Phrase {
        var scope = {
            phrase: '=',
            parentId: '='
        };

        var link: ng.IDirectiveLinkFn = function (scope: IPhraseScope, element: ng.IAugmentedJQuery, attrs: IPhraseAttributes) {

        };

        return new Phrase('/app/widgets/document-list-app/interactive-representations/lexical/phrase.html', link, scope);
    }

    angular.module("app").directive("phrase", phrase);
}