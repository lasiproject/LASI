import {autoinject} from 'aurelia-framework';
import {Router, RouterConfiguration, NavigationInstruction} from 'aurelia-router';
import UserService from './services/user';
import User from './models/user';

@autoinject export class App {
  constructor(readonly userService: UserService) {
    this.initializeAsync();
  }

  async initializeAsync() {
    // try {
    //   this.user = await this.userService.loginGet();
    //   console.log(this.user);
    // } catch (e) {
    //   console.error(e);
    //   this.user = undefined;
    // }
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    this.router = router;
    config.title = 'LASI';
    config.map([
      {name: 'app', route: 'documents', nav: true, moduleId: './docuemnts', title: 'Home'},
      {name: 'documents', route: ['', 'documents'], nav: true, moduleId: './documents', title: 'Documents'},
      {name: 'account', route: 'account', nav: true, moduleId: './account', title: 'Account', settings: {user: this.user}},
      {name: 'signin', route: 'signin', nav: true, moduleId: './signin', title: 'Signin'},
      {name: 'signout', route: 'signout', nav: true, moduleId: './signout', title: 'Signout'}
    ]);
  }

  user?: User;
  router: Router;
}