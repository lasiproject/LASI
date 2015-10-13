'use strict';
import { DocumentListItemModel } from 'document-list-service.provider';
import ngf = angular.angularFileUpload;
var log = console.log.bind(console);
export class UploadController {
    static $inject = ['$scope', '$q', 'Upload'];

    constructor(private $scope: angular.IScope,
        private $q: ng.IQService,
        private uploadService: ngf.IUploadService) {
        this.$scope.$watch('upload.files', this.uploadFiles.bind(this));
    }

    uploadFiles() {
        this.logInvalidFiles();
        var files = this.files;
        return this.$q.when((Array.isArray(files) ? files : [files]).map(file => this.uploadFile(file)));
    }
    logInvalidFiles() {
        var files = this.files;
        (Array.isArray(files) ? files : [files]).filter(file => UploadController.formats.every(format => file.type.localeCompare(format) !== 0))
            .map(file => `File ${file.name} has unaccepted format ${file.type}`)
            .forEach(log);
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
        var files = this.files;
        this.files = (Array.isArray(files) ? files : [files]).filter(f => f.name !== file.name);
        $('#file-upload-list').remove(`#upload-list-item-${index}`);
    }

    files: File | File[] = [];

    private static formats = [
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
        'application/msword',
        'application/pdf',
        'text/plain'
    ];

} 