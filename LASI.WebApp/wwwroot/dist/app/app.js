var LASI;
(function () {
    LASI = LASI || function () {
        'use strict';
        var log = console.log.bind(console), editor = $('#free-editor').change(log);
        return {
            log: log,
            editor: editor
        };
    }();
})();


//(function (log) {
//    $(function () {
//        var debugPanel = $('#debug-panel');
//        window.setInterval(function () {
//            $.getJSON('api/Tasks', { cache: false })
//               .then(function (tasks) {
//                   debugPanel.html(tasks.map(function (task) {
//                       return "<div>" + Object.keys(task).map(function (key) {
//                           return "<span>&nbsp&nbsp" + task[key] + "</span>";
//                       }) + "</div>";
//                   }).join());
//               });
//        }, 800);
//    });
//}(LASI.log));
/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */

(function () {
    'use strict';
    /** Adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
     * flatMap takes an array of items, projects each one to an array,using the provided
     * collectionSelector function,
     * and flattens the resulting array of arrays onto a single flat array.
     * A second, optional function performs a projection on each element of the resulting flat array.
     * This function is analogous to SelectMany in C#, and flatMap in Scala.
     */
    if (!Array.prototype.hasOwnProperty('flatMap')) {
        Array.prototype.flatMap = function (arraySelector, elementSelector) {
            arraySelector = arraySelector || function (array) {
                return array instanceof Array && array;
            };
            elementSelector = elementSelector || function (element) { return element; };
            return this.reduce(function (array, a) {
                return array.concat(arraySelector(a).map(elementSelector));
            }, []);
        };
    }
    /** Adds the correlate function to  Array.prototype, endowing all
     * Arrays instances with it.
     * correlate takes pairs the two arrays of elements based on
     * the key selector functions, and returns the associated elements as a new array.
     * An optional function performs a projection on each pair.
     * This function is analogous to Join in C#, and inner join in SQL.
     */
    if (!Array.prototype.hasOwnProperty('correlate')) {
        Array.prototype.correlate = function (inner, outerKeySelector, innerKeySelector, resultSelector) {
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
        };
    }
    if (!Array.prototype.hasOwnProperty('sum')) {
        Array.prototype.sum = function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || function (x) { return Number(x); };
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        };
    }
    if (!Array.prototype.hasOwnProperty('average')) {
        Array.prototype.average = function (valueSelector) {
            return this.sum(valueSelector || function (x) { return Number(x); }) / this.length;
        };
    }
}());
(function () {
    'use strict';
    $(function () {
        $('#cancel-profile-edits').click(function () {
            document.location.href = 'http://' + document.domain + ':' + document.location.port;
        });
    });
}());
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
                var subjects = menu.subjects,
                    directObjects = menu.directObjects,
                    indirectObjects = menu.indirectObjects;
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
                        } else {
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
(function (app) {
    app.enableActiveHighlighting = (function () {
        'use strict';
        var onReady = function () {
            var phrasalTextSpans = $('span.phrase'),
                highlightClass = 'active-phrase-highlight',
                recolor = function () {
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
        $(onReady);
        return onReady;
    }());
}(LASI));
// Load the Visualization API and the piechart package.
//    google.load('visualization', '1.0', { 'packages': ['corechart'] });
//    $('#myTab a').click(function (event) {
//        event.preventDefault();
//        $(this).tab('show');
//    });
//    var drawChart = function (targetElement, rowData) {
//        return function () {
//            // Create the data table.
//            var dataTable = new google.visualization.DataTable({
//                cols: [
//                    { id: 'Entity', type: 'string' },
//                    { id: 'Weight', type: 'string' }
//                ]
//            });
//            dataTable.addRows(rowData);
//            // Set chart options
//            var options = {
//                title: 'Top Results',
//                width: 500,
//                height: 500
//            };
//            // Instantiate and draw our chart, passing in some options.
//            var chart = new google.visualization.BarChart(targetElement);
//            chart.draw(dataTable, options);
//        };
//    };
//    var charts = $('.chart-container');
//    charts.each(function () {
//        var element = $(this);
//        var rowData = $.parseJSON($(element.children('span')[0]).text());
//        google.setOnLoadCallback(drawChart(element, rowData));
//    });
//}());


/* global: LASI: true */
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
    }());
    $(function () {
        var $uploadList = $('#document-upload-list');
        app.$uploadList = $uploadList;
        $(document).find('.btn-file :file')
           .change(function () {
               var $input = $(this),
                   fileCount = $uploadList.find('span.file-index').length,
                   files = $input[0].files;
               app.files = files;
               app.fileCount = fileCount;
               var generateUploadListItemMarkup = function (file, index) {
                   return '<div class="list-group-item text-left">' +
                       '<span class="glyphicon glyphicon-remove remove-file"/>' +
                       '&nbsp;&nbsp;&nbsp;<span class="file-index">' +
                       [fileCount, index, 1].sum() +
                       '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">' +
                       file.name + '</span></div>';
               },
               label = $input.val().replace(/\\/g, '/').replace(/[.]*\//, '');
               $(files).filter(function () { return app.validateFileExtension($(this)[0].name); })
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
}(LASI));
/* global: draggable, enableDragging */
(function (app) {
    'use strict';
    var log = app.log;
    var createHeaderMarkup = function (documentId, documentName) {
        return '<div class="panel panel-default">' +
            '<div class="panel-heading">' +
            '<h4 class="panel-title">' +
            '<a href="#' + documentId +
            '" data-toggle="collapse" data-parent="#accordion">' + documentName +
            '</a></h4></div></div>';
    };
    $(function () {
        app.setupDraggableDialogs = function () {
            var enableDragging = function (e) {
                var handle = $(e).find('.handle')[0];
                draggable(e, handle);
                e.style.position = '';
            };
            // Make all confirm delete modals draggable.
            $("[id^=confirm-delete-modal]").toArray().forEach(enableDragging);
            // Make the manage documents modal draggable.
            var draggableDialog = $('#manage-documents-modal');
            var dragHandle = draggableDialog.find('.handle')[0];
            if (draggableDialog[0] || dragHandle) {
                enableDragging(draggableDialog[0], dragHandle);
            }
        };
        app.setupDraggableDialogs();
        $('.document-list-item > a').click(function (event) {
            event.preventDefault();
            var $listItem = $(this);
            var $element = $(event.target);
            var documentName = $element[0].text;
            var documentId = $element.next()[0].textContent.trim();
            log('clicked Name: ' + documentName + ', Id: ' + documentId);
            var $parentListItem = $($(event.target).parent());
            var $progress = $parentListItem
                .find('.progress hidden')
                .find('.progress-bar')
                .removeClass('.hidden').css('width', '100');
            var contentRequest = $.get('Results/' + documentId).done(function (data, status, xhr) {
                var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                var panelMarkup = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + data + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(headerMarkup).append(panelMarkup);
                }
                else {
                    $('#' + documentId).remove();
                    $('#accordion').append(panelMarkup);
                }
                xhr.progress('100%');
                app.buildMenus();
                app.enableActiveHighlighting();
            }).fail(function (xhr, message, detail) {
                log(message);
            }).progress(function (data) {
                $progress.css('width', data);
            });
        });
    });
}(LASI));

(function () {
    'use strict';
    if (!LASI) {
        return;
    }
    else {
        LASI.documentList = LASI.documentList || { ngName: 'documentListApp' };
    }
})();

'use strict';
(function () {
    angular
        .module(LASI.documentList.ngName, ['ngResource', 'ui.bootstrap', 'ngFileUpload'])
        .config(configure);
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(5000)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(3)
            .setDocumentListUrl('api/UserDocuments/List');
    }
})();

/// <reference path = "IDocumentListItem.d.ts" />
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
            getDocumentList: function () {
                return resource.get();
            },
            deleteDocument: function (documentId) {
                return resource.delete({ documentId: documentId });
            }
        };
    };
    return DocumentListServiceProvider;
})();
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('documentListService', DocumentListServiceProvider);
})();

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

(function (app) {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .service('resultsService', resultsService);
    resultsService.$inject = ['$http', '$q', '$log'];
    function resultsService($http, $q, $log) {
        /* jshint validthis:true */
        this.tasks = [];
        var that = this;
        this.processDocument = function (documentId, documentName) {
            that.tasks[documentId] = { percentComplete: 0 };
            var deferred = $q.defer();
            $http.get('Results/' + documentId)
                .success(success)
                .error(error);
            return deferred.promise;
            function success(data) {
                var markupHeader = $('<div class="panel panel-default">' +
                    '<div class="panel-heading"><h4 class="panel-title"><a href="#' + documentId + '" data-toggle="collapse" data-parent="#accordion">' +
                    documentName +
                    '</a></h4></div></div>');
                var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + JSON.stringify(data) + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(markupHeader).append(markupPanel);
                }
                else {
                    $('#' + documentId).remove();
                    $('#accordion').append(markupPanel);
                }
                app.buildMenus();
                app.enableActiveHighlighting();
                that.tasks[documentId].percentComplete = 100;
                deferred.resolve('success');
            }
            function error(xhr, message, detail) {
                deferred.reject(message);
            }
        };
    }
})(LASI);

(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .directive('documentListMenuItem', documentListMenuItem);
    documentListMenuItem.$inject = ['$window', 'resultsService'];
    function documentListMenuItem($window, resultsService) {
        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/document-list-menu-item.html',
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
})();

(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .directive('documentListTabsetItem', documentListTabsetItem);
    documentListTabsetItem.$inject = [];
    function documentListTabsetItem() {
        var directive = {
            transclude: true,
            replace: true,
            restrict: 'E',
            link: link,
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '='
            },
            templateUrl: '/app/widgets/document-list-app/document-list-tabset-item.html',
        };
        return directive;
        function link(scope, element, attrs) {
            console.log(attrs);
        }
    }
})();

var tasksListServiceProvider = function () {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';
    var provider = {
        setUpdateInterval: function (milliseconds) {
            updateInterval = milliseconds;
            return this;
        }, setTasksListUrl: function (url) {
            tasksListUrl = url;
            return this;
        },
        $get: function ($resource, $window) {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource(tasksListUrl, {}, {
                'get': {
                    method: 'GET', isArray: true
                }
            });
            var tasks = [];
            var update = function () {
                tasks = Tasks.get();
                updateDebugInfo(tasks);
            };
            var getActiveTasks = function () {
                $window.setInterval(update, updateInterval);
                return tasks;
            };
            return {
                getActiveTasks: getActiveTasks
            };
        }
    };
    provider.$get.$inject = ['$resource', '$window'];
    return provider;
    function createDebugInfoUpdator(element) {
        return function (tasks) { return element.html(tasks
            .map(function (task) { return "<div>" +
            Object.keys(task).map(function (key) { return "<span>&nbsp&nbsp" + task[key] + "</span>"; }) +
            "</div>"; })
            .join()); };
    }
}();
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('tasksListService', tasksListServiceProvider);
})();

(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .controller('ListController', ListController);
    ListController.$inject = ['$q', '$log', '$rootScope', 'Upload', 'documentListService', 'tasksListService', 'DocumentsService'];
    function ListController($q, $log, $rootScope, Upload, documentListService, tasksListService, DocumentsService) {
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
            $rootScope.upload = function (files) {
                if (files && files.length) {
                    for (var i = 0; i < files.length; i++) {
                        var file = files[i];
                        Upload.upload({
                            url: 'api/UserDocuments',
                            file: file,
                            method: 'POST',
                            fileName: file.name
                        }).progress(progress)
                            .success(success);
                    }
                }
            };
            vm.deleteById = function (id) {
                var deleteResult = DocumentsService.deleteById(id);
                $log.info(deleteResult);
                vm.documents = documentListService.getDocumentList();
            };
            $q.all({
                deferredDocumentList: documentListService.getDocumentList(),
                deferredTaskStatusList: tasksListService.getActiveTasks()
            }).then(function (promises) {
                vm.documents = promises.deferredDocumentList;
                vm.documents.correlate(promises.deferredTaskStatusList, function (document) { return document.id; }, function (task) { return task.id; })
                    .forEach(function (documentWithTask) {
                    var document = documentWithTask.first, task = documentWithTask.second;
                    document.showProgress = task.state === "Ongoing" || task.state === "Complete";
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });
                vm.tasks = {};
                promises.deferredTaskStatusList.forEach(function (task) { vm.tasks[task.id] = task; });
            });
            function progress(evt) {
                var progressPercentage = 100.0 * evt.loaded / evt.total;
                $rootScope.log = 'progress: ' + progressPercentage + '% ' + evt.config.file.name + '\n' + $rootScope.log;
            }
            function success(data, status, headers, config) {
                $rootScope.log = 'file ' + config.file.name + 'uploaded. Response: ' + JSON.stringify(data) + '\n' + $rootScope.log;
                $rootScope.$apply();
            }
        }
    }
})();

//# sourceMappingURL=app.js.map