configureHttp.$inject = ['$httpProvider'];
export function configureHttp($httpProvider: ng.IHttpProvider) {
    interceptorFactory.$inject = ['$q'];
    function interceptorFactory($q: ng.IQService): ng.IHttpInterceptor {
        return {
            responseError: (reason) => {
                var deferred = $q.defer();
                deferred.reject(reason);
                return deferred.promise;
            }
        };
    }

    $httpProvider.interceptors.push(interceptorFactory);
}