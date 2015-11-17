System.register(['app/sections/navbar/navbar.html', 'app/sections/document-manager/manage-modal', 'app/sections/document-manager/manage-modal.html'], function(exports_1) {
    'use strict';
    var navbar_html_1, manage_modal_1, manage_modal_html_1;
    var NavbarController;
    function navbar() {
        return {
            controller: NavbarController,
            controllerAs: 'navbar',
            template: navbar_html_1.default,
            bindToController: {
                documents: '='
            },
            scope: true
        };
    }
    exports_1("navbar", navbar);
    return {
        setters:[
            function (navbar_html_1_1) {
                navbar_html_1 = navbar_html_1_1;
            },
            function (manage_modal_1_1) {
                manage_modal_1 = manage_modal_1_1;
            },
            function (manage_modal_html_1_1) {
                manage_modal_html_1 = manage_modal_html_1_1;
            }],
        execute: function() {
            NavbarController = (function () {
                function NavbarController($uibModal) {
                    this.$uibModal = $uibModal;
                    this.expanded = false;
                }
                NavbarController.prototype.openManageDocumentsModal = function () {
                    var _this = this;
                    var navbarController = this;
                    var modal = this.$uibModal.open({
                        controllerAs: 'manager',
                        controller: manage_modal_1.default,
                        template: manage_modal_html_1.default,
                        resolve: { documents: function () { return _this.documents; } },
                        keyboard: true
                    });
                    modal.result.then(function (result) { return console.info(result); });
                };
                // TODO: rename to toggleDropdownList
                NavbarController.prototype.toggleExpanded = function () {
                    this.expanded = !this.expanded;
                };
                NavbarController.$inject = ['$uibModal'];
                return NavbarController;
            })();
        }
    }
});
