
$(function () {
    /**************************************************
     * Custom Command Handler
     **************************************************/
    $.contextMenu.types.label = function (item, opt, root) {
        // this === item.$node
        var htmlMenuText =
        '<ul class="menuwrapper">';
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

    /**************************************************
     * Context-Menu with custom command "label"
     **************************************************/

    var splitProps = function (elementId) {
        var d = $.parseJSON($("#" + elementId).children('span').text());
        var name = Object.keys(d);
        return {
            ElementId:d.$.Verbal,
            Name: name,
            LinkedIds: d[name]
        };
    };

    $('span.Verbal').each(function (index, element) {
        $.contextMenu({
            selector: '#' + element.id,
            callback: function (key, options) {
                var m = "clicked: " + key; alert(m);
            },
            items: {
                //open: { name: "Open", callback: $.noop },
                label: { type: "label" },
                //edit: { name: "Edit", callback: $.noop },
            }
        });
    });
});


//    $.contextMenu({
//        selector: 'span.Verbal',
//        callback: function (key, options) {
//            var m = "clicked: " + key; alert(m);
//        },
//        items: {
//            //open: { name: "Open", callback: $.noop },
//            label: { type: "label", customName: $.parseJSON($("#").children('span').text()) },
//            //edit: { name: "Edit", callback: $.noop },
//        }
//    });
//});

//(function (i) {
//    //var removePunctuationAndWhiteSpace = function (str) {
//    //    return $.trim(str.replace(/[\]]|[/[\][)]|[(]|[}]|[{]|[.]|[,]|[?]|[!]/, ''));
//    //};
//    $(function () {
//        //$('span').menu();

//        //var lineLength = 0;

//        //$()/*.on({
//        //    'contextmenu': function (e) {
//        //        e.preventDefault();
//        //        $("#verbalmenus.menuwrapper").attr({ left: e.clientX + "px", top: e.clientY + "px", display: "inline", visibility: "visible", position: "absolute" });
//        //        //alert(JSON.stringify(x));
//        //        */
//        //    //}
//        ////});//('open', function (data, fn) {



//        //    data.preventDefault();
//        //    $("#verbalmenus.menuwrapper.phrasemenu")
//        //    i += 1;
//        //});
//        // for sanity
//        // Object.keys(e).forEach(function (k) {
//        // console.log("key: " + k + " value: " + e[k.toString()]);
//        // });

//        //i += 1;
//        //var ctx = JSON.parse($.trim($("#context474").text()).replace(/[;]/, ''));
//        //$('<ul id=' + id +
//        //    ' position:absolute; style="list-style-type:none;  border-top-left-radius: 10px; border-top-right-radius: 10px;' +
//        //    ' border-bottom-right-radius: 10px; border-bottom-left-radius: 10px;padding:2px; ' +
//        //    ' background-color:rgb(245,245,245); position: absolute;' +
//        //    ' left: ' + e.clientX + 'px; top: ' + e.clientY + 'px; display: inline; visibility: visible;"' +
//        //    ' class="ui-menu ui-widget ui-widget-content' +
//        //    ' ui-corner-all ui-menu-icons" role="menu" tabindex="0">' +
//        //    ' <li class="ui-menu-item" role="presentation">' +
//        //	' <a href="#' + removePunctuationAndWhiteSpace(ctx.innerText()) + '"' +
//        //    ' id="ui-id-9" class="ui-corner-all" tabindex="-1" role="menuitem">' +
//        //    ' <span class="ui-icon custom-icon-firefox"></span>Action 1</a>' +
//        //    ' </li><li class="ui-menu-item" role="presentation">' +
//        //    ' <a href="#action2" id="ui-id-10" class="ui-corner-all" tabindex="-1" role="menuitem">' +
//        //    ' <span class="ui-icon ui-icon-heart"></span>Action 2</a></li>' +
//        //    ' <li class="ui-state-disabled ui-menu-item"' +
//        //    ' role="presentation" aria-disabled="true">' +
//        //    ' <a href="#action3" id="ui-id-11" class="ui-corner-all" tabindex="-1" role="menuitem">Action 3</a></li>' +
//        //    ' </ul></li></ul>').appendTo('body');

//        //$('.ui-menu-item').click(function (e) {
//        //    var searchText = removePunctuationAndWhiteSpace($('#textspan' + (i).toString()).text());
//        //    e.preventDefault();
//        //    // just for sanity
//        //    alert(e.target.text + " " + searchText);

//        //    $('span').filter(function (xx, ee) {
//        //        var context = JSON.parse($.trim($("#context" + i.toString())[0].innerText));
//        //        return context.$values.some(function (emnt) {

//        //            return emnt.$ElementId === ee.id;

//        //        });
//        //    }).each(function (evyt) { evyt.preventDefault(); $(this).css('background-color', 'teal'); });
//        //});
//        // Context menu will dissappear when any area of the page is left clicked. This covers clicking on the menu itself.
//        //$(document).click(function (e) {
//        //    e.preventDefault();
//        //    $('#' + id).css('display', 'none');
//        //});
//        //});
//    });
//}(0));