// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    'use strict';

    interface IPhrase extends ng.IDirective {
    }


    interface IPhraseScope extends ng.IScope {
        phrase: IPhraseModel;
    }

    interface IPhraseAttributes extends ng.IAttributes {
    }


    function phrase(): IPhrase {
        return {
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/interactive-representations/lexical/phrase.html',
            link: function (scope: IPhraseScope, element: ng.IAugmentedJQuery, attrs: IPhraseAttributes) {
                console.log(attrs);
                var menu = scope.phrase.contextmenu;
            },
            scope: {
                phrase: '=',
                parentId: '='
            }
        };

    }

    angular.module('interactiveRepresentations').directive('phrase', phrase);
}