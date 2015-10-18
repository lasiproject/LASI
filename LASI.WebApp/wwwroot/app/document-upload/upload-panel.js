/// <amd-dependency path="./upload-panel.html" />
'use strict';
define(["require", "exports", './upload.controller', "./upload-panel.html"], function (require, exports, upload_controller_1) {
    var template = require('./upload-panel.html');
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