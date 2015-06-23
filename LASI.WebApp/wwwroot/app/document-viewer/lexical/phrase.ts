// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module LASI.documentViewer {
    'use strict';

    interface IPhrase extends ng.IDirective {
    }

    interface IPhraseScope extends ng.IScope {
        phrase: IPhraseModel;
        menuIsViable(menu: IVerbalContextmenu): boolean;
    }

    interface IPhraseAttributes extends ng.IAttributes {
    }

    phrase.$inject = ['lexicalMenuBuilder'];
    function phrase(lexicalMenuBuilder: ILexicalMenuBuilderFactory): IPhrase {

        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/lexical/phrase.html',
            link: function (scope: IPhraseScope, element: ng.IAugmentedJQuery, attrs: IPhraseAttributes) {
                var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
                scope.phrase.hasContextmenu = !!contextmenu;
                if (scope.phrase.hasContextmenu) {
                    (<any>scope.phrase).contextmenu = contextmenu;
                }
            },
            scope: {
                phrase: '=',
                parentId: '='
            }
        };

    }

    angular.module('documentViewer').directive('phrase', phrase);
}