import * as angular from 'angular';
startup.$inject = ['$state'];

export default function startup($state: ng.ui.IStateService) {
    $state.go('app');
}