'use strict';
import 'angular-ui-router';

import { UserService } from './user-service';

export class AppController {
    static $inject = ['$state', '$stateParams', 'UserService', 'documents'];
    currentStateName: string;
    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, private userService: UserService, private documents: DocumentListItemModel[]) {
        this.currentStateName = $state.current.name;
        userService.getUser().then(user => this.user = user);
    }
    user;
}