System.register(['angular-ui-router'], function(exports_1) {
    var AppController;
    return {
        setters:[
            function (_1) {}],
        execute: function() {
            AppController = (function () {
                function AppController($state, $stateParams, data) {
                    this.$state = $state;
                    this.$stateParams = $stateParams;
                    this.documents = [];
                    //$state.go('app.home');
                    this.documents = data;
                    //this.user = user();
                }
                AppController.$inject = ['$state', '$stateParams', 'data'];
                return AppController;
            })();
            exports_1("default", AppController);
        }
    }
});
