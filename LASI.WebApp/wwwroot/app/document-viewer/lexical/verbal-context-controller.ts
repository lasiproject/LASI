module LASI {
    'use strict';

    angular
        .module('documentViewer')
        .controller('VerbalContextController', VerbalContextController);


    class VerbalContextController {
        title = 'VerbalContextController';

        static $inject = ['$state'];

        //constructor(private $state: ng.ui.IStateService) {
        //    console.log($state);
        //    this.activate();
        //}

        activate() {

        }
    }

}