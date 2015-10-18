'use strict';
define(["require", "exports"], function (require, exports) {
    var log = console.log.bind(console);
    var UploadController = (function () {
        function UploadController($scope, $q, uploadService) {
            this.$scope = $scope;
            this.$q = $q;
            this.uploadService = uploadService;
            this.files = [];
            this.$scope.$watch('upload.files', this.uploadFiles.bind(this));
        }
        UploadController.prototype.uploadFiles = function () {
            var _this = this;
            this.logInvalidFiles();
            var files = this.files;
            return this.$q.when((Array.isArray(files) ? files : [files]).map(function (file) { return _this.uploadFile(file); }));
        };
        UploadController.prototype.logInvalidFiles = function () {
            var files = this.files;
            (Array.isArray(files) ? files : [files]).filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                .forEach(log);
        };
        UploadController.prototype.uploadFile = function (file) {
            return this.uploadService
                .upload({
                file: file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            })
                .progress(function (data) { return log("Progress: " + 100.0 * data.progress / data.percentComplete + "% " + file.name); })
                .success(function (data) { return log("File " + file.name + " uploaded. \nResponse: " + JSON.stringify(data)); })
                .error(function (data, status) { return log("Http: " + status + " Failed to upload file. \nDetails: " + data); });
        };
        UploadController.prototype.removeFile = function (file, index) {
            var files = this.files;
            this.files = (Array.isArray(files) ? files : [files]).filter(function (f) { return f.name !== file.name; });
            $('#file-upload-list').remove("#upload-list-item-" + index);
        };
        UploadController.$inject = ['$scope', '$q', 'Upload'];
        UploadController.formats = [
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            'application/msword',
            'application/pdf',
            'text/plain'
        ];
        return UploadController;
    })();
    exports.UploadController = UploadController;
});
//# sourceMappingURL=upload.controller.js.map