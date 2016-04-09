import { UserService } from 'app/user-service';

export default class AccountController {
    static $inject = ['UserService'];
    constructor(private userService: UserService) {
        this.activate();
    }

    activate() {
        return this.userService.getDetails().then(details => this.details = details)
    }

    saveDetails() {
        return this.userService.saveDetails(this.details);
    }

    emailsMatch() {
        return this.details.email === this.details.confirmEmail;
    }

    details: any;

    fields: AngularFormly.IFieldConfigurationObject[] = [
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