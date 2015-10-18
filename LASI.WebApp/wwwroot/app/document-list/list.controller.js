'use strict';
define(["require", "exports"], function (require, exports) {
    var ListController = (function () {
        function ListController($q, documentListService, tasksListService, documentsService, documentModelService) {
            this.$q = $q;
            this.documentListService = documentListService;
            this.tasksListService = tasksListService;
            this.documentsService = documentsService;
            this.documentModelService = documentModelService;
            this.expanded = false;
            this.documents = [];
            this.activate();
        }
        ListController.prototype.toggleExpanded = function () {
            this.expanded = !this.expanded;
        };
        ListController.prototype.deleteById = function (id) {
            var deleteResult = this.documentsService.deleteById(id);
            console.log(deleteResult);
            this.documents = this.documentListService.get();
        };
        Object.defineProperty(ListController.prototype, "documentCount", {
            get: function () {
                return this.documents.length;
            },
            enumerable: true,
            configurable: true
        });
        ListController.prototype.processDocument = function (document) {
            if (!this.documents.some(function (d) { return d.raeification && d.id === document.id; })) {
                this.documentModelService.processDocument(document.id)
                    .success(function (processed) { return document.raeification = processed; })
                    .error(function (error) { return console.log(error); });
            }
        };
        ListController.prototype.activate = function () {
            var _this = this;
            return this.$q.all([
                this.$q.when(this.documentListService.get()),
                this.tasksListService.getActiveTasks().then(function (tasks) { return tasks.map(function (task) {
                    _this.tasks[task.id] = task;
                    var t = _this.documents.first(function (d) { return d.name === task.name; });
                    (t && t).task = task;
                    return t;
                }); })
            ]).then(function (data) {
                var _a = data, documents = _a[0], tasks = _a[1];
                var associated = documents.correlate(tasks, function (document) { return document.id; }, function (task) { return task.id; }, function (document, task) {
                    document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });
                tasks.forEach(function (task) { _this.tasks[task.id] = task; });
                _b = [documents, tasks], _this.documents = _b[0], _this.tasks = _b[1];
                var _b;
            });
        };
        ListController.$inject = ['$q', 'documentListService', 'tasksListService', 'documentsService', 'documentModelService'];
        return ListController;
    })();
    exports.ListController = ListController;
});
//# sourceMappingURL=list.controller.js.map