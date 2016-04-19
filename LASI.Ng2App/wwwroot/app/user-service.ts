import { Injectable } from 'angular2/core';
import $ from 'jquery';
import { Http, Headers } from 'angular2/http';
import { TokenService } from 'app/token-service';
import { Subject, Observable } from 'rxjs/Rx';

@Injectable()
export class UserService {
    constructor(private $http: Http, private tokenService: TokenService) { }

    login({ email, password, rememberMe }: Credentials) {
        const data = {
            email,
            password,
            rememberMe
        };

        return this.$http.get(url('authenticate'), new Headers(UserService.requestConfig))
            .catch((error, caught) => this.$http.post(url('authenticate'), $.param(data), new Headers(UserService.requestConfig)))
            .map(user => user.json())
            .subscribe(this.loginSuccess.bind(this));

    }

    logoff() {

        return this.$http
            .post(url('authenticate/logoff'), '', new Headers(UserService.requestConfig))
            .subscribe(response => {
                console.log(response);
                this.tokenService.clearToken();
                this.loggedIn = false;
                this.user = undefined;
            });
    }

    getUser() {
        return Observable.from([this.user])
            .map(x => !x ? Observable.throw('user undefined') : x)
    }

    getDetails() {
        return this.$http.get(url('manage/account'))
            .map(data => data.json());
    }

    saveDetails(details: any) {
        return this.$http.post(('manage/account'), details);
    }

    loginSuccess = ({ user, token }) => {
        this.user = user;
        this.loggedIn = true;
        if (token) {
            this.tokenService.token = token;
        }
        return this.user;
    };

    user: User;
    loggedIn = false;

    static requestConfig = {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    };
}
const baseUrl = "//localhost:51641";
function url(path: string) {
    return `${baseUrl}/api/${path}/`;
}