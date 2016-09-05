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
    Object.keys(this)
      .correlate(Object.entries(routeConfig.settings.user), key => key, ([key]) => key)
      .forEach(({first, second: [key, value]}) => this[key] = value);

    console.log(params, routeConfig, $navigationInstruction);
  }
  canActivate(params, routeConfig: RouteConfig, $navigationInstruction) {
    return !!routeConfig.settings.user;
  }
}