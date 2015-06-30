var LASI;
(function (LASI) {
    var documentUpload;
    (function (documentUpload) {
        'use strict';
        angular.module('documentUpload', [
            'ngFileUpload'
        ]).config(configure);
        configure.$inject = [];
        function configure() { }
    })(documentUpload = LASI.documentUpload || (LASI.documentUpload = {}));
})(LASI || (LASI = {}));
