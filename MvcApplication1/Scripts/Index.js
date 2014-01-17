///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="typings\jqueryui\jqueryui.d.ts"/>
///<reference path="typings\jquery.contextMenu\jquery.contextMenu.d.ts"/>
var HomeIndex;
(function (HomeIndex) {
    ;
    var elementsById = new Map();
    $(".doNowButton").click("click", function myfunction() {
        window.navigate("Document");
    });
    $(".lexical-content-block#td").toArray().map(function (lex) {
        return elementsById[lex.domNode.id];
    }).forEach(function (lex) {
        //a.domNode.getc
        lex.associations.map(function (associated) {
            return {
                text: "a",
                callback: function () {
                    return associated.text += associated;
                }
            };
        });
    });
})(HomeIndex || (HomeIndex = {}));
//# sourceMappingURL=Index.js.map
