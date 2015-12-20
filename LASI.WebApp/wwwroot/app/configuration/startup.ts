'use strict';
startup.$inject = ['$rootScope', '$state'];
export function startup($rootScope: ng.IRootScopeService, $state: ng.ui.IStateService) {
    $rootScope.$on('$stateChangeError', console.error.bind(console));
    //$state.go('app.home');
}