var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var UploadController = (function () {
            function UploadController($scope, uploadService) {
                var _this = this;
                this.uploadService = uploadService;
                this.uploadFiles = function (files) {
                    files.filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                        .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                        .reduce(function (errors, error) { errors.push(error); return errors; }, [])
                        .forEach(LASI.log);
                    return (files || _this.files || []).map(_this.uploadFile);
                };
                this.uploadFile = function (file) {
                    var promise = _this.uploadService.upload({
                        file: file,
                        url: 'api/UserDocuments',
                        method: 'POST',
                        fileName: file.name
                    });
                    promise
                        .progress(_this.progress)
                        .success(_this.success)
                        .error(_this.error);
                    return promise;
                };
                $scope.$watch('files', function (x, y) { return _this.uploadFiles; });
                this.files = [];
            }
            UploadController.prototype.progress = function (event) {
                var progressPercentage = 100.0 * event.loaded / event.total;
                LASI.log("Progress: " + progressPercentage + "% " + event.config.file.name);
            };
            UploadController.prototype.success = function (data, status, headers, config) {
                LASI.log("File " + config.file.name + " uploaded. Response: " + JSON.stringify(data));
            };
            UploadController.prototype.error = function (data, status, headers, config) {
                LASI.log("Http: " + status + " Failed to upload file.\n                Details: " + data);
            };
            UploadController.prototype.removeItem = function (file, index) {
                this.files = this.files.filter(function (f) { return f.name !== file.name; });
                $('#file-upload-list').remove("#upload-list-item-" + index);
            };
            UploadController.$inject = ['$scope', 'Upload'];
            UploadController.formats = [
                'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
                'application/msword',
                'application/pdf',
                'text/plain'
            ];
            UploadController.fileInputAcceptText = UploadController.formats.join(',');
            return UploadController;
        })();
        angular
            .module('documentList')
            .controller('UploadController', UploadController);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
