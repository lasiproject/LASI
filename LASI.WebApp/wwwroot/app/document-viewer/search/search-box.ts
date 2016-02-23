import controller from './search-box.controller';
import template from './search-box.html';

export const searchBox: ng.IComponentOptions = {
    controller,
    controllerAs: 'search',
    template,
    bindings: {
        searchContext: '=',
        find: '='
    }
};