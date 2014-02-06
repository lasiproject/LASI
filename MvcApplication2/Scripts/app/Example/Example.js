

//var numbser = [0, 1, 2, 3, 4, 5, 6, 7];

//var squaredEvents = numbers.filter(function (element) { return element % 2 == 0; })
//                           .map(function (element) { return element * element; });

//var squaredEvent = numbers.Where(element => element % 2 == 0)
//                          .Select(element => element * element);






































(function (i) {
    var removePunctuationAndWhiteSpace = function (str) {
        return $.trim(str.replace(/[\]]|[/[\][)]|[(]|[}]|[{]|[.]|[,]|[?]|[!]/, ''));
    };
    $(function () {
        //$('span').menu();

        var lineLength = 0;

        $('span.Verbal').contextmenu('open', function (e) {
            var id = 'options' + (i).toString();

            e.preventDefault();
            $('.ui-menu-icons').each(function (i, e) {
                e.style.display = 'none';
            });
            // for sanity
            // Object.keys(e).forEach(function (k) {
            // console.log("key: " + k + " value: " + e[k.toString()]);
            // });

            i += 1;
            $('<ul id=' + id +
                ' position:absolute; style="list-style-type:none;  border-top-left-radius: 10px; border-top-right-radius: 10px;' +
                ' border-bottom-right-radius: 10px; border-bottom-left-radius: 10px;padding:2px; ' +
                ' background-color:rgb(245,245,245); position: absolute;' +
                ' left: ' + e.clientX + 'px; top: ' + e.clientY + 'px; display: inline; visibility: visible;"' +
                ' class="ui-menu ui-widget ui-widget-content' +
                ' ui-corner-all ui-menu-icons" role="menu" tabindex="0">' +
                ' <li class="ui-menu-item" role="presentation">' +
				' <a href="#action1"' +
                ' id="ui-id-9" class="ui-corner-all" tabindex="-1" role="menuitem">' +
                ' <span class="ui-icon custom-icon-firefox"></span>Action 1</a>' +
                ' </li><li class="ui-menu-item" role="presentation">' +
                ' <a href="#action2" id="ui-id-10" class="ui-corner-all" tabindex="-1" role="menuitem">' +
                ' <span class="ui-icon ui-icon-heart"></span>Action 2</a></li>' +
                ' <li class="ui-state-disabled ui-menu-item"' +
                ' role="presentation" aria-disabled="true">' +
                ' <a href="#action3" id="ui-id-11" class="ui-corner-all" tabindex="-1" role="menuitem">Action 3</a></li>' +
                ' </ul></li></ul>').appendTo('body');

            $('.ui-menu-item').click(function (e) {
                var searchText = removePunctuationAndWhiteSpace($('#textspan' + (i).toString()).text());
                e.preventDefault();
                // just for sanity
                alert(e.target.text + " " + searchText);

                $('span').filter(function (xx, ee) {
                    var context = JSON.parse($.trim($("#context" + i.toString())[0].innerText));
                    return context.$values.some(function (emnt) {

                        return emnt.$ElementId === ee.id;

                    });
                }).each(function (evyt) { evyt.preventDefault(); ($this).css('background-color', 'teal'); });
            });
            // Context menu will dissappear when any area of the page is left clicked. This covers clicking on the menu itself.
            $(document).click(function (e) {
                e.preventDefault();
                $('#' + id).css('display', 'none');
            });
        });
    });
}(0));