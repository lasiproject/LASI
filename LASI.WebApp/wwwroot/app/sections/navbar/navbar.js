'use strict';
System.register(['app/sections/document-manager/manage-modal', 'app/sections/document-manager/manage-modal.html'], function(exports_1) {
    var manage_modal_1, manage_modal_html_1;
    var NavbarController;
    return {
        setters:[
            function (manage_modal_1_1) {
                manage_modal_1 = manage_modal_1_1;
            },
            function (manage_modal_html_1_1) {
                manage_modal_html_1 = manage_modal_html_1_1;
            }],
        execute: function() {
            NavbarController = (function () {
                function NavbarController($uibModal, data) {
                    this.$uibModal = $uibModal;
                    this.expanded = false;
                    this.documents = data;
                }
                NavbarController.prototype.openManageDocumentsModal = function () {
                    var _this = this;
                    var navbarController = this;
                    var modal = this.$uibModal.open({
                        controllerAs: 'manager',
                        controller: manage_modal_1.default,
                        template: manage_modal_html_1.default,
                        resolve: { data: function () { return _this.documents; } },
                        keyboard: true
                    });
                    modal.result.then(function (result) { return console.info(result); });
                };
                // TODO: rename to toggleDropdownList
                NavbarController.prototype.toggleExpanded = function () {
                    this.expanded = !this.expanded;
                };
                NavbarController.$inject = ['$uibModal', 'data'];
                return NavbarController;
            })();
            exports_1("default", NavbarController);
        }
    }
});
