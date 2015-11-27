'use strict';
import 'angular-ui-router';
import { DocumentListItemModel } from './models';
import UserService from './user-service';
export default class AppController {
    static $inject = ['$state', '$stateParams', 'UserService', 'documents'];
    currentStateName: string;
    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, private userService: UserService, private documents: DocumentListItemModel[]) {
        //$state.go('app.home');
        this.currentStateName = $state.current.name;
        userService.getUser().then(user => this.user = user);
    }
    user;
}