import template from './navbar.html';
import controller from './navbar.controller';

export function navbar(): ng.IDirective {
    return {
        restrict: 'E',
        scope: true,
        controller,
        controllerAs: 'navbar',
        template,
        bindToController: {
            user: '='
        }
    };
}