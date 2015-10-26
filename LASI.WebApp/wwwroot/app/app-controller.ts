import 'angular-ui-router';
import { DocumentListItemModel } from './document-list/document-list-service-provider';
export default class AppController {
    static $inject = ['$state', '$stateParams', 'data'];

    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, data) {
        this.documents = data;
        //this.user = user();
    }
    documents: DocumentListItemModel[] = [];
    user;
}