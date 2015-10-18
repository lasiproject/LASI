define(["require", "exports", 'angular', './debug/debug.module', './widgets/widgets.module', './document-list/document-list.module', './document-upload/document-upload.module', './document-viewer/document-viewer.module', './document-viewer/search/search.module', 'github:twbs/bootstrap@3.3.5/css/bootstrap.css!', 'font-awesome', 'dist/app.css!', 'bootstrap', './utilities/augmentations', './LASI'], function (require, exports, angular_1, debug_module_1, widgets_module_1, document_list_module_1, document_upload_module_1, document_viewer_module_1, search_module_1) {
    var modules = [document_list_module_1.default, debug_module_1.default, widgets_module_1.default, document_upload_module_1.default, document_viewer_module_1.default, search_module_1.default];
    function register(m) {
        function validate() {
            if (!m.name) {
                throw new TypeError('name is required');
            }
            if (!m.requires) {
                throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
            }
        }
        angular_1.module(m.name, m.requires, m.configFn || (function () { }))
            .provider(m.providers || {})
            .factory(m.factories || {})
            .service(m.services || {})
            .filter(m.filters || {})
            .controller(m.controllers || {})
            .directive(m.directives || {})
            .value(m.values || {})
            .constant(m.constants || {})
            .run(m.runFn || (function () { }));
    }
    function angularBootstrap() {
        modules.forEach(register);
        angular_1.bootstrap(document, modules.map(function (m) { return m.name; }));
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = angularBootstrap;
});
//angularBootstrap(); 
//# sourceMappingURL=app.js.map