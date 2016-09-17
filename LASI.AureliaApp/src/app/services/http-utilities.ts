import { RequestInit } from 'aurelia-fetch-client';

export const headers = {
  'Content-Type': 'application/x-www-form-urlencoded'
};

export const getConfig: RequestInit = {
  headers,
  method: 'GET'
};

interface AddBody {
  headers: typeof headers;
  method: 'GET' | 'POST';
  withBody<T>(this: this, body: T): this & WithBody<T>;
}

interface WithBody<T> {
  body: T;
}

export const postConfig: AddBody = {
  headers,
  method: 'POST',
  withBody<Body>(this: AddBody & WithBody<Body>, body: Body) {
    this.body = body;
    return this;
  }
};