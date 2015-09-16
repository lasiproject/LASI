var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var UploadController = (function () {
            function UploadController($scope, $q, uploadService) {
                this.$scope = $scope;
                this.$q = $q;
                this.uploadService = uploadService;
                this.files = [];
                this.$scope.$watch('files', this.uploadFiles.bind(this));
            }
            UploadController.prototype.uploadFiles = function () {
                var _this = this;
                this.files
                    .filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                    .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                    .forEach(LASI.log);
                return this.files.map(function (file) { return _this.uploadFile(file); });
            };
            UploadController.prototype.uploadFile = function (file) {
                return this.uploadService
                    .upload({
                    file: file,
                    url: 'api/UserDocuments',
                    method: 'POST',
                    fileName: file.name
                })
                    .progress(function (data) { return LASI.log("Progress: " + 100.0 * data.progress / data.percentComplete + "% " + file.name); })
                    .success(function (data) { return LASI.log("File " + file.name + " uploaded. \nResponse: " + JSON.stringify(data)); })
                    .error(function (data, status) { return LASI.log("Http: " + status + " Failed to upload file. \nDetails: " + data); });
            };
            UploadController.prototype.removeFile = function (file, index) {
                this.files = this.files.filter(function (f) { return f.name !== file.name; });
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
        angular
            .module('documentList')
            .controller({ UploadController: UploadController });
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
