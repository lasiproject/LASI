import template from './sentence.html';

export const sentence: angular.IComponentOptions = {
    template,
    controllerAs: 'sentence',
    bindings: {
        sentence: '=',
        parentId: '='
    }
};