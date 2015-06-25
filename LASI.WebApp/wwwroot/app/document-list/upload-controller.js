var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .controller('UploadController', UploadController);
        UploadController.$inject = ['$scope', 'Upload'];
        function UploadController($scope, uploadService) {
            var vm = this;
            activate();
            vm.files = [];
            vm.uploadFile = function (file) { return uploadService.upload({
                file: file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            }).progress(progress).success(success); };
            vm.uploadFiles = function (files) { return (files || []).map(vm.uploadFile); };
            $scope.$watch('files', vm.uploadFiles);
            function progress(event) {
                var progressPercentage = 100.0 * event.loaded / event.total;
                LASI.log("Progress: " + progressPercentage + "% " + event.config.file.name);
            }
            function success(data, status, headers, config) {
                LASI.log("File " + config.file.name + " uploaded. Response: " + JSON.stringify(data));
            }
            function activate() {
                vm.formats = [
                    'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
                    'application/msword',
                    'application/pdf',
                    'text/plain'
                ];
                vm.fileInputAcceptText = vm.formats.join(',');
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
