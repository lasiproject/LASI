import 'bootstrap';
import { provide } from 'angular2/core';
import { bootstrap as ng2Bootstrap } from 'angular2/platform/browser';
import { AppComponent } from './app';
import UserService from './user-service';
import TokenService from './token-service';
import ResultService from './document-viewer/results-service';
import LexicalMenuBuilder from 'app/document-viewer/lexical-menu-builder';
import DocumentModelService from './document-viewer/document-model-service';
import { HTTP_PROVIDERS, Http, RequestOptions } from 'angular2/http';
import { RequestOptions  as MyRequestOptions } from './configuration/http';

export function bootstrap() {
    return ng2Bootstrap(AppComponent, [
        HTTP_PROVIDERS,
        provide(RequestOptions, {
            useClass: MyRequestOptions
        }),
        TokenService,
        UserService,
        ResultService,
        LexicalMenuBuilder,
        DocumentModelService,
    ]).then(() => console.log(`Bootstrap success`)).catch(reason => console.error(reason));
}