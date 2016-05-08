import { Component } from 'angular2/core';
import { HTTP_PROVIDERS } from 'angular2/http';
import { Router, RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
// import 'rxjs/Rx'; // load the full rxjs
import { Injectable } from './ng2-utils';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { NavbarComponent } from './navbar';
import { DocumentViewerComponent } from './document-viewer/components';
import template from 'app/app.html';

@Component({
    selector: 'app',
    directives: [
        ROUTER_DIRECTIVES,
        NavbarComponent,
        LoginComponent
    ],
    providers: [
        HTTP_PROVIDERS,
        ROUTER_PROVIDERS
    ],
    template
})
@RouteConfig([
    // { path: '...', name: 'Home', redirectTo:[''], useAsDefault: true },
    { path: 'documents', name: 'Documents', component: DocumentViewerComponent },
    { path: 'login', name: 'Login', component: LoginComponent },
])
export class AppComponent {
    constructor(private router: Router, private documentModelService: models.DocumentModelService) { }
}

