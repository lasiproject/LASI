import { Component, OnInit } from '@angular/core';
import { Router, RouteData, RouterLink, RouteConfig, OnActivate } from '@angular/router';
import { Injectable, Input } from 'app/ng2-utils';
import { UserService } from './user-service';
import template from './navbar.html';
@Component({
    selector: 'navbar',
    directives: [RouterLink],
    template
})
@Injectable
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
    toggleExpanded() {
        this.documentListExpanded = !this.documentListExpanded;
    }
    @Input user: models.User;
    @Input documentListExpanded = false;
}

function isUser(value): value is models.User {
    return value.email;
}
