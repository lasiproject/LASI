import 'bootstrap';
import { NgModule, PLATFORM_DIRECTIVES, ViewContainerRef } from '@angular/core';
import { provideRouter, Params, Router, RouterModule, ROUTER_DIRECTIVES } from '@angular/router';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app';
import { UserService } from './user-service';
import { TokenService } from './token-service';
import { ResultService } from './document-viewer/results-service';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import { DocumentModelService } from './document-viewer/document-model-service';
import { HTTP_PROVIDERS, Http } from '@angular/http';
import { RequestOptions as MyRequestOptions } from './configuration/http';
import { DocumentViewerComponent } from './document-viewer/components';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { ListItemComponent } from './document-list/list-item';
import AppModule from './app.module';

platformBrowserDynamic().bootstrapModule(AppModule, [PLATFORM_DIRECTIVES, ROUTER_DIRECTIVES, ViewContainerRef,
    provideRouter([{
        component: HomeComponent,
        path: '', pathMatch: '.', children: [{
            path: 'list',
            component: ListItemComponent
        }, {
            path: 'documents',
            component: DocumentViewerComponent
        }, {
            path: 'login',
            component: LoginComponent
        }
        ]
    }])
]);
// bootstrap(AppComponent, [
//     HTTP_PROVIDERS,
//     provideRouter([{
//         component: HomeComponent,
//         path: '', children: [{
//             path: 'list',
//             component: ListItemComponent
//         }, {
//             path: 'documents',
//             component: DocumentViewerComponent
//         }, {
//             path: 'login',
//             component: LoginComponent
//         }
//         ]
//     }]),
//     {
//         provide: HTTP_PROVIDERS,
//         useClass: MyRequestOptions, multi: true
//     },
//     TokenService,
//     UserService,
//     ResultService,
//     LexicalMenuBuilder,
//     DocumentModelService]);

