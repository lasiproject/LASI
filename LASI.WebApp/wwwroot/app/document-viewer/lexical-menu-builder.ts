module LASI.documentViewer {
    'use strict';
    export interface ILexicalMenuBuilderFactory {
        buildAngularMenu: (source: IVerbalContextmenu | IReferencerContextmenu) => ui.bootstrap.contextMenu.IContextMenuItem[];
    }

    lexicalMenuBuilder.$inject = [];

    function lexicalMenuBuilder(): ILexicalMenuBuilderFactory {

        function verbalMenuIsViable(m: IVerbalContextmenu) {
            return !!(m && (m.directObjectIds || m.indirectObjectIds || m.subjectIds));
        }
        function referencerMenuIsViable(m: IReferencerContextmenu) {
            return !!(m && m.refersToIds);
        }

        var buildAngularMenuForReferencer = ((menuActionTargets: { [id: string]: JQuery }) => {
            var resetCssClasses = () => Object.keys(menuActionTargets)
                .map(key => menuActionTargets[key])
                .forEach($e => $e.removeClass('referred-to-by-current'));

            return (source: IReferencerContextmenu): ui.bootstrap.contextMenu.IContextMenuItem[] => {
                return [
                    [
                        'View Referred To',
                        (s, e) => {
                            resetCssClasses();
                            source.refersToIds.forEach(id => {
                                menuActionTargets[id] = $(`#${id}`).addClass('referred-to-by-current');
                            });
                        }
                    ]
                ];
            };
        })({});

        var buildAngularMenuForVerbal = (function (menuActionTargets: { [id: string]: JQuery }) {
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
            var resetCssClasses = () => Object.keys(menuActionTargets)
                .map(key => menuActionTargets[key])
                .forEach($e => {
                Object.keys(verbalMenuCssClassMap)
                    .map(k => verbalMenuCssClassMap[k])
                    .forEach((cssClass: string) => $e.removeClass(cssClass));
            });
            return (source: IVerbalContextmenu): ui.bootstrap.contextMenu.IContextMenuItem[] => {
                var menuItems: ui.bootstrap.contextMenu.IContextMenuItem[] = [];
                if (source.subjectIds) {
                    menuItems.push([
                        'View Subjects',
                        (s, e) => {
                            resetCssClasses();
                            source.subjectIds
                                .forEach(id => {
                                menuActionTargets[id] = $(`#${id}`).addClass(verbalMenuCssClassMap['View Subjects']);
                            });
                        }
                    ]);
                }
                if (source.directObjectIds) {
                    menuItems.push([
                        'View Direct Objects',
                        (s, e) => {
                            resetCssClasses();
                            source.directObjectIds
                                .forEach(id => menuActionTargets[id] = $(`#${id}`).addClass(verbalMenuCssClassMap['View Direct Objects']));
                        }
                    ]);
                }
                if (source.indirectObjectIds) {
                    menuItems.push([
                        'View Indirect Objects',
                        (s, e) => {
                            resetCssClasses();
                            source.indirectObjectIds.forEach(id => {
                                menuActionTargets[id] = $(`#${id}`).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                            });
                        }
                    ]);
                }
                return menuItems;
            };
        })({});
        var buildAngularMenu = function (m: IVerbalContextmenu | IReferencerContextmenu): ui.bootstrap.contextMenu.IContextMenuItem[] {
            if (referencerMenuIsViable(<IReferencerContextmenu>m)) {
                return buildAngularMenuForReferencer(<IReferencerContextmenu>m);
            } else if (verbalMenuIsViable(<IVerbalContextmenu>m)) {
                return buildAngularMenuForVerbal(<IVerbalContextmenu>m);
            }
        };
        return {
            buildAngularMenu
        };
    }


    angular
        .module(moduleName)
        .factory('lexicalMenuBuilder', lexicalMenuBuilder);
}