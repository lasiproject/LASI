// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.html',
                link: function (scope, element, attrs) {
                    //console.log(attrs);
                },
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
        angular.module(LASI.documentViewer.moduleName).directive('sentence', sentence);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=sentence.js.map