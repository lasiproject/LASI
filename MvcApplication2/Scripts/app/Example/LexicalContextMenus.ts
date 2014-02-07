///<reference path=".\..\..\typings\jquery\jquery.d.ts" />
///<reference path=".\..\..\typings\jqueryui\jqueryui.d.ts" />
///<reference path=".\..\..\typings\jquery.contextMenu\jquery.contextMenu.d.ts" />
class PhraseMenuWrapper {
    constructor(syntacticRole: string= "", id: string= "") {
        var embeddedMenuJson = $("#+" + id).children("span").text();
        var jsonData = JSON.parse(embeddedMenuJson);
    }
    menu: JQueryUI.Menu;
    menuItemText: Array<string>;
}

$(() => $("span.Verbal")
    .each((i, e) => $(e)
        .menu(new PhraseMenuWrapper("verbal", e.getAttribute("id")).menuItemText)));