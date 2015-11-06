/// <amd-dependency path='./phrase.html' />
'use strict';
import template from 'app/document-viewer/directives/phrase.html';
import { PhraseModel } from 'app/models';
import { LexicalMenuBuilderFactory, VerbalContextmenuData } from './lexical-menu-builder';

phrase.$inject = ['lexicalMenuBuilder'];

export default function phrase(lexicalMenuBuilder: LexicalMenuBuilderFactory): angular.IDirective {
    return {
        restrict: 'E',
        template,
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
    menuIsViable(menu: VerbalContextmenuData): boolean;
}
