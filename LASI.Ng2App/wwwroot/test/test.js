System.register(['jspm_packages/github/twbs/bootstrap@3.3.6/css/bootstrap.css', '@angular/platform-browser-dynamic', 'app/document-viewer/document-viewer', 'app/document-viewer/lexical-menu-builder', 'app/document-viewer/document-model-service', '@angular/http'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var platform_browser_dynamic_1, document_viewer_1, lexical_menu_builder_1, document_model_service_1, http_1;
    function bootstrap() {
        return platform_browser_dynamic_1.bootstrap(document_viewer_1.DocumentViewerComponent, [http_1.HTTP_PROVIDERS, document_model_service_1.DocumentModelService, lexical_menu_builder_1.LexicalMenuBuilder])
            .then(success => console.log(`Bootstrap success`))
            .catch(error => console.log(error));
    }
    exports_1("bootstrap", bootstrap);
    return {
        setters:[
            function (_1) {},
            function (platform_browser_dynamic_1_1) {
                platform_browser_dynamic_1 = platform_browser_dynamic_1_1;
            },
            function (document_viewer_1_1) {
                document_viewer_1 = document_viewer_1_1;
            },
            function (lexical_menu_builder_1_1) {
                lexical_menu_builder_1 = lexical_menu_builder_1_1;
            },
            function (document_model_service_1_1) {
                document_model_service_1 = document_model_service_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            }],
        execute: function() {
        }
    }
});
