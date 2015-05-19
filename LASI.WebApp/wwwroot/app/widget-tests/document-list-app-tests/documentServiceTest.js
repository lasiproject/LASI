angular.module('app', []);
define(['../../widgets/document-list-app/documentService'], function () {
    angular.module('app').controller(TestController);
    function TestController(documentService) {
        console.log(documentService);
    }
    TestController.$inject = ['documentService'];
});