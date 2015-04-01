(function () {
    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .controller('ListController', ListController);

    ListController.$inject = ['documentService'];

    function ListController(documentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ListController';
        vm.documents = [];
        Object.defineProperty(vm, 'documentCount', {
            get: function () { return vm.documents.length; },
            enumerable: true,
            configurable: false
        });
        vm.expanded = false;
        activate();

        function activate() {
            documentService.getData()
                .then(function (data) {
                    vm.documents = data;
                });
        }
    }
})();
