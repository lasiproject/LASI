import template from './paragraph.html';

export const paragraph: ng.IComponentOptions = {
    template,
    controllerAs: 'paragraph',
    bindings: {
        paragraph: '=',
        parentId: '='
    }
};