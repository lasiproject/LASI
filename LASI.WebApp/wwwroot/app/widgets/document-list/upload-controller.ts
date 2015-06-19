module LASI.documentList {
    'use strict';

    interface IUploadController {

    }
    interface IUploadControllerScope extends ng.IScope {
        uploadFiles(files: File[]): ng.IHttpPromise<IDocumentListItemModel>[];
        uploadFile(file: File): ng.IHttpPromise<IDocumentListItemModel>;
        files: File[];
    }
    class UploadController implements IUploadController {
        static $inject: string[] = ['$scope', '$log', 'Upload'];

        constructor($scope: IUploadControllerScope, $log: ng.ILogService, uploadService: ng.angularFileUpload.IUploadService) {
            $scope.files = [];
            $scope.uploadFile = (file) => uploadService.upload<IDocumentListItemModel>({
                url: 'api/UserDocuments',
                file: file,
                method: 'POST',
                fileName: file.name
            }).progress(progress).success(success);
            $scope.uploadFiles = (files) => (files || []).map($scope.uploadFile);

            $scope.$watch('files', $scope.uploadFiles);

            function progress(evt) {
                var progressPercentage = 100.0 * evt.loaded / evt.total;
                $log.info(`Progress: ${progressPercentage}% ${evt.config.file.name}`);
            }
            function success(data, status, headers, config) {
                $log.info(`File '${config.file.name} 'uploaded. Response: ${JSON.stringify(data) }`);
                //$rootScope.$apply();
            }
        }

    }

    angular
        .module(moduleName)
        .controller('UploadController', UploadController);
}