// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('phrase', phrase);

    phrase.$inject = ['lexicalMenuBuilder'];

    function phrase(lexicalMenuBuilder: LexicalMenuBuilderFactory): ng.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/phrase.html',
            scope: {
                phrase: '=',
                parentId: '='
            },
            link
        };

        function link(scope: PhraseScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                (<any>scope.phrase).contextmenu = contextmenu;
            }
        }
    }


    interface PhraseScope extends ng.IScope {
        phrase: models.PhraseModel;
        parentId?: string;
        menuIsViable(menu: VerbalContextmenuDataSource): boolean;
    }
}