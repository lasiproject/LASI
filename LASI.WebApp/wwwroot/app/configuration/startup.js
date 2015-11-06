System.register([], function(exports_1) {
    function startup($state) {
        $state.go('app');
    }
    exports_1("default", startup);
    return {
        setters:[],
        execute: function() {
            startup.$inject = ['$state'];
        }
    }
});
