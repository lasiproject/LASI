namespace LASI.documentList {
    'use strict';
    import ngf = angular.angularFileUpload;

    class UploadController {
        static $inject = ['$scope', 'Upload'];

        constructor(private $scope: angular.IScope, private uploadService: ngf.IUploadService) {
            this.$scope.$watch('files', this.uploadFiles.bind(this));
        }

        uploadFiles() {
            this.files
                .filter(file => UploadController.formats.every(format => file.type.localeCompare(format) !== 0))
                .map(file => `File ${file.name} has unaccepted format ${file.type}`)
                .reduce((errors, error) => { errors.push(error); return errors; }, [])
                .forEach(log);
            return this.files.map(file => this.uploadFile(file));
        }

        uploadFile(file: File) {
            return this.uploadService
                .upload<DocumentListItemModel>({
                    file,
                    url: 'api/UserDocuments',
                    method: 'POST',
                    fileName: file.name
                })
                .progress(data => log(`Progress: ${100.0 * data.progress / data.percentComplete}% ${file.name}`))
                .success(data => log(`File ${file.name} uploaded. \nResponse: ${JSON.stringify(data) }`))
                .error((data, status) => log(`Http: ${status} Failed to upload file. \nDetails: ${data}`));
        }

        removeFile(file: File, index: number) {
            this.files = this.files.filter(f => f.name !== file.name);
            $('#file-upload-list').remove(`#upload-list-item-${index}`);
        }

        files: File[] = [];

        static formats = [
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            'application/msword',
            'application/pdf',
            'text/plain'
        ];

    }

    angular
        .module('documentList')
        .controller('UploadController', UploadController);
}