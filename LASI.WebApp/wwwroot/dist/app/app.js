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
            return resultSelector ? results.map(function (e) {
                return resultSelector(e.first, e.second);
            }) : results;
        },
        sum: function (valueSelector) {
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
var LASI;
(function (LASI) {
    'use strict';
    LASI.buildMenus;
    LASI.enableActiveHighlighting;
    LASI.setupDraggableDialogs;
    LASI.log = console.log.bind(console);
    LASI.editor = $('#free-editor').change(LASI.log);
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular.module('documentList', [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu',
            'ngFileUpload',
            'documentViewer',
            'debug'
        ]).config(configure);
        configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
        function configure(tasksListServiceProvider, documentListServiceProvider) {
            tasksListServiceProvider
                .setUpdateInterval(100)
                .setTasksListUrl('api/Tasks');
            documentListServiceProvider
                .setRecentDocumentCount(3)
                .setDocumentListUrl('api/UserDocuments/List');
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer', [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu'
        ]).config(configure);
        configure.$inject = [];
        function configure() {
        }
        ;
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var upload;
    (function (upload) {
        'use strict';
        angular.module('uploads', [
            'ngFileUpload'
        ]);
    })(upload = LASI.upload || (LASI.upload = {}));
})(LASI || (LASI = {}));
/// <reference path="../../../typings/jquery/jquery.d.ts" />
(function () {
    'use strict';
    $(function () {
        $('#cancel-profile-edits').click(function () {
            document.location.href = "http://" + document.domain + ":" + document.location.port;
        });
    });
}());
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
/// <reference path='../../../typings/jquery/jquery.d.ts' />
/// <reference path='../../../typings/angularjs/angular.d.ts' />
var LASI;
(function (LASI) {
    var debug;
    (function (debug) {
        'use strict';
        DebugPanelController.$inject = ['tasksListService'];
        function DebugPanelController(tasksListService) {
            var _this = this;
            tasksListService.getActiveTasks().then(function (data) { return _this.tasks = data.sort(function (x, y) { return x.id.localeCompare(y.id); }); });
            this.show = false;
            this.getbuttonText = function () { return _this.show ? 'Hide' : 'Show'; };
            this.toggle = function () { return _this.show = !_this.show; };
        }
        angular
            .module('debug', [])
            .controller('DebugPanelController', DebugPanelController);
    })(debug = LASI.debug || (LASI.debug = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .directive('documentListMenuItem', documentListMenuItem);
        documentListMenuItem.$inject = ['$window', 'resultsService'];
        function documentListMenuItem($window, resultsService) {
            return {
                transclude: true,
                replace: true,
                restrict: 'E',
                templateUrl: '/app/document-list/document-list-menu-item.html',
                scope: {
                    name: '=',
                    documentId: '='
                },
                link: function (scope, element, attrs, ctrl) {
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
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
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
            DocumentListServiceProvider.prototype.$get = function ($resource) {
                var resource = $resource(this.documentListUrl + '/' + this.recentDocumentCount, {}, {
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
                    getDocumentList: resource.get
                };
            };
            ;
            return DocumentListServiceProvider;
        })();
        angular
            .module('documentList')
            .provider('documentListService', DocumentListServiceProvider);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .directive('documentListTabsetItem', documentListTabsetItem);
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
                templateUrl: '/app/document-list/document-list-tabset-item.html'
            };
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .factory('documentsService', documentsService);
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
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .controller('ListController', ListController);
        ListController.$inject = [
            '$q', '$log', '$rootScope', 'documentListService',
            'tasksListService', 'documentsService', 'DocumentModelService'
        ];
        function ListController($q, $log, $rootScope, documentListService, tasksListService, documentsService, documentModelService) {
            var vm = this;
            vm.title = 'ListController';
            vm.documents = [];
            Object.defineProperty(vm, 'documentCount', {
                get: function () { return vm.documents.length; },
                enumerable: true,
                configurable: false
            });
            vm.expanded = false;
            activate();
            function activate() {
                vm.deleteById = function (id) {
                    var deleteResult = documentsService.deleteById(id);
                    $log.info(deleteResult);
                    vm.documents = documentListService.getDocumentList();
                };
                vm.processDocument = function (documentId) {
                    var deferred = $q.defer();
                    $q.when(documentModelService.processDocument(documentId)).then(function (data) {
                        $q.when(data).then(function (d) {
                            deferred.resolve(d);
                            vm.documents.filter(function (d) { return d.id === documentId; })[0].documentModel = d;
                            if (!$rootScope.$$phase) {
                                $rootScope.$apply();
                            }
                        });
                    });
                    return deferred.promise;
                };
                vm.documents = documentListService.getDocumentList();
                vm.tasks = tasksListService.getActiveTasks(function (tasks) { return tasks.map(function (task) {
                    vm.tasks[task.id] = task;
                    return (vm.documents.filter(function (d) { return d.name === task.name; })[0] || {}).task = task;
                }); });
                $q.all([vm.documents, vm.tasks]).then(function (data) {
                    var _a = data, documents = _a[0], tasks = _a[1];
                    var associated = documents.correlate(tasks, function (document) { return document.id; }, function (task) { return task.id; }, function (document, task) {
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });
                    tasks.forEach(function (task) { vm.tasks[task.id] = task; });
                });
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .factory('resultsService', resultsService);
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
                    LASI.buildMenus();
                    LASI.enableActiveHighlighting();
                    tasks[documentId].percentComplete = 100;
                    deferred.resolve(data);
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
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .provider('tasksListService', tasksListServiceProvider);
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
                var getActiveTasks = function (callback) {
                    var _this = this;
                    var deferred = $q.defer();
                    $interval(function () {
                        callback && callback(_this.tasks);
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
                return function (tasks) { return element
                    .html(tasks
                    .map(function (task) { return ("<div>" + Object.keys(task).map(function (key) { return ("<span>&nbsp&nbsp" + task[key] + "</span>"); }) + "</div>"); })
                    .join()); };
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var UploadController = (function () {
            function UploadController($scope, uploadService) {
                var _this = this;
                this.uploadService = uploadService;
                this.uploadFiles = function (files) {
                    files.filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                        .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                        .reduce(function (errors, error) { errors.push(error); return errors; }, [])
                        .forEach(LASI.log);
                    return (files || _this.files || []).map(_this.uploadFile);
                };
                this.uploadFile = function (file) {
                    var promise = _this.uploadService.upload({
                        file: file,
                        url: 'api/UserDocuments',
                        method: 'POST',
                        fileName: file.name
                    });
                    promise
                        .progress(_this.progress)
                        .success(_this.success)
                        .error(_this.error);
                    return promise;
                };
                $scope.$watch('files', function (x, y) { return _this.uploadFiles; });
                this.files = [];
            }
            UploadController.prototype.progress = function (event) {
                var progressPercentage = 100.0 * event.loaded / event.total;
                LASI.log("Progress: " + progressPercentage + "% " + event.config.file.name);
            };
            UploadController.prototype.success = function (data, status, headers, config) {
                LASI.log("File " + config.file.name + " uploaded. Response: " + JSON.stringify(data));
            };
            UploadController.prototype.error = function (data, status, headers, config) {
                LASI.log("Http: " + status + " Failed to upload file.\n                Details: " + data);
            };
            UploadController.prototype.removeItem = function (file, index) {
                this.files = this.files.filter(function (f) { return f.name !== file.name; });
                $('#file-upload-list').remove("#upload-list-item-" + index);
            };
            UploadController.$inject = ['$scope', 'Upload'];
            UploadController.formats = [
                'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
                'application/msword',
                'application/pdf',
                'text/plain'
            ];
            UploadController.fileInputAcceptText = UploadController.formats.join(',');
            return UploadController;
        })();
        angular
            .module('documentList')
            .controller('UploadController', UploadController);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentController = (function () {
            function DocumentController(documentModelService, $location) {
                this.documentModelService = documentModelService;
                this.$location = $location;
                this.title = 'DocumentController';
            }
            DocumentController.prototype.processDocument = function (documentId) {
                if (this.documentModel.id !== documentId) {
                    return this.documentModelService.processDocument(documentId);
                }
                else {
                    return this.documentModel;
                }
            };
            DocumentController.$inject = ['MockDocumentModelService', '$location'];
            return DocumentController;
        })();
        angular
            .module('documentViewer')
            .controller('DocumentController', DocumentController);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentModelService = (function () {
            function DocumentModelService($resource) {
                this.$resource = $resource;
                this.documentSource = $resource('Analysis/:documentId');
            }
            DocumentModelService.prototype.processDocument = function (documentId) {
                return this.documentSource.get({ documentId: documentId });
            };
            DocumentModelService.$inject = ['$resource'];
            return DocumentModelService;
        })();
        angular
            .module('documentViewer')
            .service('DocumentModelService', DocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        directive.$inject = [];
        function directive() {
            function link(scope, element, attrs) {
                LASI.log(scope);
                LASI.log(element);
                LASI.log(attrs);
            }
            return {
                restrict: 'E',
                link: link,
                templateUrl: '/app/document-viewer/document-page.html',
                replace: true,
                scope: {
                    page: '=',
                    document: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive('documentPage', directive);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        documentViewerDirective.$inject = [];
        function documentViewerDirective() {
            var link = function (scope, element, attrs) {
                LASI.log(scope);
                LASI.log(element);
                LASI.log(attrs);
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/document-viewer-directive.html',
                replace: true,
                scope: {
                    document: '='
                },
                link: link
            };
        }
        angular
            .module('documentViewer')
            .directive('documentViewerDirective', documentViewerDirective);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .factory('lexicalMenuBuilder', lexicalMenuBuilder);
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
        function createForReferencerMenuBuilder(menuActionTargets) {
            var resetReferencerAsssotionCssClasses = function () {
                return Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); });
            };
            return function (source) { return [
                ['View Referred To', function (itemScope, event) {
                        resetReferencerAsssotionCssClasses();
                        source.refersToIds
                            .forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'); });
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
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .directive('phrase', phrase);
        phrase.$inject = ['lexicalMenuBuilder'];
        function phrase(lexicalMenuBuilder) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/lexical/phrase.html',
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
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            var link = function (scope, element, attrs) {
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/paragraph.html',
                link: link,
                scope: {
                    paragraph: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive('paragraph', paragraph);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .directive('sentence', sentence);
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.html',
                link: function (scope, element, attrs) { },
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var MockDocumentModelService = (function () {
            function MockDocumentModelService($resource) {
                this.$resource = $resource;
                this.documentSource = $resource('Analysis/:documentId');
            }
            MockDocumentModelService.prototype.processDocument = function (documentId) {
                return this.$resource('tests/test-data/doc.json').get();
            };
            MockDocumentModelService.$inject = ['$resource'];
            return MockDocumentModelService;
        })();
        angular
            .module('documentViewer')
            .service('MockDocumentModelService', MockDocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
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
/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../lasi.ts"/>
(function (app) {
    app.enableActiveHighlighting = (function () {
        'use strict';
        var onReady = function () {
            var phrasalTextSpans = $('span.phrase'), highlightClass = 'active-phrase-highlight', recolor = function () {
                phrasalTextSpans.each(function () {
                    $(this).removeClass(highlightClass);
                });
                $(this).addClass(highlightClass);
            };
            phrasalTextSpans.click(recolor);
            phrasalTextSpans.on('contextmenu', recolor);
            $('[data-toggle="tooltip"]').tooltip({
                delay: 250,
                container: 'body'
            });
        };
        $(onReady);
        return onReady;
    }());
}(LASI));
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
                LASI.setupDraggableDialogs = function () {
                    var enableDragging = function (e, h) {
                        var handle = $(e).find('.handle')[0];
                        draggable(e, handle);
                        e.style.position = '';
                    };
                    $('[id^=confirm-delete-modal]').toArray().forEach(enableDragging);
                    var draggableDialog = $('#manage-documents-modal');
                    var dragHandle = draggableDialog.find('.handle')[0];
                    if (draggableDialog[0] || dragHandle) {
                        enableDragging(draggableDialog[0], dragHandle);
                    }
                };
                LASI.setupDraggableDialogs();
                var $tabs = $('.document-viewer-tab-heading');
                var $listItemRefs = $('.document-list-item > a');
                var click = function (event) {
                    event.preventDefault();
                    var $listItem = $(this);
                    var $element = $(event.target);
                    var documentName = $element[0].innerText;
                    var documentId = $element.next('.item-id-hidden')[0].innerText.trim();
                    LASI.log('clicked Name: ' + documentName + ', Id: ' + documentId);
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
                    }).fail(function (xhr, message, detail) {
                        LASI.log(message);
                    }).progress(function (data) {
                        $progress.css('width', data);
                    });
                };
                $listItemRefs.click(click);
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
//# sourceMappingURL=app.js.map