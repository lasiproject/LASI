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
'use strict';
