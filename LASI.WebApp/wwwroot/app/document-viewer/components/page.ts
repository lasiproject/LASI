import template from './page.html';

export const documentPage: ng.IComponentOptions = {
    template,
    controller: class { },
    controllerAs: 'documentPage',
    bindings: {
        page: '=',
        document: '='
    }
};