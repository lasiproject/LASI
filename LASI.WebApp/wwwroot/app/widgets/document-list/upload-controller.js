var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var UploadController = (function () {
            function UploadController($scope, $log, uploadService) {
                $scope.files = [];
                $scope.uploadFile = function (file) { return uploadService.upload({
                    url: 'api/UserDocuments',
                    file: file,
                    method: 'POST',
                    fileName: file.name
                }).progress(progress).success(success); };
                $scope.uploadFiles = function (files) { return (files || []).map($scope.uploadFile); };
                $scope.$watch('files', $scope.uploadFiles);
                function progress(evt) {
                    var progressPercentage = 100.0 * evt.loaded / evt.total;
                    $log.info("Progress: " + progressPercentage + "% " + evt.config.file.name);
                }
                function success(data, status, headers, config) {
                    $log.info("File '" + config.file.name + " 'uploaded. Response: " + JSON.stringify(data));
                    //$rootScope.$apply();
                }
            }
            UploadController.$inject = ['$scope', '$log', 'Upload'];
            return UploadController;
        })();
        angular
            .module(documentList.moduleName)
            .controller('UploadController', UploadController);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=upload-controller.js.map