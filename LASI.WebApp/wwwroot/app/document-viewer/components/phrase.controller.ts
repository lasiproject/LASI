export default class {
    static $inject = ['lexicalMenuBuilder'];

    constructor(lexicalMenuBuilder: LexicalMenuBuilderFactory) {
        var contextmenu = lexicalMenuBuilder.buildAngularMenu(this.phrase.contextmenu);
        this.phrase.hasContextmenuData = !!contextmenu;
        if (this.phrase.hasContextmenuData) {
            (this.phrase as any).contextmenu = contextmenu;
        }
    }
    phrase: PhraseModel;
    parentId;

} 