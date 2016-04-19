import 'bootstrap/css/bootstrap.min.css';
import { provide } from 'angular2/core';
import { bootstrap as ng2Bootstrap } from 'angular2/platform/browser';
import { AppComponent } from './app.component';
import { UserService } from './user-service';
import { TokenService } from './token-service';
import { HTTP_PROVIDERS, Http, RequestOptions } from 'angular2/http';
import { RequestOptions  as MyRequestOptions } from './configuration/http';
export function bootstrap() {
    return ng2Bootstrap(AppComponent, [HTTP_PROVIDERS, provide(RequestOptions, { useClass: MyRequestOptions }), TokenService, UserService])
        .then(success => console.log(`Bootstrap success`))
        .catch(error => console.log(error));
}