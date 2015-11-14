System.register(['app/angular-shim', './upload-panel'], function(exports_1) {
    'use strict';
    var upload_panel_1;
    return {
        setters:[
            function (_1) {},
            function (upload_panel_1_1) {
                upload_panel_1 = upload_panel_1_1;
            }],
        execute: function() {
            exports_1("default",{
                name: 'documentUpload',
                requires: ['ngFileUpload'],
                directives: { uploadPanel: upload_panel_1.uploadPanel }
            });
        }
    }
});
