import $ from 'jquery';

import VerbalMenu from 'app/models/verbal-menu';
import { LexicalMenuCore, ReferencerMenu, LexicalMenu, ContextMenuDataSource } from 'app/models/lexical-menu';

export default class LexicalMenuBuilder {
  buildForVerbal = createForVerbalMenuBuilder({});
  buildForReferencer = createForReferencerMenuBuilder({});

  // tslint:disable-next-line:no-any
  build = (source: ContextMenuDataSource) => menuIsReferencerMenu(source)
    ? this.buildForReferencer(source)
    : menuIsVerbalMenu(source)
      ? this.buildForVerbal(source)
      : undefined
}

function menuIsVerbalMenu(menu: LexicalMenu): menu is VerbalMenu {
  const verbalMenu = menu as VerbalMenu;
  return !!(menu && (verbalMenu.directObjectIds || verbalMenu.indirectObjectIds || verbalMenu.subjectIds));
}
function menuIsReferencerMenu(menu: LexicalMenu): menu is ReferencerMenu {
  const referencerMenu = menu as ReferencerMenu;
  return !!(menu && referencerMenu.refersToIds);
}

function createForReferencerMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
  const resetReferencerAsssotionCssClasses = () =>
    Object.keys(menuActionTargets)
      .map(key => menuActionTargets[key])
      .forEach($e => $e.removeClass('referred-to-by-current'));
  return (source: ReferencerMenu) => [
    ['View Referred To', () => {
      resetReferencerAsssotionCssClasses();
      source.refersToIds.forEach(id => menuActionTargets[id] = $('#' + id)
        .addClass('referred-to-by-current'));
    }]
  ];
}

function createForVerbalMenuBuilder(menuActionTargets: { [id: string]: JQuery }) {
  return (function (verbalMenuCssClassMap: { [mapping: string]: string }) {
    return (source: VerbalMenu) => {
      const menuItems = [];
      if (source.subjectIds) {
        menuItems.push(['View Subjects', () => {
          resetVerbalAssociationCssClasses();
          source.subjectIds
            .forEach(id => {
              menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Subjects']);
            });
        }]);
      }
      if (source.directObjectIds) {
        menuItems.push(['View Direct Objects', () => {
          resetVerbalAssociationCssClasses();
          source.directObjectIds
            .forEach(id => menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Direct Objects']));
        }]);
      }
      if (source.indirectObjectIds) {
        menuItems.push(['View Indirect Objects', () => {
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
