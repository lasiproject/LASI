/// <amd-dependency path="./page.html" />
'use strict';
import template from 'app/document-viewer/directives/page.html';
export default function documentPage(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            page: '=',
            document: '='
        }
    };
}