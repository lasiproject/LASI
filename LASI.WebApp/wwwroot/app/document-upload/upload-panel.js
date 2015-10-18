'use strict';
define(["require", "exports", './upload.controller'], function (require, exports, upload_controller_1) {
    var template = require('app/document-upload/upload-panel.html');
    uploadPanel.$inject = ['$window'];
    function uploadPanel($window) {
        return {
            restrict: 'E',
            scope: false,
            controller: upload_controller_1.UploadController,
            template: template,
            controllerAs: 'upload'
        };
    }
    exports.uploadPanel = uploadPanel;
});
//# sourceMappingURL=upload-panel.js.map