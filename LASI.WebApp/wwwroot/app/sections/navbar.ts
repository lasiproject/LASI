import template from './navbar.html';
import controller from './navbar.controller';

export const navbar: ng.IComponentOptions = {
    template,
    controller,
    controllerAs: 'navbar',
    bindings: {
        user: '='
    }
};