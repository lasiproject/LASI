import './shims';
import '../../styles/site.css!css';
import {Router, RouterConfiguration} from 'aurelia-router';

export class App {

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Aurelia';

    config.map([
      { name: 'app', route: 'home', nav: true, moduleId: './home', title: 'Home' },
      { name: 'documents', route: ['', 'documents'], nav: true, moduleId: './documents', title: 'Documents' },
      { name: 'account', route: 'account', nav: true, moduleId: './account', title: 'Accounts' }
    ]);

    this.router = router;
  }
  router: Router;
}

