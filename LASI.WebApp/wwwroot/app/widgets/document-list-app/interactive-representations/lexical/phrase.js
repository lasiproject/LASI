var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    "use strict";
    var Phrase = (function (_super) {
        __extends(Phrase, _super);
        function Phrase(templateUrl, link, scope) {
            _super.call(this, templateUrl, link, undefined, scope);
            this.scope = scope;
        }
        return Phrase;
    })(App.TextualDirective);
    phrase.$inject = ["$window"];
    function phrase($window) {
        var scope = {
            phrase: '=',
            parentId: '='
        };
        var link = function (scope, element, attrs) {
        };
        return new Phrase('/app/widgets/document-list-app/interactive-representations/lexical/phrase.html', link, scope);
    }
    angular.module("app").directive("phrase", phrase);
})(App || (App = {}));
