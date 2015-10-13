System.config({
  baseURL: "/",
  defaultJSExtensions: true,
  transpiler: "none",
  paths: {
    "github:*": "jspm_packages/github/*",
    "npm:*": "jspm_packages/npm/*"
  },
  "*.html": {
    "loader": "html"
  },

  map: {
    "angular": "github:angular/bower-angular@1.4.7",
    "angular-bootstrap": "github:angular-ui/bootstrap-bower@0.13.4",
    "angular-bootstrap-contextmenu": "github:angular-ui/angular-bootstrap-contextmenu.js",
    "angular-file-upload": "github:danialfarid/ng-file-upload-bower@9.0.6",
    "angular-resource": "github:angular/bower-angular-resource@1.4.7",
    "core-js": "npm:core-js@1.2.1",
    "html": "github:systemjs/plugin-text@0.0.2",
    "jquery": "github:components/jquery@2.1.4",
    "systemjs/plugin-text": "github:systemjs/plugin-text@0.0.2",
    "github:jspm/nodelibs-assert@0.1.0": {
      "assert": "npm:assert@1.3.0"
    },
    "github:jspm/nodelibs-process@0.1.2": {
      "process": "npm:process@0.11.2"
    },
    "github:jspm/nodelibs-util@0.1.0": {
      "util": "npm:util@0.10.3"
    },
    "npm:assert@1.3.0": {
      "util": "npm:util@0.10.3"
    },
    "npm:core-js@1.2.1": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "systemjs-json": "github:systemjs/plugin-json@0.1.0"
    },
    "npm:inherits@2.0.1": {
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:process@0.11.2": {
      "assert": "github:jspm/nodelibs-assert@0.1.0"
    },
    "npm:util@0.10.3": {
      "inherits": "npm:inherits@2.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2"
    }
  }
});
