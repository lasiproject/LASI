import controller from './search-box.controller';
import template from './search-box.html';

export function searchBox(): angular.IDirective {
    return {
        restrict: 'E',
        scope: true,
        controller,
        controllerAs: 'search',
        template,
        bindToController: {
            searchContext: '=',
            find: '='
        }
    };
}