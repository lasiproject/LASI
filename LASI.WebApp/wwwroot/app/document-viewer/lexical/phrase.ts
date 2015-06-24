// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('phrase', phrase);

    phrase.$inject = ['lexicalMenuBuilder'];

    function phrase(lexicalMenuBuilder: ILexicalMenuBuilderFactory): IPhrase {

        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/lexical/phrase.html',
            link,
            scope: {
                phrase: '=',
                parentId: '='
            }
        };

        function link(scope: IPhraseScope, element: ng.IAugmentedJQuery, attrs: IPhraseAttributes) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenu = !!contextmenu;
            if (scope.phrase.hasContextmenu) {
                (<any>scope.phrase).contextmenu = contextmenu;
            }
        }

    }
    interface IPhrase extends ng.IDirective {
    }

    interface IPhraseScope extends ng.IScope {
        phrase: IPhraseModel;
        menuIsViable(menu: IVerbalContextmenuDataSource): boolean;
    }

    interface IPhraseAttributes extends ng.IAttributes {
    }

}