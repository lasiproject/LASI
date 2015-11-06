System.register([], function(exports_1) {
    'use strict';
    var ConfirmDeleteModal;
    return {
        setters:[],
        execute: function() {
            ConfirmDeleteModal = (function () {
                function ConfirmDeleteModal($uibModalInstance, data) {
                    this.$uibModalInstance = $uibModalInstance;
                    this.document = data;
                }
                ConfirmDeleteModal.prototype.confirm = function () { this.$uibModalInstance.close(true); };
                ConfirmDeleteModal.prototype.cancel = function () { this.$uibModalInstance.close(false); };
                ConfirmDeleteModal.prototype.dismiss = function () { this.$uibModalInstance.dismiss(); };
                ConfirmDeleteModal.$inject = ['$uibModalInstance', 'data'];
                return ConfirmDeleteModal;
            })();
            exports_1("default", ConfirmDeleteModal);
        }
    }
});
