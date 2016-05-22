import {autoinject} from 'aurelia-framework';
import {Router, RouterConfiguration} from 'aurelia-router';
import './shims';
import '../styles/site.css!css';
import { UserService } from './user-service';
@autoinject
export class App {

  constructor(private userService: UserService) {
    var user = userService.loginGet()
      .then(response => {
        console.log(response);

      }).catch(reason => {
        console.error(reason);

      });

  }
  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Aurelia';

    config.map([
      { name: 'app', route: 'home', nav: true, moduleId: './home', title: 'Home' },
      { name: 'documents', route: ['', 'documents'], nav: true, moduleId: './documents', title: 'Documents' },
      { name: 'account', route: 'account', nav: true, moduleId: './account', title: 'Accounts' },
      { name: 'login', route: 'login', nav: true, moduleId: './login', title: 'Login' }
    ]);
    // .addAuthorizeStep({
    //   run: (step) => {
    //     if (!this.userService.user) {
    //       return this.userService.getUser().then(() => step.config.navigationStrategy(step));
    //     }
    //   }
    // });

    this.router = router;
  }
  router: Router;
}

