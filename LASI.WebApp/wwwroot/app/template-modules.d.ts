/**
 * When using the System module format, static assets such as templates and stylesheets must be imported
 * using various flavors of the ES6 module import syntax. However, while the TypeScript compiler resolves paths 
 * and correctly transpiles these imports into calls to System.Register, it gives types errors when you bind these
 * imports to names in TypeScript code, because they are naturally not valid modules TypeScript or JavaScript modules. 
 * This is not an issue with stylesheets as we currently use them, but templates must be referenced in Angular Directive
 * Definition Objects to specify inline templates. In order to work around this, for each template that is subject to being
 * imported in TypeScript code, add a module named after the full path of the template releative to the wwwroot folder, 
 * and having a dummy default export to this file.
 */
declare module 'app/app.html' { export default ''; }
declare module 'app/sections/home/home.html' { export default ''; }
declare module 'app/sections/navbar/navbar.html' { export default ''; }
declare module 'app/sections/account.html' { export default ''; }
declare module 'app/sections/login.html' { export default ''; }
declare module 'app/sections/document-manager/manage-modal.html' { export default ''; }
declare module 'app/sections/document-manager/confirm-delete-modal.html' { export default ''; }
declare module 'app/document-upload/upload-panel.html' { export default ''; }
declare module 'app/debug/debug-panel.html' { export default ''; }
declare module 'app/document-list/document-list-menu-item.html' { export default ''; }
declare module 'app/document-list/document-list-tabset-item.html' { export default ''; }
declare module 'app/document-viewer/search/search-box.html' { export default ''; }
declare module 'app/document-viewer/directives/document-viewer.html' { export default ''; }
declare module 'app/document-viewer/directives/page.html' { export default ''; }
declare module 'app/document-viewer/directives/paragraph.html' { export default ''; }
declare module 'app/document-viewer/directives/sentence.html' { export default ''; }
declare module 'app/document-viewer/directives/phrase.html' { export default ''; }
declare module 'app/widgets/processing-task-progress-bar.html' { export default ''; }