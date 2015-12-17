configureHttp.$inject = ['$httpProvider'];
export function configureHttp($httpProvider: ng.IHttpProvider) {
    var interceptorFactory: ng.IHttpInterceptorFactory = ($q: ng.IQService) => {

        return {
            responseError: (reason) => {
                var deferred = $q.defer();
                deferred.reject(reason);
                return deferred.promise;
            }
        };

    }
    interceptorFactory.$inject = ['$q'];
    $httpProvider.interceptors.push(interceptorFactory);
}