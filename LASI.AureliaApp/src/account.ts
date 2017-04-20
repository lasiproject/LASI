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

  activate(params: {}, routeConfig: RouteConfig & { settings: { user: Account } }, $navigation: NavigationInstruction) {
    Object.keys(this)
      .correlate(Object.entries(routeConfig.settings.user), key => key, ([key]) => key)
      .forEach(({ first, second: [key, value] }) => this[key as keyof this] = value);

    console.log(params, routeConfig, $navigation);
  }
  canActivate(params: {}, routeConfig: RouteConfig, $navigation: NavigationInstruction) {
    return !!routeConfig.settings.user;
  }
}