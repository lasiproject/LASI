'use strict';
import 'angular-ui-router';
import { DocumentListItemModel } from './models';
export default class AppController {
    static $inject = ['$state', '$stateParams', 'documents'];
    currentStateName: string;
    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, documents) {
        //$state.go('app.home');
        this.documents = documents;
        this.currentStateName = $state.current.name;
        //this.user = user();
    }
    documents: DocumentListItemModel[] = [];
    user;
}