import TokenService from 'app/token-service';

export class UserService {
    static $inject = ['$q', '$http', 'TokenService'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService, private tokenService: TokenService) { }

    login({ email, password, rememberMe }: Credentials): ng.IPromise<User> {
        const promise = this.loggedIn ? this.getUser() : this.$q.resolve({});

        const data = {
            email,
            password,
            rememberMe
        };
        return promise.then(() => this.loginGet()
            .then(user => {
                if (!user) {
                    throw 'not logged in';
                } else {
                    return user;
                }
            }))
            .catch(error => this.loginPost(data))
            .then(this.loginSuccess)
            .catch(error => {
                console.info(error);
                return undefined;
            });

    }
    loginPost(data: {}) {


        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        return this.$http
            .post<AuthenticationResult>('/api/authenticate', angular.element.param(data), UserService.requestConfig)
            .then(response => response.data);
    }

    loginGet() {
        if (this.tokenService.token) {
            return this.$http.get<AuthenticationResult>('/api/authenticate', UserService.requestConfig)
                .then(response => response.data)
                .catch(reason => this.$q.reject(reason));
        } else {
            return this.$q.reject('not logged in');
        }
    }

    logoff(): ng.IPromise<any> {

        return this.$http
            .post<User>('/api/authenticate/logoff', {}, UserService.requestConfig)
            .then(response => {
                console.log(response);
                this.tokenService.clearToken();
                this.loggedIn = false;
                this.user = undefined;
                return response.data;
            });


    }

    getUser(): ng.IPromise<User> {
        if (this.user) {
            return this.$q.resolve(this.user);
        } else if (this.tokenService.token) {
            return this.loginGet()
                .then(this.loginSuccess);
        } else {
            console.info('not logged in');
            return this.$q.resolve(undefined);
        }
    }

    getDetails(): ng.IPromise<any> {
        return this.$http.get('/api/manage/account')
            .then(({ data }) => data);
    }

    saveDetails(details: any): ng.IPromise<any> {
        return this.$http.post('api/manage/account', details);
    }

    loginSuccess = ({user, token}) => {
        this.user = user;
        this.loggedIn = true;
        if (token) {
            this.tokenService.token = token;
        }
        return this.user;
    };

    user: User;
    loggedIn = false;

    static requestConfig: ng.IRequestShortcutConfig = { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } };
}