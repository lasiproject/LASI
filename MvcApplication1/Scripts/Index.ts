///<reference path="typings\jquery\jquery.d.ts"/>
///<reference path="typings\jqueryui\jqueryui.d.ts"/>
///<reference path="typings\jquery.contextMenu\jquery.contextMenu.d.ts"/>

module HomeIndex {
    interface Lexical {
        associations: Lexical[];
        text: string;
        domNode: HTMLElement;
    };
    var elementsById = new Map<string, Lexical>();
    $(".doNowButton").click("click", function myfunction() {
        window.navigate("Document");
    });
    $(".lexical-content-block#td").toArray()
        .map((lex: Lexical) => elementsById[lex.domNode.id])
        .forEach((lex: Lexical) => {
            //a.domNode.getc
            lex.associations.map(associated=> {
                return {
                    text: "a",
                    callback: () => associated.text += associated
                };
            });
        });
}
