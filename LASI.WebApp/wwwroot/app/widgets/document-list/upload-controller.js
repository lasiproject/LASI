var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .controller('UploadController', UploadController);
        UploadController.$inject = ['$scope', '$log', 'Upload'];
        function UploadController($scope, $log, uploadService) {
            $scope.files = [];
            $scope.uploadFile = function (file) { return uploadService.upload({
                file: file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            }).progress(progress).success(success); };
            $scope.uploadFiles = function (files) { return (files || []).map($scope.uploadFile); };
            $scope.$watch('files', $scope.uploadFiles);
            function progress(event) {
                var progressPercentage = 100.0 * event.loaded / event.total;
                $log.info("Progress: " + progressPercentage + "% " + event.config.file.name);
            }
            function success(data, status, headers, config) {
                $log.info("File " + config.file.name + " uploaded. Response: " + JSON.stringify(data));
                //$rootScope.$apply();
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
