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
            templateUrl: '/app/document-viewer/phrase.html',
            scope: {
                phrase: '=',
                parentId: '='
            },
            link
        };

        function link(scope: IPhraseScope, element: ng.IAugmentedJQuery, attrs: IPhraseAttributes) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                (<any>scope.phrase).contextmenu = contextmenu;
            }
        }
    }

    interface IPhrase extends ng.IDirective { }

    interface IPhraseScope extends ng.IScope {
        phrase: models.IPhraseModel;
        parentId?: string;
        menuIsViable(menu: IVerbalContextmenuDataSource): boolean;
    }

    interface IPhraseAttributes extends ng.IAttributes { }

}