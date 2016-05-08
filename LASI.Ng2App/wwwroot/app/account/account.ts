import { Component, OnInit } from 'angular2/core';
import UserService  from 'app/user-service';
import template from  './account.html';
@Component({
    selector: 'account',
    template
})
export class AccountComponent implements OnInit {
    constructor(private userService: UserService) { }

    ngOnInit() {
        this.activate();
    }

    activate() {
        return this.userService.getDetails().subscribe(details => this.details = details)
    }

    saveDetails() {
        return this.userService.saveDetails(this.details);
    }

    emailsMatch() {
        return this.details.email === this.details.confirmEmail;
    }

    details: any;

    fields/*: AngularFormly.IFieldConfigurationObject[]*/ = [
        {
            key: 'email',
            type: 'input',
            validators: {
                emailsMatch: {
                    expression: (viewValue, viewModel, scope) => !scope.model.confirmEmail || (viewValue || viewModel) === scope.model.confirmEmail,
                    message: 'Eamils do not match'
                }
            },
            wrapper: ['bootstrapHasError'],
            templateOptions: {
                label: 'Email',
                required: true
            }
        },
        {
            key: 'confirmEmail',
            type: 'input',
            wrapper: ['bootstrapHasError'],
            validators: {
                emailsMatch: {
                    expression: (viewValue, viewModel, scope) => (viewValue || viewModel) === scope.model.email,
                    message: 'Eamils do not match'
                }
            },
            templateOptions: {
                label: 'Confirm Email',
                required: true
            }
        }
    ];
}