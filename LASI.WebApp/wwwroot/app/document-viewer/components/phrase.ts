import controller from './phrase.controller';
import template from './phrase.html';

export const phrase: angular.IComponentOptions = {
    bindings: {
        phrase: '=',
        parentId: '='
    },
    template,
    controller,
    controllerAs: 'phrase'
};