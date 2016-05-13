import {autoinject} from 'aurelia-framework';
declare type ContextMenu = any;

@autoinject export default class LexicalMenuBuilder implements models.LexicalMenuBuilder {
    buildForVerbal = createForVerbalMenuBuilder({});
    buildForReferencer = createForReferencerMenuBuilder({});

    buildAngularMenu = source => menuIsReferencerMenu(source)
        ? this.buildForReferencer(source)
        : menuIsVerbalMenu(source)
            ? this.buildForVerbal(source)
            : undefined;
}

function menuIsVerbalMenu(menu: models.LexicalContextmenuData): menu is models.VerbalContextmenuData {
    const verbalMenu = menu as models.VerbalContextmenuData;
    return !!(menu && (verbalMenu.directObjectIds || verbalMenu.indirectObjectIds || verbalMenu.subjectIds));
}
function menuIsReferencerMenu(menu: models.LexicalContextmenuData): menu is models.ReferencerContextmenuData {
    const referencerMenu = menu as models.ReferencerContextmenuData;
    return !!(menu && referencerMenu.refersToIds);
}

function createForReferencerMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
    const resetReferencerAsssotionCssClasses = () =>
        Object.keys(menuActionTargets)
            .map(key => menuActionTargets[key])
            .forEach($e => $e.removeClass('referred-to-by-current'));
    return (source: models.ReferencerContextmenuData): ContextMenu => [
        ['View Referred To', (itemScope, event) => {
            resetReferencerAsssotionCssClasses();
            source.refersToIds.forEach(id => menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'));
        }]
    ];
}

function createForVerbalMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
    return (function (verbalMenuCssClassMap: { [mapping: string]: string }) {
        return (source: models.VerbalContextmenuData) => {
            const menuItems: ContextMenu = [];
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
                .forEach($e => {
                    Object.keys(verbalMenuCssClassMap)
                        .map(key => verbalMenuCssClassMap[key])
                        .forEach(cssClass => $e.removeClass(cssClass));
                });
        }
    })({
        'View Subjects': 'subject-of-current',
        'View Direct Objects': 'direct-object-of-current',
        'View Indirect Objects': 'indirect-object-of-current'
    });
}