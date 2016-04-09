import controller from './phrase.controller';
import template from './phrase.html';

export const phrase: ng.IComponentOptions = {
    bindings: {
        phrase: '=',
        parentId: '='
    },
    template,
    controller,
    controllerAs: 'phrase'
};