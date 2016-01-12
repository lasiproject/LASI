export default class TokenService {
    static $inject = ['$window'];
    constructor(private $window: ng.IWindowService) { }

    get token(): string {
        return this.$window.sessionStorage[this.tokenKey];
    }

    set token(value) {
        this.$window.sessionStorage[this.tokenKey] = value;
    }

    clearToken() {
        this.$window.sessionStorage.removeItem(this.tokenKey);
    }

    tokenKey = "auth_token";
}