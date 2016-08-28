import { NgModule, PLATFORM_DIRECTIVES } from '@angular/core';
import { CommonModule } from '@angular/common';
// import {  } from '@angular/platform-browser-dynamic';
import { provideRouter, Params, Router, RouterModule, ROUTER_DIRECTIVES } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic, bootstrap } from '@angular/platform-browser-dynamic';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app';
import { UserService } from './user-service';
import { TokenService } from './token-service';
import { ResultService } from './document-viewer/results-service';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import { DocumentModelService } from './document-viewer/document-model-service';
import { HTTP_PROVIDERS, Http } from '@angular/http';
import { RequestOptions as MyRequestOptions } from './configuration/http';
import documentViewerDirectives, { DocumentViewerComponent } from './document-viewer/components';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { ListItemComponent } from './document-list/list-item';

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
        BrowserModule,
        RouterModule.forRoot([{
            component: HomeComponent,
            outlet: 'main',
            path: '',
            children: [{
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
        }]), ...ROUTER_DIRECTIVES],
    bootstrap: [AppComponent],
    declarations: [...documentViewerDirectives, AppComponent, LoginComponent, ListItemComponent, HomeComponent],
    entryComponents: [AppComponent],
    providers: [UserService,
        TokenService,
        ResultService,
        LexicalMenuBuilder,
        DocumentModelService,
        {
            provide: HTTP_PROVIDERS,
            useClass: MyRequestOptions, multi: true
        }],
}) export default class Core { }