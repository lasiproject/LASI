'use strict';
define(["require", "exports", './upload-panel', 'app/angular-shim'], function (require, exports, upload_panel_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentUpload',
        requires: ['ngFileUpload'],
        directives: { uploadPanel: upload_panel_1.uploadPanel }
    };
});
//# sourceMappingURL=document-upload.module.js.map