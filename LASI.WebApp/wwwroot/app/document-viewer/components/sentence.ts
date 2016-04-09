import template from './sentence.html';

export const sentence: ng.IComponentOptions = {
    template,
    controllerAs: 'sentence',
    bindings: {
        sentence: '=',
        parentId: '='
    }
};