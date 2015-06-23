/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    'use strict';
    var arrayAgumentations = [
        {
            name: 'flatMap',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (arraySelector, elementSelector) {
                    arraySelector = arraySelector || function (array) {
                        return array instanceof Array && array;
                    };
                    elementSelector = elementSelector || function (element) { return element; };
                    return this.reduce(function (array, a) {
                        return array.concat(arraySelector(a).map(elementSelector));
                    }, []);
                }
            }
        }, {
            name: 'correlate',
            value: {
                writable: false, enumerable: false, configurable: false, value: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
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
                }
            }
        }, {
            name: 'sum',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (valueSelector) {
                    // If the a valueSelector was not provided, define a function which will attempt 
                    // to convert its argument to a number.
                    var projection = valueSelector || (function (x) { return Number(x); });
                    return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
                }
            }
        },
        {
            name: 'average',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (valueSelector) {
                    return this.sum(valueSelector || (function (x) { return Number(x); })) / this.length;
                }
            }
        }
    ];
    arrayAgumentations
        .filter(function (property) { return !Object.prototype.hasOwnProperty.call(Array.prototype, property.name); })
        .forEach(function (property) { return Object.defineProperty(Array.prototype, property.name, property.value); });
})();
var LASI;
(function (LASI) {
    'use strict';
    LASI.buildMenus;
    LASI.enableActiveHighlighting;
    LASI.setupDraggableDialogs;
    LASI.log = console.log.bind(console);
    LASI.editor = $('#free-editor').change(LASI.log);
    var documentList;
    (function (documentList) {
        documentList.moduleName = 'documentList';
    })(documentList = LASI.documentList || (LASI.documentList = {}));
    var documentViewer;
    (function (documentViewer) {
        documentViewer.moduleName = 'documentViewer';
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module(LASI.documentViewer.moduleName, [
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
    var documentList;
    (function (documentList) {
        'use strict';
        angular.module(LASI.documentList.moduleName, [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu',
            'ngFileUpload',
            LASI.documentViewer.moduleName
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
/// <reference path = '../../typings/angular-bootstrap-contextMenu/angular-bootstrap-contextMenu.d.ts'/>
var ContextMenuTests;
(function (ContextMenuTests) {
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
})(ContextMenuTests || (ContextMenuTests = {}));
/// <reference path="../../../typings/jquery/jquery.d.ts" />
(function (log) {
    $(function () {
        var debugPanel = $('#debug-panel');
        var visible = true;
        var toggleButton = $('#toggle-debug-panel');
        toggleButton.click(function () {
            visible = !visible;
            debugPanel.toggle();
            toggleButton.text(visible ? 'hide' : 'show')
                .toggleClass('btn-danger')
                .toggleClass('btn-info');
        });
        window.setInterval(function () {
            $.getJSON('api/Tasks', { cache: false })
                .then(function (tasks) {
                debugPanel.html(tasks.map(function (task) {
                    return "<div>" + Object.keys(task).map(function (key) {
                        return "<span>&nbsp&nbsp" + task[key] + "</span>";
                    }) + "</div>";
                }).join());
            });
        }, 800);
    });
}(console.log.bind(console)));
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
                this.activate();
            }
            DocumentController.prototype.processDocument = function (documentId) {
                return this.documentModelService.processDocument(documentId);
            };
            DocumentController.prototype.activate = function () {
                //this.documentModel = this.documentModelService.getData();
            };
            DocumentController.$inject = ['DocumentModelService', '$location'];
            return DocumentController;
        })();
        angular
            .module(LASI.documentViewer.moduleName)
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
            DocumentModelService.prototype.getData = function () {
                return this.$resource('tests/test-data/doc.json').get();
            };
            DocumentModelService.prototype.processDocument = function (documentId) {
                return this.documentSource.get({ documentId: documentId });
            };
            DocumentModelService.$inject = ['$resource'];
            return DocumentModelService;
        })();
        angular
            .module(documentViewer.moduleName)
            .service('DocumentModelService', DocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        directive.$inject = ['$window'];
        function directive($window) {
            function link(scope, element, attrs) {
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
            .module(documentViewer.moduleName)
            .directive('Directive', directive);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        document.$inject = ['$window'];
        function document($window) {
            var link = function (scope, element, attrs) {
                console.log(scope);
                console.log(element);
                console.log(attrs);
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/document.html',
                replace: true,
                scope: {
                    document: '='
                },
                link: link
            };
            //return new Document('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);
        }
        angular
            .module(LASI.documentViewer.moduleName)
            .directive('document', document);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        lexicalMenuBuilder.$inject = [];
        function lexicalMenuBuilder() {
            function verbalMenuIsViable(m) {
                return !!(m && (m.directObjectIds || m.indirectObjectIds || m.subjectIds));
            }
            function referencerMenuIsViable(m) {
                return !!(m && m.refersToIds);
            }
            var buildAngularMenuForReferencer = (function (menuActionTargets) {
                var resetCssClasses = function () { return Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); }); };
                return function (source) {
                    return [
                        [
                            'View Referred To',
                            function (s, e) {
                                resetCssClasses();
                                source.refersToIds.forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass('referred-to-by-current');
                                });
                            }
                        ]
                    ];
                };
            })({});
            var buildAngularMenuForVerbal = (function (menuActionTargets) {
                var verbalMenuTextToElementsMap = {
                    'View Subjects': 'subjects',
                    'View Direct Objects': 'directObjects',
                    'View Indirect Objects': 'indirectObjects'
                };
                var verbalMenuCssClassMap = {
                    'View Subjects': 'subject-of-current',
                    'View Direct Objects': 'direct-object-of-current',
                    'View Indirect Objects': 'indirect-object-of-current'
                };
                var resetCssClasses = function () { return Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) {
                    Object.keys(verbalMenuCssClassMap)
                        .map(function (k) { return verbalMenuCssClassMap[k]; })
                        .forEach(function (cssClass) { return $e.removeClass(cssClass); });
                }); };
                return function (source) {
                    var menuItems = [];
                    if (source.subjectIds) {
                        menuItems.push([
                            'View Subjects',
                            function (s, e) {
                                resetCssClasses();
                                source.subjectIds
                                    .forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Subjects']);
                                });
                            }
                        ]);
                    }
                    if (source.directObjectIds) {
                        menuItems.push([
                            'View Direct Objects',
                            function (s, e) {
                                resetCssClasses();
                                source.directObjectIds
                                    .forEach(function (id) { return menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Direct Objects']); });
                            }
                        ]);
                    }
                    if (source.indirectObjectIds) {
                        menuItems.push([
                            'View Indirect Objects',
                            function (s, e) {
                                resetCssClasses();
                                source.indirectObjectIds.forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                                });
                            }
                        ]);
                    }
                    return menuItems;
                };
            })({});
            var buildAngularMenu = function (m) {
                if (referencerMenuIsViable(m)) {
                    return buildAngularMenuForReferencer(m);
                }
                else if (verbalMenuIsViable(m)) {
                    return buildAngularMenuForVerbal(m);
                }
            };
            return {
                buildAngularMenu: buildAngularMenu
            };
        }
        angular
            .module(documentViewer.moduleName)
            .factory('lexicalMenuBuilder', lexicalMenuBuilder);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        phrase.$inject = ['lexicalMenuBuilder'];
        function phrase(lexicalMenuBuilder) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/lexical/phrase.html',
                link: function (scope, element, attrs) {
                    var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
                    scope.phrase.hasContextmenu = !!contextmenu;
                    if (scope.phrase.hasContextmenu) {
                        scope.phrase.contextmenu = contextmenu;
                    }
                },
                scope: {
                    phrase: '=',
                    parentId: '='
                }
            };
        }
        angular.module('documentViewer').directive('phrase', phrase);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
    'use strict';
    var VerbalContextController = (function () {
        function VerbalContextController() {
            this.title = 'VerbalContextController';
        }
        //constructor(private $state: ng.ui.IStateService) {
        //    console.log($state);
        //    this.activate();
        //}
        VerbalContextController.prototype.activate = function () {
        };
        VerbalContextController.$inject = ['$state'];
        return VerbalContextController;
    })();
    angular
        .module(LASI.documentViewer.moduleName)
        .controller('VerbalContextController', VerbalContextController);
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            var link = function (scope, element, attrs) {
                //console.log(scope.parentId);
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
            .module(LASI.documentViewer.moduleName)
            .directive('paragraph', paragraph);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.html',
                link: function (scope, element, attrs) {
                    //console.log(attrs);
                },
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
        angular.module(LASI.documentViewer.moduleName).directive('sentence', sentence);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
/* global:draggable */
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var draggable = window.draggable;
        var log = LASI.log;
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
                    // Make all confirm delete modals draggable.
                    $('[id^=confirm-delete-modal]').toArray().forEach(enableDragging);
                    // Make the manage documents modal draggable.
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
                    //var documentName = $element[0].text;
                    var documentName = $element[0].innerText;
                    var documentId = $element.find('span.hidden')[0].textContent.trim();
                    log('clicked Name: ' + documentName + ', Id: ' + documentId);
                    var $parentListItem = $($(event.target).parent());
                    var $progress = $parentListItem.find('.progress hidden')
                        .find('.progress-bar')
                        .removeClass('.hidden').css('width', '100');
                    $.get('Analysis/' + documentId).done(function (data, status, xhr) {
                        var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                        var panelMarkup = $("<div id=\"" + documentId + "\">\n                           <document document=\"document\"></document></div></div>");
                        if (!$('#' + documentId).length) {
                            $('#accordion').append(headerMarkup).append(panelMarkup);
                        }
                        else {
                            $('#' + documentId).remove();
                            $('#accordion').append(panelMarkup);
                        }
                        xhr.progress('100%');
                        LASI.buildMenus();
                        LASI.enableActiveHighlighting();
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
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module(LASI.documentList.moduleName)
            .directive('documentListMenuItem', documentListMenuItem);
        documentListMenuItem.$inject = ['$window', 'resultsService'];
        function documentListMenuItem($window, resultsService) {
            return {
                transclude: true,
                replace: true,
                restrict: 'E',
                templateUrl: '/app/widgets/document-list/document-list-menu-item.html',
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
            /**
             * @param $resource an instance of the Resource Service supplied by the angular-resource module.
             */
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
            .module(documentList.moduleName)
            .provider('documentListService', DocumentListServiceProvider);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
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
                templateUrl: '/app/widgets/document-list/document-list-tabset-item.html'
            };
        }
        angular
            .module(documentList.moduleName)
            .directive('documentListTabsetItem', documentListTabsetItem);
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module(LASI.documentList.moduleName)
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
            .module(LASI.documentList.moduleName)
            .controller('ListController', ListController);
        ListController.$inject = [
            '$q', '$log', '$rootScope', 'documentListService',
            'tasksListService', 'documentsService', 'DocumentModelService'
        ];
        function ListController($q, $log, $rootScope, documentListService, tasksListService, documentsService, documentModelService) {
            /* jshint validthis:true */
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
                            vm.documents.filter(function (d) { return d.id === documentId; })[0].documentModel = d;
                            if (!$rootScope.$$phase) {
                                $rootScope.$apply();
                            }
                        });
                    });
                };
                $q.all([
                    $q.when(documentListService.getDocumentList()),
                    $q.when(tasksListService.getActiveTasks(function (tasks) { return tasks.forEach(function (task) {
                        vm.tasks[task.id] = task;
                        vm.documents.filter(function (d) { return d.name === task.name; })[0].task = task;
                    }); }))
                ]).then(function (promises) {
                    vm.documents = promises[0];
                    vm.documents.correlate(promises[1], function (document) { return document.id; }, function (task) { return task.id; })
                        .forEach(function (documentWithTask) {
                        var document = documentWithTask.first, task = documentWithTask.second;
                        document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                        document.progress = Math.round(task.percentComplete);
                        document.statusMessage = task.statusMessage;
                    });
                    vm.tasks = {};
                    promises[1].forEach(function (task) { vm.tasks[task.id] = task; });
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
            .module(documentList.moduleName)
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
        var tasksListServiceProvider = function () {
            var updateInterval = 200;
            var tasksListUrl = 'api/Tasks';
            $get.$inject = ['$resource', '$window'];
            return {
                $get: $get,
                setUpdateInterval: function (milliseconds) {
                    updateInterval = milliseconds;
                    return this;
                }, setTasksListUrl: function (url) {
                    tasksListUrl = url;
                    return this;
                }
            };
            function $get($resource, $window) {
                var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
                var Tasks = $resource(tasksListUrl, { cache: false }, {
                    'get': {
                        method: 'GET', isArray: true
                    }
                });
                var tasks = [];
                var update = function () {
                    tasks = Tasks.get();
                    updateDebugInfo(tasks);
                };
                var getActiveTasks = function (callback) {
                    $window.setInterval(function () {
                        callback(tasks);
                        update();
                    }, updateInterval);
                    return tasks;
                };
                return {
                    getActiveTasks: getActiveTasks
                };
            }
            function createDebugInfoUpdator(element) {
                return function (tasks) { return element.html(tasks
                    .map(function (task) { return '<div>' +
                    Object.keys(task).map(function (key) { return '<span>&nbsp&nbsp' + task[key] + '</span>'; }) +
                    '</div>'; })
                    .join()); };
            }
        }();
        (function () {
            'use strict';
            angular
                .module(documentList.moduleName)
                .provider('tasksListService', tasksListServiceProvider);
        })();
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var UploadController = (function () {
            function UploadController($scope, $log, uploadService) {
                $scope.files = [];
                $scope.uploadFile = function (file) { return uploadService.upload({
                    url: 'api/UserDocuments',
                    file: file,
                    method: 'POST',
                    fileName: file.name
                }).progress(progress).success(success); };
                $scope.uploadFiles = function (files) { return (files || []).map($scope.uploadFile); };
                $scope.$watch('files', $scope.uploadFiles);
                function progress(evt) {
                    var progressPercentage = 100.0 * evt.loaded / evt.total;
                    $log.info("Progress: " + progressPercentage + "% " + evt.config.file.name);
                }
                function success(data, status, headers, config) {
                    $log.info("File '" + config.file.name + " 'uploaded. Response: " + JSON.stringify(data));
                    //$rootScope.$apply();
                }
            }
            UploadController.$inject = ['$scope', '$log', 'Upload'];
            return UploadController;
        })();
        angular
            .module(documentList.moduleName)
            .controller('UploadController', UploadController);
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
        $(document).find('.btn-file :file')
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
            })
                .forEach(function (file, index) {
                $uploadList.append(generateUploadListItemMarkup(file, index));
                $('span.glyphicon.glyphicon-remove.remove-file')
                    .click(function () {
                    $(this).removeData(file.name);
                    $(this).parent().parent().find('span.file-name')
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