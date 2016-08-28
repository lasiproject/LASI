import { component } from 'ng2-conventions-decorators';
import { Router, CanActivate, RouterOutlet } from '@angular/router';
import { UserService } from './user-service';
import { LoginComponent } from './login';
import { ListItemComponent } from './document-list/list-item';
import { DocumentViewerComponent } from './document-viewer/components';
import template from './home.html';

@component(template, {directives:[]})
export class HomeComponent {
    constructor(private userService: UserService, private router: Router) { }

    // ngOnInit() {
    //     if (!this.userService.loggedIn) {
    //         this.router.navigate(['Login']);
    //     }
    //     else {
    //         this.router.navigate(['Home']);
    //     }
    // }
}