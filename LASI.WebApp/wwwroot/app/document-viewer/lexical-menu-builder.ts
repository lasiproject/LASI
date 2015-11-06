'use strict';

type ContextMenu = angular.ui.bootstrap.contextMenu.ContextMenu;

lexicalMenuBuilder.$inject = [];

export function lexicalMenuBuilder(): LexicalMenuBuilderFactory {
    var [buildForVerbal, buildForReferencer] = [createForVerbalMenuBuilder({}), createForReferencerMenuBuilder({})];

    return {
        buildAngularMenu: source =>
            referencerMenuIsViable(<ReferencerContextmenuData>source) ?
                buildForReferencer(<ReferencerContextmenuData>source) :
                verbalMenuIsViable(<VerbalContextmenuData>source) ?
                    buildForVerbal(<VerbalContextmenuData>source) :
                    undefined
    };

    function verbalMenuIsViable(menu: VerbalContextmenuData) {
        return !!(menu && (menu.directObjectIds || menu.indirectObjectIds || menu.subjectIds));
    }
    function referencerMenuIsViable(menu: ReferencerContextmenuData) {
        return !!(menu && menu.refersToIds);
    }

}
function createForReferencerMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
    var resetReferencerAsssotionCssClasses = () =>
        Object.keys(menuActionTargets)
            .map(key => menuActionTargets[key])
            .forEach($e => $e.removeClass('referred-to-by-current'));
    return (source: ReferencerContextmenuData): ContextMenu => [
        ['View Referred To', (itemScope, event) => {
            resetReferencerAsssotionCssClasses();
            source.refersToIds.forEach(id => menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'));
        }]
    ];
}
function createForVerbalMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
    return (function (verbalMenuCssClassMap: { [mapping: string]: string }) {
        return (source: VerbalContextmenuData) => {
            var menuItems: ContextMenu = [];
            if (source.subjectIds) {
                menuItems.push(['View Subjects', (itemScope, event) => {
                    resetVerbalAssociationCssClasses();
                    source.subjectIds
                        .forEach(id => {
                            menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Subjects']);
                        });
                }]);
            }
            if (source.directObjectIds) {
                menuItems.push(['View Direct Objects', (itemScope, event) => {
                    resetVerbalAssociationCssClasses();
                    source.directObjectIds
                        .forEach(id => menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Direct Objects']));
                }]);
            }
            if (source.indirectObjectIds) {
                menuItems.push(['View Indirect Objects', (itemScope, event) => {
                    resetVerbalAssociationCssClasses();
                    source.indirectObjectIds.forEach(id => {
                        menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                    });
                }]);
            }
            return menuItems;
        };
        function resetVerbalAssociationCssClasses() {
            Object.keys(menuActionTargets)
                .map(key => menuActionTargets[key])
                .forEach($e =>
                    Object.keys(verbalMenuCssClassMap)
                        .map((k: string): string => verbalMenuCssClassMap[k])
                        .forEach(cssClass => $e.removeClass(cssClass)));
        }
    })({
        'View Subjects': 'subject-of-current',
        'View Direct Objects': 'direct-object-of-current',
        'View Indirect Objects': 'indirect-object-of-current'
    });
}
export interface LexicalMenuBuilderFactory {
    buildAngularMenu: (source: LexicalContextmenuData | VerbalContextmenuData | ReferencerContextmenuData) => ContextMenu;
}

export interface LexicalContextmenuData {
    /**
    * The id of the lexical element for which the menu is defined.
    */
    lexicalId: string | number;
}
export interface VerbalContextmenuData extends LexicalContextmenuData {
       
    /**
     * The ids of any subjects.
     */
    subjectIds: number[];
    /**
     * The ids of any direct objects.
     */
    directObjectIds: number[];
    /**
     * The ids of any direct objects.
     */
    indirectObjectIds: number[];
}

export interface ReferencerContextmenuData extends LexicalContextmenuData {
    /**
     * The id of the referencer for which the menu is defined.
     */
    lexicalId: number;
    /**
     * The ids of any entities the referred to.
     */
    refersToIds: number[];
}