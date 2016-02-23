import controller from './upload.controller';
import template from './upload-panel.html';

uploadPanel.$inject = ['$window'];
export function uploadPanel($window): ng.IDirective {
    return {
        restrict: 'E',
        scope: false,
        controller,
        template,
        controllerAs: 'upload'
    };
}