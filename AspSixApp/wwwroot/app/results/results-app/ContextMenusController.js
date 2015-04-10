(function () {
    'use strict';

    angular
        .module(LASI.results.ngName)
        .controller('ContextMenusController', ContextMenusController);

    ContextMenusController.$inject = ['$location'];

    function ContextMenusController($location) {
        var vm = this;
        vm.title = 'ContextMenusController';

        activate();

        function activate() {
            vm.menuOptions = [
                ['Select', function ($itemScope) {
                    vm.selected = $itemScope.item.name;
                }],
                null, // Dividier 
                ['Remove', function ($itemScope) {
                    vm.items.splice($itemScope.$index, 1);
                }]
            ];
        }
    }
})();
