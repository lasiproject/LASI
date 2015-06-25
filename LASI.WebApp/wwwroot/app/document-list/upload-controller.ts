module LASI.documentList {
    'use strict';
    angular
        .module('documentList')
        .controller('UploadController', UploadController);

    interface IUploadControllerScope extends ng.IScope {
        files: File[];
    }
    UploadController.$inject = ['$scope', 'Upload'];

    function UploadController($scope: IUploadControllerScope, uploadService: ng.angularFileUpload.IUploadService) {
        var vm = this;
        activate();
        vm.files = [];
        vm.uploadFile = file => uploadService.upload<IDocumentListItemModel>({
            file,
            url: 'api/UserDocuments',
            method: 'POST',
            fileName: file.name
        }).progress(progress).success(success);

        vm.uploadFiles = files => (files || []).map(vm.uploadFile);

        $scope.$watch('files', vm.uploadFiles);

        function progress(event) {
            var progressPercentage = 100.0 * event.loaded / event.total;
            log(`Progress: ${progressPercentage}% ${event.config.file.name}`);
        }
        function success(data, status, headers, config) {
            log(`File ${config.file.name} uploaded. Response: ${JSON.stringify(data) }`);
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
}