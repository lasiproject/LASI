import { Component } from '@angular/core';
import { HTTP_PROVIDERS, Http } from '@angular/http';
import { Router, ROUTER_DIRECTIVES, Params } from '@angular/router';
// import 'rxjs/Rx'; // load the full rxjs
import { Injectable } from './ng2-utils';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { NavbarComponent } from './navbar';
import { DocumentViewerComponent } from './document-viewer/components';
import { DocumentModelService } from './document-viewer/document-model-service';
import template from 'app/app.html';

@Component({
    selector: 'app',
    directives: [
        ROUTER_DIRECTIVES,
        NavbarComponent,
        LoginComponent
    ],
    providers: [
        DocumentModelService,
        HTTP_PROVIDERS
    ],
    template
})
export class AppComponent {
    constructor(private documentModelService: DocumentModelService) { }
}

