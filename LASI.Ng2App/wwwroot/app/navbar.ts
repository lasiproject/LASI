import { Component, OnInit, Inject, Injectable } from 'angular2/core';
import { Router, RouteData, RouteConfig, OnActivate } from 'angular2/router';
import { UserService } from './user-service';
@Component({
    selector: 'navbar',
    templateUrl: 'path/name.component.html'
})
@Injectable()
export class NavbarComponent implements OnActivate {
    constructor(private userService: UserService) { }

    routerOnActivate() {
        this.userService.getUser().subscribe(user => {
            if (isUser(user)) {
                this.user = user;
            } else {
                this.user = undefined;
            }
        });
    }

    user: models.User;

}

function isUser(value): value is models.User {
    return value.email;
}
