System.register([], function(exports_1) {
    'use strict';
    var ListController;
    return {
        setters:[],
        execute: function() {
            ListController = (function () {
                function ListController($q, documentListService, tasksListService, documentsService, documentModelService) {
                    this.$q = $q;
                    this.documentListService = documentListService;
                    this.tasksListService = tasksListService;
                    this.documentsService = documentsService;
                    this.documentModelService = documentModelService;
                    this.expanded = false;
                    this.documents = [];
                    this.tasks = [];
                    this.activate();
                }
                ListController.prototype.toggleExpanded = function () {
                    this.expanded = !this.expanded;
                };
                ListController.prototype.deleteById = function (id) {
                    var _this = this;
                    var deleteResult = this.documentsService.deleteById(id);
                    console.log(deleteResult);
                    this.documentListService.get().then(function (documents) { return _this.documents = documents; });
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
                        this.documentModelService.processDocument(document.id).then(function (processed) { return document.raeification = processed; }, function (reason) { return console.error(reason); });
                    }
                };
                ListController.prototype.activate = function () {
                    var _this = this;
                    return this.$q.all([
                        this.documentListService.get(),
                        this.$q.when(this.tasksListService.getActiveTasks().map(function (task) {
                            _this.tasks[task.id] = task;
                            var t = _this.documents.first(function (d) { return d.name === task.name; });
                            if (t) {
                                t.task = task;
                            }
                            return t;
                        }))
                    ]).then(function (data) {
                        var _a = data, documents = _a[0], tasks = _a[1];
                        var associated = documents.correlate(tasks.filter(function (t) { return !!t; }), function (document) { return document.id; }, function (task) { return task.id; }, function (document, task) {
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
            exports_1("ListController", ListController);
        }
    }
});
