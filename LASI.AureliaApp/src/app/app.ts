import 'src/styles/site.css!css';
import {autoinject} from 'aurelia-framework';
import {Router, RouterConfiguration, NavigationInstruction} from 'aurelia-router';
import './shims';
import {UserService} from './user-service';
import {User} from 'models';

@autoinject export class App {
  constructor(private userService: UserService) { }

  async activate(params, routeConfig: RouterConfiguration, $navigationInstruction: NavigationInstruction) {
    try {
      console.log(arguments);

      const {user} = await this.userService.loginGet();
      this.user = user;
      console.log(user);
    } catch (e) {
      console.error(e);
      this.user = undefined;
    }
  }
  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Aurelia';

    config.map([
      { name: 'app', route: 'home', nav: true, moduleId: './home', title: 'Home' },
      { name: 'documents', route: ['', 'documents'], nav: true, moduleId: './documents', title: 'Documents' },
      { name: 'account', route: 'account', nav: true, moduleId: './account', title: 'Accounts' },
      { name: 'login', route: 'login', nav: true, moduleId: './login', title: 'Login' }
    ]);

    this.router = router;
  }

  user: User;

  router: Router;
}

