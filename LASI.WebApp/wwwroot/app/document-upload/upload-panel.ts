'use strict';
import { UploadController as controller } from './upload.controller';
var template = require('app/document-upload/upload-panel.html');
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