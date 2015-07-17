module LASI.documentList {
    'use strict';

    interface UploadControllerStructure {
        uploadFile(file: File): ng.angularFileUpload.IUploadPromise<DocumentListItemModel>;
        uploadFiles(files: File[]): ng.angularFileUpload.IUploadPromise<DocumentListItemModel>[];
    }

    class UploadController implements UploadControllerStructure {
        static $inject = ['$scope', 'Upload'];

        static formats = [
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            'application/msword',
            'application/pdf',
            'text/plain'
        ];

        files: File[];

        static fileInputAcceptText = UploadController.formats.join(',');
        constructor($scope: ng.IScope, private uploadService: ng.angularFileUpload.IUploadService) {
            $scope.$watch('files', (x, y) => this.uploadFiles);
            this.files = [];
        }
        uploadFiles = (files: File[]) => {
            files.filter(file => UploadController.formats.every(format => file.type.localeCompare(format) !== 0))
                .map(file => `File ${file.name} has unaccepted format ${file.type}`)
                .reduce((errors, error) => { errors.push(error); return errors; }, [])
                .forEach(log);
            return (files || this.files || []).map(this.uploadFile);
        };

        uploadFile = (file: File) => {
            var promise = this.uploadService.upload<DocumentListItemModel>({
                file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            });
            promise
                .progress(this.progress)
                .success(this.success)
                .error(this.error);
            return promise;
        };
        progress(event) {
            var progressPercentage = 100.0 * event.loaded / event.total;
            log(`Progress: ${progressPercentage}% ${event.config.file.name}`);
        }

        success(data, status, headers, config) {
            log(`File ${config.file.name} uploaded. Response: ${JSON.stringify(data) }`);
        }
        error(data, status, headers, config) {
            log(`Http: ${status} Failed to upload file.
                Details: ${data}`);
        }
        removeItem(file: File, index: number) {
            this.files = this.files.filter(f => f.name !== file.name);
            $('#file-upload-list').remove(`#upload-list-item-${index}`);
        }
    }

    angular
        .module('documentList')
        .controller('UploadController', UploadController);

}