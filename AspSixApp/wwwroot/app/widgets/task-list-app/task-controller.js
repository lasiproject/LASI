(function () {
    'use strict';

    angular
        .module(LASI.taskList.ngName)
        .controller('TaskController', TaskController);

    TaskController.$inject = ['$location'];

    function TaskController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TaskController';

        activate();

        function activate() { }
    }
})();
