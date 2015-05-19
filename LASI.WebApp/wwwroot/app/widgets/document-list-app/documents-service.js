/// <reference path = "../../../typings/angularjs/angular.d.ts" />
/// <reference path = "../../../typings/angularjs/angular-resource.d.ts" />
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .service('DocumentsService', DocumentsService);
    DocumentsService.$inject = ['$resource'];
    function DocumentsService($resource) {
        var userDocouments = $resource('api/UserDocuments/:id');
        this.getbyId = function (id) {
            return userDocouments.get({ id: id });
        };
        this.deleteById = function (id) {
            return userDocouments.delete({ id: id });
        };
    }
})();
