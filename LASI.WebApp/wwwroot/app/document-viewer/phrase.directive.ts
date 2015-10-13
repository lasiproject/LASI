'use strict';
import { PhraseModel } from 'app/models';
import { LexicalMenuBuilderFactory, VerbalContextmenuDataSource } from './lexical-menu-builder.service';
phrase.$inject = ['lexicalMenuBuilder'];

export function phrase(lexicalMenuBuilder: LexicalMenuBuilderFactory): angular.IDirective {
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
    phrase: PhraseModel;
    parentId?: string;
    menuIsViable(menu: VerbalContextmenuDataSource): boolean;
}

angular
    .module('documentViewer')
    .directive({ phrase });
