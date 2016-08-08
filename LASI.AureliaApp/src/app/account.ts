import { RouteConfig } from 'aurelia-router';
export class Account {
    userName;
    fieldOfStudy;
    birthDate;
    primaryEmail: string;
    emails = [
        {
            default: true,
            address: this.primaryEmail
        }
    ];

    activate(params, routeConfig, $navigationInstruction) {
        console.log(params, routeConfig, $navigationInstruction);
    }
    canActivate(params, routeConfig: RouteConfig, $navigationInstruction) {
        return !!routeConfig.settings.user;
    }
}