import { Component, Inject, OnInit, Injectable } from 'angular2/core';
import { HTTP_PROVIDERS, Http } from 'angular2/http';
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { Observable, Observer } from 'rxjs/Rx'; // load the full rxjs
import template from 'app/app.component.html';
@Component({
    selector: 'app',
    template,
    directives: [ROUTER_DIRECTIVES, LoginComponent],
    providers: [
        HTTP_PROVIDERS,
        ROUTER_PROVIDERS
    ]
})
@RouteConfig([
    // { path: '...', component: AppComponent, useAsDefault: true, as: 'App' },
    { path: '/login', as: 'Login', component: LoginComponent },
    { path: '/home', as: 'Home', component: HomeComponent }
])
@Injectable()
export class AppComponent implements OnInit {
    constructor(private http: Http) { }

    values = [];
    async getData() {
        return await Observable.interval(200)
            .flatMap(() => this.http.get(`app/data.json?cacheBuster=${Math.random()}`))
            .map(data => data.json() as number[])
            .subscribe(values => {
                this.values = values;
            });
    }
    ngOnInit() {
        // this.getData();
    }
}