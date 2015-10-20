/// <amd-dependency path="./upload-panel.html" />
'use strict';
import { UploadController as controller } from './upload.controller';
import template from 'app/document-upload/upload-panel.html';

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