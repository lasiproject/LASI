/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    'use strict';
    var arrayAgumentations = {
        flatMap: function (arraySelector, elementSelector) {
            arraySelector = arraySelector || function (array) {
                return array instanceof Array && array;
            };
            elementSelector = elementSelector || function (element) { return element; };
            return this.reduce(function (array, a) {
                return array.concat(arraySelector(a).map(elementSelector));
            }, []);
        },
        correlate: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            var outerKeyed, innerKeyed, i, j, results = [];
            outerKeyed = this.map(function (e) {
                return { element: e, key: outerKeySelector(e) };
            });
            innerKeyed = inner.map(function (e) {
                return { element: e, key: innerKeySelector(e) };
            });
            for (i = 0; i < outerKeyed.length; i += 1) {
                for (j = 0; j < innerKeyed.length; j += 1) {
                    if (outerKeyed[i].key === innerKeyed[j].key) {
                        results.push({ first: outerKeyed[i].element, second: innerKeyed[j].element });
                    }
                }
            }
            // If a function was supplied, map it over the array of result pairs and return the result.
            // otherwise, just return the array of result pairs.
            return resultSelector ? results.map(function (e) {
                return resultSelector(e.first, e.second);
            }) : results;
        },
        sum: function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || (function (x) { return Number(x); });
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        },
        average: function (valueSelector) {
            return this.sum(valueSelector || (function (x) { return Number(x); })) / this.length;
        },
        first: function (predicate) {
            if (!predicate) {
                return this[0];
            }
            for (var i = 0; i < this.length; ++i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        },
        last: function (predicate) {
            if (!predicate) {
                return this[this.length - 1];
            }
            for (var i = this.length - 1; i > 0; --i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        }
    };
    Object.keys(arrayAgumentations)
        .filter(function (key) { return !Object.prototype.hasOwnProperty.call(Array.prototype, key); })
        .map(function (key) { return [key,
        {
            writable: false,
            enumerable: false,
            configurable: false,
            value: arrayAgumentations[key]
        }]; })
        .forEach(function (_a) {
        var name = _a[0], property = _a[1];
        return Object.defineProperty(Array.prototype, name, property);
    });
})();

/// <reference path="../../typings/tsd.d.ts" />
'use strict';
define(["require", "exports", 'jquery'], function (require, exports, $) {
    //export var setupDraggableDialogs: () => void; 
    exports.log = console.log.bind(console);
    exports.editor = $('#free-editor').change(exports.log); // TODO: parameterize selector.
});

'use strict';
define(["require", "exports"], function (require, exports) {
    function tasksListServiceProvider() {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';
        $get.$inject = ['$q', '$resource', '$interval'];
        return { $get: $get, setUpdateInterval: setUpdateInterval, setTasksListUrl: setTasksListUrl };
        function setUpdateInterval(milliseconds) {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url) {
            tasksListUrl = url;
            return this;
        }
        function $get($q, $resource, $interval) {
            var Tasks = $resource(tasksListUrl, {}, {
                get: {
                    method: 'GET', isArray: true
                }
            });
            var getActiveTasks = function () {
                var _this = this;
                var deferred = $q.defer();
                $interval(function () {
                    _this.tasks = Tasks.get();
                    deferred.resolve(_this.tasks);
                }, updateInterval);
                return deferred.promise;
            };
            return {
                getActiveTasks: getActiveTasks,
                tasks: []
            };
        }
        function createDebugInfoUpdator(element) {
            return function (tasks) { return element.html(tasks.map(function (task) { return ("<div>" + Object.keys(task).map(function (key) { return ("<span>&nbsp&nbsp" + task[key] + "</span>"); }) + "</div>"); }).join()); };
        }
    }
    exports.tasksListServiceProvider = tasksListServiceProvider;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    var DebugPanelController = (function () {
        function DebugPanelController(tasksListService) {
            var _this = this;
            this.tasks = [];
            this.infoVisible = false;
            tasksListService.getActiveTasks().then(function (data) { return _this.tasks = data.sort(function (x, y) { return x.id.localeCompare(y.id); }); });
        }
        Object.defineProperty(DebugPanelController.prototype, "buttonText", {
            get: function () { return this.infoVisible ? 'Hide' : 'Show'; },
            enumerable: true,
            configurable: true
        });
        DebugPanelController.prototype.toggle = function () { this.infoVisible = !this.infoVisible; };
        DebugPanelController.$inject = ['tasksListService'];
        return DebugPanelController;
    })();
    function debugPanel() {
        return {
            template: require('app/debug/debug-panel.directive.html!'),
            controller: DebugPanelController,
            bindToController: {},
            controllerAs: 'debug'
        };
    }
    exports.debugPanel = debugPanel;
});

define(["require", "exports", './debug-panel.directive'], function (require, exports, debug_panel_directive_1) {
    var m = {
        name: 'debug',
        requires: [],
        directives: { debugPanel: debug_panel_directive_1.debugPanel }
    };
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = m;
});

define(["require", "exports", 'angular', 'angular-resource', 'angular-bootstrap', 'angular-bootstrap-contextmenu', 'angular-file-upload', 'jquery'], function (require, exports, angular, resource, angularBootstrap, angularBootstrapContextmenu, angularFileUpload, $) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        $: $, angular: angular, resource: resource, angularBootstrap: angularBootstrap, angularBootstrapContextmenu: angularBootstrapContextmenu, angularFileUpload: angularFileUpload
    };
});

'use strict';
define(["require", "exports"], function (require, exports) {
    uploadPanel.$inject = ['$window'];
    function uploadPanel($window) {
        return {
            restrict: 'E'
        };
    }
    exports.uploadPanel = uploadPanel;
});

'use strict';
define(["require", "exports", 'app/angular-shim'], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentUpload',
        requires: ['ngFileUpload'],
    };
});

'use strict';
define(["require", "exports"], function (require, exports) {
    documentsService.$inject = ['$resource'];
    function documentsService($resource) {
        var userDocouments = $resource('api/UserDocuments/:documentId');
        function getbyId(documentId) {
            return userDocouments.get({ documentId: documentId });
        }
        function deleteById(documentId) {
            return userDocouments.delete({ documentId: documentId });
        }
        return {
            deleteById: deleteById,
            getbyId: getbyId
        };
    }
    exports.documentsService = documentsService;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    lexicalMenuBuilder.$inject = [];
    function lexicalMenuBuilder() {
        var _a = [createForVerbalMenuBuilder({}), createForReferencerMenuBuilder({})], buildForVerbal = _a[0], buildForReferencer = _a[1];
        return {
            buildAngularMenu: function (source) {
                return referencerMenuIsViable(source) ?
                    buildForReferencer(source) :
                    verbalMenuIsViable(source) ?
                        buildForVerbal(source) :
                        undefined;
            }
        };
        function verbalMenuIsViable(menu) {
            return !!(menu && (menu.directObjectIds || menu.indirectObjectIds || menu.subjectIds));
        }
        function referencerMenuIsViable(menu) {
            return !!(menu && menu.refersToIds);
        }
    }
    exports.lexicalMenuBuilder = lexicalMenuBuilder;
    function createForReferencerMenuBuilder(menuActionTargets) {
        var resetReferencerAsssotionCssClasses = function () {
            return Object.keys(menuActionTargets)
                .map(function (key) { return menuActionTargets[key]; })
                .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); });
        };
        return function (source) { return [
            ['View Referred To', function (itemScope, event) {
                    resetReferencerAsssotionCssClasses();
                    source.refersToIds.forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'); });
                }]
        ]; };
    }
    function createForVerbalMenuBuilder(menuActionTargets) {
        return (function (verbalMenuCssClassMap) {
            return function (source) {
                var menuItems = [];
                if (source.subjectIds) {
                    menuItems.push(['View Subjects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.subjectIds
                                .forEach(function (id) {
                                menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Subjects']);
                            });
                        }]);
                }
                if (source.directObjectIds) {
                    menuItems.push(['View Direct Objects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.directObjectIds
                                .forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Direct Objects']); });
                        }]);
                }
                if (source.indirectObjectIds) {
                    menuItems.push(['View Indirect Objects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.indirectObjectIds.forEach(function (id) {
                                menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                            });
                        }]);
                }
                return menuItems;
            };
            function resetVerbalAssociationCssClasses() {
                Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) {
                    return Object.keys(verbalMenuCssClassMap)
                        .map(function (k) { return verbalMenuCssClassMap[k]; })
                        .forEach(function (cssClass) { return $e.removeClass(cssClass); });
                });
            }
        })({
            'View Subjects': 'subject-of-current',
            'View Direct Objects': 'direct-object-of-current',
            'View Indirect Objects': 'indirect-object-of-current'
        });
    }
});

define(["require", "exports"], function (require, exports) {
});

'use strict';
define(["require", "exports", 'app/LASI'], function (require, exports, LASI_1) {
    resultsService.$inject = ['$http', '$q'];
    function resultsService($http, $q) {
        var tasks = [];
        var processDocument = function (documentId, documentName) {
            tasks[documentId] = { percentComplete: 0 };
            var deferred = $q.defer();
            $http.get('Analysis/' + documentId)
                .success(success)
                .error(error);
            return deferred.promise;
            function success(data) {
                var markupHeader = $('<div class="panel panel-default">' +
                    '<div class="panel-heading"><h4 class="panel-title"><a href="#' +
                    documentId +
                    '" data-toggle="collapse" data-parent="#accordion">' +
                    documentName +
                    '</a></h4></div></div>');
                var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' +
                    JSON.stringify(data) + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(markupHeader).append(markupPanel);
                }
                else {
                    $('#' + documentId).remove();
                    $('#accordion').append(markupPanel);
                }
                LASI_1.buildMenus();
                LASI_1.enableActiveHighlighting();
                tasks[documentId].percentComplete = 100;
                deferred.resolve(data);
                alert(JSON.stringify(data));
            }
            function error(xhr, message, detail) {
                deferred.reject(message);
            }
        };
        return {
            tasks: tasks,
            processDocument: processDocument
        };
    }
    exports.resultsService = resultsService;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    documentModelService.$inject = ['$http'];
    function documentModelService($http) {
        return {
            processDocument: function (documentId) {
                return $http.get("Analysis/" + documentId, { cache: false });
            }
        };
    }
    exports.documentModelService = documentModelService;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    var DocumentListServiceProvider = (function () {
        function DocumentListServiceProvider() {
            this.$inject = ['$resource'];
            this.$get.$inject = ['$resource'];
        }
        DocumentListServiceProvider.prototype.setDocumentListUrl = function (url) {
            this.documentListUrl = url;
            return this;
        };
        DocumentListServiceProvider.prototype.setRecentDocumentCount = function (count) {
            this.recentDocumentCount = count;
            return this;
        };
        /**
         * @param $resource an instance of the Resource Service supplied by the angular-resource module.
         */
        DocumentListServiceProvider.prototype.$get = function ($resource) {
            var resource = $resource(this.documentListUrl + '?limit=' + this.recentDocumentCount, {}, {
                get: {
                    method: 'GET',
                    isArray: true
                },
                delete: {
                    method: 'DELETE',
                    isArray: false
                }
            });
            return {
                deleteDocument: function (documentId) {
                    return resource.delete({ documentId: documentId })[0];
                },
                get: resource.get
            };
        };
        ;
        return DocumentListServiceProvider;
    })();
    exports.DocumentListServiceProvider = DocumentListServiceProvider;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    var log = console.log.bind(console);
    var UploadController = (function () {
        function UploadController($scope, $q, uploadService) {
            this.$scope = $scope;
            this.$q = $q;
            this.uploadService = uploadService;
            this.files = [];
            this.$scope.$watch('upload.files', this.uploadFiles.bind(this));
        }
        UploadController.prototype.uploadFiles = function () {
            var _this = this;
            this.logInvalidFiles();
            var files = this.files;
            return this.$q.when((Array.isArray(files) ? files : [files]).map(function (file) { return _this.uploadFile(file); }));
        };
        UploadController.prototype.logInvalidFiles = function () {
            var files = this.files;
            (Array.isArray(files) ? files : [files]).filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                .forEach(log);
        };
        UploadController.prototype.uploadFile = function (file) {
            return this.uploadService
                .upload({
                file: file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            })
                .progress(function (data) { return log("Progress: " + 100.0 * data.progress / data.percentComplete + "% " + file.name); })
                .success(function (data) { return log("File " + file.name + " uploaded. \nResponse: " + JSON.stringify(data)); })
                .error(function (data, status) { return log("Http: " + status + " Failed to upload file. \nDetails: " + data); });
        };
        UploadController.prototype.removeFile = function (file, index) {
            var files = this.files;
            this.files = (Array.isArray(files) ? files : [files]).filter(function (f) { return f.name !== file.name; });
            $('#file-upload-list').remove("#upload-list-item-" + index);
        };
        UploadController.$inject = ['$scope', '$q', 'Upload'];
        UploadController.formats = [
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            'application/msword',
            'application/pdf',
            'text/plain'
        ];
        return UploadController;
    })();
    exports.UploadController = UploadController;
});

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

'use strict';
define(["require", "exports"], function (require, exports) {
    documentListTabsetItem.$inject = ['resultsService'];
    function documentListTabsetItem(resultsService) {
        return {
            restrict: 'E',
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.stopPropagation();
                    resultsService.processDocument(scope.documentId, scope.name);
                    event.preventDefault();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () { return scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete; });
                });
                console.log(attrs);
            },
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '='
            },
            template: require('/app/document-list/document-list-tabset-item.directive.html!')
        };
    }
    exports.documentListTabsetItem = documentListTabsetItem;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    documentListMenuItem.$inject = ['$window', 'resultsService'];
    function documentListMenuItem($window, resultsService) {
        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            template: require('/app/document-list/document-list-menu-item.directive.html!'),
            scope: {
                name: '=',
                documentId: '='
            },
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () {
                        scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    });
                });
            }
        };
    }
    exports.documentListMenuItem = documentListMenuItem;
});

'use strict';
define(["require", "exports", './documents.service', './results.service', './document-list-service.provider', './tasks-list-service.provider', './upload.controller', './list.controller', './document-list-tabset-item.directive', './document-list-menu-item.directive'], function (require, exports, documents_service_1, results_service_1, document_list_service_provider_1, tasks_list_service_provider_1, upload_controller_1, list_controller_1, document_list_tabset_item_directive_1, document_list_menu_item_directive_1) {
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(5)
            .setDocumentListUrl('api/UserDocuments/List');
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentList',
        requires: [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu',
            'ngFileUpload',
            'documentViewer',
            'debug'
        ],
        configFn: configure,
        factories: { resultsService: results_service_1.resultsService, documentsService: documents_service_1.documentsService },
        providers: { documentListService: document_list_service_provider_1.DocumentListServiceProvider, tasksListService: tasks_list_service_provider_1.tasksListServiceProvider },
        directives: { documentListTabsetItem: document_list_tabset_item_directive_1.documentListTabsetItem, documentListMenuItem: document_list_menu_item_directive_1.documentListMenuItem },
        controllers: { ListController: list_controller_1.ListController, UploadController: upload_controller_1.UploadController },
    };
});

'use strict';
define(["require", "exports"], function (require, exports) {
    var DocumentController = (function () {
        function DocumentController($q, documentModelService) {
            this.$q = $q;
            this.documentModelService = documentModelService;
        }
        DocumentController.prototype.processDocument = function (id) {
            if (this.documentModel.id !== id) {
                return this.documentModelService.processDocument(id);
            }
            else {
                return this.$q.reject(this.documentModel);
            }
        };
        DocumentController.$inject = ['$q', 'MockDocumentModelService'];
        return DocumentController;
    })();
    exports.DocumentController = DocumentController;
});

'use strict';
define(["require", "exports", './document.controller', './document-model.service', './lexical-menu-builder.service'], function (require, exports, document_controller_1, document_model_service_1, lexical_menu_builder_service_1) {
    var module = {
        name: 'documentViewer',
        requires: [
            'documentViewer.search',
            'widgets',
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu'
        ],
        controllers: { DocumentController: document_controller_1.DocumentController },
        factories: { documentModelService: document_model_service_1.documentModelService, lexicalMenuBuilder: lexical_menu_builder_service_1.lexicalMenuBuilder }
    };
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = module;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'widgets',
        requires: ['ui.bootstrap']
    };
});

'use strict';
define(["require", "exports"], function (require, exports) {
    var template = require('app/document-viewer/search/search-box.directive.html!html');
    var SearchBoxController = (function () {
        function SearchBoxController($q) {
            this.$q = $q;
        }
        SearchBoxController.prototype.getWords = function () {
            return (this.phrases || []).flatMap(function (p) { return p.words; });
        };
        SearchBoxController.prototype.search = function (searchOptions, searchContext) {
            var deferred = this.$q.defer();
            var value = searchOptions.value;
            var term = typeof value === 'string' ? value :
                typeof value !== 'undefined' ? value.detailText : undefined;
            if (!term) {
                deferred.reject('search term was undefined');
            }
            else if (!searchContext) {
                deferred.reject('nothing to search');
                this.phrases.forEach(function (phrase) { return phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''); });
            }
            else {
                this.phrases = this.phrases || searchContext.flatMap(function (m) { return m.paragraphs; }).flatMap(function (p) { return p.sentences; }).flatMap(function (s) { return s.phrases; });
                var results = [];
                this.phrases.forEach(function (phrase) {
                    var matched = phrase.words.some(function (word) { return word.text === value; });
                    if (!matched) {
                        phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
                    }
                    else {
                        phrase.style.cssClass += ' matched-by-search';
                        results.push(phrase);
                    }
                });
                deferred.resolve(results.map(function (r) { return r.text; }));
            }
            return deferred.promise;
        };
        SearchBoxController.$inject = ['$q'];
        return SearchBoxController;
    })();
    function searchBox() {
        return {
            restrict: 'E',
            controller: SearchBoxController,
            controllerAs: 'search',
            scope: {},
            bindToController: {
                searchContext: '=',
                find: '='
            },
            template: template
        };
    }
    exports.searchBox = searchBox;
});

'use strict';
define(["require", "exports", './search-box.directive'], function (require, exports, search_box_directive_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentViewer.search',
        requires: ['ui.bootstrap.typeahead'],
        directives: { searchBox: search_box_directive_1.searchBox }
    };
});



/// <reference path = '../../typings/angular-bootstrap-contextMenu/angular-bootstrap-contextmenu.d.ts'/>
var contextmenuTests;
(function (contextmenuTests) {
    'use strict';
    var item1 = [
        function (s, e) { return 'item1'; },
        function (s, e) { return console.log('item1 clicked'); },
        function (s, e) { return true; }
    ];
    var item2 = [
        'item2',
        function (s) { return console.log(s); }
    ];
    var item3 = [
        'item3',
        function (s, e) { return console.log('item3 clicked'); },
        function (s, e) { return Object.keys(s).every(function (k1) { return Object.keys(e).some(function (k2) { return k1 === k2; }); }); }
    ];
    var menu = [
        item1, item2
    ];
})(contextmenuTests || (contextmenuTests = {}));

/// <reference path="../../../typings/jquery/jquery.d.ts" />
var LASI;
(function (LASI) {
    var accountManagement;
    (function (accountManagement) {
        'use strict';
        $(function () { return $('#cancel-profile-edits').click(function () { return document.location.href = "http://" + document.domain + ":" + document.location.port; }); });
    })(accountManagement = LASI.accountManagement || (LASI.accountManagement = {}));
})(LASI || (LASI = {}));



/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../lasi.ts"/>
(function (app) {
    'use strict';
    app.buildMenus = (function (contextualElementIdSelectors) {
        var verbalMenuTextToElementsMap = {
            'View Subjects': 'subjects',
            'View Direct Objects': 'directObjects',
            'View Indirect Objects': 'indirectObjects'
        };
        var relationshipCssClassNameMap = {
            'View Subjects': 'subject-of-current',
            'View Direct Objects': 'direct-object-of-current',
            'View Indirect Objects': 'indirect-object-of-current'
        };
        return function () {
            var forVerbal = function (context) {
                var menu = JSON.parse($('#context' + context[0].id).text());
                var subjects = menu.subjects, directObjects = menu.directObjects, indirectObjects = menu.indirectObjects;
                var temp = {
                    subjects: subjects,
                    directObjects: directObjects,
                    indirectObjects: indirectObjects
                }, result = {};
                Object.keys(temp)
                    .filter(function (key) { return temp[key]; })
                    .forEach(function (key) { result[key] = temp[key]; });
                return result;
            }, forReferencer = function (context) {
                var menu = JSON.parse($('#context' + context[0].id).text());
                return menu;
            };
            $('span.referencer').contextmenu({
                target: '#referencer-context-menu',
                before: function (event, context) {
                    var data = forReferencer(context);
                    event.target.lexicalContextMenu = data;
                    return data.referredTo && data.referredTo.length > 0;
                },
                onItem: function (context) {
                    context[0].lexicalContextMenu.referredTo.map(function (id) {
                        return $('#' + id);
                    }).forEach(function (element) {
                        element.css('background-color', 'red');
                    });
                }
            });
            $('span.verbal').contextmenu({
                target: '#verbal-context-menu',
                before: function (e, context) {
                    var count = 0;
                    var menu = forVerbal(context);
                    e.target.lexicalContextMenu = {};
                    Object.keys(menu).forEach(function (key) {
                        e.target.lexicalContextMenu[key] = menu[key].map(function (id) {
                            var idSelector = '#' + id;
                            if (!contextualElementIdSelectors.some(function (e) { return e === idSelector; })) {
                                contextualElementIdSelectors.push(idSelector);
                            }
                            return idSelector;
                        });
                    });
                    [
                        { name: 'subjects', id: '#subjects-item' },
                        { name: 'directObjects', id: '#directobjects-item' },
                        { name: 'indirectObjects', id: '#indirectobjects-item' }
                    ].forEach(function (x) {
                        if (!menu[x.name]) {
                            $(x.id).hide();
                        }
                        else {
                            count += 1;
                            $(x.id).show();
                        }
                    });
                    return count > 0;
                },
                onItem: function (context, event) {
                    var menu = context[0].lexicalContextMenu;
                    contextualElementIdSelectors
                        .flatMap(function (e) { return $(e).toArray(); }, $)
                        .forEach(function (e) {
                        Object.keys(relationshipCssClassNameMap).forEach(function (key) {
                            e.removeClass(relationshipCssClassNameMap[key]);
                        });
                    });
                    menu[verbalMenuTextToElementsMap[event.target.text]]
                        .map($)
                        .forEach(function (e) {
                        e.addClass(relationshipCssClassNameMap[event.target.text]);
                    });
                }
            });
            $(document).on('click', function () {
                $('#verbal-context-menu').hide();
                $('#referencer-context-menu').hide();
            });
        };
    }([]));
    $(app.buildMenus);
}(LASI));

define(["require", "exports", 'app/LASI'], function (require, exports, LASI) {
    LASI.enableActiveHighlighting = function () {
        'use strict';
        var enableActiveHighlighting = function () {
            var phrasalTextSpans = $('span.phrase');
            var highlightClass = 'active-phrase-highlight';
            var recolor = function () {
                phrasalTextSpans.each(function () {
                    $(this).removeClass(highlightClass);
                });
                $(this).addClass(highlightClass);
            };
            phrasalTextSpans.click(recolor);
            phrasalTextSpans.on('contextmenu', recolor);
            // bootstrap requires that tooltips be manually enabled. The data-toggle="tooltip" attributes set on each element
            // have no effect without this or an equivalent call. By setting container to 'body', we allow the contents of the 
            // tooltip to overflow its container. This is generally close to the desired behavior as it is difficult to predict width
            // and this gives good flexibility. There is probably a cleaner and more precise/obvious way of doing this, change to that if discovered.
            $('[data-toggle="tooltip"]').tooltip({
                delay: 250,
                container: 'body'
            });
            // TODO: look into fixing tooltips on elements containing a line break or remove such breaks.
        };
        $(enableActiveHighlighting);
        return enableActiveHighlighting;
    };
});

'use strict';
define(["require", "exports"], function (require, exports) {
    function documentPage() {
        function link(scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        }
        return {
            restrict: 'E',
            link: link,
            template: require('/app/document-viewer/document-page.directive.html!'),
            replace: true,
            scope: {
                page: '=',
                document: '='
            }
        };
    }
    exports.documentPage = documentPage;
});

'use strict';
define(["require", "exports"], function (require, exports) {
    documentViewerDirective.$inject = [];
    function documentViewerDirective() {
        var link = function (scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/document-viewer.directive.html',
            replace: true,
            scope: {
                document: '='
            },
            link: link
        };
    }
    exports.documentViewerDirective = documentViewerDirective;
});

var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/paragraph.directive.html',
                scope: {
                    paragraph: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive({ paragraph: paragraph });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));

'use strict';
define(["require", "exports"], function (require, exports) {
    phrase.$inject = ['lexicalMenuBuilder'];
    function phrase(lexicalMenuBuilder) {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/phrase.directive.html',
            scope: {
                phrase: '=',
                parentId: '='
            },
            link: link
        };
        function link(scope, element, attrs) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                scope.phrase.contextmenu = contextmenu;
            }
        }
    }
    exports.phrase = phrase;
    angular
        .module('documentViewer')
        .directive({ phrase: phrase });
});

var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.directive.html',
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive({ sentence: sentence });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));

/* global:draggable */
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var draggable = window.draggable;
        var createHeaderMarkup = function (documentId, documentName) {
            return '<div>' +
                '<div class="panel-heading">' +
                '<h4 class="panel-title">' +
                '<a href="#' + documentId + '">'
                + documentName + '</a></h4></div></div>';
        };
        $(function () {
            window.setTimeout(function () {
                //setupDraggableDialogs = function () {
                //    var enableDragging = function (e, h?) {
                //        var handle = $(e).find('.handle')[0];
                //        draggable(e, handle);
                //        e.style.position = '';
                //    };
                //    // Make all confirm delete modals draggable.
                //    $('[id^=confirm-delete-modal]').toArray().forEach(enableDragging);
                //    // Make the manage documents modal draggable.
                //    var draggableDialog = $('#manage-documents-modal');
                //    var dragHandle = draggableDialog.find('.handle')[0];
                //    if (draggableDialog[0] || dragHandle) {
                //        enableDragging(draggableDialog[0], dragHandle);
                //    }
                //};
                //setupDraggableDialogs();
                var $tabs = $('.document-viewer-tab-heading');
                var $listItemRefs = $('.document-list-item > a');
                var click = function (event) {
                    event.preventDefault();
                    var $listItem = $(this);
                    var $element = $(event.target);
                    //var documentName = $element[0].text;
                    var documentName = $element[0].innerText;
                    var documentId = $element.next('.item-id-hidden')[0].innerText.trim();
                    console.log('clicked Name: ' + documentName + ', Id: ' + documentId);
                    var $parentListItem = $($(event.target).parent());
                    var $progress = $parentListItem.find('.progress hidden')
                        .find('.progress-bar')
                        .removeClass('.hidden').css('width', '100');
                    $.get('Analysis/' + documentId).done(function (data, status, xhr) {
                        var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                        var panelMarkup = $("<div id=\"" + documentId + "\">\n                           <document-viewer-directive document=\"document\"></document-viewer-directive></div></div>");
                        if (!$('#' + documentId).length) {
                            $('#accordion').append(headerMarkup).append(panelMarkup);
                        }
                        else {
                            $('#' + documentId).remove();
                            $('#accordion').append(panelMarkup);
                        }
                        xhr.progress('100%');
                        //LASI.buildMenus();
                        //enableActiveHighlighting();
                    }).fail(function (xhr, message, detail) {
                        log(message);
                    }).progress(function (data) {
                        $progress.css('width', data);
                    });
                };
                $listItemRefs.click(click);
                //$tabs.click(click);
            }, 1000);
        });
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));

/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../lasi.ts" />
(function (app) {
    'use strict';
    app.validateFileExtension = (function () {
        var accepted = Object.freeze(['.txt', '.docx', '.pdf', 'doc']);
        return function (fileName) {
            var extension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
            return accepted.some(function (ext) {
                return ext === extension;
            });
        };
    })();
    $(function () {
        var $uploadList = $('#document-upload-list');
        app.$uploadList = $uploadList;
        $(document)
            .find('.btn-file :file')
            .change(function () {
            var $input = $(this), fileCount = $uploadList.find('span.file-index').length, files = $input[0].files;
            app.files = files;
            app.fileCount = fileCount;
            var generateUploadListItemMarkup = function (file, index) {
                return '<div class="list-group-item text-left">' +
                    '<span class="glyphicon glyphicon-remove remove-file"/>' +
                    '&nbsp;&nbsp;&nbsp;<span class="file-index">' +
                    [fileCount, index, 1].sum() +
                    '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">' +
                    file.name + '</span></div>';
            }, label = $input.val().replace(/\\/g, '/').replace(/[.]*\//, '');
            $(files).filter(function (index, element) { return app.validateFileExtension(element.name); })
                .toArray()
                .filter(function (file) {
                return !($uploadList.children('span').toArray().some(function () {
                    return $(this).text() === file.name;
                }));
            }).forEach(function (file, index) {
                $uploadList.append(generateUploadListItemMarkup(file, index));
                $('span.glyphicon.glyphicon-remove.remove-file')
                    .click(function () {
                    $(this).removeData(file.name)
                        .parent()
                        .parent()
                        .find('span.file-name')
                        .filter(function () { return $(this).text() === file.name; })
                        .each(function () { return $(this).parent('div').remove(); });
                    $uploadList.find('span.file-index')
                        .each(function (index) { $(this).text(index + 1); });
                });
            });
            $input.trigger('fileselect', [files && files.length, label]);
        });
    });
})(LASI);

var LASI;
(function (LASI) {
    var widgets;
    (function (widgets) {
        'use strict';
        processingTaskProgressBar.$inject = [];
        function processingTaskProgressBar() {
            return {
                restrict: 'E',
                scope: {
                    task: '='
                },
                templateUrl: '/app/widgets/processing-task-progress-bar.directive.html'
            };
        }
        angular
            .module('widgets')
            .directive({ processingTaskProgressBar: processingTaskProgressBar });
    })(widgets = LASI.widgets || (LASI.widgets = {}));
})(LASI || (LASI = {}));

//module LASI.documentViewer {
//    'use strict';
//    export interface IDocumentModelService {
//        processDocument(documentId: string): models.IDocumentModel;
//    }
//    class MockDocumentModelService implements IDocumentModelService {
//        static $inject: string[] = ['$resource'];
//        documentSource: ng.resource.IResourceClass<models.IDocumentModel>;
//        constructor(private $resource: ng.resource.IResourceService) {
//            this.documentSource = $resource<models.IDocumentModel>('Analysis/:documentId');
//        }
//        processDocument(documentId: string) {
//            return this.$resource<models.IDocumentModel>('tests/test-data/doc.json').get();
//        }
//    }
//    angular
//        .module('documentViewer')
//        .service('MockDocumentModelService', MockDocumentModelService);
//} 

/// <amd-dependency path="angular" />
/// <amd-dependency path="angular-resource" />
/// <amd-dependency path="angular-bootstrap" />
/// <amd-dependency path="angular-bootstrap-contextmenu" />
/// <amd-dependency path="angular-file-upload" />
/// <amd-dependency path="jquery" />
/// <amd-dependency path="app/utilities/augmentations"
define(["require", "exports", 'angular', './app/debug/debug.module', './app/widgets/widgets.module', './app/document-list/document-list.module', './app/document-upload/document-upload.module', './app/document-viewer/document-viewer.module', './app/document-viewer/search/search.module', "angular", "angular-resource", "angular-bootstrap", "angular-bootstrap-contextmenu", "angular-file-upload", "jquery", "app/utilities/augmentations", 'app/utilities/augmentations'], function (require, exports, angular_1, debug_module_1, widgets_module_1, document_list_module_1, document_upload_module_1, document_viewer_module_1, search_module_1) {
    function register(m) {
        function validate() {
            if (!m.name) {
                throw new TypeError("name is required");
            }
            if (!m.requires) {
                throw new TypeError("requires must be an array. Did you intend to invoke the setter?");
            }
        }
        angular_1.module(m.name, m.requires, m.configFn || (function () { }))
            .provider(m.providers || {})
            .factory(m.factories || {})
            .service(m.services || {})
            .filter(m.filters || {})
            .controller(m.controllers || {})
            .directive(m.directives || {})
            .value(m.values || {})
            .constant(m.constants || {})
            .run(m.runFn || (function () { }));
    }
    function angularBootstrap() {
        function registerAll() {
            var modules = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                modules[_i - 0] = arguments[_i];
            }
            modules.forEach(register);
        }
        return function () {
            registerAll(debug_module_1.default, widgets_module_1.default, document_list_module_1.default, document_upload_module_1.default, document_viewer_module_1.default, search_module_1.default);
            angular_1.bootstrap(document.body, ['documentList']);
        };
    }
    exports.angularBootstrap = angularBootstrap;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC91dGlsaXRpZXMvYXVnbWVudGF0aW9ucy50cyIsImFwcC9MQVNJLnRzIiwiYXBwL2RvY3VtZW50LWxpc3QvdGFza3MtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyLnRzIiwiYXBwL2RlYnVnL2RlYnVnLXBhbmVsLmRpcmVjdGl2ZS50cyIsImFwcC9kZWJ1Zy9kZWJ1Zy5tb2R1bGUudHMiLCJhcHAvYW5ndWxhci1zaGltLnRzIiwiYXBwL2RvY3VtZW50LXVwbG9hZC91cGxvYWQtcGFuZWwuZGlyZWN0aXZlLnRzIiwiYXBwL2RvY3VtZW50LXVwbG9hZC9kb2N1bWVudC11cGxvYWQubW9kdWxlLnRzIiwiYXBwL2RvY3VtZW50LWxpc3QvZG9jdW1lbnRzLnNlcnZpY2UudHMiLCJhcHAvZG9jdW1lbnQtdmlld2VyL2xleGljYWwtbWVudS1idWlsZGVyLnNlcnZpY2UudHMiLCJtb2RlbHMuanMiLCJhcHAvZG9jdW1lbnQtbGlzdC9yZXN1bHRzLnNlcnZpY2UudHMiLCJhcHAvZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LW1vZGVsLnNlcnZpY2UudHMiLCJhcHAvZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LXNlcnZpY2UucHJvdmlkZXIudHMiLCJhcHAvZG9jdW1lbnQtbGlzdC91cGxvYWQuY29udHJvbGxlci50cyIsImFwcC9kb2N1bWVudC1saXN0L2xpc3QuY29udHJvbGxlci50cyIsImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QtdGFic2V0LWl0ZW0uZGlyZWN0aXZlLnRzIiwiYXBwL2RvY3VtZW50LWxpc3QvZG9jdW1lbnQtbGlzdC1tZW51LWl0ZW0uZGlyZWN0aXZlLnRzIiwiYXBwL2RvY3VtZW50LWxpc3QvZG9jdW1lbnQtbGlzdC5tb2R1bGUudHMiLCJhcHAvZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LmNvbnRyb2xsZXIudHMiLCJhcHAvZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LXZpZXdlci5tb2R1bGUudHMiLCJhcHAvd2lkZ2V0cy93aWRnZXRzLm1vZHVsZS50cyIsImFwcC9kb2N1bWVudC12aWV3ZXIvc2VhcmNoL3NlYXJjaC1ib3guZGlyZWN0aXZlLnRzIiwiYXBwL2RvY3VtZW50LXZpZXdlci9zZWFyY2gvc2VhcmNoLm1vZHVsZS50cyIsImFuZ3VsYXItbW9kdWxlLXV0aWxzLmpzIiwiYXBwL2NvbnRleHRtZW51LWRlZmluaXRpb25zLXRlc3RzLnRzIiwiYXBwL2FjY291bnQvbWFuYWdlLnRzIiwiYXBwL2RvY3VtZW50LWxpc3QvZG9jdW1lbnQtbGlzdC1pdGVtLnRzIiwiYXBwL3Jlc3VsdHMvY29udGV4dC1tZW51LXByb3ZpZGVyLnRzIiwiYXBwL3Jlc3VsdHMvcmVzdWx0LWNoYXJ0LXByb3ZpZGVyLnRzIiwiYXBwL2RvY3VtZW50LXZpZXdlci9kb2N1bWVudC1wYWdlLmRpcmVjdGl2ZS50cyIsImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtdmlld2VyLmRpcmVjdGl2ZS50cyIsImFwcC9kb2N1bWVudC12aWV3ZXIvcGFyYWdyYXBoLmRpcmVjdGl2ZS50cyIsImFwcC9kb2N1bWVudC12aWV3ZXIvcGhyYXNlLmRpcmVjdGl2ZS50cyIsImFwcC9kb2N1bWVudC12aWV3ZXIvc2VudGVuY2UuZGlyZWN0aXZlLnRzIiwiYXBwL3dpZGdldHMvZG9jdW1lbnQtbGlzdC50cyIsImFwcC93aWRnZXRzL2RvY3VtZW50LXVwbG9hZC50cyIsImFwcC93aWRnZXRzL3Byb2Nlc3NpbmctdGFzay1wcm9ncmVzcy1iYXIuZGlyZWN0aXZlLnRzIiwiYXBwL2RvY3VtZW50LXZpZXdlci90ZXN0cy9tb2NrLWRvY3VtZW50LW1vZGVsLXNlcnZpY2UudHMiLCJtYWluLnRzIl0sIm5hbWVzIjpbInRhc2tzTGlzdFNlcnZpY2VQcm92aWRlciIsInRhc2tzTGlzdFNlcnZpY2VQcm92aWRlci5zZXRVcGRhdGVJbnRlcnZhbCIsInRhc2tzTGlzdFNlcnZpY2VQcm92aWRlci5zZXRUYXNrc0xpc3RVcmwiLCJ0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIuJGdldCIsInRhc2tzTGlzdFNlcnZpY2VQcm92aWRlci5jcmVhdGVEZWJ1Z0luZm9VcGRhdG9yIiwiRGVidWdQYW5lbENvbnRyb2xsZXIiLCJEZWJ1Z1BhbmVsQ29udHJvbGxlci5jb25zdHJ1Y3RvciIsIkRlYnVnUGFuZWxDb250cm9sbGVyLmJ1dHRvblRleHQiLCJEZWJ1Z1BhbmVsQ29udHJvbGxlci50b2dnbGUiLCJkZWJ1Z1BhbmVsIiwidXBsb2FkUGFuZWwiLCJkb2N1bWVudHNTZXJ2aWNlIiwiZG9jdW1lbnRzU2VydmljZS5nZXRieUlkIiwiZG9jdW1lbnRzU2VydmljZS5kZWxldGVCeUlkIiwibGV4aWNhbE1lbnVCdWlsZGVyIiwibGV4aWNhbE1lbnVCdWlsZGVyLnZlcmJhbE1lbnVJc1ZpYWJsZSIsImxleGljYWxNZW51QnVpbGRlci5yZWZlcmVuY2VyTWVudUlzVmlhYmxlIiwiY3JlYXRlRm9yUmVmZXJlbmNlck1lbnVCdWlsZGVyIiwiY3JlYXRlRm9yVmVyYmFsTWVudUJ1aWxkZXIiLCJyZXNldFZlcmJhbEFzc29jaWF0aW9uQ3NzQ2xhc3NlcyIsInJlc3VsdHNTZXJ2aWNlIiwic3VjY2VzcyIsImVycm9yIiwiZG9jdW1lbnRNb2RlbFNlcnZpY2UiLCJkb2N1bWVudE1vZGVsU2VydmljZS5wcm9jZXNzRG9jdW1lbnQiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuY29uc3RydWN0b3IiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuc2V0RG9jdW1lbnRMaXN0VXJsIiwiRG9jdW1lbnRMaXN0U2VydmljZVByb3ZpZGVyLnNldFJlY2VudERvY3VtZW50Q291bnQiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuJGdldCIsIlVwbG9hZENvbnRyb2xsZXIiLCJVcGxvYWRDb250cm9sbGVyLmNvbnN0cnVjdG9yIiwiVXBsb2FkQ29udHJvbGxlci51cGxvYWRGaWxlcyIsIlVwbG9hZENvbnRyb2xsZXIubG9nSW52YWxpZEZpbGVzIiwiVXBsb2FkQ29udHJvbGxlci51cGxvYWRGaWxlIiwiVXBsb2FkQ29udHJvbGxlci5yZW1vdmVGaWxlIiwiTGlzdENvbnRyb2xsZXIiLCJMaXN0Q29udHJvbGxlci5jb25zdHJ1Y3RvciIsIkxpc3RDb250cm9sbGVyLnRvZ2dsZUV4cGFuZGVkIiwiTGlzdENvbnRyb2xsZXIuZGVsZXRlQnlJZCIsIkxpc3RDb250cm9sbGVyLmRvY3VtZW50Q291bnQiLCJMaXN0Q29udHJvbGxlci5wcm9jZXNzRG9jdW1lbnQiLCJMaXN0Q29udHJvbGxlci5hY3RpdmF0ZSIsImRvY3VtZW50TGlzdFRhYnNldEl0ZW0iLCJkb2N1bWVudExpc3RNZW51SXRlbSIsImNvbmZpZ3VyZSIsIkRvY3VtZW50Q29udHJvbGxlciIsIkRvY3VtZW50Q29udHJvbGxlci5jb25zdHJ1Y3RvciIsIkRvY3VtZW50Q29udHJvbGxlci5wcm9jZXNzRG9jdW1lbnQiLCJTZWFyY2hCb3hDb250cm9sbGVyIiwiU2VhcmNoQm94Q29udHJvbGxlci5jb25zdHJ1Y3RvciIsIlNlYXJjaEJveENvbnRyb2xsZXIuZ2V0V29yZHMiLCJTZWFyY2hCb3hDb250cm9sbGVyLnNlYXJjaCIsInNlYXJjaEJveCIsImNvbnRleHRtZW51VGVzdHMiLCJMQVNJIiwiTEFTSS5hY2NvdW50TWFuYWdlbWVudCIsImRvY3VtZW50UGFnZSIsImRvY3VtZW50UGFnZS5saW5rIiwiZG9jdW1lbnRWaWV3ZXJEaXJlY3RpdmUiLCJMQVNJLmRvY3VtZW50Vmlld2VyIiwiTEFTSS5kb2N1bWVudFZpZXdlci5wYXJhZ3JhcGgiLCJwaHJhc2UiLCJwaHJhc2UubGluayIsIkxBU0kuZG9jdW1lbnRWaWV3ZXIuc2VudGVuY2UiLCJMQVNJLmRvY3VtZW50TGlzdCIsIkxBU0kud2lkZ2V0cyIsIkxBU0kud2lkZ2V0cy5wcm9jZXNzaW5nVGFza1Byb2dyZXNzQmFyIiwicmVnaXN0ZXIiLCJyZWdpc3Rlci52YWxpZGF0ZSIsImFuZ3VsYXJCb290c3RyYXAiLCJhbmd1bGFyQm9vdHN0cmFw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QUNBLE9BQU9BLENBQUNBLFFBQVFBLENBQUNBLENBQUNBO1FBQzlCQSxDQUFDQTtRQUVERCxNQUFNQSxDQUFDQTtZQUNILFdBQVcsQ0FBQyxzQkFBSyxFQUFFLHdCQUFPLEVBQUUsOEJBQVksRUFBRSxnQ0FBYyxFQUFFLGdDQUFjLEVBQUUsdUJBQW9CLENBQUMsQ0FBQztZQUNoRyxtQkFBUyxDQUFDLFFBQVEsQ0FBQyxJQUFJLEVBQUUsQ0FBQyxjQUFjLENBQUMsQ0FBQyxDQUFDO1FBQy9DLENBQUMsQ0FBQ0E7SUFDTkEsQ0FBQ0E7SUFUZSx3QkFBZ0IsbUJBUy9CLENBQUEiLCJmaWxlIjoiYXBwLmpzIiwic291cmNlc0NvbnRlbnQiOlsiLyoqXHJcbiAqIEF1Z21lbnRzIEFycmF5IHdpdGggdGhlIGFkZGl0aW9uYWwgbWV0aG9kcywgaWYgYW5kIG9ubHkgaWYgYSBtZW1iZXIgd2l0aCB0aGUgc2FtZSBuYW1lIGFzIHRoZSBtZXRob2QgaGFzIG5vdCBhbHJlYWR5IGJlZW4gZGVmaW5lZC5cclxuICovXHJcblxyXG4oZnVuY3Rpb24gKCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG4gICAgdmFyIGFycmF5QWd1bWVudGF0aW9ucyA9IHtcclxuICAgICAgICBmbGF0TWFwOiBmdW5jdGlvbiAoYXJyYXlTZWxlY3RvciwgZWxlbWVudFNlbGVjdG9yKSB7XHJcbiAgICAgICAgICAgIGFycmF5U2VsZWN0b3IgPSBhcnJheVNlbGVjdG9yIHx8IGZ1bmN0aW9uIChhcnJheSkge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIGFycmF5IGluc3RhbmNlb2YgQXJyYXkgJiYgYXJyYXk7XHJcbiAgICAgICAgICAgIH07XHJcbiAgICAgICAgICAgIGVsZW1lbnRTZWxlY3RvciA9IGVsZW1lbnRTZWxlY3RvciB8fCBmdW5jdGlvbiAoZWxlbWVudCkgeyByZXR1cm4gZWxlbWVudDsgfTtcclxuICAgICAgICAgICAgcmV0dXJuIHRoaXMucmVkdWNlKGZ1bmN0aW9uIChhcnJheSwgYSkge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIGFycmF5LmNvbmNhdChhcnJheVNlbGVjdG9yKGEpLm1hcChlbGVtZW50U2VsZWN0b3IpKTtcclxuICAgICAgICAgICAgfSwgW10pO1xyXG4gICAgICAgIH0sXHJcbiAgICAgICAgY29ycmVsYXRlOiBmdW5jdGlvbiAoaW5uZXIsIG91dGVyS2V5U2VsZWN0b3IsIGlubmVyS2V5U2VsZWN0b3IsIHJlc3VsdFNlbGVjdG9yKSB7XHJcbiAgICAgICAgICAgIHZhciBvdXRlcktleWVkLCBpbm5lcktleWVkLCBpLCBqLCByZXN1bHRzOiB7IGZpcnN0OiBhbnksIHNlY29uZDogYW55IH1bXSA9IFtdO1xyXG4gICAgICAgICAgICBvdXRlcktleWVkID0gdGhpcy5tYXAoZnVuY3Rpb24gKGUpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiB7IGVsZW1lbnQ6IGUsIGtleTogb3V0ZXJLZXlTZWxlY3RvcihlKSB9O1xyXG4gICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgaW5uZXJLZXllZCA9IGlubmVyLm1hcChmdW5jdGlvbiAoZSkge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIHsgZWxlbWVudDogZSwga2V5OiBpbm5lcktleVNlbGVjdG9yKGUpIH07XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICBmb3IgKGkgPSAwOyBpIDwgb3V0ZXJLZXllZC5sZW5ndGg7IGkgKz0gMSkge1xyXG4gICAgICAgICAgICAgICAgZm9yIChqID0gMDsgaiA8IGlubmVyS2V5ZWQubGVuZ3RoOyBqICs9IDEpIHtcclxuICAgICAgICAgICAgICAgICAgICBpZiAob3V0ZXJLZXllZFtpXS5rZXkgPT09IGlubmVyS2V5ZWRbal0ua2V5KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHJlc3VsdHMucHVzaCh7IGZpcnN0OiBvdXRlcktleWVkW2ldLmVsZW1lbnQsIHNlY29uZDogaW5uZXJLZXllZFtqXS5lbGVtZW50IH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAvLyBJZiBhIGZ1bmN0aW9uIHdhcyBzdXBwbGllZCwgbWFwIGl0IG92ZXIgdGhlIGFycmF5IG9mIHJlc3VsdCBwYWlycyBhbmQgcmV0dXJuIHRoZSByZXN1bHQuXHJcbiAgICAgICAgICAgIC8vIG90aGVyd2lzZSwganVzdCByZXR1cm4gdGhlIGFycmF5IG9mIHJlc3VsdCBwYWlycy5cclxuICAgICAgICAgICAgcmV0dXJuIHJlc3VsdFNlbGVjdG9yID8gcmVzdWx0cy5tYXAoZnVuY3Rpb24gKGUpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiByZXN1bHRTZWxlY3RvcihlLmZpcnN0LCBlLnNlY29uZCk7XHJcbiAgICAgICAgICAgIH0pIDogcmVzdWx0cztcclxuICAgICAgICB9LFxyXG4gICAgICAgIHN1bTogZnVuY3Rpb24gKHZhbHVlU2VsZWN0b3I6IChhbnkpID0+IG51bWJlcikge1xyXG4gICAgICAgICAgICAvLyBJZiB0aGUgYSB2YWx1ZVNlbGVjdG9yIHdhcyBub3QgcHJvdmlkZWQsIGRlZmluZSBhIGZ1bmN0aW9uIHdoaWNoIHdpbGwgYXR0ZW1wdCBcclxuICAgICAgICAgICAgLy8gdG8gY29udmVydCBpdHMgYXJndW1lbnQgdG8gYSBudW1iZXIuXHJcbiAgICAgICAgICAgIHZhciBwcm9qZWN0aW9uID0gdmFsdWVTZWxlY3RvciB8fCAoeCA9PiBOdW1iZXIoeCkpO1xyXG4gICAgICAgICAgICByZXR1cm4gdGhpcy5sZW5ndGggPT09IDAgPyAwIDogdGhpcy5yZWR1Y2UoZnVuY3Rpb24gKHRvdGFsLCBlbGVtZW50KSB7IHJldHVybiB0b3RhbCArIHByb2plY3Rpb24oZWxlbWVudCk7IH0sIDApO1xyXG4gICAgICAgIH0sXHJcbiAgICAgICAgYXZlcmFnZTogZnVuY3Rpb24gKHZhbHVlU2VsZWN0b3IpIHtcclxuICAgICAgICAgICAgcmV0dXJuIHRoaXMuc3VtKHZhbHVlU2VsZWN0b3IgfHwgKHggPT4gTnVtYmVyKHgpKSkgLyB0aGlzLmxlbmd0aDtcclxuICAgICAgICB9LFxyXG4gICAgICAgIGZpcnN0OiBmdW5jdGlvbiAocHJlZGljYXRlPzogKGVsZW1lbnQ6IGFueSkgPT4gYm9vbGVhbikge1xyXG4gICAgICAgICAgICBpZiAoIXByZWRpY2F0ZSkge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIHRoaXNbMF07XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgZm9yIChsZXQgaSA9IDA7IGkgPCB0aGlzLmxlbmd0aDsgKytpKSB7XHJcbiAgICAgICAgICAgICAgICBpZiAocHJlZGljYXRlKHRoaXNbaV0pKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgcmV0dXJuIHRoaXNbaV07XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgcmV0dXJuIHVuZGVmaW5lZDtcclxuICAgICAgICB9LFxyXG4gICAgICAgIGxhc3Q6IGZ1bmN0aW9uIChwcmVkaWNhdGU/OiAoZWxlbWVudDogYW55KSA9PiBib29sZWFuKSB7XHJcbiAgICAgICAgICAgIGlmICghcHJlZGljYXRlKSB7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gdGhpc1t0aGlzLmxlbmd0aCAtIDFdO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIGZvciAobGV0IGkgPSB0aGlzLmxlbmd0aCAtIDE7IGkgPiAwOyAtLWkpIHtcclxuICAgICAgICAgICAgICAgIGlmIChwcmVkaWNhdGUodGhpc1tpXSkpIHtcclxuICAgICAgICAgICAgICAgICAgICByZXR1cm4gdGhpc1tpXTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICByZXR1cm4gdW5kZWZpbmVkO1xyXG4gICAgICAgIH1cclxuICAgIH07XHJcbiAgICBPYmplY3Qua2V5cyhhcnJheUFndW1lbnRhdGlvbnMpXHJcbiAgICAgICAgLmZpbHRlcihrZXkgPT4gIU9iamVjdC5wcm90b3R5cGUuaGFzT3duUHJvcGVydHkuY2FsbChBcnJheS5wcm90b3R5cGUsIGtleSkpXHJcbiAgICAgICAgLm1hcCgoa2V5KTogKFtzdHJpbmcsIFByb3BlcnR5RGVzY3JpcHRvcl0pID0+IFtrZXksXHJcbiAgICAgICAge1xyXG4gICAgICAgICAgICB3cml0YWJsZTogZmFsc2UsXHJcbiAgICAgICAgICAgIGVudW1lcmFibGU6IGZhbHNlLFxyXG4gICAgICAgICAgICBjb25maWd1cmFibGU6IGZhbHNlLFxyXG4gICAgICAgICAgICB2YWx1ZTogYXJyYXlBZ3VtZW50YXRpb25zW2tleV1cclxuICAgICAgICB9XSlcclxuICAgICAgICAuZm9yRWFjaCgoW25hbWUsIHByb3BlcnR5XSkgPT4gT2JqZWN0LmRlZmluZVByb3BlcnR5KEFycmF5LnByb3RvdHlwZSwgbmFtZSwgcHJvcGVydHkpKTtcclxufSkoKTtcclxuaW50ZXJmYWNlIEFycmF5PFQ+IHtcclxuICAgIGZsYXRNYXA8VEFycmF5LCBUUmVzdWx0PihhcnJheVNlbGVjdG9yOiAoZWxlbWVudDogVCkgPT4gVEFycmF5W10sIGVsZW1lbnRTZWxlY3RvcjogKGVsZW1lbnQ6IFRBcnJheSkgPT4gVFJlc3VsdCk6IFRSZXN1bHRbXTtcclxuICAgIGZsYXRNYXA8VEFycmF5PihhcnJheVNlbGVjdG9yOiAoZWxlbWVudDogVCkgPT4gVEFycmF5W10pOiBUQXJyYXlbXTtcclxuICAgIC8vZmxhdE1hcDxVPihhcnJheVNlbGVjdG9yOiAoZWxlbWVudDogVCkgPT4gVVtdLCBlbGVtZW50U2VsZWN0b3I/OiAoZWxlbWVudDogVCkgPT4gVSk6IFVbXTtcclxuICAgIC8vZmxhdE1hcDxVIGV4dGVuZHMgVFtdPigpOiBUW107XHJcbiAgICBmbGF0TWFwKCk6IGFueVtdO1xyXG4gICAgY29ycmVsYXRlPFRJbm5lciwgVEtleT4oaW5uZXI6IFRJbm5lcltdLCBvdXRlcktleVNlbGVjdG9yOiAoZTogVCkgPT4gVEtleSwgaW5uZXJLZXlTZWxlY3RvcjogKGU6IFRJbm5lcikgPT4gVEtleSk6IHsgZmlyc3Q6IFQsIHNlY29uZDogVElubmVyIH1bXTtcclxuICAgIC8qKlxyXG4gICAgKiBAZnVuY3Rpb24gY29ycmVsYXRlIENvcnJlbGF0ZXMgdGhlIGVsZW1lbnRzIG9mIHRoZSBhcnJheSB3aXRoIHRoZSBlbGVtZW50cyBvZiBhbm90aGVyIFxyXG4gICAgKiBiYXNlZCBvbiB0aGUgc3RyaWN0ICg9PT0pIGVxdWFsaXR5IG9mIHRoZSBrZXlzIHNlbGVjdGVkIGZyb20gZWFjaCBlbGVtZW50XHJcbiAgICAqIGJ5IHRoZSBwcm92aWRlZCBzZWxlY3RvciBmdW5jdGlvbnMuIE1vZGVscyB0aGUgc2VtYW50aWNzIG9mIGEgcmVsYXRpb25hbCBpbm5lciBqb2luLlxyXG4gICAgKiBSZXR1cm5zIGFuIGFycmF5IG9mIGVsZW1lbnRzIHByb2R1Y2VkIGJ5IGludm9raW5nIHRoZSBzcGVjaWZpZWQgcmVzdWx0U2VsZWN0b3IgZnVuY3Rpb24gb24gZWFjaCBwYWlyIG9mIGNvcnJlbGF0ZWQgZWxlbWVudHMuXHJcbiAgICAqIEBwYXJhbSBpbm5lciBBbiBhcnJheSB3aG9zZSBlbGVtZW50cyB3aWxsIGJlIGNvcnJlbGF0ZWQgd2l0aCB0aGUgc291cmNlIGFycmF5LlxyXG4gICAgKiBAcGFyYW0gb3V0ZXJLZXlTZWxlY3RvciBBIGZ1bmN0aW9uIHRvIHByb2R1Y2UgYSBrZXkgdmFsdWUgZnJvbSBlYWNoIGVsZW1lbnQgaW4gdGhlIG91dGVyIGFycmF5LlxyXG4gICAgKiBAcGFyYW0gaW5uZXJLZXlTZWxlY3RvciBBIGZ1bmN0aW9uIHRvIHByb2R1Y2UgYSBrZXkgdmFsdWUgZnJvbSBlYWNoIGVsZW1lbnQgaW4gdGhlIGlubmVyIGFycmF5LlxyXG4gICAgKiBAcGFyYW0gcmVzdWx0U2VsZWN0b3IgQSBmdW5jdGlvbiB0byB0cmFuc2Zvcm0gZWFjaCBwYWlyIG9mIGNvcnJlbGF0ZWQgZWxlbWVudHMuXHJcbiAgICAqIEByZXR1cm5zIEFuIGFycmF5IG9mIGVsZW1lbnRzIHByb2R1Y2VkIGJ5IGludm9raW5nIHRoZSBzcGVjaWZpZWQgcmVzdWx0U2VsZWN0b3IgZnVuY3Rpb24gb24gZWFjaCBwYWlyIG9mIGNvcnJlbGF0ZWQgZWxlbWVudHMuIFxyXG4gICAgKi9cclxuICAgIGNvcnJlbGF0ZTxUSW5uZXIsIFRLZXksIFRSZXN1bHQ+KFxyXG4gICAgICAgIGlubmVyOiBUSW5uZXJbXSxcclxuICAgICAgICBvdXRlcktleVNlbGVjdG9yOiAoZTogVCkgPT4gVEtleSxcclxuICAgICAgICBpbm5lcktleVNlbGVjdG9yOiAoZTogVElubmVyKSA9PiBUS2V5LFxyXG4gICAgICAgIHJlc3VsdFNlbGVjdG9yOiAoZmlyc3Q6IFQsIHNlY29uZDogVElubmVyKSA9PiBUUmVzdWx0XHJcbiAgICAgICAgKTogVFJlc3VsdFtdO1xyXG4gICAgc3VtKHZhbHVlU2VsZWN0b3I/OiAoYW55KSA9PiBudW1iZXIpOiBudW1iZXI7XHJcbiAgICBhdmVyYWdlKHZhbHVlU2VsZWN0b3I/OiAoYW55KSA9PiBudW1iZXIpOiBudW1iZXI7XHJcblxyXG4gICAgZmlyc3QocHJlZGljYXRlPzogKGVsZW1lbnQ6IFQpID0+IGJvb2xlYW4pOiBUO1xyXG4gICAgbGFzdChwcmVkaWNhdGU/OiAoZWxlbWVudDogVCkgPT4gYm9vbGVhbik6IFQ7XHJcblxyXG59ICIsIi8vLyA8cmVmZXJlbmNlIHBhdGg9XCIuLi8uLi90eXBpbmdzL3RzZC5kLnRzXCIgLz5cclxuJ3VzZSBzdHJpY3QnO1xyXG5pbXBvcnQgKiBhcyAkIGZyb20gJ2pxdWVyeSc7XHJcbmV4cG9ydCB2YXIgYnVpbGRNZW51czogKCkgPT4gdm9pZDtcclxuZXhwb3J0IHZhciBlbmFibGVBY3RpdmVIaWdobGlnaHRpbmc6ICgpID0+IHZvaWQ7XHJcbi8vZXhwb3J0IHZhciBzZXR1cERyYWdnYWJsZURpYWxvZ3M6ICgpID0+IHZvaWQ7IFxyXG5leHBvcnQgdmFyIGxvZzogKG1lc3NhZ2U/OiBhbnksIC4uLm9wdGlvbmFsUGFyYW1zOiBhbnlbXSkgPT4gdm9pZCA9IGNvbnNvbGUubG9nLmJpbmQoY29uc29sZSk7XHJcbmV4cG9ydCB2YXIgZWRpdG9yID0gJCgnI2ZyZWUtZWRpdG9yJykuY2hhbmdlKGxvZyk7IC8vIFRPRE86IHBhcmFtZXRlcml6ZSBzZWxlY3Rvci4iLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCB7IElRU2VydmljZSwgSUludGVydmFsU2VydmljZSwgSVByb21pc2UgfSBmcm9tICdhbmd1bGFyJztcclxuaW1wb3J0IHsgSVJlc291cmNlU2VydmljZSB9IGZyb20gJ2FuZ3VsYXItcmVzb3VyY2UnO1xyXG5cclxuZXhwb3J0IGludGVyZmFjZSBUYXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIge1xyXG4gICAgJGdldDogKCRxLCAkcmVzb3VyY2U6IElSZXNvdXJjZVNlcnZpY2UsICRpbnRlcnZhbDogSUludGVydmFsU2VydmljZSkgPT4gVGFza3NMaXN0U2VydmljZTtcclxuICAgIHNldFRhc2tzTGlzdFVybCh1cmw6IHN0cmluZyk6IFRhc2tzTGlzdFNlcnZpY2VQcm92aWRlcjtcclxuICAgIHNldFVwZGF0ZUludGVydmFsKG1pbGxpY29uZHM6IG51bWJlcik6IFRhc2tzTGlzdFNlcnZpY2VQcm92aWRlcjtcclxufVxyXG5cclxuZXhwb3J0IGludGVyZmFjZSBUYXNrIHtcclxuICAgIGlkOiBzdHJpbmc7XHJcbiAgICBuYW1lOiBzdHJpbmc7XHJcbiAgICBzdGF0ZTogc3RyaW5nO1xyXG4gICAgcGVyY2VudENvbXBsZXRlOiBudW1iZXI7XHJcbiAgICBzdGF0dXNNZXNzYWdlOiBzdHJpbmc7XHJcbn1cclxuXHJcbmV4cG9ydCBpbnRlcmZhY2UgVGFza3NMaXN0U2VydmljZSB7XHJcbiAgICBnZXRBY3RpdmVUYXNrcygpOiBJUHJvbWlzZTxUYXNrW10+O1xyXG4gICAgdGFza3M6IFRhc2tbXTtcclxufVxyXG5leHBvcnQgZnVuY3Rpb24gdGFza3NMaXN0U2VydmljZVByb3ZpZGVyKCk6IFRhc2tzTGlzdFNlcnZpY2VQcm92aWRlciB7XHJcbiAgICB2YXIgdXBkYXRlSW50ZXJ2YWwgPSAyMDA7XHJcbiAgICB2YXIgdGFza3NMaXN0VXJsID0gJ2FwaS9UYXNrcyc7XHJcblxyXG4gICAgJGdldC4kaW5qZWN0ID0gWyckcScsICckcmVzb3VyY2UnLCAnJGludGVydmFsJ107XHJcblxyXG4gICAgcmV0dXJuIHsgJGdldCwgc2V0VXBkYXRlSW50ZXJ2YWwsIHNldFRhc2tzTGlzdFVybCB9O1xyXG5cclxuICAgIGZ1bmN0aW9uIHNldFVwZGF0ZUludGVydmFsKG1pbGxpc2Vjb25kczogbnVtYmVyKTogVGFza3NMaXN0U2VydmljZVByb3ZpZGVyIHtcclxuICAgICAgICB1cGRhdGVJbnRlcnZhbCA9IG1pbGxpc2Vjb25kcztcclxuICAgICAgICByZXR1cm4gdGhpcztcclxuICAgIH1cclxuICAgIGZ1bmN0aW9uIHNldFRhc2tzTGlzdFVybCh1cmw6IHN0cmluZyk6IFRhc2tzTGlzdFNlcnZpY2VQcm92aWRlciB7XHJcbiAgICAgICAgdGFza3NMaXN0VXJsID0gdXJsO1xyXG4gICAgICAgIHJldHVybiB0aGlzO1xyXG4gICAgfVxyXG5cclxuICAgIGZ1bmN0aW9uICRnZXQoJHE6IElRU2VydmljZSwgJHJlc291cmNlOiBJUmVzb3VyY2VTZXJ2aWNlLCAkaW50ZXJ2YWw6IElJbnRlcnZhbFNlcnZpY2UpOiBUYXNrc0xpc3RTZXJ2aWNlIHtcclxuICAgICAgICB2YXIgVGFza3MgPSAkcmVzb3VyY2U8VGFza1tdPih0YXNrc0xpc3RVcmwsIHt9LCB7XHJcbiAgICAgICAgICAgIGdldDoge1xyXG4gICAgICAgICAgICAgICAgbWV0aG9kOiAnR0VUJywgaXNBcnJheTogdHJ1ZVxyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfSk7XHJcbiAgICAgICAgdmFyIGdldEFjdGl2ZVRhc2tzID0gZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICB2YXIgZGVmZXJyZWQgPSAkcS5kZWZlcjxUYXNrW10+KCk7XHJcbiAgICAgICAgICAgICRpbnRlcnZhbCgoKSA9PiB7XHJcbiAgICAgICAgICAgICAgICB0aGlzLnRhc2tzID0gVGFza3MuZ2V0KCk7XHJcbiAgICAgICAgICAgICAgICBkZWZlcnJlZC5yZXNvbHZlKHRoaXMudGFza3MpO1xyXG4gICAgICAgICAgICB9LCB1cGRhdGVJbnRlcnZhbCk7XHJcbiAgICAgICAgICAgIHJldHVybiBkZWZlcnJlZC5wcm9taXNlO1xyXG4gICAgICAgIH07XHJcblxyXG4gICAgICAgIHJldHVybiB7XHJcbiAgICAgICAgICAgIGdldEFjdGl2ZVRhc2tzLFxyXG4gICAgICAgICAgICB0YXNrczogW11cclxuICAgICAgICB9O1xyXG4gICAgfVxyXG4gICAgZnVuY3Rpb24gY3JlYXRlRGVidWdJbmZvVXBkYXRvcihlbGVtZW50OiBKUXVlcnkpOiAodGFza3M6IFRhc2tbXSkgPT4gSlF1ZXJ5IHtcclxuICAgICAgICByZXR1cm4gdGFza3MgPT4gZWxlbWVudC5odG1sKHRhc2tzLm1hcChcclxuICAgICAgICAgICAgdGFzayA9PiBgPGRpdj4ke09iamVjdC5rZXlzKHRhc2spLm1hcChrZXkgPT4gYDxzcGFuPiZuYnNwJm5ic3Ake3Rhc2tba2V5XX08L3NwYW4+YCkgfTwvZGl2PmBcclxuICAgICAgICApLmpvaW4oKSk7XHJcbiAgICB9XHJcbn1cclxuIiwiJ3VzZSBzdHJpY3QnO1xyXG4vLy8gPGFtZC1kZXBlbmRlbmN5IHBhdGg9XCJ0ZXh0IS4vZGVidWctcGFuZWwuZGlyZWN0aXZlLmh0bWxcIj5cclxuaW1wb3J0IHsgVGFza3NMaXN0U2VydmljZSwgVGFzayB9IGZyb20gJ2FwcC9kb2N1bWVudC1saXN0L3Rhc2tzLWxpc3Qtc2VydmljZS5wcm92aWRlcic7XHJcblxyXG5jbGFzcyBEZWJ1Z1BhbmVsQ29udHJvbGxlciB7XHJcbiAgICBzdGF0aWMgJGluamVjdCA9IFsndGFza3NMaXN0U2VydmljZSddO1xyXG4gICAgY29uc3RydWN0b3IodGFza3NMaXN0U2VydmljZTogVGFza3NMaXN0U2VydmljZSkge1xyXG4gICAgICAgIHRhc2tzTGlzdFNlcnZpY2UuZ2V0QWN0aXZlVGFza3MoKS50aGVuKGRhdGEgPT4gdGhpcy50YXNrcyA9IGRhdGEuc29ydCgoeCwgeSkgPT4geC5pZC5sb2NhbGVDb21wYXJlKHkuaWQpKSk7XHJcbiAgICB9XHJcblxyXG4gICAgZ2V0IGJ1dHRvblRleHQoKSB7IHJldHVybiB0aGlzLmluZm9WaXNpYmxlID8gJ0hpZGUnIDogJ1Nob3cnOyB9XHJcblxyXG4gICAgdG9nZ2xlKCkgeyB0aGlzLmluZm9WaXNpYmxlID0gIXRoaXMuaW5mb1Zpc2libGU7IH1cclxuXHJcbiAgICB0YXNrczogVGFza1tdID0gW107XHJcbiAgICBpbmZvVmlzaWJsZSA9IGZhbHNlO1xyXG59XHJcbmV4cG9ydCBmdW5jdGlvbiBkZWJ1Z1BhbmVsKCk6IG5nLklEaXJlY3RpdmUge1xyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICB0ZW1wbGF0ZTogcmVxdWlyZSgnYXBwL2RlYnVnL2RlYnVnLXBhbmVsLmRpcmVjdGl2ZS5odG1sIScpLFxyXG4gICAgICAgIGNvbnRyb2xsZXI6IERlYnVnUGFuZWxDb250cm9sbGVyLFxyXG4gICAgICAgIGJpbmRUb0NvbnRyb2xsZXI6IHt9LFxyXG4gICAgICAgIGNvbnRyb2xsZXJBczogJ2RlYnVnJ1xyXG4gICAgfTtcclxufSIsImltcG9ydCB7IGRlYnVnUGFuZWwgfSBmcm9tICcuL2RlYnVnLXBhbmVsLmRpcmVjdGl2ZSc7XHJcblxyXG52YXIgbTogQW5ndWxhck1vZHVsZU9wdGlvbnMgPSB7XHJcbiAgICBuYW1lOiAnZGVidWcnLFxyXG4gICAgcmVxdWlyZXM6IFtdLFxyXG4gICAgZGlyZWN0aXZlczogeyBkZWJ1Z1BhbmVsIH1cclxufTtcclxuZXhwb3J0IGRlZmF1bHQgbTsgICAiLCJcclxuaW1wb3J0ICogYXMgYW5ndWxhciBmcm9tICdhbmd1bGFyJztcclxuaW1wb3J0ICogYXMgcmVzb3VyY2UgZnJvbSAnYW5ndWxhci1yZXNvdXJjZSc7XHJcbmltcG9ydCAqIGFzIGFuZ3VsYXJCb290c3RyYXAgZnJvbSAnYW5ndWxhci1ib290c3RyYXAnO1xyXG5pbXBvcnQgKiBhcyBhbmd1bGFyQm9vdHN0cmFwQ29udGV4dG1lbnUgZnJvbSAnYW5ndWxhci1ib290c3RyYXAtY29udGV4dG1lbnUnO1xyXG5pbXBvcnQgKiBhcyBhbmd1bGFyRmlsZVVwbG9hZCBmcm9tICdhbmd1bGFyLWZpbGUtdXBsb2FkJztcclxuaW1wb3J0ICogYXMgJCBmcm9tICdqcXVlcnknO1xyXG5leHBvcnQgZGVmYXVsdCB7XHJcbiAgICAkLCBhbmd1bGFyLCByZXNvdXJjZSwgYW5ndWxhckJvb3RzdHJhcCwgYW5ndWxhckJvb3RzdHJhcENvbnRleHRtZW51LCBhbmd1bGFyRmlsZVVwbG9hZFxyXG59XHJcbiIsIid1c2Ugc3RyaWN0JztcclxuXHJcbnVwbG9hZFBhbmVsLiRpbmplY3QgPSBbJyR3aW5kb3cnXTtcclxuXHJcbmV4cG9ydCBmdW5jdGlvbiB1cGxvYWRQYW5lbCgkd2luZG93KTogbmcuSURpcmVjdGl2ZSB7XHJcbiAgICByZXR1cm4ge1xyXG4gICAgICAgIHJlc3RyaWN0OiAnRSdcclxuICAgIH07XHJcbn0iLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCAnYXBwL2FuZ3VsYXItc2hpbSc7XHJcbmltcG9ydCAqIGFzIGFuZ3VsYXIgZnJvbSAnYW5ndWxhcic7XHJcbmltcG9ydCB7IHVwbG9hZFBhbmVsIH0gZnJvbSAnLi91cGxvYWQtcGFuZWwuZGlyZWN0aXZlJztcclxuXHJcbmV4cG9ydCBkZWZhdWx0IHtcclxuICAgIG5hbWU6ICdkb2N1bWVudFVwbG9hZCcsXHJcbiAgICByZXF1aXJlczogWyduZ0ZpbGVVcGxvYWQnXSxcclxufSIsIid1c2Ugc3RyaWN0JztcclxuaW1wb3J0IHsgSVJlc291cmNlU2VydmljZSB9IGZyb20gJ2FuZ3VsYXItcmVzb3VyY2UnO1xyXG5cclxudHlwZSBEb2N1bWVudExpc3RJdGVtID0gTEFTSS5kb2N1bWVudExpc3QuRG9jdW1lbnRMaXN0SXRlbTtcclxuZG9jdW1lbnRzU2VydmljZS4kaW5qZWN0ID0gWyckcmVzb3VyY2UnXTtcclxuZXhwb3J0IGludGVyZmFjZSBEb2N1bWVudHNTZXJ2aWNlIHtcclxuICAgIGdldGJ5SWQoZG9jdW1lbnRJZDogc3RyaW5nKTogRG9jdW1lbnRMaXN0SXRlbVtdO1xyXG4gICAgZGVsZXRlQnlJZChkb2N1bWVudElkOiBzdHJpbmcpOiBEb2N1bWVudExpc3RJdGVtW107XHJcbn1cclxuZXhwb3J0IGZ1bmN0aW9uIGRvY3VtZW50c1NlcnZpY2UoJHJlc291cmNlOiBJUmVzb3VyY2VTZXJ2aWNlKTogRG9jdW1lbnRzU2VydmljZSB7XHJcbiAgICB2YXIgdXNlckRvY291bWVudHMgPSAkcmVzb3VyY2U8RG9jdW1lbnRMaXN0SXRlbVtdPignYXBpL1VzZXJEb2N1bWVudHMvOmRvY3VtZW50SWQnKTtcclxuICAgIGZ1bmN0aW9uIGdldGJ5SWQoZG9jdW1lbnRJZCkge1xyXG4gICAgICAgIHJldHVybiB1c2VyRG9jb3VtZW50cy5nZXQoeyBkb2N1bWVudElkIH0pO1xyXG4gICAgfVxyXG4gICAgZnVuY3Rpb24gZGVsZXRlQnlJZChkb2N1bWVudElkKSB7XHJcbiAgICAgICAgcmV0dXJuIHVzZXJEb2NvdW1lbnRzLmRlbGV0ZSh7IGRvY3VtZW50SWQgfSk7XHJcbiAgICB9XHJcbiAgICByZXR1cm4ge1xyXG4gICAgICAgIGRlbGV0ZUJ5SWQsXHJcbiAgICAgICAgZ2V0YnlJZFxyXG4gICAgfTtcclxufSBcclxuIiwiJ3VzZSBzdHJpY3QnO1xyXG5cclxudHlwZSBDb250ZXh0TWVudSA9IGFuZ3VsYXIudWkuYm9vdHN0cmFwLmNvbnRleHRNZW51LkNvbnRleHRNZW51O1xyXG5cclxubGV4aWNhbE1lbnVCdWlsZGVyLiRpbmplY3QgPSBbXTtcclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBsZXhpY2FsTWVudUJ1aWxkZXIoKTogTGV4aWNhbE1lbnVCdWlsZGVyRmFjdG9yeSB7XHJcbiAgICB2YXIgW2J1aWxkRm9yVmVyYmFsLCBidWlsZEZvclJlZmVyZW5jZXJdID0gW2NyZWF0ZUZvclZlcmJhbE1lbnVCdWlsZGVyKHt9KSwgY3JlYXRlRm9yUmVmZXJlbmNlck1lbnVCdWlsZGVyKHt9KV07XHJcblxyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICBidWlsZEFuZ3VsYXJNZW51OiBzb3VyY2UgPT5cclxuICAgICAgICAgICAgcmVmZXJlbmNlck1lbnVJc1ZpYWJsZSg8UmVmZXJlbmNlckNvbnRleHRtZW51RGF0YVNvdXJjZT5zb3VyY2UpID9cclxuICAgICAgICAgICAgICAgIGJ1aWxkRm9yUmVmZXJlbmNlcig8UmVmZXJlbmNlckNvbnRleHRtZW51RGF0YVNvdXJjZT5zb3VyY2UpIDpcclxuICAgICAgICAgICAgICAgIHZlcmJhbE1lbnVJc1ZpYWJsZSg8VmVyYmFsQ29udGV4dG1lbnVEYXRhU291cmNlPnNvdXJjZSkgP1xyXG4gICAgICAgICAgICAgICAgICAgIGJ1aWxkRm9yVmVyYmFsKDxWZXJiYWxDb250ZXh0bWVudURhdGFTb3VyY2U+c291cmNlKSA6XHJcbiAgICAgICAgICAgICAgICAgICAgdW5kZWZpbmVkXHJcbiAgICB9O1xyXG5cclxuICAgIGZ1bmN0aW9uIHZlcmJhbE1lbnVJc1ZpYWJsZShtZW51OiBWZXJiYWxDb250ZXh0bWVudURhdGFTb3VyY2UpIHtcclxuICAgICAgICByZXR1cm4gISEobWVudSAmJiAobWVudS5kaXJlY3RPYmplY3RJZHMgfHwgbWVudS5pbmRpcmVjdE9iamVjdElkcyB8fCBtZW51LnN1YmplY3RJZHMpKTtcclxuICAgIH1cclxuICAgIGZ1bmN0aW9uIHJlZmVyZW5jZXJNZW51SXNWaWFibGUobWVudTogUmVmZXJlbmNlckNvbnRleHRtZW51RGF0YVNvdXJjZSkge1xyXG4gICAgICAgIHJldHVybiAhIShtZW51ICYmIG1lbnUucmVmZXJzVG9JZHMpO1xyXG4gICAgfVxyXG5cclxufVxyXG5mdW5jdGlvbiBjcmVhdGVGb3JSZWZlcmVuY2VyTWVudUJ1aWxkZXIobWVudUFjdGlvblRhcmdldHM6IHsgW2lkOiBzdHJpbmddOiBKUXVlcnkgfSkge1xyXG4gICAgdmFyIHJlc2V0UmVmZXJlbmNlckFzc3NvdGlvbkNzc0NsYXNzZXMgPSAoKSA9PlxyXG4gICAgICAgIE9iamVjdC5rZXlzKG1lbnVBY3Rpb25UYXJnZXRzKVxyXG4gICAgICAgICAgICAubWFwKGtleSA9PiBtZW51QWN0aW9uVGFyZ2V0c1trZXldKVxyXG4gICAgICAgICAgICAuZm9yRWFjaCgkZSA9PiAkZS5yZW1vdmVDbGFzcygncmVmZXJyZWQtdG8tYnktY3VycmVudCcpKTtcclxuICAgIHJldHVybiAoc291cmNlOiBSZWZlcmVuY2VyQ29udGV4dG1lbnVEYXRhU291cmNlKTogQ29udGV4dE1lbnUgPT4gW1xyXG4gICAgICAgIFsnVmlldyBSZWZlcnJlZCBUbycsIChpdGVtU2NvcGUsIGV2ZW50KSA9PiB7XHJcbiAgICAgICAgICAgIHJlc2V0UmVmZXJlbmNlckFzc3NvdGlvbkNzc0NsYXNzZXMoKTtcclxuICAgICAgICAgICAgc291cmNlLnJlZmVyc1RvSWRzLmZvckVhY2goaWQgPT4gbWVudUFjdGlvblRhcmdldHNbaWRdID0gJCgnIycgKyBpZCkuYWRkQ2xhc3MoJ3JlZmVycmVkLXRvLWJ5LWN1cnJlbnQnKSk7XHJcbiAgICAgICAgfV1cclxuICAgIF07XHJcbn1cclxuZnVuY3Rpb24gY3JlYXRlRm9yVmVyYmFsTWVudUJ1aWxkZXIobWVudUFjdGlvblRhcmdldHM6IHsgW2lkOiBzdHJpbmddOiBKUXVlcnkgfSkge1xyXG4gICAgcmV0dXJuIChmdW5jdGlvbiAodmVyYmFsTWVudUNzc0NsYXNzTWFwOiB7IFttYXBwaW5nOiBzdHJpbmddOiBzdHJpbmcgfSkge1xyXG4gICAgICAgIHJldHVybiAoc291cmNlOiBWZXJiYWxDb250ZXh0bWVudURhdGFTb3VyY2UpID0+IHtcclxuICAgICAgICAgICAgdmFyIG1lbnVJdGVtczogQ29udGV4dE1lbnUgPSBbXTtcclxuICAgICAgICAgICAgaWYgKHNvdXJjZS5zdWJqZWN0SWRzKSB7XHJcbiAgICAgICAgICAgICAgICBtZW51SXRlbXMucHVzaChbJ1ZpZXcgU3ViamVjdHMnLCAoaXRlbVNjb3BlLCBldmVudCkgPT4ge1xyXG4gICAgICAgICAgICAgICAgICAgIHJlc2V0VmVyYmFsQXNzb2NpYXRpb25Dc3NDbGFzc2VzKCk7XHJcbiAgICAgICAgICAgICAgICAgICAgc291cmNlLnN1YmplY3RJZHNcclxuICAgICAgICAgICAgICAgICAgICAgICAgLmZvckVhY2goaWQgPT4ge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgbWVudUFjdGlvblRhcmdldHNbaWRdID0gJCgnIycgKyBpZCkuYWRkQ2xhc3ModmVyYmFsTWVudUNzc0NsYXNzTWFwWydWaWV3IFN1YmplY3RzJ10pO1xyXG4gICAgICAgICAgICAgICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgICAgIH1dKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICBpZiAoc291cmNlLmRpcmVjdE9iamVjdElkcykge1xyXG4gICAgICAgICAgICAgICAgbWVudUl0ZW1zLnB1c2goWydWaWV3IERpcmVjdCBPYmplY3RzJywgKGl0ZW1TY29wZSwgZXZlbnQpID0+IHtcclxuICAgICAgICAgICAgICAgICAgICByZXNldFZlcmJhbEFzc29jaWF0aW9uQ3NzQ2xhc3NlcygpO1xyXG4gICAgICAgICAgICAgICAgICAgIHNvdXJjZS5kaXJlY3RPYmplY3RJZHNcclxuICAgICAgICAgICAgICAgICAgICAgICAgLmZvckVhY2goaWQgPT4gbWVudUFjdGlvblRhcmdldHNbaWRdID0gJCgnIycgKyBpZCkuYWRkQ2xhc3ModmVyYmFsTWVudUNzc0NsYXNzTWFwWydWaWV3IERpcmVjdCBPYmplY3RzJ10pKTtcclxuICAgICAgICAgICAgICAgIH1dKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICBpZiAoc291cmNlLmluZGlyZWN0T2JqZWN0SWRzKSB7XHJcbiAgICAgICAgICAgICAgICBtZW51SXRlbXMucHVzaChbJ1ZpZXcgSW5kaXJlY3QgT2JqZWN0cycsIChpdGVtU2NvcGUsIGV2ZW50KSA9PiB7XHJcbiAgICAgICAgICAgICAgICAgICAgcmVzZXRWZXJiYWxBc3NvY2lhdGlvbkNzc0NsYXNzZXMoKTtcclxuICAgICAgICAgICAgICAgICAgICBzb3VyY2UuaW5kaXJlY3RPYmplY3RJZHMuZm9yRWFjaChpZCA9PiB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIG1lbnVBY3Rpb25UYXJnZXRzW2lkXSA9ICQoJyMnICsgaWQpLmFkZENsYXNzKHZlcmJhbE1lbnVDc3NDbGFzc01hcFsnVmlldyBJbmRpcmVjdCBPYmplY3RzJ10pO1xyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgfV0pO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIHJldHVybiBtZW51SXRlbXM7XHJcbiAgICAgICAgfTtcclxuICAgICAgICBmdW5jdGlvbiByZXNldFZlcmJhbEFzc29jaWF0aW9uQ3NzQ2xhc3NlcygpIHtcclxuICAgICAgICAgICAgT2JqZWN0LmtleXMobWVudUFjdGlvblRhcmdldHMpXHJcbiAgICAgICAgICAgICAgICAubWFwKGtleSA9PiBtZW51QWN0aW9uVGFyZ2V0c1trZXldKVxyXG4gICAgICAgICAgICAgICAgLmZvckVhY2goJGUgPT5cclxuICAgICAgICAgICAgICAgICAgICBPYmplY3Qua2V5cyh2ZXJiYWxNZW51Q3NzQ2xhc3NNYXApXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIC5tYXAoKGs6IHN0cmluZyk6IHN0cmluZyA9PiB2ZXJiYWxNZW51Q3NzQ2xhc3NNYXBba10pXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIC5mb3JFYWNoKGNzc0NsYXNzID0+ICRlLnJlbW92ZUNsYXNzKGNzc0NsYXNzKSkpO1xyXG4gICAgICAgIH1cclxuICAgIH0pKHtcclxuICAgICAgICAnVmlldyBTdWJqZWN0cyc6ICdzdWJqZWN0LW9mLWN1cnJlbnQnLFxyXG4gICAgICAgICdWaWV3IERpcmVjdCBPYmplY3RzJzogJ2RpcmVjdC1vYmplY3Qtb2YtY3VycmVudCcsXHJcbiAgICAgICAgJ1ZpZXcgSW5kaXJlY3QgT2JqZWN0cyc6ICdpbmRpcmVjdC1vYmplY3Qtb2YtY3VycmVudCdcclxuICAgIH0pO1xyXG59XHJcbmV4cG9ydCBpbnRlcmZhY2UgTGV4aWNhbE1lbnVCdWlsZGVyRmFjdG9yeSB7XHJcbiAgICBidWlsZEFuZ3VsYXJNZW51OiAoc291cmNlOiBMZXhpY2FsQ29udGV4dE1lbnVEYXRhU291cmNlIHwgVmVyYmFsQ29udGV4dG1lbnVEYXRhU291cmNlIHwgUmVmZXJlbmNlckNvbnRleHRtZW51RGF0YVNvdXJjZSkgPT4gQ29udGV4dE1lbnU7XHJcbn1cclxuXHJcbmV4cG9ydCBpbnRlcmZhY2UgTGV4aWNhbENvbnRleHRNZW51RGF0YVNvdXJjZSB7XHJcbiAgICAvKipcclxuICAgICogVGhlIGlkIG9mIHRoZSBsZXhpY2FsIGVsZW1lbnQgZm9yIHdoaWNoIHRoZSBtZW51IGlzIGRlZmluZWQuXHJcbiAgICAqL1xyXG4gICAgbGV4aWNhbElkOiBzdHJpbmcgfCBudW1iZXI7XHJcbn1cclxuZXhwb3J0IGludGVyZmFjZSBWZXJiYWxDb250ZXh0bWVudURhdGFTb3VyY2UgZXh0ZW5kcyBMZXhpY2FsQ29udGV4dE1lbnVEYXRhU291cmNlIHtcclxuICAgICAgIFxyXG4gICAgLyoqXHJcbiAgICAgKiBUaGUgaWRzIG9mIGFueSBzdWJqZWN0cy5cclxuICAgICAqL1xyXG4gICAgc3ViamVjdElkczogbnVtYmVyW107XHJcbiAgICAvKipcclxuICAgICAqIFRoZSBpZHMgb2YgYW55IGRpcmVjdCBvYmplY3RzLlxyXG4gICAgICovXHJcbiAgICBkaXJlY3RPYmplY3RJZHM6IG51bWJlcltdO1xyXG4gICAgLyoqXHJcbiAgICAgKiBUaGUgaWRzIG9mIGFueSBkaXJlY3Qgb2JqZWN0cy5cclxuICAgICAqL1xyXG4gICAgaW5kaXJlY3RPYmplY3RJZHM6IG51bWJlcltdO1xyXG59XHJcblxyXG5leHBvcnQgaW50ZXJmYWNlIFJlZmVyZW5jZXJDb250ZXh0bWVudURhdGFTb3VyY2UgZXh0ZW5kcyBMZXhpY2FsQ29udGV4dE1lbnVEYXRhU291cmNlIHtcclxuICAgIC8qKlxyXG4gICAgICogVGhlIGlkIG9mIHRoZSByZWZlcmVuY2VyIGZvciB3aGljaCB0aGUgbWVudSBpcyBkZWZpbmVkLlxyXG4gICAgICovXHJcbiAgICBsZXhpY2FsSWQ6IG51bWJlcjtcclxuICAgIC8qKlxyXG4gICAgICogVGhlIGlkcyBvZiBhbnkgZW50aXRpZXMgdGhlIHJlZmVycmVkIHRvLlxyXG4gICAgICovXHJcbiAgICByZWZlcnNUb0lkczogbnVtYmVyW107XHJcbn0iLG51bGwsIid1c2Ugc3RyaWN0JztcclxuaW1wb3J0IHsgRG9jdW1lbnRNb2RlbCB9IGZyb20gJ2FwcC9tb2RlbHMnO1xyXG5pbXBvcnQgeyBUYXNrIH0gZnJvbSAndGFza3MtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyJztcclxuaW1wb3J0IHsgYnVpbGRNZW51cywgZW5hYmxlQWN0aXZlSGlnaGxpZ2h0aW5nIH0gZnJvbSAnYXBwL0xBU0knO1xyXG5cclxucmVzdWx0c1NlcnZpY2UuJGluamVjdCA9IFsnJGh0dHAnLCAnJHEnXTtcclxuXHJcbmludGVyZmFjZSBSZXN1bHRzU2VydmljZSB7XHJcbiAgICB0YXNrczogVGFza1tdO1xyXG4gICAgcHJvY2Vzc0RvY3VtZW50KGRvY3VtZW50SWQ6IHN0cmluZywgZG9jdW1lbnROYW1lOiBzdHJpbmcpOiBhbmd1bGFyLklQcm9taXNlPERvY3VtZW50TW9kZWw+O1xyXG59XHJcbmZ1bmN0aW9uIHJlc3VsdHNTZXJ2aWNlKCRodHRwOiBhbmd1bGFyLklIdHRwU2VydmljZSwgJHE6IGFuZ3VsYXIuSVFTZXJ2aWNlKTogUmVzdWx0c1NlcnZpY2Uge1xyXG4gICAgdmFyIHRhc2tzID0gW107XHJcbiAgICB2YXIgcHJvY2Vzc0RvY3VtZW50ID0gZnVuY3Rpb24gKGRvY3VtZW50SWQsIGRvY3VtZW50TmFtZSk6IGFuZ3VsYXIuSVByb21pc2U8RG9jdW1lbnRNb2RlbD4ge1xyXG4gICAgICAgIHRhc2tzW2RvY3VtZW50SWRdID0geyBwZXJjZW50Q29tcGxldGU6IDAgfTtcclxuXHJcbiAgICAgICAgdmFyIGRlZmVycmVkID0gJHEuZGVmZXI8RG9jdW1lbnRNb2RlbD4oKTtcclxuICAgICAgICAkaHR0cC5nZXQ8RG9jdW1lbnRNb2RlbD4oJ0FuYWx5c2lzLycgKyBkb2N1bWVudElkKVxyXG4gICAgICAgICAgICAuc3VjY2VzcyhzdWNjZXNzKVxyXG4gICAgICAgICAgICAuZXJyb3IoZXJyb3IpO1xyXG4gICAgICAgIHJldHVybiBkZWZlcnJlZC5wcm9taXNlO1xyXG5cclxuICAgICAgICBmdW5jdGlvbiBzdWNjZXNzKGRhdGEpIHtcclxuICAgICAgICAgICAgdmFyIG1hcmt1cEhlYWRlciA9ICQoJzxkaXYgY2xhc3M9XCJwYW5lbCBwYW5lbC1kZWZhdWx0XCI+JyArXHJcbiAgICAgICAgICAgICAgICAnPGRpdiBjbGFzcz1cInBhbmVsLWhlYWRpbmdcIj48aDQgY2xhc3M9XCJwYW5lbC10aXRsZVwiPjxhIGhyZWY9XCIjJyArXHJcbiAgICAgICAgICAgICAgICBkb2N1bWVudElkICtcclxuICAgICAgICAgICAgICAgICdcIiBkYXRhLXRvZ2dsZT1cImNvbGxhcHNlXCIgZGF0YS1wYXJlbnQ9XCIjYWNjb3JkaW9uXCI+JyArXHJcbiAgICAgICAgICAgICAgICBkb2N1bWVudE5hbWUgK1xyXG4gICAgICAgICAgICAgICAgJzwvYT48L2g0PjwvZGl2PjwvZGl2PicpO1xyXG4gICAgICAgICAgICB2YXIgbWFya3VwUGFuZWwgPSAkKCc8ZGl2IGlkPVwiJyArIGRvY3VtZW50SWQgKyAnXCIgY2xhc3M9XCJwYW5lbC1jb2xsYXBzZSBjb2xsYXBzZSBpblwiPicgK1xyXG4gICAgICAgICAgICAgICAgSlNPTi5zdHJpbmdpZnkoZGF0YSkgKyAnPC9kaXY+JyArICc8L2Rpdj4nKTtcclxuICAgICAgICAgICAgaWYgKCEkKCcjJyArIGRvY3VtZW50SWQpLmxlbmd0aCkge1xyXG4gICAgICAgICAgICAgICAgJCgnI2FjY29yZGlvbicpLmFwcGVuZChtYXJrdXBIZWFkZXIpLmFwcGVuZChtYXJrdXBQYW5lbCk7XHJcbiAgICAgICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgICAgICAkKCcjJyArIGRvY3VtZW50SWQpLnJlbW92ZSgpO1xyXG4gICAgICAgICAgICAgICAgJCgnI2FjY29yZGlvbicpLmFwcGVuZChtYXJrdXBQYW5lbCk7XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgYnVpbGRNZW51cygpO1xyXG4gICAgICAgICAgICBlbmFibGVBY3RpdmVIaWdobGlnaHRpbmcoKTtcclxuICAgICAgICAgICAgdGFza3NbZG9jdW1lbnRJZF0ucGVyY2VudENvbXBsZXRlID0gMTAwO1xyXG4gICAgICAgICAgICBkZWZlcnJlZC5yZXNvbHZlKGRhdGEpO1xyXG4gICAgICAgICAgICBhbGVydChKU09OLnN0cmluZ2lmeShkYXRhKSk7XHJcbiAgICAgICAgfVxyXG4gICAgICAgIGZ1bmN0aW9uIGVycm9yKHhociwgbWVzc2FnZSwgZGV0YWlsKSB7XHJcbiAgICAgICAgICAgIGRlZmVycmVkLnJlamVjdChtZXNzYWdlKTtcclxuICAgICAgICB9XHJcbiAgICB9O1xyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICB0YXNrcyxcclxuICAgICAgICBwcm9jZXNzRG9jdW1lbnRcclxuICAgIH07XHJcbn1cclxuZXhwb3J0IHsgcmVzdWx0c1NlcnZpY2UgfSIsIid1c2Ugc3RyaWN0JztcclxuaW1wb3J0IHsgRG9jdW1lbnRNb2RlbCB9IGZyb20gJ2FwcC9tb2RlbHMnO1xyXG5leHBvcnQgaW50ZXJmYWNlIERvY3VtZW50TW9kZWxTZXJ2aWNlIHtcclxuICAgIHByb2Nlc3NEb2N1bWVudChkb2N1bWVudElkOiBzdHJpbmcpOiBhbmd1bGFyLklIdHRwUHJvbWlzZTxEb2N1bWVudE1vZGVsPjtcclxufVxyXG5kb2N1bWVudE1vZGVsU2VydmljZS4kaW5qZWN0ID0gWyckaHR0cCddO1xyXG5leHBvcnQgZnVuY3Rpb24gZG9jdW1lbnRNb2RlbFNlcnZpY2UoJGh0dHA6IGFuZ3VsYXIuSUh0dHBTZXJ2aWNlKTogRG9jdW1lbnRNb2RlbFNlcnZpY2Uge1xyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICBwcm9jZXNzRG9jdW1lbnQoZG9jdW1lbnRJZCkge1xyXG4gICAgICAgICAgICByZXR1cm4gJGh0dHAuZ2V0KGBBbmFseXNpcy8ke2RvY3VtZW50SWR9YCwgeyBjYWNoZTogZmFsc2UgfSlcclxuICAgICAgICB9XHJcbiAgICB9O1xyXG59IiwiJ3VzZSBzdHJpY3QnO1xyXG5pbXBvcnQgeyBEb2N1bWVudE1vZGVsU2VydmljZSB9IGZyb20gJy4vLi4vZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LW1vZGVsLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyBEb2N1bWVudE1vZGVsIH0gZnJvbSAnYXBwL21vZGVscyc7XHJcbmV4cG9ydCBpbnRlcmZhY2UgRG9jdW1lbnRMaXN0U2VydmljZUNvbmZpZyB7XHJcbiAgICBzZXRSZWNlbnREb2N1bWVudENvdW50KGNvdW50OiBudW1iZXIpOiBEb2N1bWVudExpc3RTZXJ2aWNlQ29uZmlnO1xyXG4gICAgc2V0RG9jdW1lbnRMaXN0VXJsKHVybDogc3RyaW5nKTogRG9jdW1lbnRMaXN0U2VydmljZUNvbmZpZztcclxufVxyXG5leHBvcnQgaW50ZXJmYWNlIERvY3VtZW50TGlzdFNlcnZpY2Uge1xyXG4gICAgZ2V0KCk6IERvY3VtZW50TGlzdEl0ZW1Nb2RlbFtdO1xyXG4gICAgZGVsZXRlRG9jdW1lbnQoZG9jdW1lbnRJZDogc3RyaW5nKTogRG9jdW1lbnRMaXN0SXRlbU1vZGVsO1xyXG59XHJcbmV4cG9ydCBpbnRlcmZhY2UgRG9jdW1lbnRMaXN0SXRlbU1vZGVsIHtcclxuICAgIGlkOiBzdHJpbmc7XHJcbiAgICBuYW1lOiBzdHJpbmc7XHJcbiAgICBwcm9ncmVzczogbnVtYmVyO1xyXG4gICAgcGVyY2VudENvbXBsZXRlOiBudW1iZXI7XHJcbiAgICBzaG93UHJvZ3Jlc3M6IGJvb2xlYW47XHJcbiAgICBzdGF0dXNNZXNzYWdlOiBzdHJpbmc7XHJcbiAgICByYWVpZmljYXRpb246IERvY3VtZW50TW9kZWw7XHJcbn1cclxuZXhwb3J0IGNsYXNzIERvY3VtZW50TGlzdFNlcnZpY2VQcm92aWRlciBpbXBsZW1lbnRzIERvY3VtZW50TGlzdFNlcnZpY2VDb25maWcsIGFuZ3VsYXIuSVNlcnZpY2VQcm92aWRlciB7XHJcbiAgICBwcml2YXRlIGRvY3VtZW50TGlzdFVybDogc3RyaW5nO1xyXG4gICAgcHJpdmF0ZSByZWNlbnREb2N1bWVudENvdW50OiBudW1iZXI7XHJcbiAgICBjb25zdHJ1Y3RvcigpIHtcclxuICAgICAgICB0aGlzLiRnZXQuJGluamVjdCA9IFsnJHJlc291cmNlJ107XHJcbiAgICB9XHJcbiAgICBzZXREb2N1bWVudExpc3RVcmwodXJsOiBzdHJpbmcpIHtcclxuICAgICAgICB0aGlzLmRvY3VtZW50TGlzdFVybCA9IHVybDtcclxuICAgICAgICByZXR1cm4gdGhpcztcclxuICAgIH1cclxuICAgIHNldFJlY2VudERvY3VtZW50Q291bnQoY291bnQ6IG51bWJlcikge1xyXG4gICAgICAgIHRoaXMucmVjZW50RG9jdW1lbnRDb3VudCA9IGNvdW50O1xyXG4gICAgICAgIHJldHVybiB0aGlzO1xyXG4gICAgfVxyXG5cclxuICAgICRpbmplY3QgPSBbJyRyZXNvdXJjZSddO1xyXG4gICAgLyoqXHJcbiAgICAgKiBAcGFyYW0gJHJlc291cmNlIGFuIGluc3RhbmNlIG9mIHRoZSBSZXNvdXJjZSBTZXJ2aWNlIHN1cHBsaWVkIGJ5IHRoZSBhbmd1bGFyLXJlc291cmNlIG1vZHVsZS5cclxuICAgICAqL1xyXG4gICAgJGdldCgkcmVzb3VyY2U6IGFuZ3VsYXIucmVzb3VyY2UuSVJlc291cmNlU2VydmljZSk6IERvY3VtZW50TGlzdFNlcnZpY2Uge1xyXG4gICAgICAgIHZhciByZXNvdXJjZSA9ICRyZXNvdXJjZTxEb2N1bWVudExpc3RJdGVtTW9kZWxbXT4odGhpcy5kb2N1bWVudExpc3RVcmwgKyAnP2xpbWl0PScgKyB0aGlzLnJlY2VudERvY3VtZW50Q291bnQsIHt9LCB7XHJcbiAgICAgICAgICAgIGdldDoge1xyXG4gICAgICAgICAgICAgICAgbWV0aG9kOiAnR0VUJyxcclxuICAgICAgICAgICAgICAgIGlzQXJyYXk6IHRydWVcclxuICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgZGVsZXRlOiB7XHJcbiAgICAgICAgICAgICAgICBtZXRob2Q6ICdERUxFVEUnLFxyXG4gICAgICAgICAgICAgICAgaXNBcnJheTogZmFsc2VcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICBkZWxldGVEb2N1bWVudDogZnVuY3Rpb24gKGRvY3VtZW50SWQ6IHN0cmluZykge1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIHJlc291cmNlLmRlbGV0ZSh7IGRvY3VtZW50SWQgfSlbMF07XHJcbiAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgIGdldDogcmVzb3VyY2UuZ2V0XHJcbiAgICAgICAgfTtcclxuICAgIH07XHJcbn0iLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCB7IERvY3VtZW50TGlzdEl0ZW1Nb2RlbCB9IGZyb20gJ2RvY3VtZW50LWxpc3Qtc2VydmljZS5wcm92aWRlcic7XHJcbmltcG9ydCBuZ2YgPSBhbmd1bGFyLmFuZ3VsYXJGaWxlVXBsb2FkO1xyXG52YXIgbG9nID0gY29uc29sZS5sb2cuYmluZChjb25zb2xlKTtcclxuZXhwb3J0IGNsYXNzIFVwbG9hZENvbnRyb2xsZXIge1xyXG4gICAgc3RhdGljICRpbmplY3QgPSBbJyRzY29wZScsICckcScsICdVcGxvYWQnXTtcclxuXHJcbiAgICBjb25zdHJ1Y3Rvcihwcml2YXRlICRzY29wZTogYW5ndWxhci5JU2NvcGUsXHJcbiAgICAgICAgcHJpdmF0ZSAkcTogbmcuSVFTZXJ2aWNlLFxyXG4gICAgICAgIHByaXZhdGUgdXBsb2FkU2VydmljZTogbmdmLklVcGxvYWRTZXJ2aWNlKSB7XHJcbiAgICAgICAgdGhpcy4kc2NvcGUuJHdhdGNoKCd1cGxvYWQuZmlsZXMnLCB0aGlzLnVwbG9hZEZpbGVzLmJpbmQodGhpcykpO1xyXG4gICAgfVxyXG5cclxuICAgIHVwbG9hZEZpbGVzKCkge1xyXG4gICAgICAgIHRoaXMubG9nSW52YWxpZEZpbGVzKCk7XHJcbiAgICAgICAgdmFyIGZpbGVzID0gdGhpcy5maWxlcztcclxuICAgICAgICByZXR1cm4gdGhpcy4kcS53aGVuKChBcnJheS5pc0FycmF5KGZpbGVzKSA/IGZpbGVzIDogW2ZpbGVzXSkubWFwKGZpbGUgPT4gdGhpcy51cGxvYWRGaWxlKGZpbGUpKSk7XHJcbiAgICB9XHJcbiAgICBsb2dJbnZhbGlkRmlsZXMoKSB7XHJcbiAgICAgICAgdmFyIGZpbGVzID0gdGhpcy5maWxlcztcclxuICAgICAgICAoQXJyYXkuaXNBcnJheShmaWxlcykgPyBmaWxlcyA6IFtmaWxlc10pLmZpbHRlcihmaWxlID0+IFVwbG9hZENvbnRyb2xsZXIuZm9ybWF0cy5ldmVyeShmb3JtYXQgPT4gZmlsZS50eXBlLmxvY2FsZUNvbXBhcmUoZm9ybWF0KSAhPT0gMCkpXHJcbiAgICAgICAgICAgIC5tYXAoZmlsZSA9PiBgRmlsZSAke2ZpbGUubmFtZX0gaGFzIHVuYWNjZXB0ZWQgZm9ybWF0ICR7ZmlsZS50eXBlfWApXHJcbiAgICAgICAgICAgIC5mb3JFYWNoKGxvZyk7XHJcbiAgICB9XHJcbiAgICB1cGxvYWRGaWxlKGZpbGU6IEZpbGUpIHtcclxuICAgICAgICByZXR1cm4gdGhpcy51cGxvYWRTZXJ2aWNlXHJcbiAgICAgICAgICAgIC51cGxvYWQ8RG9jdW1lbnRMaXN0SXRlbU1vZGVsPih7XHJcbiAgICAgICAgICAgICAgICBmaWxlLFxyXG4gICAgICAgICAgICAgICAgdXJsOiAnYXBpL1VzZXJEb2N1bWVudHMnLFxyXG4gICAgICAgICAgICAgICAgbWV0aG9kOiAnUE9TVCcsXHJcbiAgICAgICAgICAgICAgICBmaWxlTmFtZTogZmlsZS5uYW1lXHJcbiAgICAgICAgICAgIH0pXHJcbiAgICAgICAgICAgIC5wcm9ncmVzcyhkYXRhID0+IGxvZyhgUHJvZ3Jlc3M6ICR7MTAwLjAgKiBkYXRhLnByb2dyZXNzIC8gZGF0YS5wZXJjZW50Q29tcGxldGV9JSAke2ZpbGUubmFtZX1gKSlcclxuICAgICAgICAgICAgLnN1Y2Nlc3MoZGF0YSA9PiBsb2coYEZpbGUgJHtmaWxlLm5hbWV9IHVwbG9hZGVkLiBcXG5SZXNwb25zZTogJHtKU09OLnN0cmluZ2lmeShkYXRhKSB9YCkpXHJcbiAgICAgICAgICAgIC5lcnJvcigoZGF0YSwgc3RhdHVzKSA9PiBsb2coYEh0dHA6ICR7c3RhdHVzfSBGYWlsZWQgdG8gdXBsb2FkIGZpbGUuIFxcbkRldGFpbHM6ICR7ZGF0YX1gKSk7XHJcbiAgICB9XHJcblxyXG4gICAgcmVtb3ZlRmlsZShmaWxlOiBGaWxlLCBpbmRleDogbnVtYmVyKSB7XHJcbiAgICAgICAgdmFyIGZpbGVzID0gdGhpcy5maWxlcztcclxuICAgICAgICB0aGlzLmZpbGVzID0gKEFycmF5LmlzQXJyYXkoZmlsZXMpID8gZmlsZXMgOiBbZmlsZXNdKS5maWx0ZXIoZiA9PiBmLm5hbWUgIT09IGZpbGUubmFtZSk7XHJcbiAgICAgICAgJCgnI2ZpbGUtdXBsb2FkLWxpc3QnKS5yZW1vdmUoYCN1cGxvYWQtbGlzdC1pdGVtLSR7aW5kZXh9YCk7XHJcbiAgICB9XHJcblxyXG4gICAgZmlsZXM6IEZpbGUgfCBGaWxlW10gPSBbXTtcclxuXHJcbiAgICBwcml2YXRlIHN0YXRpYyBmb3JtYXRzID0gW1xyXG4gICAgICAgICdhcHBsaWNhdGlvbi92bmQub3BlbnhtbGZvcm1hdHMtb2ZmaWNlZG9jdW1lbnQud29yZHByb2Nlc3NpbmdtbC5kb2N1bWVudCcsXHJcbiAgICAgICAgJ2FwcGxpY2F0aW9uL21zd29yZCcsXHJcbiAgICAgICAgJ2FwcGxpY2F0aW9uL3BkZicsXHJcbiAgICAgICAgJ3RleHQvcGxhaW4nXHJcbiAgICBdO1xyXG5cclxufSAiLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCB7IERvY3VtZW50TGlzdEl0ZW1Nb2RlbCwgRG9jdW1lbnRMaXN0U2VydmljZSB9IGZyb20gJy4vZG9jdW1lbnQtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyJztcclxuaW1wb3J0IHsgVGFzaywgVGFza3NMaXN0U2VydmljZSB9IGZyb20gJy4vdGFza3MtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyJztcclxuaW1wb3J0IHsgRG9jdW1lbnRzU2VydmljZSB9IGZyb20gJy4vZG9jdW1lbnRzLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyBEb2N1bWVudE1vZGVsU2VydmljZSB9IGZyb20gJ2FwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtbW9kZWwuc2VydmljZSc7XHJcbmV4cG9ydCBjbGFzcyBMaXN0Q29udHJvbGxlciB7XHJcbiAgICBleHBhbmRlZCA9IGZhbHNlO1xyXG4gICAgZG9jdW1lbnRzOiBEb2N1bWVudExpc3RJdGVtTW9kZWxbXSA9IFtdO1xyXG4gICAgdGFza3M6IFRhc2tbXTtcclxuICAgIHN0YXRpYyAkaW5qZWN0ID0gWyckcScsICdkb2N1bWVudExpc3RTZXJ2aWNlJywgJ3Rhc2tzTGlzdFNlcnZpY2UnLCAnZG9jdW1lbnRzU2VydmljZScsICdkb2N1bWVudE1vZGVsU2VydmljZSddO1xyXG4gICAgY29uc3RydWN0b3IoXHJcbiAgICAgICAgcHJpdmF0ZSAkcTogYW5ndWxhci5JUVNlcnZpY2UsXHJcbiAgICAgICAgcHJpdmF0ZSBkb2N1bWVudExpc3RTZXJ2aWNlOiBEb2N1bWVudExpc3RTZXJ2aWNlLFxyXG4gICAgICAgIHByaXZhdGUgdGFza3NMaXN0U2VydmljZTogVGFza3NMaXN0U2VydmljZSxcclxuICAgICAgICBwcml2YXRlIGRvY3VtZW50c1NlcnZpY2U6IERvY3VtZW50c1NlcnZpY2UsXHJcbiAgICAgICAgcHJpdmF0ZSBkb2N1bWVudE1vZGVsU2VydmljZTogRG9jdW1lbnRNb2RlbFNlcnZpY2UpIHtcclxuICAgICAgICB0aGlzLmFjdGl2YXRlKCk7XHJcbiAgICB9XHJcblxyXG4gICAgdG9nZ2xlRXhwYW5kZWQoKSB7XHJcbiAgICAgICAgdGhpcy5leHBhbmRlZCA9ICF0aGlzLmV4cGFuZGVkO1xyXG4gICAgfVxyXG5cclxuICAgIGRlbGV0ZUJ5SWQoaWQ6IHN0cmluZykge1xyXG4gICAgICAgIHZhciBkZWxldGVSZXN1bHQgPSB0aGlzLmRvY3VtZW50c1NlcnZpY2UuZGVsZXRlQnlJZChpZCk7XHJcbiAgICAgICAgY29uc29sZS5sb2coZGVsZXRlUmVzdWx0KTtcclxuICAgICAgICB0aGlzLmRvY3VtZW50cyA9IHRoaXMuZG9jdW1lbnRMaXN0U2VydmljZS5nZXQoKTtcclxuICAgIH1cclxuICAgIGdldCBkb2N1bWVudENvdW50KCkge1xyXG4gICAgICAgIHJldHVybiB0aGlzLmRvY3VtZW50cy5sZW5ndGg7XHJcbiAgICB9XHJcblxyXG4gICAgcHJvY2Vzc0RvY3VtZW50KGRvY3VtZW50OiBEb2N1bWVudExpc3RJdGVtTW9kZWwpIHtcclxuICAgICAgICBpZiAoIXRoaXMuZG9jdW1lbnRzLnNvbWUoZCA9PiBkLnJhZWlmaWNhdGlvbiAmJiBkLmlkID09PSBkb2N1bWVudC5pZCkpIHtcclxuICAgICAgICAgICAgdGhpcy5kb2N1bWVudE1vZGVsU2VydmljZS5wcm9jZXNzRG9jdW1lbnQoZG9jdW1lbnQuaWQpXHJcbiAgICAgICAgICAgICAgICAuc3VjY2Vzcyhwcm9jZXNzZWQgPT4gZG9jdW1lbnQucmFlaWZpY2F0aW9uID0gcHJvY2Vzc2VkKVxyXG4gICAgICAgICAgICAgICAgLmVycm9yKGVycm9yID0+IGNvbnNvbGUubG9nKGVycm9yKSk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG5cclxuICAgIGFjdGl2YXRlKCkge1xyXG4gICAgICAgIHJldHVybiB0aGlzLiRxLmFsbChbXHJcbiAgICAgICAgICAgIHRoaXMuJHEud2hlbih0aGlzLmRvY3VtZW50TGlzdFNlcnZpY2UuZ2V0KCkpLFxyXG4gICAgICAgICAgICB0aGlzLnRhc2tzTGlzdFNlcnZpY2UuZ2V0QWN0aXZlVGFza3MoKS50aGVuKHRhc2tzID0+IHRhc2tzLm1hcCh0YXNrID0+IHtcclxuICAgICAgICAgICAgICAgIHRoaXMudGFza3NbdGFzay5pZF0gPSB0YXNrO1xyXG4gICAgICAgICAgICAgICAgdmFyIHQgPSB0aGlzLmRvY3VtZW50cy5maXJzdChkID0+IGQubmFtZSA9PT0gdGFzay5uYW1lKTtcclxuICAgICAgICAgICAgICAgICh0ICYmICg8YW55PnQpKS50YXNrID0gdGFzaztcclxuICAgICAgICAgICAgICAgIHJldHVybiB0O1xyXG4gICAgICAgICAgICB9KSlcclxuICAgICAgICBdKS50aGVuKGRhdGEgPT4ge1xyXG4gICAgICAgICAgICBsZXQgW2RvY3VtZW50cywgdGFza3NdID0gPFtEb2N1bWVudExpc3RJdGVtTW9kZWxbXSwgVGFza1tdXT5kYXRhO1xyXG4gICAgICAgICAgICBsZXQgYXNzb2NpYXRlZCA9IGRvY3VtZW50cy5jb3JyZWxhdGUodGFza3MsIGRvY3VtZW50ID0+IGRvY3VtZW50LmlkLCB0YXNrID0+IHRhc2suaWQsXHJcbiAgICAgICAgICAgICAgICAoZG9jdW1lbnQsIHRhc2spID0+IHtcclxuICAgICAgICAgICAgICAgICAgICBkb2N1bWVudC5zaG93UHJvZ3Jlc3MgPSB0YXNrLnN0YXRlID09PSAnT25nb2luZycgfHwgdGFzay5zdGF0ZSA9PT0gJ0NvbXBsZXRlJztcclxuICAgICAgICAgICAgICAgICAgICBkb2N1bWVudC5wcm9ncmVzcyA9IE1hdGgucm91bmQodGFzay5wZXJjZW50Q29tcGxldGUpO1xyXG4gICAgICAgICAgICAgICAgICAgIGRvY3VtZW50LnN0YXR1c01lc3NhZ2UgPSB0YXNrLnN0YXR1c01lc3NhZ2U7XHJcbiAgICAgICAgICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgICAgIHRhc2tzLmZvckVhY2godGFzayA9PiB7IHRoaXMudGFza3NbdGFzay5pZF0gPSB0YXNrOyB9KTtcclxuICAgICAgICAgICAgW3RoaXMuZG9jdW1lbnRzLCB0aGlzLnRhc2tzXSA9IFtkb2N1bWVudHMsIHRhc2tzXTtcclxuICAgICAgICB9KTtcclxuXHJcbiAgICB9XHJcbn0iLCIndXNlIHN0cmljdCc7XHJcblxyXG5kb2N1bWVudExpc3RUYWJzZXRJdGVtLiRpbmplY3QgPSBbJ3Jlc3VsdHNTZXJ2aWNlJ107XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZG9jdW1lbnRMaXN0VGFic2V0SXRlbShyZXN1bHRzU2VydmljZSk6IGFuZ3VsYXIuSURpcmVjdGl2ZSB7XHJcblxyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICByZXN0cmljdDogJ0UnLFxyXG4gICAgICAgIGxpbms6IChzY29wZTogRG9jdW1lbnRMaXN0VGFic2V0SXRlbVNjb3BlLCBlbGVtZW50LCBhdHRycykgPT4ge1xyXG4gICAgICAgICAgICBlbGVtZW50LmNsaWNrKGV2ZW50ID0+IHtcclxuICAgICAgICAgICAgICAgIGV2ZW50LnN0b3BQcm9wYWdhdGlvbigpO1xyXG4gICAgICAgICAgICAgICAgcmVzdWx0c1NlcnZpY2UucHJvY2Vzc0RvY3VtZW50KHNjb3BlLmRvY3VtZW50SWQsIHNjb3BlLm5hbWUpOyBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgICAgICAgICAgICAgdmFyIHByb21pc2UgPSByZXN1bHRzU2VydmljZS5wcm9jZXNzRG9jdW1lbnQoc2NvcGUuZG9jdW1lbnRJZCwgc2NvcGUubmFtZSk7XHJcbiAgICAgICAgICAgICAgICBzY29wZS5hbmFseXNpc1Byb2dyZXNzID0gcmVzdWx0c1NlcnZpY2UudGFza3Nbc2NvcGUuZG9jdW1lbnRJZF0ucGVyY2VudENvbXBsZXRlO1xyXG4gICAgICAgICAgICAgICAgc2NvcGUuc2hvd1Byb2dyZXNzID0gdHJ1ZTtcclxuICAgICAgICAgICAgICAgIHByb21pc2UudGhlbigoKSA9PiBzY29wZS5hbmFseXNpc1Byb2dyZXNzID0gcmVzdWx0c1NlcnZpY2UudGFza3Nbc2NvcGUuZG9jdW1lbnRJZF0ucGVyY2VudENvbXBsZXRlKTtcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgIGNvbnNvbGUubG9nKGF0dHJzKTtcclxuICAgICAgICB9LFxyXG4gICAgICAgIHNjb3BlOiB7XHJcbiAgICAgICAgICAgIGRvY3VtZW50SWQ6ICc9JyxcclxuICAgICAgICAgICAgbmFtZTogJz0nLFxyXG4gICAgICAgICAgICBwZXJjZW50Q29tcGxldGU6ICc9J1xyXG4gICAgICAgIH0sXHJcbiAgICAgICAgdGVtcGxhdGU6IHJlcXVpcmUoJy9hcHAvZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LXRhYnNldC1pdGVtLmRpcmVjdGl2ZS5odG1sIScpXHJcbiAgICB9O1xyXG59XHJcblxyXG5pbnRlcmZhY2UgRG9jdW1lbnRMaXN0VGFic2V0SXRlbVNjb3BlIGV4dGVuZHMgYW5ndWxhci5JU2NvcGUge1xyXG4gICAgZG9jdW1lbnRJZDogc3RyaW5nO1xyXG4gICAgbmFtZTogc3RyaW5nO1xyXG4gICAgYW5hbHlzaXNQcm9ncmVzczogYW55O1xyXG4gICAgc2hvd1Byb2dyZXNzOiBib29sZWFuO1xyXG59ICIsIid1c2Ugc3RyaWN0JztcclxuZG9jdW1lbnRMaXN0TWVudUl0ZW0uJGluamVjdCA9IFsnJHdpbmRvdycsICdyZXN1bHRzU2VydmljZSddO1xyXG5cclxuaW50ZXJmYWNlIERvY3VtZW50TGlzdEl0ZW1TY29wZSBleHRlbmRzIGFuZ3VsYXIuSVNjb3BlIHtcclxuICAgIGRvY3VtZW50SWQ6IHN0cmluZztcclxuICAgIG5hbWU6IHN0cmluZztcclxuICAgIGFuYWx5c2lzUHJvZ3Jlc3M6IG51bWJlcjtcclxuICAgIHNob3dQcm9ncmVzczogYm9vbGVhbjtcclxufVxyXG5leHBvcnQgZnVuY3Rpb24gZG9jdW1lbnRMaXN0TWVudUl0ZW0oJHdpbmRvdywgcmVzdWx0c1NlcnZpY2UpOiBhbmd1bGFyLklEaXJlY3RpdmUge1xyXG5cclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgdHJhbnNjbHVkZTogdHJ1ZSxcclxuICAgICAgICByZXBsYWNlOiB0cnVlLFxyXG4gICAgICAgIHJlc3RyaWN0OiAnRScsXHJcbiAgICAgICAgdGVtcGxhdGU6IHJlcXVpcmUoJy9hcHAvZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LW1lbnUtaXRlbS5kaXJlY3RpdmUuaHRtbCEnKSxcclxuICAgICAgICBzY29wZToge1xyXG4gICAgICAgICAgICBuYW1lOiAnPScsXHJcbiAgICAgICAgICAgIGRvY3VtZW50SWQ6ICc9J1xyXG4gICAgICAgIH0sXHJcbiAgICAgICAgbGluazogZnVuY3Rpb24gKHNjb3BlOiBEb2N1bWVudExpc3RJdGVtU2NvcGUsIGVsZW1lbnQ6IEpRdWVyeSwgYXR0cnM6IGFuZ3VsYXIuSUF0dHJpYnV0ZXMpIHtcclxuICAgICAgICAgICAgZWxlbWVudC5jbGljayhmdW5jdGlvbiAoZXZlbnQpIHtcclxuICAgICAgICAgICAgICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbiAgICAgICAgICAgICAgICBldmVudC5zdG9wUHJvcGFnYXRpb24oKTtcclxuICAgICAgICAgICAgICAgIHZhciBwcm9taXNlID0gcmVzdWx0c1NlcnZpY2UucHJvY2Vzc0RvY3VtZW50KHNjb3BlLmRvY3VtZW50SWQsIHNjb3BlLm5hbWUpO1xyXG4gICAgICAgICAgICAgICAgc2NvcGUuYW5hbHlzaXNQcm9ncmVzcyA9IHJlc3VsdHNTZXJ2aWNlLnRhc2tzW3Njb3BlLmRvY3VtZW50SWRdLnBlcmNlbnRDb21wbGV0ZTtcclxuICAgICAgICAgICAgICAgIHNjb3BlLnNob3dQcm9ncmVzcyA9IHRydWU7XHJcbiAgICAgICAgICAgICAgICBwcm9taXNlLnRoZW4oZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICAgICAgICAgIHNjb3BlLmFuYWx5c2lzUHJvZ3Jlc3MgPSByZXN1bHRzU2VydmljZS50YXNrc1tzY29wZS5kb2N1bWVudElkXS5wZXJjZW50Q29tcGxldGU7XHJcbiAgICAgICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIH1cclxuICAgIH07XHJcbn0gIiwiJ3VzZSBzdHJpY3QnO1xyXG5cclxuaW1wb3J0IHsgZG9jdW1lbnRzU2VydmljZSB9IGZyb20gJy4vZG9jdW1lbnRzLnNlcnZpY2UnO1xyXG5pbXBvcnQgeyByZXN1bHRzU2VydmljZSB9IGZyb20gJy4vcmVzdWx0cy5zZXJ2aWNlJztcclxuaW1wb3J0IHsgRG9jdW1lbnRMaXN0U2VydmljZUNvbmZpZywgRG9jdW1lbnRMaXN0U2VydmljZVByb3ZpZGVyIH0gZnJvbSAnLi9kb2N1bWVudC1saXN0LXNlcnZpY2UucHJvdmlkZXInO1xyXG5pbXBvcnQgeyBUYXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIsIHRhc2tzTGlzdFNlcnZpY2VQcm92aWRlciB9IGZyb20gJy4vdGFza3MtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyJztcclxuaW1wb3J0IHsgVXBsb2FkQ29udHJvbGxlciB9IGZyb20gJy4vdXBsb2FkLmNvbnRyb2xsZXInO1xyXG5pbXBvcnQgeyBMaXN0Q29udHJvbGxlciB9IGZyb20gJy4vbGlzdC5jb250cm9sbGVyJztcclxuaW1wb3J0IHsgZG9jdW1lbnRMaXN0VGFic2V0SXRlbSB9IGZyb20gJy4vZG9jdW1lbnQtbGlzdC10YWJzZXQtaXRlbS5kaXJlY3RpdmUnO1xyXG5pbXBvcnQgeyBkb2N1bWVudExpc3RNZW51SXRlbSB9IGZyb20gJy4vZG9jdW1lbnQtbGlzdC1tZW51LWl0ZW0uZGlyZWN0aXZlJztcclxuXHJcbmNvbmZpZ3VyZS4kaW5qZWN0ID0gWyd0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXInLCAnZG9jdW1lbnRMaXN0U2VydmljZVByb3ZpZGVyJ107XHJcblxyXG5mdW5jdGlvbiBjb25maWd1cmUodGFza3NMaXN0U2VydmljZVByb3ZpZGVyOiBUYXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIsIGRvY3VtZW50TGlzdFNlcnZpY2VQcm92aWRlcjogRG9jdW1lbnRMaXN0U2VydmljZUNvbmZpZykge1xyXG4gICAgdGFza3NMaXN0U2VydmljZVByb3ZpZGVyXHJcbiAgICAgICAgLnNldFVwZGF0ZUludGVydmFsKDUwMClcclxuICAgICAgICAuc2V0VGFza3NMaXN0VXJsKCdhcGkvVGFza3MnKTtcclxuICAgIGRvY3VtZW50TGlzdFNlcnZpY2VQcm92aWRlclxyXG4gICAgICAgIC5zZXRSZWNlbnREb2N1bWVudENvdW50KDUpXHJcbiAgICAgICAgLnNldERvY3VtZW50TGlzdFVybCgnYXBpL1VzZXJEb2N1bWVudHMvTGlzdCcpO1xyXG59XHJcblxyXG5leHBvcnQgZGVmYXVsdCB7XHJcbiAgICBuYW1lOiAnZG9jdW1lbnRMaXN0JyxcclxuICAgIHJlcXVpcmVzOiBbXHJcbiAgICAgICAgJ25nUmVzb3VyY2UnLFxyXG4gICAgICAgICd1aS5ib290c3RyYXAnLFxyXG4gICAgICAgICd1aS5ib290c3RyYXAuY29udGV4dE1lbnUnLCBcclxuICAgICAgICAnbmdGaWxlVXBsb2FkJyxcclxuICAgICAgICAnZG9jdW1lbnRWaWV3ZXInLFxyXG4gICAgICAgICdkZWJ1ZydcclxuICAgIF0sXHJcbiAgICBjb25maWdGbjogY29uZmlndXJlLFxyXG4gICAgZmFjdG9yaWVzOiB7IHJlc3VsdHNTZXJ2aWNlLCBkb2N1bWVudHNTZXJ2aWNlIH0sXHJcbiAgICBwcm92aWRlcnM6IHsgZG9jdW1lbnRMaXN0U2VydmljZTogRG9jdW1lbnRMaXN0U2VydmljZVByb3ZpZGVyLCB0YXNrc0xpc3RTZXJ2aWNlOiB0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIgfSxcclxuICAgIGRpcmVjdGl2ZXM6IHsgZG9jdW1lbnRMaXN0VGFic2V0SXRlbSwgZG9jdW1lbnRMaXN0TWVudUl0ZW0gfSxcclxuICAgIGNvbnRyb2xsZXJzOiB7IExpc3RDb250cm9sbGVyLCBVcGxvYWRDb250cm9sbGVyIH0sXHJcbn0iLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCB7IERvY3VtZW50TW9kZWxTZXJ2aWNlIH0gZnJvbSAnLi9kb2N1bWVudC1tb2RlbC5zZXJ2aWNlJztcclxuaW1wb3J0IHsgRG9jdW1lbnRNb2RlbCB9IGZyb20gJ2FwcC9tb2RlbHMnO1xyXG5leHBvcnQgY2xhc3MgRG9jdW1lbnRDb250cm9sbGVyIHtcclxuICAgIHN0YXRpYyAkaW5qZWN0ID0gWyckcScsICdNb2NrRG9jdW1lbnRNb2RlbFNlcnZpY2UnXTtcclxuXHJcbiAgICBwcml2YXRlIGRvY3VtZW50TW9kZWw6IERvY3VtZW50TW9kZWw7XHJcblxyXG5cclxuICAgIGNvbnN0cnVjdG9yKHByaXZhdGUgJHE6IGFuZ3VsYXIuSVFTZXJ2aWNlLFxyXG4gICAgICAgIHByaXZhdGUgZG9jdW1lbnRNb2RlbFNlcnZpY2U6IERvY3VtZW50TW9kZWxTZXJ2aWNlKSB7XHJcbiAgICB9XHJcbiAgICBwcm9jZXNzRG9jdW1lbnQoaWQ6IHN0cmluZykge1xyXG4gICAgICAgIGlmICh0aGlzLmRvY3VtZW50TW9kZWwuaWQgIT09IGlkKSB7XHJcbiAgICAgICAgICAgIHJldHVybiB0aGlzLmRvY3VtZW50TW9kZWxTZXJ2aWNlLnByb2Nlc3NEb2N1bWVudChpZCk7XHJcbiAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgcmV0dXJuIHRoaXMuJHEucmVqZWN0KHRoaXMuZG9jdW1lbnRNb2RlbCk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG59IiwiJ3VzZSBzdHJpY3QnO1xyXG5pbXBvcnQgeyBEb2N1bWVudENvbnRyb2xsZXIgfSBmcm9tICcuL2RvY3VtZW50LmNvbnRyb2xsZXInO1xyXG5pbXBvcnQgeyBkb2N1bWVudE1vZGVsU2VydmljZSB9IGZyb20gJy4vZG9jdW1lbnQtbW9kZWwuc2VydmljZSc7XHJcbmltcG9ydCB7IGxleGljYWxNZW51QnVpbGRlciB9IGZyb20gJy4vbGV4aWNhbC1tZW51LWJ1aWxkZXIuc2VydmljZSc7XHJcblxyXG52YXIgbW9kdWxlOiBBbmd1bGFyTW9kdWxlT3B0aW9ucyA9IHtcclxuICAgIG5hbWU6ICdkb2N1bWVudFZpZXdlcicsXHJcbiAgICByZXF1aXJlczogW1xyXG4gICAgICAgICdkb2N1bWVudFZpZXdlci5zZWFyY2gnLFxyXG4gICAgICAgICd3aWRnZXRzJyxcclxuICAgICAgICAnbmdSZXNvdXJjZScsXHJcbiAgICAgICAgJ3VpLmJvb3RzdHJhcCcsXHJcbiAgICAgICAgJ3VpLmJvb3RzdHJhcC5jb250ZXh0TWVudSdcclxuICAgIF0sXHJcbiAgICBjb250cm9sbGVyczogeyBEb2N1bWVudENvbnRyb2xsZXIgfSxcclxuICAgIGZhY3RvcmllczogeyBkb2N1bWVudE1vZGVsU2VydmljZSwgbGV4aWNhbE1lbnVCdWlsZGVyIH1cclxufTtcclxuZXhwb3J0IGRlZmF1bHQgbW9kdWxlOyIsIid1c2Ugc3RyaWN0JztcclxuXHJcbmV4cG9ydCBkZWZhdWx0IHtcclxuICAgIG5hbWU6ICd3aWRnZXRzJyxcclxuICAgIHJlcXVpcmVzOiBbJ3VpLmJvb3RzdHJhcCddXHJcbn0iLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCB7IExleGljYWxNb2RlbCwgV29yZE1vZGVsLCBQaHJhc2VNb2RlbCwgVGV4dEZyYWdtZW50TW9kZWwgfSBmcm9tICdhcHAvbW9kZWxzJztcclxudmFyIHRlbXBsYXRlID0gcmVxdWlyZSgnYXBwL2RvY3VtZW50LXZpZXdlci9zZWFyY2gvc2VhcmNoLWJveC5kaXJlY3RpdmUuaHRtbCFodG1sJyk7XHJcblxyXG5jbGFzcyBTZWFyY2hCb3hDb250cm9sbGVyIHtcclxuICAgIHN0YXRpYyAkaW5qZWN0ID0gWyckcSddO1xyXG4gICAgY29uc3RydWN0b3IocHJpdmF0ZSAkcTogYW5ndWxhci5JUVNlcnZpY2UpIHsgfVxyXG4gICAgcGhyYXNlczogUGhyYXNlTW9kZWxbXTtcclxuICAgIHdvcmRzOiBXb3JkTW9kZWxbXTtcclxuXHJcbiAgICBnZXRXb3JkcygpIHtcclxuICAgICAgICByZXR1cm4gKHRoaXMucGhyYXNlcyB8fCBbXSkuZmxhdE1hcChwID0+IHAud29yZHMpO1xyXG4gICAgfVxyXG4gICAgZmluZDogU2VhcmNoTW9kZWw7XHJcbiAgICBzZWFyY2hDb250ZXh0OiBUZXh0RnJhZ21lbnRNb2RlbFtdO1xyXG5cclxuICAgIHNlYXJjaChzZWFyY2hPcHRpb25zOiBTZWFyY2hPcHRpb25zLCBzZWFyY2hDb250ZXh0OiBUZXh0RnJhZ21lbnRNb2RlbFtdKSB7XHJcbiAgICAgICAgbGV0IGRlZmVycmVkID0gdGhpcy4kcS5kZWZlcjxTZWFyY2hNb2RlbFtdPigpO1xyXG4gICAgICAgIGxldCB2YWx1ZSA9IHNlYXJjaE9wdGlvbnMudmFsdWU7XHJcbiAgICAgICAgY29uc3QgdGVybSA9XHJcbiAgICAgICAgICAgIHR5cGVvZiB2YWx1ZSA9PT0gJ3N0cmluZycgPyB2YWx1ZSA6XHJcbiAgICAgICAgICAgICAgICB0eXBlb2YgdmFsdWUgIT09ICd1bmRlZmluZWQnID8gdmFsdWUuZGV0YWlsVGV4dCA6IHVuZGVmaW5lZDtcclxuICAgICAgICBpZiAoIXRlcm0pIHtcclxuICAgICAgICAgICAgZGVmZXJyZWQucmVqZWN0KCdzZWFyY2ggdGVybSB3YXMgdW5kZWZpbmVkJyk7XHJcbiAgICAgICAgfSBlbHNlIGlmICghc2VhcmNoQ29udGV4dCkge1xyXG4gICAgICAgICAgICBkZWZlcnJlZC5yZWplY3QoJ25vdGhpbmcgdG8gc2VhcmNoJyk7XHJcbiAgICAgICAgICAgIHRoaXMucGhyYXNlcy5mb3JFYWNoKHBocmFzZSA9PiBwaHJhc2Uuc3R5bGUuY3NzQ2xhc3MgPSBwaHJhc2Uuc3R5bGUuY3NzQ2xhc3MucmVwbGFjZSgnbWF0Y2hlZC1ieS1zZWFyY2gnLCAnJykpO1xyXG4gICAgICAgIH0gZWxzZSB7XHJcbiAgICAgICAgICAgIHRoaXMucGhyYXNlcyA9IHRoaXMucGhyYXNlcyB8fCBzZWFyY2hDb250ZXh0LmZsYXRNYXAobSA9PiBtLnBhcmFncmFwaHMpLmZsYXRNYXAocCA9PiBwLnNlbnRlbmNlcykuZmxhdE1hcChzID0+IHMucGhyYXNlcyk7XHJcblxyXG4gICAgICAgICAgICB2YXIgcmVzdWx0czogUGhyYXNlTW9kZWxbXSA9IFtdO1xyXG4gICAgICAgICAgICB0aGlzLnBocmFzZXMuZm9yRWFjaChwaHJhc2UgPT4ge1xyXG4gICAgICAgICAgICAgICAgbGV0IG1hdGNoZWQgPSBwaHJhc2Uud29yZHMuc29tZSh3b3JkID0+IHdvcmQudGV4dCA9PT0gdmFsdWUpO1xyXG4gICAgICAgICAgICAgICAgaWYgKCFtYXRjaGVkKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgcGhyYXNlLnN0eWxlLmNzc0NsYXNzID0gcGhyYXNlLnN0eWxlLmNzc0NsYXNzLnJlcGxhY2UoJ21hdGNoZWQtYnktc2VhcmNoJywgJycpO1xyXG4gICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICBwaHJhc2Uuc3R5bGUuY3NzQ2xhc3MgKz0gJyBtYXRjaGVkLWJ5LXNlYXJjaCc7XHJcbiAgICAgICAgICAgICAgICAgICAgcmVzdWx0cy5wdXNoKHBocmFzZSk7XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICBkZWZlcnJlZC5yZXNvbHZlKHJlc3VsdHMubWFwKHIgPT4gci50ZXh0KSk7XHJcbiAgICAgICAgfVxyXG4gICAgICAgIHJldHVybiBkZWZlcnJlZC5wcm9taXNlO1xyXG4gICAgfVxyXG59XHJcbmV4cG9ydCBmdW5jdGlvbiBzZWFyY2hCb3goKTogYW5ndWxhci5JRGlyZWN0aXZlIHtcclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgcmVzdHJpY3Q6ICdFJyxcclxuICAgICAgICBjb250cm9sbGVyOiBTZWFyY2hCb3hDb250cm9sbGVyLFxyXG4gICAgICAgIGNvbnRyb2xsZXJBczogJ3NlYXJjaCcsXHJcbiAgICAgICAgc2NvcGU6IHt9LFxyXG4gICAgICAgIGJpbmRUb0NvbnRyb2xsZXI6IHtcclxuICAgICAgICAgICAgc2VhcmNoQ29udGV4dDogJz0nLFxyXG4gICAgICAgICAgICBmaW5kOiAnPSdcclxuICAgICAgICB9LFxyXG4gICAgICAgIHRlbXBsYXRlOiB0ZW1wbGF0ZVxyXG4gICAgfTtcclxufVxyXG5cclxudHlwZSBTZWFyY2hNb2RlbCA9IHN0cmluZyB8IExleGljYWxNb2RlbDtcclxuaW50ZXJmYWNlIFNlYXJjaE9wdGlvbnMge1xyXG4gICAgdmFsdWU6IFNlYXJjaE1vZGVsO1xyXG4gICAgbGlmdGVkPzogYm9vbGVhbjtcclxufSAiLCIndXNlIHN0cmljdCc7XHJcbmltcG9ydCAqIGFzIGFuZ3VsYXIgZnJvbSAnYW5ndWxhcidcclxuaW1wb3J0IHsgc2VhcmNoQm94IH0gZnJvbSAnLi9zZWFyY2gtYm94LmRpcmVjdGl2ZSc7XHJcblxyXG5leHBvcnQgZGVmYXVsdCB7XHJcbiAgICBuYW1lOiAnZG9jdW1lbnRWaWV3ZXIuc2VhcmNoJyxcclxuICAgIHJlcXVpcmVzOiBbJ3VpLmJvb3RzdHJhcC50eXBlYWhlYWQnXSxcclxuICAgIGRpcmVjdGl2ZXM6IHsgc2VhcmNoQm94IH1cclxufSIsbnVsbCwiLy8vIDxyZWZlcmVuY2UgcGF0aCA9ICcuLi8uLi90eXBpbmdzL2FuZ3VsYXItYm9vdHN0cmFwLWNvbnRleHRNZW51L2FuZ3VsYXItYm9vdHN0cmFwLWNvbnRleHRtZW51LmQudHMnLz5cclxubW9kdWxlIGNvbnRleHRtZW51VGVzdHMge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgIHR5cGUgSXRlbSA9IGFuZ3VsYXIudWkuYm9vdHN0cmFwLmNvbnRleHRNZW51Lk1lbnVJdGVtO1xyXG4gICAgaW1wb3J0IEl0ZW1FdmVudCA9IGFuZ3VsYXIudWkuYm9vdHN0cmFwLmNvbnRleHRNZW51Lkl0ZW1FdmVudDtcclxuXHJcbiAgICB0eXBlIFQgPSBzdHJpbmc7XHJcbiAgICBpbXBvcnQgSXRlbVNjb3BlID0gYW5ndWxhci51aS5ib290c3RyYXAuY29udGV4dE1lbnUuSXRlbVNjb3BlO1xyXG5cclxuXHJcbiAgICB2YXIgaXRlbTE6IEl0ZW0gPSBbXHJcbiAgICAgICAgKHM6IEl0ZW1TY29wZTxzdHJpbmc+LCBlOiBKUXVlcnlFdmVudE9iamVjdCkgPT4gJ2l0ZW0xJyxcclxuICAgICAgICAoczogSXRlbVNjb3BlPHN0cmluZz4sIGU6IEpRdWVyeUV2ZW50T2JqZWN0KSA9PiBjb25zb2xlLmxvZygnaXRlbTEgY2xpY2tlZCcpLFxyXG4gICAgICAgIChzOiBJdGVtU2NvcGU8c3RyaW5nPiwgZTogSlF1ZXJ5RXZlbnRPYmplY3QpID0+IHRydWVcclxuICAgIF07XHJcbiAgICB2YXIgaXRlbTI6IEl0ZW0gPSBbXHJcbiAgICAgICAgJ2l0ZW0yJyxcclxuICAgICAgICAocykgPT4gY29uc29sZS5sb2cocylcclxuICAgIF07XHJcbiAgICB2YXIgaXRlbTM6IEl0ZW0gPSBbXHJcbiAgICAgICAgJ2l0ZW0zJyxcclxuICAgICAgICAoczogSXRlbVNjb3BlPHN0cmluZz4sIGU6IEpRdWVyeUV2ZW50T2JqZWN0KSA9PiBjb25zb2xlLmxvZygnaXRlbTMgY2xpY2tlZCcpLFxyXG4gICAgICAgIChzLCBlKSA9PiBPYmplY3Qua2V5cyhzKS5ldmVyeShrMSA9PiBPYmplY3Qua2V5cyhlKS5zb21lKGsyID0+IGsxID09PSBrMikpXHJcbiAgICBdO1xyXG4gICAgdmFyIG1lbnU6IEl0ZW1bXSA9IFtcclxuICAgICAgICBpdGVtMSwgaXRlbTJcclxuICAgIF07XHJcbn0iLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vLi4vLi4vdHlwaW5ncy9qcXVlcnkvanF1ZXJ5LmQudHNcIiAvPlxyXG5uYW1lc3BhY2UgTEFTSS5hY2NvdW50TWFuYWdlbWVudCB7XHJcbiAgICAndXNlIHN0cmljdCc7XHJcbiAgICAkKCgpID0+ICQoJyNjYW5jZWwtcHJvZmlsZS1lZGl0cycpLmNsaWNrKCgpID0+IGRvY3VtZW50LmxvY2F0aW9uLmhyZWYgPSBgaHR0cDovLyR7ZG9jdW1lbnQuZG9tYWlufToke2RvY3VtZW50LmxvY2F0aW9uLnBvcnR9YCkpO1xyXG59IiwibmFtZXNwYWNlIExBU0kuZG9jdW1lbnRMaXN0IHtcclxuICAgIGV4cG9ydCBpbnRlcmZhY2UgRG9jdW1lbnRMaXN0SXRlbSB7XHJcbiAgICAgICAgLyoqXHJcbiAgICAgICAgICogVGhlIG5hbWUgb2YgdGhlIGRvY3VtZW50XHJcbiAgICAgICAgICovXHJcbiAgICAgICAgbmFtZTogc3RyaW5nO1xyXG4gICAgICAgIC8qKlxyXG4gICAgICAgICAqIFRoZSBpZCBvZiB0aGUgZG9jdW1lbnRcclxuICAgICAgICAgKi9cclxuICAgICAgICBpZDogc3RyaW5nO1xyXG4gICAgICAgIC8qKlxyXG4gICAgICAgICAqIFRoZSBjb250ZW50IGlzIG9wdGlvbmFsIGFzIHRoZSBsaXN0IGl0ZW0gbWF5IGp1c3QgYmUgYSBwbGFjZWhvbGRlciBmb3IgdGhlIGRvY3VtZW50LlxyXG4gICAgICAgICAqL1xyXG4gICAgICAgIGNvbnRlbnQ/OiBzdHJpbmc7XHJcbiAgICB9XHJcbn0iLCIvLy8gPHJlZmVyZW5jZSBwYXRoPVwiLi4vLi4vLi4vdHlwaW5ncy9qcXVlcnkvanF1ZXJ5LmQudHNcIi8+XHJcbi8vLyA8cmVmZXJlbmNlIHBhdGg9XCIuLi9sYXNpLnRzXCIvPlxyXG5cclxuaW50ZXJmYWNlIEpRdWVyeSB7IGNvbnRleHRtZW51OiBhbnk7IH1cclxuKGZ1bmN0aW9uIChhcHApIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuICAgIGFwcC5idWlsZE1lbnVzID0gKGZ1bmN0aW9uIChjb250ZXh0dWFsRWxlbWVudElkU2VsZWN0b3JzKSB7XHJcbiAgICAgICAgdmFyIHZlcmJhbE1lbnVUZXh0VG9FbGVtZW50c01hcCA9IHtcclxuICAgICAgICAgICAgJ1ZpZXcgU3ViamVjdHMnOiAnc3ViamVjdHMnLFxyXG4gICAgICAgICAgICAnVmlldyBEaXJlY3QgT2JqZWN0cyc6ICdkaXJlY3RPYmplY3RzJyxcclxuICAgICAgICAgICAgJ1ZpZXcgSW5kaXJlY3QgT2JqZWN0cyc6ICdpbmRpcmVjdE9iamVjdHMnXHJcbiAgICAgICAgfTtcclxuICAgICAgICB2YXIgcmVsYXRpb25zaGlwQ3NzQ2xhc3NOYW1lTWFwID0ge1xyXG4gICAgICAgICAgICAnVmlldyBTdWJqZWN0cyc6ICdzdWJqZWN0LW9mLWN1cnJlbnQnLFxyXG4gICAgICAgICAgICAnVmlldyBEaXJlY3QgT2JqZWN0cyc6ICdkaXJlY3Qtb2JqZWN0LW9mLWN1cnJlbnQnLFxyXG4gICAgICAgICAgICAnVmlldyBJbmRpcmVjdCBPYmplY3RzJzogJ2luZGlyZWN0LW9iamVjdC1vZi1jdXJyZW50J1xyXG4gICAgICAgIH07XHJcbiAgICAgICAgcmV0dXJuIGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgdmFyIGZvclZlcmJhbCA9IGZ1bmN0aW9uIChjb250ZXh0KSB7XHJcblxyXG4gICAgICAgICAgICAgICAgdmFyIG1lbnUgPSBKU09OLnBhcnNlKCQoJyNjb250ZXh0JyArIGNvbnRleHRbMF0uaWQpLnRleHQoKSk7XHJcbiAgICAgICAgICAgICAgICB2YXIgc3ViamVjdHMgPSBtZW51LnN1YmplY3RzLFxyXG4gICAgICAgICAgICAgICAgICAgIGRpcmVjdE9iamVjdHMgPSBtZW51LmRpcmVjdE9iamVjdHMsXHJcbiAgICAgICAgICAgICAgICAgICAgaW5kaXJlY3RPYmplY3RzID0gbWVudS5pbmRpcmVjdE9iamVjdHM7XHJcbiAgICAgICAgICAgICAgICB2YXIgdGVtcCA9IHtcclxuICAgICAgICAgICAgICAgICAgICBzdWJqZWN0czogc3ViamVjdHMsXHJcbiAgICAgICAgICAgICAgICAgICAgZGlyZWN0T2JqZWN0czogZGlyZWN0T2JqZWN0cyxcclxuICAgICAgICAgICAgICAgICAgICBpbmRpcmVjdE9iamVjdHM6IGluZGlyZWN0T2JqZWN0c1xyXG4gICAgICAgICAgICAgICAgfSwgcmVzdWx0ID0ge307XHJcbiAgICAgICAgICAgICAgICBPYmplY3Qua2V5cyh0ZW1wKVxyXG4gICAgICAgICAgICAgICAgICAgIC5maWx0ZXIoZnVuY3Rpb24gKGtleSkgeyByZXR1cm4gdGVtcFtrZXldOyB9KVxyXG4gICAgICAgICAgICAgICAgICAgIC5mb3JFYWNoKGZ1bmN0aW9uIChrZXkpIHsgcmVzdWx0W2tleV0gPSB0ZW1wW2tleV07IH0pO1xyXG4gICAgICAgICAgICAgICAgcmV0dXJuIHJlc3VsdDtcclxuICAgICAgICAgICAgfSwgZm9yUmVmZXJlbmNlciA9IGZ1bmN0aW9uIChjb250ZXh0KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdmFyIG1lbnUgPSBKU09OLnBhcnNlKCQoJyNjb250ZXh0JyArIGNvbnRleHRbMF0uaWQpLnRleHQoKSk7XHJcbiAgICAgICAgICAgICAgICAgICAgcmV0dXJuIG1lbnU7XHJcbiAgICAgICAgICAgICAgICB9O1xyXG4gICAgICAgICAgICAkKCdzcGFuLnJlZmVyZW5jZXInKS5jb250ZXh0bWVudSh7XHJcbiAgICAgICAgICAgICAgICB0YXJnZXQ6ICcjcmVmZXJlbmNlci1jb250ZXh0LW1lbnUnLFxyXG4gICAgICAgICAgICAgICAgYmVmb3JlOiBmdW5jdGlvbiAoZXZlbnQsIGNvbnRleHQpIHtcclxuICAgICAgICAgICAgICAgICAgICB2YXIgZGF0YSA9IGZvclJlZmVyZW5jZXIoY29udGV4dCk7XHJcbiAgICAgICAgICAgICAgICAgICAgZXZlbnQudGFyZ2V0LmxleGljYWxDb250ZXh0TWVudSA9IGRhdGE7XHJcbiAgICAgICAgICAgICAgICAgICAgcmV0dXJuIGRhdGEucmVmZXJyZWRUbyAmJiBkYXRhLnJlZmVycmVkVG8ubGVuZ3RoID4gMDtcclxuICAgICAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgICAgICBvbkl0ZW06IGZ1bmN0aW9uIChjb250ZXh0KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgY29udGV4dFswXS5sZXhpY2FsQ29udGV4dE1lbnUucmVmZXJyZWRUby5tYXAoZnVuY3Rpb24gKGlkKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHJldHVybiAkKCcjJyArIGlkKTtcclxuICAgICAgICAgICAgICAgICAgICB9KS5mb3JFYWNoKGZ1bmN0aW9uIChlbGVtZW50KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIGVsZW1lbnQuY3NzKCdiYWNrZ3JvdW5kLWNvbG9yJywgJ3JlZCcpO1xyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgJCgnc3Bhbi52ZXJiYWwnKS5jb250ZXh0bWVudSh7XHJcbiAgICAgICAgICAgICAgICB0YXJnZXQ6ICcjdmVyYmFsLWNvbnRleHQtbWVudScsXHJcbiAgICAgICAgICAgICAgICBiZWZvcmU6IGZ1bmN0aW9uIChlLCBjb250ZXh0KSB7XHJcblxyXG4gICAgICAgICAgICAgICAgICAgIHZhciBjb3VudCA9IDA7XHJcbiAgICAgICAgICAgICAgICAgICAgdmFyIG1lbnUgPSBmb3JWZXJiYWwoY29udGV4dCk7XHJcbiAgICAgICAgICAgICAgICAgICAgZS50YXJnZXQubGV4aWNhbENvbnRleHRNZW51ID0ge307XHJcbiAgICAgICAgICAgICAgICAgICAgT2JqZWN0LmtleXMobWVudSkuZm9yRWFjaChmdW5jdGlvbiAoa2V5KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIGUudGFyZ2V0LmxleGljYWxDb250ZXh0TWVudVtrZXldID0gbWVudVtrZXldLm1hcChmdW5jdGlvbiAoaWQpIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgICAgIHZhciBpZFNlbGVjdG9yID0gJyMnICsgaWQ7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICBpZiAoIWNvbnRleHR1YWxFbGVtZW50SWRTZWxlY3RvcnMuc29tZShmdW5jdGlvbiAoZSkgeyByZXR1cm4gZSA9PT0gaWRTZWxlY3RvcjsgfSkpIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICBjb250ZXh0dWFsRWxlbWVudElkU2VsZWN0b3JzLnB1c2goaWRTZWxlY3Rvcik7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICByZXR1cm4gaWRTZWxlY3RvcjtcclxuICAgICAgICAgICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgICAgICAgICAgW1xyXG4gICAgICAgICAgICAgICAgICAgICAgICB7IG5hbWU6ICdzdWJqZWN0cycsIGlkOiAnI3N1YmplY3RzLWl0ZW0nIH0sXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHsgbmFtZTogJ2RpcmVjdE9iamVjdHMnLCBpZDogJyNkaXJlY3RvYmplY3RzLWl0ZW0nIH0sXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIHsgbmFtZTogJ2luZGlyZWN0T2JqZWN0cycsIGlkOiAnI2luZGlyZWN0b2JqZWN0cy1pdGVtJyB9XHJcbiAgICAgICAgICAgICAgICAgICAgXS5mb3JFYWNoKGZ1bmN0aW9uICh4KSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIGlmICghbWVudVt4Lm5hbWVdKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAkKHguaWQpLmhpZGUoKTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgICAgIGNvdW50ICs9IDE7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAkKHguaWQpLnNob3coKTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybiBjb3VudCA+IDA7XHJcbiAgICAgICAgICAgICAgICB9LFxyXG4gICAgICAgICAgICAgICAgb25JdGVtOiBmdW5jdGlvbiAoY29udGV4dCwgZXZlbnQpIHtcclxuICAgICAgICAgICAgICAgICAgICB2YXIgbWVudSA9IGNvbnRleHRbMF0ubGV4aWNhbENvbnRleHRNZW51O1xyXG4gICAgICAgICAgICAgICAgICAgIGNvbnRleHR1YWxFbGVtZW50SWRTZWxlY3RvcnNcclxuICAgICAgICAgICAgICAgICAgICAgICAgLmZsYXRNYXAoZnVuY3Rpb24gKGUpIHsgcmV0dXJuICQoZSkudG9BcnJheSgpOyB9LCAkKVxyXG4gICAgICAgICAgICAgICAgICAgICAgICAuZm9yRWFjaChmdW5jdGlvbiAoZSkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICBPYmplY3Qua2V5cyhyZWxhdGlvbnNoaXBDc3NDbGFzc05hbWVNYXApLmZvckVhY2goZnVuY3Rpb24gKGtleSkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgZS5yZW1vdmVDbGFzcyhyZWxhdGlvbnNoaXBDc3NDbGFzc05hbWVNYXBba2V5XSk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIG1lbnVbdmVyYmFsTWVudVRleHRUb0VsZW1lbnRzTWFwW2V2ZW50LnRhcmdldC50ZXh0XV1cclxuICAgICAgICAgICAgICAgICAgICAgICAgLm1hcCgkKVxyXG4gICAgICAgICAgICAgICAgICAgICAgICAuZm9yRWFjaChmdW5jdGlvbiAoZSkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICBlLmFkZENsYXNzKHJlbGF0aW9uc2hpcENzc0NsYXNzTmFtZU1hcFtldmVudC50YXJnZXQudGV4dF0pO1xyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG5cclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgICQoZG9jdW1lbnQpLm9uKCdjbGljaycsIGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgICAgICQoJyN2ZXJiYWwtY29udGV4dC1tZW51JykuaGlkZSgpO1xyXG4gICAgICAgICAgICAgICAgJCgnI3JlZmVyZW5jZXItY29udGV4dC1tZW51JykuaGlkZSgpO1xyXG4gICAgICAgICAgICB9KTtcclxuICAgICAgICB9O1xyXG4gICAgfSAoW10pKTtcclxuICAgICQoYXBwLmJ1aWxkTWVudXMpO1xyXG59IChMQVNJKSk7IiwiaW1wb3J0ICogYXMgTEFTSSBmcm9tICdhcHAvTEFTSSc7XHJcblxyXG5MQVNJLmVuYWJsZUFjdGl2ZUhpZ2hsaWdodGluZyA9IGZ1bmN0aW9uICgpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuICAgIHZhciBlbmFibGVBY3RpdmVIaWdobGlnaHRpbmcgPSBmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgdmFyIHBocmFzYWxUZXh0U3BhbnMgPSAkKCdzcGFuLnBocmFzZScpO1xyXG4gICAgICAgIHZhciBoaWdobGlnaHRDbGFzcyA9ICdhY3RpdmUtcGhyYXNlLWhpZ2hsaWdodCc7XHJcbiAgICAgICAgdmFyIHJlY29sb3IgPSBmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgIHBocmFzYWxUZXh0U3BhbnMuZWFjaChmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgICAgICAkKHRoaXMpLnJlbW92ZUNsYXNzKGhpZ2hsaWdodENsYXNzKTtcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgICQodGhpcykuYWRkQ2xhc3MoaGlnaGxpZ2h0Q2xhc3MpO1xyXG4gICAgICAgIH07XHJcbiAgICAgICAgcGhyYXNhbFRleHRTcGFucy5jbGljayhyZWNvbG9yKTtcclxuICAgICAgICBwaHJhc2FsVGV4dFNwYW5zLm9uKCdjb250ZXh0bWVudScsIHJlY29sb3IpO1xyXG5cclxuICAgICAgICAvLyBib290c3RyYXAgcmVxdWlyZXMgdGhhdCB0b29sdGlwcyBiZSBtYW51YWxseSBlbmFibGVkLiBUaGUgZGF0YS10b2dnbGU9XCJ0b29sdGlwXCIgYXR0cmlidXRlcyBzZXQgb24gZWFjaCBlbGVtZW50XHJcbiAgICAgICAgLy8gaGF2ZSBubyBlZmZlY3Qgd2l0aG91dCB0aGlzIG9yIGFuIGVxdWl2YWxlbnQgY2FsbC4gQnkgc2V0dGluZyBjb250YWluZXIgdG8gJ2JvZHknLCB3ZSBhbGxvdyB0aGUgY29udGVudHMgb2YgdGhlIFxyXG4gICAgICAgIC8vIHRvb2x0aXAgdG8gb3ZlcmZsb3cgaXRzIGNvbnRhaW5lci4gVGhpcyBpcyBnZW5lcmFsbHkgY2xvc2UgdG8gdGhlIGRlc2lyZWQgYmVoYXZpb3IgYXMgaXQgaXMgZGlmZmljdWx0IHRvIHByZWRpY3Qgd2lkdGhcclxuICAgICAgICAvLyBhbmQgdGhpcyBnaXZlcyBnb29kIGZsZXhpYmlsaXR5LiBUaGVyZSBpcyBwcm9iYWJseSBhIGNsZWFuZXIgYW5kIG1vcmUgcHJlY2lzZS9vYnZpb3VzIHdheSBvZiBkb2luZyB0aGlzLCBjaGFuZ2UgdG8gdGhhdCBpZiBkaXNjb3ZlcmVkLlxyXG4gICAgICAgICQoJ1tkYXRhLXRvZ2dsZT1cInRvb2x0aXBcIl0nKS50b29sdGlwKHtcclxuICAgICAgICAgICAgZGVsYXk6IDI1MCxcclxuICAgICAgICAgICAgY29udGFpbmVyOiAnYm9keSdcclxuICAgICAgICB9KTtcclxuICAgICAgICAvLyBUT0RPOiBsb29rIGludG8gZml4aW5nIHRvb2x0aXBzIG9uIGVsZW1lbnRzIGNvbnRhaW5pbmcgYSBsaW5lIGJyZWFrIG9yIHJlbW92ZSBzdWNoIGJyZWFrcy5cclxuICAgIH07XHJcbiAgICAkKGVuYWJsZUFjdGl2ZUhpZ2hsaWdodGluZyk7XHJcbiAgICByZXR1cm4gZW5hYmxlQWN0aXZlSGlnaGxpZ2h0aW5nO1xyXG59OyIsIid1c2Ugc3RyaWN0JztcclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBkb2N1bWVudFBhZ2UoKTogYW5ndWxhci5JRGlyZWN0aXZlIHtcclxuICAgIGZ1bmN0aW9uIGxpbmsoc2NvcGU6IGFuZ3VsYXIuSVNjb3BlLCBlbGVtZW50OiBhbmd1bGFyLklBdWdtZW50ZWRKUXVlcnksIGF0dHJzOiBhbmd1bGFyLklBdHRyaWJ1dGVzKSB7XHJcbiAgICAgICAgY29uc29sZS5sb2coc2NvcGUpO1xyXG4gICAgICAgIGNvbnNvbGUubG9nKGVsZW1lbnQpO1xyXG4gICAgICAgIGNvbnNvbGUubG9nKGF0dHJzKTtcclxuICAgIH1cclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgcmVzdHJpY3Q6ICdFJyxcclxuICAgICAgICBsaW5rOiBsaW5rLFxyXG4gICAgICAgIHRlbXBsYXRlOiByZXF1aXJlKCcvYXBwL2RvY3VtZW50LXZpZXdlci9kb2N1bWVudC1wYWdlLmRpcmVjdGl2ZS5odG1sIScpLFxyXG4gICAgICAgIHJlcGxhY2U6IHRydWUsXHJcbiAgICAgICAgc2NvcGU6IHtcclxuICAgICAgICAgICAgcGFnZTogJz0nLFxyXG4gICAgICAgICAgICBkb2N1bWVudDogJz0nXHJcbiAgICAgICAgfVxyXG4gICAgfTtcclxufSIsIid1c2Ugc3RyaWN0JztcclxuXHJcbmRvY3VtZW50Vmlld2VyRGlyZWN0aXZlLiRpbmplY3QgPSBbXTtcclxuZXhwb3J0IGZ1bmN0aW9uIGRvY3VtZW50Vmlld2VyRGlyZWN0aXZlKCk6IGFuZ3VsYXIuSURpcmVjdGl2ZSB7XHJcblxyXG4gICAgdmFyIGxpbms6IGFuZ3VsYXIuSURpcmVjdGl2ZUxpbmtGbiA9IGZ1bmN0aW9uIChzY29wZTogYW5ndWxhci5JU2NvcGUsIGVsZW1lbnQ6IGFuZ3VsYXIuSUF1Z21lbnRlZEpRdWVyeSwgYXR0cnM6IGFuZ3VsYXIuSUF0dHJpYnV0ZXMpIHtcclxuICAgICAgICBjb25zb2xlLmxvZyhzY29wZSk7XHJcbiAgICAgICAgY29uc29sZS5sb2coZWxlbWVudCk7XHJcbiAgICAgICAgY29uc29sZS5sb2coYXR0cnMpO1xyXG4gICAgfTtcclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgcmVzdHJpY3Q6ICdFJyxcclxuICAgICAgICB0ZW1wbGF0ZVVybDogJy9hcHAvZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LXZpZXdlci5kaXJlY3RpdmUuaHRtbCcsXHJcbiAgICAgICAgcmVwbGFjZTogdHJ1ZSxcclxuICAgICAgICBzY29wZToge1xyXG4gICAgICAgICAgICBkb2N1bWVudDogJz0nXHJcbiAgICAgICAgfSxcclxuICAgICAgICBsaW5rXHJcbiAgICB9O1xyXG59IiwibmFtZXNwYWNlIExBU0kuZG9jdW1lbnRWaWV3ZXIge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgIHBhcmFncmFwaC4kaW5qZWN0ID0gWyckd2luZG93J107XHJcbiAgICBmdW5jdGlvbiBwYXJhZ3JhcGgoJHdpbmRvdzogYW5ndWxhci5JV2luZG93U2VydmljZSk6IGFuZ3VsYXIuSURpcmVjdGl2ZSB7XHJcbiAgICAgICAgcmV0dXJuIHtcclxuICAgICAgICAgICAgcmVzdHJpY3Q6ICdFJyxcclxuICAgICAgICAgICAgdGVtcGxhdGVVcmw6ICcvYXBwL2RvY3VtZW50LXZpZXdlci9wYXJhZ3JhcGguZGlyZWN0aXZlLmh0bWwnLFxyXG4gICAgICAgICAgICBzY29wZToge1xyXG4gICAgICAgICAgICAgICAgcGFyYWdyYXBoOiAnPScsXHJcbiAgICAgICAgICAgICAgICBwYXJlbnRJZDogJz0nXHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9O1xyXG5cclxuICAgIH1cclxuICAgIGFuZ3VsYXJcclxuICAgICAgICAubW9kdWxlKCdkb2N1bWVudFZpZXdlcicpXHJcbiAgICAgICAgLmRpcmVjdGl2ZSh7IHBhcmFncmFwaCB9KTtcclxufSIsIid1c2Ugc3RyaWN0JztcclxuaW1wb3J0IHsgUGhyYXNlTW9kZWwgfSBmcm9tICdhcHAvbW9kZWxzJztcclxuaW1wb3J0IHsgTGV4aWNhbE1lbnVCdWlsZGVyRmFjdG9yeSwgVmVyYmFsQ29udGV4dG1lbnVEYXRhU291cmNlIH0gZnJvbSAnLi9sZXhpY2FsLW1lbnUtYnVpbGRlci5zZXJ2aWNlJztcclxucGhyYXNlLiRpbmplY3QgPSBbJ2xleGljYWxNZW51QnVpbGRlciddO1xyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHBocmFzZShsZXhpY2FsTWVudUJ1aWxkZXI6IExleGljYWxNZW51QnVpbGRlckZhY3RvcnkpOiBhbmd1bGFyLklEaXJlY3RpdmUge1xyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICByZXN0cmljdDogJ0UnLFxyXG4gICAgICAgIHRlbXBsYXRlVXJsOiAnL2FwcC9kb2N1bWVudC12aWV3ZXIvcGhyYXNlLmRpcmVjdGl2ZS5odG1sJyxcclxuICAgICAgICBzY29wZToge1xyXG4gICAgICAgICAgICBwaHJhc2U6ICc9JyxcclxuICAgICAgICAgICAgcGFyZW50SWQ6ICc9J1xyXG4gICAgICAgIH0sXHJcbiAgICAgICAgbGlua1xyXG4gICAgfTtcclxuXHJcbiAgICBmdW5jdGlvbiBsaW5rKHNjb3BlOiBQaHJhc2VTY29wZSwgZWxlbWVudDogYW5ndWxhci5JQXVnbWVudGVkSlF1ZXJ5LCBhdHRyczogYW5ndWxhci5JQXR0cmlidXRlcykge1xyXG4gICAgICAgIHZhciBjb250ZXh0bWVudSA9IGxleGljYWxNZW51QnVpbGRlci5idWlsZEFuZ3VsYXJNZW51KHNjb3BlLnBocmFzZS5jb250ZXh0bWVudSk7XHJcbiAgICAgICAgc2NvcGUucGhyYXNlLmhhc0NvbnRleHRtZW51RGF0YSA9ICEhY29udGV4dG1lbnU7XHJcbiAgICAgICAgaWYgKHNjb3BlLnBocmFzZS5oYXNDb250ZXh0bWVudURhdGEpIHtcclxuICAgICAgICAgICAgKDxhbnk+c2NvcGUucGhyYXNlKS5jb250ZXh0bWVudSA9IGNvbnRleHRtZW51O1xyXG4gICAgICAgIH1cclxuICAgIH1cclxufVxyXG5cclxuXHJcbmludGVyZmFjZSBQaHJhc2VTY29wZSBleHRlbmRzIGFuZ3VsYXIuSVNjb3BlIHtcclxuICAgIHBocmFzZTogUGhyYXNlTW9kZWw7XHJcbiAgICBwYXJlbnRJZD86IHN0cmluZztcclxuICAgIG1lbnVJc1ZpYWJsZShtZW51OiBWZXJiYWxDb250ZXh0bWVudURhdGFTb3VyY2UpOiBib29sZWFuO1xyXG59XHJcblxyXG5hbmd1bGFyXHJcbiAgICAubW9kdWxlKCdkb2N1bWVudFZpZXdlcicpXHJcbiAgICAuZGlyZWN0aXZlKHsgcGhyYXNlIH0pO1xyXG4iLCJuYW1lc3BhY2UgTEFTSS5kb2N1bWVudFZpZXdlciB7XHJcbiAgICAndXNlIHN0cmljdCc7XHJcblxyXG4gICAgZnVuY3Rpb24gc2VudGVuY2UoKTogYW5ndWxhci5JRGlyZWN0aXZlIHtcclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICByZXN0cmljdDogJ0UnLFxyXG4gICAgICAgICAgICB0ZW1wbGF0ZVVybDogJy9hcHAvZG9jdW1lbnQtdmlld2VyL3NlbnRlbmNlLmRpcmVjdGl2ZS5odG1sJyxcclxuICAgICAgICAgICAgc2NvcGU6IHtcclxuICAgICAgICAgICAgICAgIHNlbnRlbmNlOiAnPScsXHJcbiAgICAgICAgICAgICAgICBwYXJlbnRJZDogJz0nXHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9O1xyXG4gICAgfVxyXG5cclxuICAgIGFuZ3VsYXJcclxuICAgICAgICAubW9kdWxlKCdkb2N1bWVudFZpZXdlcicpXHJcbiAgICAgICAgLmRpcmVjdGl2ZSh7IHNlbnRlbmNlIH0pO1xyXG59IiwiLyogZ2xvYmFsOmRyYWdnYWJsZSAqL1xyXG5uYW1lc3BhY2UgTEFTSS5kb2N1bWVudExpc3Qge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG4gICAgdmFyIGRyYWdnYWJsZSA9ICh3aW5kb3cgYXMgYW55KS5kcmFnZ2FibGU7XHJcbiAgICB2YXIgY3JlYXRlSGVhZGVyTWFya3VwID0gZnVuY3Rpb24gKGRvY3VtZW50SWQsIGRvY3VtZW50TmFtZSkge1xyXG4gICAgICAgIHJldHVybiAnPGRpdj4nICtcclxuICAgICAgICAgICAgJzxkaXYgY2xhc3M9XCJwYW5lbC1oZWFkaW5nXCI+JyArXHJcbiAgICAgICAgICAgICc8aDQgY2xhc3M9XCJwYW5lbC10aXRsZVwiPicgK1xyXG4gICAgICAgICAgICAnPGEgaHJlZj1cIiMnICsgZG9jdW1lbnRJZCArICdcIj4nXHJcbiAgICAgICAgICAgICsgZG9jdW1lbnROYW1lICsgJzwvYT48L2g0PjwvZGl2PjwvZGl2Pic7XHJcbiAgICB9O1xyXG4gICAgJChmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgd2luZG93LnNldFRpbWVvdXQoZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICAvL3NldHVwRHJhZ2dhYmxlRGlhbG9ncyA9IGZ1bmN0aW9uICgpIHtcclxuICAgICAgICAgICAgLy8gICAgdmFyIGVuYWJsZURyYWdnaW5nID0gZnVuY3Rpb24gKGUsIGg/KSB7XHJcbiAgICAgICAgICAgIC8vICAgICAgICB2YXIgaGFuZGxlID0gJChlKS5maW5kKCcuaGFuZGxlJylbMF07XHJcbiAgICAgICAgICAgIC8vICAgICAgICBkcmFnZ2FibGUoZSwgaGFuZGxlKTtcclxuICAgICAgICAgICAgLy8gICAgICAgIGUuc3R5bGUucG9zaXRpb24gPSAnJztcclxuICAgICAgICAgICAgLy8gICAgfTtcclxuICAgICAgICAgICAgLy8gICAgLy8gTWFrZSBhbGwgY29uZmlybSBkZWxldGUgbW9kYWxzIGRyYWdnYWJsZS5cclxuICAgICAgICAgICAgLy8gICAgJCgnW2lkXj1jb25maXJtLWRlbGV0ZS1tb2RhbF0nKS50b0FycmF5KCkuZm9yRWFjaChlbmFibGVEcmFnZ2luZyk7XHJcbiAgICAgICAgICAgIC8vICAgIC8vIE1ha2UgdGhlIG1hbmFnZSBkb2N1bWVudHMgbW9kYWwgZHJhZ2dhYmxlLlxyXG4gICAgICAgICAgICAvLyAgICB2YXIgZHJhZ2dhYmxlRGlhbG9nID0gJCgnI21hbmFnZS1kb2N1bWVudHMtbW9kYWwnKTtcclxuICAgICAgICAgICAgLy8gICAgdmFyIGRyYWdIYW5kbGUgPSBkcmFnZ2FibGVEaWFsb2cuZmluZCgnLmhhbmRsZScpWzBdO1xyXG4gICAgICAgICAgICAvLyAgICBpZiAoZHJhZ2dhYmxlRGlhbG9nWzBdIHx8IGRyYWdIYW5kbGUpIHtcclxuICAgICAgICAgICAgLy8gICAgICAgIGVuYWJsZURyYWdnaW5nKGRyYWdnYWJsZURpYWxvZ1swXSwgZHJhZ0hhbmRsZSk7XHJcbiAgICAgICAgICAgIC8vICAgIH1cclxuICAgICAgICAgICAgLy99O1xyXG4gICAgICAgICAgICAvL3NldHVwRHJhZ2dhYmxlRGlhbG9ncygpO1xyXG4gICAgICAgICAgICB2YXIgJHRhYnMgPSAkKCcuZG9jdW1lbnQtdmlld2VyLXRhYi1oZWFkaW5nJyk7XHJcbiAgICAgICAgICAgIHZhciAkbGlzdEl0ZW1SZWZzID0gJCgnLmRvY3VtZW50LWxpc3QtaXRlbSA+IGEnKTtcclxuICAgICAgICAgICAgdmFyIGNsaWNrID0gZnVuY3Rpb24gKGV2ZW50KSB7XHJcbiAgICAgICAgICAgICAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgICAgICAgICAgICAgdmFyICRsaXN0SXRlbSA9ICQodGhpcyk7XHJcbiAgICAgICAgICAgICAgICB2YXIgJGVsZW1lbnQgPSAkKGV2ZW50LnRhcmdldCk7XHJcbiAgICAgICAgICAgICAgICAvL3ZhciBkb2N1bWVudE5hbWUgPSAkZWxlbWVudFswXS50ZXh0O1xyXG4gICAgICAgICAgICAgICAgdmFyIGRvY3VtZW50TmFtZSA9ICRlbGVtZW50WzBdLmlubmVyVGV4dDtcclxuICAgICAgICAgICAgICAgIHZhciBkb2N1bWVudElkID0gJGVsZW1lbnQubmV4dCgnLml0ZW0taWQtaGlkZGVuJylbMF0uaW5uZXJUZXh0LnRyaW0oKTtcclxuICAgICAgICAgICAgICAgIGNvbnNvbGUubG9nKCdjbGlja2VkIE5hbWU6ICcgKyBkb2N1bWVudE5hbWUgKyAnLCBJZDogJyArIGRvY3VtZW50SWQpO1xyXG4gICAgICAgICAgICAgICAgdmFyICRwYXJlbnRMaXN0SXRlbSA9ICQoJChldmVudC50YXJnZXQpLnBhcmVudCgpKTtcclxuICAgICAgICAgICAgICAgIHZhciAkcHJvZ3Jlc3MgPSAkcGFyZW50TGlzdEl0ZW0uZmluZCgnLnByb2dyZXNzIGhpZGRlbicpXHJcbiAgICAgICAgICAgICAgICAgICAgLmZpbmQoJy5wcm9ncmVzcy1iYXInKVxyXG4gICAgICAgICAgICAgICAgICAgIC5yZW1vdmVDbGFzcygnLmhpZGRlbicpLmNzcygnd2lkdGgnLCAnMTAwJyk7XHJcbiAgICAgICAgICAgICAgICAkLmdldCgnQW5hbHlzaXMvJyArIGRvY3VtZW50SWQpLmRvbmUoZnVuY3Rpb24gKGRhdGEsIHN0YXR1cywgeGhyKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgdmFyIGhlYWRlck1hcmt1cCA9ICQoY3JlYXRlSGVhZGVyTWFya3VwKGRvY3VtZW50SWQsIGRvY3VtZW50TmFtZSkpO1xyXG4gICAgICAgICAgICAgICAgICAgIHZhciBwYW5lbE1hcmt1cCA9XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICQoYDxkaXYgaWQ9XCIke2RvY3VtZW50SWR9XCI+XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgIDxkb2N1bWVudC12aWV3ZXItZGlyZWN0aXZlIGRvY3VtZW50PVwiZG9jdW1lbnRcIj48L2RvY3VtZW50LXZpZXdlci1kaXJlY3RpdmU+PC9kaXY+PC9kaXY+YCk7XHJcbiAgICAgICAgICAgICAgICAgICAgaWYgKCEkKCcjJyArIGRvY3VtZW50SWQpLmxlbmd0aCkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAkKCcjYWNjb3JkaW9uJykuYXBwZW5kKGhlYWRlck1hcmt1cCkuYXBwZW5kKHBhbmVsTWFya3VwKTtcclxuXHJcbiAgICAgICAgICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgJCgnIycgKyBkb2N1bWVudElkKS5yZW1vdmUoKTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgJCgnI2FjY29yZGlvbicpLmFwcGVuZChwYW5lbE1hcmt1cCk7XHJcbiAgICAgICAgICAgICAgICAgICAgfVxyXG4gICAgICAgICAgICAgICAgICAgIHhoci5wcm9ncmVzcygnMTAwJScpO1xyXG4gICAgICAgICAgICAgICAgICAgIC8vTEFTSS5idWlsZE1lbnVzKCk7XHJcbiAgICAgICAgICAgICAgICAgICAgLy9lbmFibGVBY3RpdmVIaWdobGlnaHRpbmcoKTtcclxuICAgICAgICAgICAgICAgIH0pLmZhaWwoZnVuY3Rpb24gKHhociwgbWVzc2FnZSwgZGV0YWlsKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgbG9nKG1lc3NhZ2UpO1xyXG4gICAgICAgICAgICAgICAgfSkucHJvZ3Jlc3MoZnVuY3Rpb24gKGRhdGEpIHtcclxuICAgICAgICAgICAgICAgICAgICAkcHJvZ3Jlc3MuY3NzKCd3aWR0aCcsIGRhdGEpO1xyXG4gICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgIH07XHJcbiAgICAgICAgICAgICRsaXN0SXRlbVJlZnMuY2xpY2soY2xpY2spO1xyXG4gICAgICAgICAgICAvLyR0YWJzLmNsaWNrKGNsaWNrKTtcclxuICAgICAgICB9LCAxMDAwKTtcclxuICAgIH0pO1xyXG59IiwiLy8vIDxyZWZlcmVuY2UgcGF0aD1cIi4uLy4uLy4uL3R5cGluZ3MvanF1ZXJ5L2pxdWVyeS5kLnRzXCIgLz5cclxuLy8vIDxyZWZlcmVuY2UgcGF0aD1cIi4uL2xhc2kudHNcIiAvPlxyXG4oZnVuY3Rpb24gKGFwcCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG4gICAgYXBwLnZhbGlkYXRlRmlsZUV4dGVuc2lvbiA9IChmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgdmFyIGFjY2VwdGVkID0gT2JqZWN0LmZyZWV6ZShbJy50eHQnLCAnLmRvY3gnLCAnLnBkZicsICdkb2MnXSk7XHJcbiAgICAgICAgcmV0dXJuIGZ1bmN0aW9uIChmaWxlTmFtZSkge1xyXG4gICAgICAgICAgICB2YXIgZXh0ZW5zaW9uID0gZmlsZU5hbWUuc3Vic3RyaW5nKGZpbGVOYW1lLmxhc3RJbmRleE9mKCcuJykpLnRvTG93ZXJDYXNlKCk7XHJcbiAgICAgICAgICAgIHJldHVybiBhY2NlcHRlZC5zb21lKGZ1bmN0aW9uIChleHQpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiBleHQgPT09IGV4dGVuc2lvbjtcclxuICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgfTtcclxuICAgIH0pKCk7XHJcbiAgICAkKGZ1bmN0aW9uICgpIHtcclxuICAgICAgICB2YXIgJHVwbG9hZExpc3QgPSAkKCcjZG9jdW1lbnQtdXBsb2FkLWxpc3QnKTtcclxuICAgICAgICBhcHAuJHVwbG9hZExpc3QgPSAkdXBsb2FkTGlzdDtcclxuICAgICAgICAkKGRvY3VtZW50KVxyXG4gICAgICAgICAgICAuZmluZCgnLmJ0bi1maWxlIDpmaWxlJylcclxuICAgICAgICAgICAgLmNoYW5nZShmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgIHZhciAkaW5wdXQgPSAkKHRoaXMpLFxyXG4gICAgICAgICAgICAgICAgZmlsZUNvdW50ID0gJHVwbG9hZExpc3QuZmluZCgnc3Bhbi5maWxlLWluZGV4JykubGVuZ3RoLFxyXG4gICAgICAgICAgICAgICAgZmlsZXMgPSAoPEhUTUxJbnB1dEVsZW1lbnQ+JGlucHV0WzBdKS5maWxlcztcclxuICAgICAgICAgICAgYXBwLmZpbGVzID0gZmlsZXM7XHJcbiAgICAgICAgICAgIGFwcC5maWxlQ291bnQgPSBmaWxlQ291bnQ7XHJcbiAgICAgICAgICAgIHZhciBnZW5lcmF0ZVVwbG9hZExpc3RJdGVtTWFya3VwID0gZnVuY3Rpb24gKGZpbGUsIGluZGV4KSB7XHJcbiAgICAgICAgICAgICAgICByZXR1cm4gJzxkaXYgY2xhc3M9XCJsaXN0LWdyb3VwLWl0ZW0gdGV4dC1sZWZ0XCI+JyArXHJcbiAgICAgICAgICAgICAgICAgICAgJzxzcGFuIGNsYXNzPVwiZ2x5cGhpY29uIGdseXBoaWNvbi1yZW1vdmUgcmVtb3ZlLWZpbGVcIi8+JyArXHJcbiAgICAgICAgICAgICAgICAgICAgJyZuYnNwOyZuYnNwOyZuYnNwOzxzcGFuIGNsYXNzPVwiZmlsZS1pbmRleFwiPicgK1xyXG4gICAgICAgICAgICAgICAgICAgIFtmaWxlQ291bnQsIGluZGV4LCAxXS5zdW0oKSArXHJcbiAgICAgICAgICAgICAgICAgICAgJzwvc3Bhbj4mbmJzcDsmbmJzcDsmbmJzcDsmbmJzcDs8c3BhbiBjbGFzcz1cImZpbGUtbmFtZVwiPicgK1xyXG4gICAgICAgICAgICAgICAgICAgIGZpbGUubmFtZSArICc8L3NwYW4+PC9kaXY+JztcclxuICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgICAgIGxhYmVsID0gJGlucHV0LnZhbCgpLnJlcGxhY2UoL1xcXFwvZywgJy8nKS5yZXBsYWNlKC9bLl0qXFwvLywgJycpO1xyXG4gICAgICAgICAgICAkKGZpbGVzKS5maWx0ZXIoKGluZGV4LCBlbGVtZW50KSA9PiBhcHAudmFsaWRhdGVGaWxlRXh0ZW5zaW9uKCg8SFRNTElucHV0RWxlbWVudD5lbGVtZW50KS5uYW1lKSlcclxuICAgICAgICAgICAgICAgIC50b0FycmF5KClcclxuICAgICAgICAgICAgICAgIC5maWx0ZXIoZnVuY3Rpb24gKGZpbGUpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiAhKCR1cGxvYWRMaXN0LmNoaWxkcmVuKCdzcGFuJykudG9BcnJheSgpLnNvbWUoZnVuY3Rpb24gKCkge1xyXG4gICAgICAgICAgICAgICAgICAgIHJldHVybiAkKHRoaXMpLnRleHQoKSA9PT0gZmlsZS5uYW1lO1xyXG4gICAgICAgICAgICAgICAgfSkpO1xyXG4gICAgICAgICAgICB9KS5mb3JFYWNoKGZ1bmN0aW9uIChmaWxlLCBpbmRleCkge1xyXG4gICAgICAgICAgICAgICAgJHVwbG9hZExpc3QuYXBwZW5kKGdlbmVyYXRlVXBsb2FkTGlzdEl0ZW1NYXJrdXAoZmlsZSwgaW5kZXgpKTtcclxuICAgICAgICAgICAgICAgICQoJ3NwYW4uZ2x5cGhpY29uLmdseXBoaWNvbi1yZW1vdmUucmVtb3ZlLWZpbGUnKVxyXG4gICAgICAgICAgICAgICAgICAgIC5jbGljayhmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgJCh0aGlzKS5yZW1vdmVEYXRhKGZpbGUubmFtZSlcclxuICAgICAgICAgICAgICAgICAgICAgICAgLnBhcmVudCgpXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIC5wYXJlbnQoKVxyXG4gICAgICAgICAgICAgICAgICAgICAgICAuZmluZCgnc3Bhbi5maWxlLW5hbWUnKVxyXG4gICAgICAgICAgICAgICAgICAgICAgICAuZmlsdGVyKGZ1bmN0aW9uICgpIHsgcmV0dXJuICQodGhpcykudGV4dCgpID09PSBmaWxlLm5hbWU7IH0pXHJcbiAgICAgICAgICAgICAgICAgICAgICAgIC5lYWNoKGZ1bmN0aW9uICgpIHsgcmV0dXJuICQodGhpcykucGFyZW50KCdkaXYnKS5yZW1vdmUoKTsgfSk7XHJcbiAgICAgICAgICAgICAgICAgICAgJHVwbG9hZExpc3QuZmluZCgnc3Bhbi5maWxlLWluZGV4JylcclxuICAgICAgICAgICAgICAgICAgICAgICAgLmVhY2goZnVuY3Rpb24gKGluZGV4KSB7ICQodGhpcykudGV4dChpbmRleCArIDEpOyB9KTtcclxuICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgJGlucHV0LnRyaWdnZXIoJ2ZpbGVzZWxlY3QnLCBbZmlsZXMgJiYgZmlsZXMubGVuZ3RoLCBsYWJlbF0pO1xyXG4gICAgICAgIH0pO1xyXG4gICAgfSk7XHJcbn0pKExBU0kpOyIsIm5hbWVzcGFjZSBMQVNJLndpZGdldHMge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgIHByb2Nlc3NpbmdUYXNrUHJvZ3Jlc3NCYXIuJGluamVjdCA9IFtdO1xyXG4gICAgZnVuY3Rpb24gcHJvY2Vzc2luZ1Rhc2tQcm9ncmVzc0JhcigpOiBhbmd1bGFyLklEaXJlY3RpdmUge1xyXG4gICAgICAgIHJldHVybiB7XHJcbiAgICAgICAgICAgIHJlc3RyaWN0OiAnRScsXHJcbiAgICAgICAgICAgIHNjb3BlOiB7XHJcbiAgICAgICAgICAgICAgICB0YXNrOiAnPSdcclxuICAgICAgICAgICAgfSxcclxuICAgICAgICAgICAgdGVtcGxhdGVVcmw6ICcvYXBwL3dpZGdldHMvcHJvY2Vzc2luZy10YXNrLXByb2dyZXNzLWJhci5kaXJlY3RpdmUuaHRtbCdcclxuICAgICAgICB9O1xyXG4gICAgfVxyXG5cclxuICAgIGFuZ3VsYXJcclxuICAgICAgICAubW9kdWxlKCd3aWRnZXRzJylcclxuICAgICAgICAuZGlyZWN0aXZlKHsgcHJvY2Vzc2luZ1Rhc2tQcm9ncmVzc0JhciB9KTtcclxufSIsIi8vbW9kdWxlIExBU0kuZG9jdW1lbnRWaWV3ZXIge1xyXG4vLyAgICAndXNlIHN0cmljdCc7XHJcblxyXG4vLyAgICBleHBvcnQgaW50ZXJmYWNlIElEb2N1bWVudE1vZGVsU2VydmljZSB7XHJcbi8vICAgICAgICBwcm9jZXNzRG9jdW1lbnQoZG9jdW1lbnRJZDogc3RyaW5nKTogbW9kZWxzLklEb2N1bWVudE1vZGVsO1xyXG4vLyAgICB9XHJcblxyXG4vLyAgICBjbGFzcyBNb2NrRG9jdW1lbnRNb2RlbFNlcnZpY2UgaW1wbGVtZW50cyBJRG9jdW1lbnRNb2RlbFNlcnZpY2Uge1xyXG4vLyAgICAgICAgc3RhdGljICRpbmplY3Q6IHN0cmluZ1tdID0gWyckcmVzb3VyY2UnXTtcclxuLy8gICAgICAgIGRvY3VtZW50U291cmNlOiBuZy5yZXNvdXJjZS5JUmVzb3VyY2VDbGFzczxtb2RlbHMuSURvY3VtZW50TW9kZWw+O1xyXG4vLyAgICAgICAgY29uc3RydWN0b3IocHJpdmF0ZSAkcmVzb3VyY2U6IG5nLnJlc291cmNlLklSZXNvdXJjZVNlcnZpY2UpIHtcclxuLy8gICAgICAgICAgICB0aGlzLmRvY3VtZW50U291cmNlID0gJHJlc291cmNlPG1vZGVscy5JRG9jdW1lbnRNb2RlbD4oJ0FuYWx5c2lzLzpkb2N1bWVudElkJyk7XHJcbi8vICAgICAgICB9XHJcbi8vICAgICAgICBwcm9jZXNzRG9jdW1lbnQoZG9jdW1lbnRJZDogc3RyaW5nKSB7XHJcbi8vICAgICAgICAgICAgcmV0dXJuIHRoaXMuJHJlc291cmNlPG1vZGVscy5JRG9jdW1lbnRNb2RlbD4oJ3Rlc3RzL3Rlc3QtZGF0YS9kb2MuanNvbicpLmdldCgpO1xyXG4vLyAgICAgICAgfVxyXG4vLyAgICB9XHJcblxyXG4vLyAgICBhbmd1bGFyXHJcbi8vICAgICAgICAubW9kdWxlKCdkb2N1bWVudFZpZXdlcicpXHJcbi8vICAgICAgICAuc2VydmljZSgnTW9ja0RvY3VtZW50TW9kZWxTZXJ2aWNlJywgTW9ja0RvY3VtZW50TW9kZWxTZXJ2aWNlKTtcclxuLy99IiwiLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwiYW5ndWxhclwiIC8+XHJcbi8vLyA8YW1kLWRlcGVuZGVuY3kgcGF0aD1cImFuZ3VsYXItcmVzb3VyY2VcIiAvPlxyXG4vLy8gPGFtZC1kZXBlbmRlbmN5IHBhdGg9XCJhbmd1bGFyLWJvb3RzdHJhcFwiIC8+XHJcbi8vLyA8YW1kLWRlcGVuZGVuY3kgcGF0aD1cImFuZ3VsYXItYm9vdHN0cmFwLWNvbnRleHRtZW51XCIgLz5cclxuLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwiYW5ndWxhci1maWxlLXVwbG9hZFwiIC8+XHJcbi8vLyA8YW1kLWRlcGVuZGVuY3kgcGF0aD1cImpxdWVyeVwiIC8+XHJcbi8vLyA8YW1kLWRlcGVuZGVuY3kgcGF0aD1cImFwcC91dGlsaXRpZXMvYXVnbWVudGF0aW9uc1wiXHJcblxyXG5pbXBvcnQgeyBtb2R1bGUsIGJvb3RzdHJhcCB9IGZyb20gJ2FuZ3VsYXInO1xyXG5pbXBvcnQgJ2FwcC91dGlsaXRpZXMvYXVnbWVudGF0aW9ucyc7XHJcbmltcG9ydCBkZWJ1ZyBmcm9tICcuL2FwcC9kZWJ1Zy9kZWJ1Zy5tb2R1bGUnO1xyXG5pbXBvcnQgd2lkZ2V0cyBmcm9tICcuL2FwcC93aWRnZXRzL3dpZGdldHMubW9kdWxlJztcclxuaW1wb3J0IGRvY3VtZW50TGlzdCBmcm9tICcuL2FwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QubW9kdWxlJztcclxuaW1wb3J0IGRvY3VtZW50VXBsb2FkIGZyb20gJy4vYXBwL2RvY3VtZW50LXVwbG9hZC9kb2N1bWVudC11cGxvYWQubW9kdWxlJztcclxuaW1wb3J0IGRvY3VtZW50Vmlld2VyIGZyb20gJy4vYXBwL2RvY3VtZW50LXZpZXdlci9kb2N1bWVudC12aWV3ZXIubW9kdWxlJztcclxuaW1wb3J0IGRvY3VtZW50Vmlld2VyU2VhcmNoIGZyb20gJy4vYXBwL2RvY3VtZW50LXZpZXdlci9zZWFyY2gvc2VhcmNoLm1vZHVsZSc7XHJcblxyXG5mdW5jdGlvbiByZWdpc3RlcihtOiBBbmd1bGFyTW9kdWxlT3B0aW9ucykge1xyXG4gICAgZnVuY3Rpb24gdmFsaWRhdGUoKSB7XHJcbiAgICAgICAgaWYgKCFtLm5hbWUpIHsgdGhyb3cgbmV3IFR5cGVFcnJvcihcIm5hbWUgaXMgcmVxdWlyZWRcIik7IH1cclxuICAgICAgICBpZiAoIW0ucmVxdWlyZXMpIHsgdGhyb3cgbmV3IFR5cGVFcnJvcihcInJlcXVpcmVzIG11c3QgYmUgYW4gYXJyYXkuIERpZCB5b3UgaW50ZW5kIHRvIGludm9rZSB0aGUgc2V0dGVyP1wiKTsgfVxyXG4gICAgfVxyXG4gICAgbW9kdWxlKG0ubmFtZSwgbS5yZXF1aXJlcywgbS5jb25maWdGbiB8fCAoKCkgPT4geyB9KSlcclxuICAgICAgICAucHJvdmlkZXIobS5wcm92aWRlcnMgfHwge30pXHJcbiAgICAgICAgLmZhY3RvcnkobS5mYWN0b3JpZXMgfHwge30pXHJcbiAgICAgICAgLnNlcnZpY2UobS5zZXJ2aWNlcyB8fCB7fSlcclxuICAgICAgICAuZmlsdGVyKG0uZmlsdGVycyB8fCB7fSlcclxuICAgICAgICAuY29udHJvbGxlcihtLmNvbnRyb2xsZXJzIHx8IHt9KVxyXG4gICAgICAgIC5kaXJlY3RpdmUobS5kaXJlY3RpdmVzIHx8IHt9KVxyXG4gICAgICAgIC52YWx1ZShtLnZhbHVlcyB8fCB7fSlcclxuICAgICAgICAuY29uc3RhbnQobS5jb25zdGFudHMgfHwge30pXHJcbiAgICAgICAgLnJ1bihtLnJ1bkZuIHx8ICgoKSA9PiB7IH0pKTtcclxufVxyXG5leHBvcnQgZnVuY3Rpb24gYW5ndWxhckJvb3RzdHJhcCgpIHtcclxuICAgIGZ1bmN0aW9uIHJlZ2lzdGVyQWxsKC4uLm1vZHVsZXM6IEFuZ3VsYXJNb2R1bGVPcHRpb25zW10pIHtcclxuICAgICAgICBtb2R1bGVzLmZvckVhY2gocmVnaXN0ZXIpO1xyXG4gICAgfVxyXG5cclxuICAgIHJldHVybiBmdW5jdGlvbiAoKSB7XHJcbiAgICAgICAgcmVnaXN0ZXJBbGwoZGVidWcsIHdpZGdldHMsIGRvY3VtZW50TGlzdCwgZG9jdW1lbnRVcGxvYWQsIGRvY3VtZW50Vmlld2VyLCBkb2N1bWVudFZpZXdlclNlYXJjaCk7XHJcbiAgICAgICAgYm9vdHN0cmFwKGRvY3VtZW50LmJvZHksIFsnZG9jdW1lbnRMaXN0J10pO1xyXG4gICAgfTtcclxufSJdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==
