var App;
(function (App) {
    var TextualDirective = (function () {
        function TextualDirective(templateUrl, link, parent, scope) {
            this.templateUrl = templateUrl;
            this.link = link;
            this.parent = parent;
            this.scope = scope;
            this.restrict = "E";
            this.replace = true;
        }
        return TextualDirective;
    })();
    App.TextualDirective = TextualDirective;
})(App || (App = {}));
