import TokenService from 'app/token-service';

configureHttp.$inject = ['$httpProvider'];
export default function configureHttp($httpProvider: ng.IHttpProvider) {
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers['accept'] = 'application/json';
    $httpProvider.defaults.headers['Upgrade-Insecure-Requests'] = '1';
    interceptorFactory.$inject = ['TokenService'];

    function interceptorFactory(tokenService: TokenService): ng.IHttpInterceptor {
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
                switch (reason.status) {
                    case 401:
                        console.info('Anauthorized');

                        break;
                    case 404:
                        console.info('not found');
                        break;
                    default:
                        return;
                }
            }
        };
    }
    $httpProvider.interceptors.push(interceptorFactory);
}