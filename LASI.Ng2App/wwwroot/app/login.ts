import $ from 'jquery';
import { Component, OnInit, Injectable, Inject, Input } from 'angular2/core';
import { Router, RouteConfig, ROUTER_PROVIDERS } from 'angular2/router';
import { UserService } from 'app/user-service';
import { HomeComponent } from 'app/home';
import { Observable } from 'rxjs/Rx';
import errorModalTemplate from 'app/templates/error-modal.html';
import template from 'app/login.html';
@Component({
    selector: 'login',
    template,

    exportAs: 'login',
    providers: [ROUTER_PROVIDERS],
    properties: ['username',
        'password',
        'rememberMe'
    ],

})
@Injectable()
export class LoginComponent implements OnInit {
    constructor(private userService: UserService, private router: Router) { }
    ngOnInit() { }

    login() {
        return this.userService
            .login({
                email: this.username,
                password: this.password,
                rememberMe: this.rememberMe || this.user && this.user.rememberMe
            }).add(() => this.router.navigate(['Home']));

        // .catch((error, caught) => {
        //     console.debug(error);
        //     return caught;
        // })
        // .catch(reason => {
        //     return this.$uibModal.open({
        //         controllerAs: 'modal',
        //         bindToController: true,
        //         template: errorModalTemplate,
        //         controller: class {
        //             static $inject = ['$uibModalInstance'];
        //             constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) { }

        //             header = 'Login Failed';

        //             error = reason;

        //             ok() {
        //                 this.$uibModalInstance.close();
        //                 return this.$uibModalInstance.result;
        //             }
        //         }
        //     }).result;
        // })

    }


    @Input() user: models.User;
    @Input() username = '';
    @Input() password = '';
    @Input() rememberMe = false;

}