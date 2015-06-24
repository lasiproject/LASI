module LASI.documentList {
    'use strict';
    angular
        .module('documentList')
        .controller('UploadController', UploadController);


    interface IUploadControllerScope extends ng.IScope {
        uploadFiles(files: File[]): ng.IHttpPromise<IDocumentListItemModel>[];
        uploadFile(file: File): ng.IHttpPromise<IDocumentListItemModel>;
        files: File[];
    }
    UploadController.$inject = ['$scope', '$log', 'Upload'];

    function UploadController($scope: IUploadControllerScope, $log: ng.ILogService, uploadService: ng.angularFileUpload.IUploadService) {
        $scope.files = [];
        $scope.uploadFile = file => uploadService.upload<IDocumentListItemModel>({
            file,
            url: 'api/UserDocuments',
            method: 'POST',
            fileName: file.name
        }).progress(progress).success(success);

        $scope.uploadFiles = files => (files || []).map($scope.uploadFile);

        $scope.$watch('files', $scope.uploadFiles);

        function progress(event) {
            var progressPercentage = 100.0 * event.loaded / event.total;
            $log.info(`Progress: ${progressPercentage}% ${event.config.file.name}`);
        }
        function success(data, status, headers, config) {
            $log.info(`File ${config.file.name} uploaded. Response: ${JSON.stringify(data) }`);
            //$rootScope.$apply();
        }
    }
}