'use strict';
import { DocumentsService } from './document-list/documents-service';
import { UserService } from './user-service';

export class AppController {
    static $inject = ['$state', '$stateParams', 'documentListService'];

    currentStateName: string;
    constructor(private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService, private documentsService: DocumentsService) {
        this.currentStateName = $state.current.name;
        $state.go('app.home');
    }
}