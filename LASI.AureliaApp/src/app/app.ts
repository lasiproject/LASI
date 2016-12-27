import {autoinject} from 'aurelia-framework';
import {Router, RouterConfiguration, NavigationInstruction} from 'aurelia-router';
import UserService from './services/user';
import {User} from 'src/models';

@autoinject export class App {
  constructor(private userService: UserService) { }

  async activate(params, routeConfig: RouterConfiguration, $navigationInstruction: NavigationInstruction) {
    try {
      console.log(arguments);
      this.user = await this.userService.loginGet();
      console.log(this.user);
    } catch (e) {
      console.error(e);
      this.user = undefined;
    }
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this.router = router;
    config.title = 'LASI';
    config.map([
      { name: 'app', route: 'home', nav: true, moduleId: './home', title: 'Home' },
      { name: 'documents', route: ['', 'documents'], nav: true, moduleId: './documents', title: 'Documents' },
      { name: 'account', route: 'account', nav: true, moduleId: './account', title: 'Account', settings: { user: this.user } },
      { name: 'signin', route: 'signin', nav: true, moduleId: './signin', title: 'Signin' },
      { name: 'signout', route: 'signout', nav: true, moduleId: './signout', title: 'Signout' }
    ]);
  }

  user?: User;
  router: Router;
}

