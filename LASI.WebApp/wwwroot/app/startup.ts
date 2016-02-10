startup.$inject = ['$rootScope', '$state'];
export default function startup($rootScope: ng.IRootScopeService, $state: ng.ui.IStateService) {
    $rootScope.$on('unauthorized', () => $state.go('app.login', {}, { reload : true,}));
    $rootScope.$on('$stateChangeError', console.error.bind(console));
}