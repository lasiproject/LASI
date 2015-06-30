module LASI.documentViewer.search {
    'use strict';

    angular
        .module('documentViewer.search')
        .directive('textSearch', textSearch);
    textSearch.$inject = [];

    function textSearch(): ng.IDirective {
        return {
            scope: {
                find: '=',
                searchContext: '=',
                onFound: '@',
                onContextChanged: '@'
            },
            bindToController: true,
            controllerAs: 'search',
            controller,
            link
        };
        controller.$inject = ['$q', '$interval', '$timeout', '$window'];
        var toFind: models.ILexicalModel = {
            id: 1,
            contextmenu: {
                lexicalId: 1
            },
            detailText: '',
            hasContextmenuData: true, style: { cssClass: 'lexical' },
            text: ''
        };
        controller(null, null, null, null, null)
            .search(toFind, [], { lifted: true })
            .then(results => log(results[0]));
        function controller($scope: ng.IScope, $q: ng.IQService, $interval: ng.IIntervalService, $timeout: ng.ITimeoutService, $window: ng.IWindowService): ITextSearchController {
            return {
                matchedModels: [],
                matchedTexts: [],
                search<TFor>(find: TFor, options: ITextSearchOptions<TFor>) {
                    var deferred = $q.defer<TFor>();
                    $timeout(() => {
                        deferred.resolve();
                    }, 0)
                    return deferred.promise;
                }
            };
        }
        function link(scope: ITextSearchScope, element: ng.IAugmentedJQuery, attrs: ITextSearchAttributes, controller: ITextSearchController) {
            [scope, element, attrs, controller].filter(e => !!e).forEach(log);
            let find = scope.find;
            let searchContext = scope.searchContext;
            if (!(searchContext instanceof Array)) {
                let context = [<models.ITextFragmentModel>searchContext];

                if (typeof find === 'string') {
                    controller.search(find, context).then(data=> log(data[0]));

                } else {
                    controller.search(find, context).then(data=> log(data[0].detailText));
                }
            }
        }
    }
    interface ITextSearchAttributes extends ng.IAttributes {
        find: string;
        onFound: string;
        searchContext: string;
        onContextChanged: string;
    }
    interface ITextSearchScope extends ng.IScope {
        find: string | models.ILexicalModel;
        searchContext: models.ITextFragmentModel | models.ITextFragmentModel[];
        onFound: (searched: string | models.ILexicalModel) => void;
        onContextChanged: (newContext: models.ITextFragmentModel[]) => void;
    }

    export interface ITextSearchController {
        search<TFor>(find: TFor, searchContext: models.ITextFragmentModel[], options?: ITextSearchOptions<TFor>): ng.IPromise<TFor[]>;
        matchedModels: models.ILexicalModel[];
        matchedTexts: string[];
    }
    export interface ITextSearchOptions<TFor> {
        lifted?: boolean;
    }
}