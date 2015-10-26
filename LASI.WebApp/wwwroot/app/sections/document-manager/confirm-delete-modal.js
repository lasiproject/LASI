'use strict';
System.register([], function(exports_1) {
    var ConfirmDeleteModal;
    return {
        setters:[],
        execute: function() {
            ConfirmDeleteModal = (function () {
                function ConfirmDeleteModal($modalInstance, data) {
                    this.$modalInstance = $modalInstance;
                    this.document = data;
                }
                ConfirmDeleteModal.prototype.confirm = function () { this.$modalInstance.close(true); };
                ConfirmDeleteModal.prototype.cancel = function () { this.$modalInstance.close(false); };
                ConfirmDeleteModal.prototype.dismiss = function () { this.$modalInstance.dismiss(); };
                ConfirmDeleteModal.$inject = ['$modalInstance', 'data'];
                return ConfirmDeleteModal;
            })();
            exports_1("default", ConfirmDeleteModal);
        }
    }
});
