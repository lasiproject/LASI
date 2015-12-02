'use strict';
import template from 'app/document-viewer/directives/page.html';
export function documentPage(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            page: '=',
            document: '='
        }
    };
}