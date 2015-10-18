/// <amd-dependency path="./upload-panel.html" />
'use strict';
System.register(['./upload.controller', './upload-panel.html'], function(exports_1) {
    var upload_controller_1, upload_panel_html_1;
    function uploadPanel($window) {
        return {
            restrict: 'E',
            scope: false,
            controller: upload_controller_1.UploadController,
            template: upload_panel_html_1.default,
            controllerAs: 'upload'
        };
    }
    exports_1("uploadPanel", uploadPanel);
    return {
        setters:[
            function (upload_controller_1_1) {
                upload_controller_1 = upload_controller_1_1;
            },
            function (upload_panel_html_1_1) {
                upload_panel_html_1 = upload_panel_html_1_1;
            }],
        execute: function() {
            uploadPanel.$inject = ['$window'];
        }
    }
});
//# sourceMappingURL=upload-panel.js.map