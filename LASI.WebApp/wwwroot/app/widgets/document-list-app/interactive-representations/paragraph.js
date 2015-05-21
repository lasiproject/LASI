var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    var Paragraph = (function (_super) {
        __extends(Paragraph, _super);
        function Paragraph(templateUrl, link, scope) {
            _super.call(this, templateUrl, link, undefined, scope);
            this.scope = scope;
        }
        return Paragraph;
    })(App.TextualDirective);
    paragraph.$inject = ['$window'];
    function paragraph($window) {
        var scope = {
            paragraph: '=',
            parentId: '='
        };
        return new Paragraph('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);
        var link = function (scope, element, attrs) {
        };
    }
    angular.module('app').directive('paragraph', paragraph);
})(App || (App = {}));
