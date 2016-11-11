import $ from 'jquery';

export default function buildMenus() {
  const contextualElementIdSelectors = [];
  const verbalMenuTextToElementsMap = {
    'View Subjects': 'subjects',
    'View Direct Objects': 'directObjects',
    'View Indirect Objects': 'indirectObjects'
  };
  const relationshipCssClassNameMap = {
    'View Subjects': 'subject-of-current',
    'View Direct Objects': 'direct-object-of-current',
    'View Indirect Objects': 'indirect-object-of-current'
  };
  return function () {
    const forVerbal = function (context) {

      const menu = JSON.parse($('#context' + context[0].id).text());
      const subjects = menu.subjects,
        directObjects = menu.directObjects,
        indirectObjects = menu.indirectObjects;
      const temp = {
        subjects: subjects,
        directObjects: directObjects,
        indirectObjects: indirectObjects
      }, result = {};
      Object.keys(temp).filter(key => temp[key]).forEach(key => result[key] = temp[key]);
      return result;
    };

    const forReferencer = context => JSON.parse($('#context' + context[0].id).text());

    $('span.referencer').contextmenu({
      target: '#referencer-context-menu',
      before: function (event, context) {
        const data = forReferencer(context);
        event.target.lexicalContextMenu = data;
        return data.referredTo && data.referredTo.length > 0;
      },
      onItem: function (context) {
        context[0].lexicalContextMenu.referredTo
          .map(id => $('#' + id))
          .forEach($e => $e.css('background-color', 'red'));
      }
    });

    $('span.verbal').contextmenu({
      target: '#verbal-context-menu',
      before: (e, context) => {
        let count = 0;
        const menu = forVerbal(context);
        e.target.lexicalContextMenu = {};
        Object.keys(menu).forEach(function (key) {
          e.target.lexicalContextMenu[key] = menu[key].map(function (id) {
            const idSelector = '#' + id;
            if (!contextualElementIdSelectors.some(e => e === idSelector)) {
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
        const menu = context[0].lexicalContextMenu;
        contextualElementIdSelectors
          .flatMap(e => $(e).toArray(), $)
          .forEach(e => {
            Object.keys(relationshipCssClassNameMap).forEach(key => {
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
}