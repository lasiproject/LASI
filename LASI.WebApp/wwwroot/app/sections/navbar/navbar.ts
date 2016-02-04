'use strict';
import template from 'app/sections/navbar/navbar.html';
import controller from './navbar.controller';

export function navbar(): ng.IDirective {
    return {
        restrict: 'E',
        scope: true,
        controller,
        controllerAs: 'navbar',
        bindToController: {
            user: '='
        },
        template,
    };
}