'use strict';
import 'angular-ui-router';

import { UserService } from './user-service';

export class AppController {
    static $inject = ['$state', '$stateParams', 'documents'];
    currentStateName: string;
    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, private documents: DocumentListItemModel[]) {
        this.currentStateName = $state.current.name;
        $state.go('app.home');
    }
}