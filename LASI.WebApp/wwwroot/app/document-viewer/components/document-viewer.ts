import template from './document-viewer.html';

export const documentViewer: ng.IComponentOptions = {
    template,
    controllerAs: 'documentViewer',
    bindings: { document: '=' }
};