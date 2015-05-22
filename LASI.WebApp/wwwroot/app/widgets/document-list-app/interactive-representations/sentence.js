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
    var Sentence = (function (_super) {
        __extends(Sentence, _super);
        function Sentence(templateUrl, link, scope) {
            _super.call(this, templateUrl, link, undefined, scope);
            this.scope = scope;
        }
        return Sentence;
    })(App.TextualDirective);
    sentence.$inject = ["$window"];
    function sentence($window) {
        var scope = {
            sentence: '=',
            parentId: '='
        };
        var link = function (scope, element, attrs) {
        };
        return new Sentence('/app/widgets/document-list-app/interactive-representations/sentence.html', link, scope);
    }
    angular.module("app").directive("sentence", sentence);
})(App || (App = {}));
