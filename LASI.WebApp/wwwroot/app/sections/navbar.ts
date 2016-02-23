import template from './navbar.html';
import controller from './navbar.controller';

export const navbar: ng.IComponentOptions = {
    controller,
    controllerAs: 'navbar',
    template,
    bindings: {
        user: '='
    }
};