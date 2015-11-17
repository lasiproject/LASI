System.register(['angular-ui-router'], function(exports_1) {
    'use strict';
    var AppController;
    return {
        setters:[
            function (_1) {}],
        execute: function() {
            AppController = (function () {
                function AppController($state, $stateParams, documents) {
                    this.$state = $state;
                    this.$stateParams = $stateParams;
                    this.documents = [];
                    //$state.go('app.home');
                    this.documents = documents;
                    this.currentStateName = $state.current.name;
                    //this.user = user();
                }
                AppController.$inject = ['$state', '$stateParams', 'documents'];
                return AppController;
            })();
            exports_1("default", AppController);
        }
    }
});
