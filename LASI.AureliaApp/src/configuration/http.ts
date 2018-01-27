// import {
//     autoinject,
//     Lazy,
//     factory,
//     inject,
//     Factory,
//     resolver,
//     Resolver
// } from 'aurelia-framework';
// import {
//     HttpClient,
//     HttpClientConfiguration
// } from 'aurelia-fetch-client';
// import TokenService from 'src/app/token-service';

// export class ConfigProvider {
//     @inject(TokenService, HttpClient)
//     static configure = (tokenService: TokenService, httpClient: HttpClient) => {
//         return httpClient.configure(config => config
//             .useStandardConfiguration()
//             .withBaseUrl('//localhost:51641')
//             .withDefaults({
//                 credentials: 'include',
//                 mode: 'cors'
//             })
//             .rejectErrorResponses()
//             .withInterceptor({
//                 request(config) {
//                     if (tokenService.token) {
//                         config.headers.append('Authorization', `Bearer ${tokenService.token}`);
//                     }
//                     config.headers.append('Scheme', 'Bearer');
//                     config.headers.append('WWW-Authenticate', 'Bearer');
//                     return config
//                 }
//             }));
//     }
//     @inject(TokenService, HttpClient)
//     static apply = (ConfigProvider.configure);
// }
