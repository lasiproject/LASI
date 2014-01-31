var elementsById = new Map();

;
$(function () {
    $('.lexical-content-block#td').map(function (e) {
        return elementsById[e.id];
    }).each(function (e) {
        e.contextMenu = e.associations.each(function (a) {
            return {
                text: 'a',
                action: function () {
                    a.text += a;
                }
            };
        });
    });
});
//# sourceMappingURL=Index.js.map
