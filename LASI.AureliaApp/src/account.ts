import {RouteConfig, NavigationInstruction} from 'aurelia-router';

export class Account {
  userName: string;
  fieldOfStudy: {};
  birthDate: Date;
  primaryEmail: string;
  emails = [
    {
      default: true,
      address: this.primaryEmail
    }
  ];

  activate(params: {}, routeConfig: RouteConfig & {settings: {user: Account}}, $navigation: NavigationInstruction) {
    (Object.keys(this) as (keyof this)[])
      .correlate(Object.entries(routeConfig.settings.user), key => key, ([key]) => key)
      .forEach(({first: key, second: [, value]}) => this[key] = value);

    console.log(params, routeConfig, $navigation);
  }

  canActivate(params: {}, routeConfig: RouteConfig, $navigation: NavigationInstruction) {
    return !!routeConfig.settings.user;
  }
}