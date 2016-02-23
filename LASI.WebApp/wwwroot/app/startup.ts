import TokenService from './token-service';

startup.$inject = ['$rootScope', '$state'];
export default function startup($rootScope: ng.IRootScopeService, $state: ng.ui.IStateService, tokenService: TokenService) {
    $rootScope.$on('unauthorized', () => {
        tokenService.clearToken();
        $state.go('app.login', {}, { reload: true, });
    });
    $rootScope.$on('$stateChangeError', console.error.bind(console));
    //$state.go('app.home');
}