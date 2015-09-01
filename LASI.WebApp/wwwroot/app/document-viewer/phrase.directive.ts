namespace LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .directive('phrase', phrase);

    phrase.$inject = ['lexicalMenuBuilder'];

    function phrase(lexicalMenuBuilder: LexicalMenuBuilderFactory): angular.IDirective {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/phrase.directive.html',
            scope: {
                phrase: '=',
                parentId: '='
            },
            link
        };

        function link(scope: PhraseScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                (<any>scope.phrase).contextmenu = contextmenu;
            }
        }
    }


    interface PhraseScope extends angular.IScope {
        phrase: models.PhraseModel;
        parentId?: string;
        menuIsViable(menu: VerbalContextmenuDataSource): boolean;
    }
}