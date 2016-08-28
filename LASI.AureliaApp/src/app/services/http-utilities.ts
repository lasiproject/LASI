import { RequestInit } from 'aurelia-fetch-client';

export const headers = {
    'Content-Type': 'application/x-www-form-urlencoded'
};

export const getConfig: RequestInit = {
    headers,
    method: 'GET'
};
interface AddBody {
    headers: typeof headers; method: 'GET' | 'POST';
    /*tslint:disable */ // This is a bug, tslint should not be flagging this code.
    withBody<T>(this: this, body: T): this & WithBody<T>;
    /*tslint:enable */
}
interface WithBody<T> {
    body: T;
}

export const postConfig: AddBody = {
    headers,
    method: 'POST',
    /*tslint:disable no-bitwise */ // This is a bug, tslint should not be flagging this code.
    withBody<Body>(this: AddBody & WithBody<Body>, body: Body) {
        /*tslint:enable */
        this.body = body;
        return this;
    }
    /*tslint:disable no-unreachable */ // This is a bug, tslint should not be flagging this code.
};
/*tslint:enable */