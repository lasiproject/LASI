import TokenService from './token-service';

startup.$inject = ['$rootScope', '$state', 'TokenService'];
export default function startup($rootScope: ng.IRootScopeService, $state: ng.ui.IStateService, tokenService: TokenService) {
    $rootScope.$on('unauthorized', () => {
        tokenService.clearToken();
        $state.go('app.login', {}, { reload: true, });
    });
    $rootScope.$on('$stateChangeError', (e) => {
        console.error(e);
        $state.go('app.home');
    });
}