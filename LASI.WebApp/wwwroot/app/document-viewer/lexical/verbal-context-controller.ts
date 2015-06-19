// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module LASI {
    'use strict';

    interface IVerbalContextController {
        title: string;
        activate: () => void;
    }

    class VerbalContextController implements IVerbalContextController {
        title: string = 'VerbalContextController';

        static $inject = ['$state'];

        //constructor(private $state: ng.ui.IStateService) {
        //    console.log($state);
        //    this.activate();
        //}

        activate() {

        }
    }

    angular
        .module(LASI.documentViewer.moduleName)
        .controller('VerbalContextController', VerbalContextController);
}