'use strict';
import { UserService } from 'app/user-service';

export class AccountController {
    static $inject = ['UserService'];

    constructor(private userService: UserService) {
        this.activate();
    }

    activate() {
        return this.userService.getDetails()
            .then(details => this.details = details);
    }

    saveDetails() {
        return this.userService.saveDetails(this.details);
    }

    emailsMatch() {
        return this.details.email === this.details.confirmEmail;
    }

    details: any;

}