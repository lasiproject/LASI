$('#results a').click(function (e) {
    'use strict';
    e.preventDefault()
    $(this).tab('show')
})

$(function () {
    'use strict';
    $.contextMenu.types.label = function (item, opt, root) {

        // this === item.$node
        var htmlMenuText =
        '<ul class="menuwrapper phrasemenu">';
        //item.$node.arrayProducer().forEach(function (element, index) {
        //    htmlMenuText += '<li class="phrasemenuitem" title="' + element.Name, element.LinkedIds + '">' + element.Name, element.LinkedIds + '</li>';
        //});
        htmlMenuText +=
         + '</ul>';
        $(htmlMenuText)
        .appendTo(this)
        .on('click', 'li', function () {
            // do some funky stuff
            console.log('Clicked on ' + $(this).text());
            // hide the menu
            root.$menu.trigger('contextmenu:hide');
        });

        this.addClass('labels').on('contextmenu:focus', function (e) {
            // setup some awesome stuff
        }).on('contextmenu:blur', function (e) {
            // tear down whatever you did
        }).on('keydown', function (e) {
            // some funky key handling, maybe?
        });
    };



    var splitProps = function (elementId) {
        var d = $.parseJSON($("#" + elementId).children('span').text());
        var n = Object.keys(d);
        return {
            data: d,
            name: n
        };
    };

    $('span.Verbal').each(function (index, element) {
        var i, itemsd = {};
        var splitUp = splitProps(element.id);
        for (i = 0; i < splitUp.length; i += 1) {
            itemsd["element" + i] = { name: "Edit" + i, callback: $.noop };
        }
        $.contextMenu({
            selector: '#' + element.id,
            callback: function (key, options) {
                var m = "clicked: " + key; alert(m);
            },
            items: itemsd
            //label: { type: "label" },
            //edit: { name: "Edit", callback: $.noop },

        });
    });
});
