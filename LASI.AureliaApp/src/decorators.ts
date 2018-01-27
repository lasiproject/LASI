import 'core-js';
import {HttpClient} from 'aurelia-fetch-client';

export const api = <Target extends HttpClient>(resource?: string) => (target: Target) => {
    const normalizedResource = resource && resource;
    return target.configure(config => config.withBaseUrl(`${config.baseUrl}/api/${resource}/`));
};

export type ApiClientTarget = {
    new <Http extends HttpClient>(http: Http): ApiClientTarget;
};