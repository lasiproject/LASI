import TokenService from 'app/token-service';

configureHttp.$inject = ['$httpProvider'];
export default function configureHttp($httpProvider: ng.IHttpProvider) {
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers['accept'] = 'application/json';
    $httpProvider.defaults.headers['Upgrade-Insecure-Requests'] = '1';
    interceptorFactory.$inject = ['$q', '$rootScope', 'TokenService'];

    function interceptorFactory($q: ng.IQService, $rootScope: ng.IRootScopeService, tokenService: TokenService): ng.IHttpInterceptor {
        return {
            request: requestConfig => {
                requestConfig.withCredentials = true;
                if (tokenService.token) {
                    requestConfig.headers['Authorization'] = `Bearer ${tokenService.token}`;

                }
                requestConfig.headers['Scheme'] = 'Bearer';
                requestConfig.headers['WWW-Authenticate'] = 'Bearer';
                return requestConfig;
            },
            responseError: reason => {
                if (reason.status === 401) {
                    $rootScope.$broadcast('unauthorized');
                }
                var deferred = $q.defer();
                deferred.reject(reason);
                return deferred.promise;
            }
        };
    }
    $httpProvider.interceptors.push(interceptorFactory);
}