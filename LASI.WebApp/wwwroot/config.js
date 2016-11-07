System.config({
  baseURL: "/",
  defaultJSExtensions: true,
  transpiler: "ts",
  babelOptions: {
    "optional": [
      "runtime",
      "optimisation.modules.system"
    ]
  },
  typescriptOptions: {
    "module": "system",
    "target": "es5",
    "sourceMap": true,
    "inlineSourceMap": true,
    "experimentalDecorators": true,
    "emitDecoratorMetadata": true,
    "experimentalAsyncFunctions": true,
    "typeCheck": false
  },
  paths: {
    "github:*": "jspm_packages/github/*",
    "npm:*": "jspm_packages/npm/*"
  },

  packages: {
    "app": {
      "main": "app",
      "defaultExtension": "ts",
      "meta": {
        "*.js": {
          "loader": "ts"
        }
      }
    },
    "test": {
      "defaultExtension": "ts",
      "main": "index.ts",
      "deps": [
        "qunit",
        "jquery"
      ]
    }
  },

  meta: {
    "*.ts": {
      "loader": "ts"
    },
    "*.css": {
      "loader": "css"
    },
    "*.html": {
      "loader": "text"
    }
  },

  map: {
    "angular": "npm:angular@1.5.3",
    "angular-bootstrap": "github:angular-ui/bootstrap-bower@1.1.2",
    "angular-bootstrap-contextmenu": "github:Templarian/ui.bootstrap.contextMenu@0.9.9",
    "angular-file-upload": "github:danialfarid/ng-file-upload-bower@10.1.14",
    "angular-formly": "npm:angular-formly@7.5.2",
    "angular-formly-templates-bootstrap": "npm:angular-formly-templates-bootstrap@6.4.0",
    "angular-messages": "npm:angular-messages@1.5.8",
    "angular-ui-router": "npm:angular-ui-router@0.2.18",
    "api-check": "npm:api-check@7.5.5",
    "babel": "npm:babel-core@5.8.38",
    "babel-runtime": "npm:babel-runtime@5.8.38",
    "bootstrap": "github:twbs/bootstrap@3.3.7",
    "clean-css": "npm:clean-css@3.4.20",
    "core-js": "npm:core-js@1.2.7",
    "css": "github:systemjs/plugin-css@0.1.32",
    "font-awesome": "npm:font-awesome@4.7.0",
    "jquery": "github:components/jquery@2.2.4",
    "jquery-validation": "github:jzaefferer/jquery-validation@1.15.1",
    "jquery-validation-unobtrusive": "github:aspnet/jquery-validation-unobtrusive@3.2.6",
    "qunit": "github:qunitjs/qunit@1.23.1",
    "qunit-css": "github:jquery/qunit@1.21.0",
    "systemjs-hot-reloader": "github:capaj/systemjs-hot-reloader@0.5.9",
    "systemjs/plugin-css": "github:systemjs/plugin-css@0.1.32",
    "text": "github:systemjs/plugin-text@0.0.2",
    "ts": "github:frankwallis/plugin-typescript@4.0.16",
    "ts-runtime": "npm:babel-runtime@5.8.38",
    "typescript": "npm:typescript@1.8.9",
    "github:aspnet/jquery-validation-unobtrusive@3.2.6": {
      "jquery-validation": "github:jzaefferer/jquery-validation@1.15.1"
    },
    "github:capaj/systemjs-hot-reloader@0.5.9": {
      "debug": "npm:debug@2.2.0",
      "socket.io-client": "github:socketio/socket.io-client@1.5.1",
      "weakee": "npm:weakee@1.0.0"
    },
    "github:frankwallis/plugin-typescript@4.0.16": {
      "typescript": "npm:typescript@1.8.9"
    },
    "github:jspm/nodelibs-assert@0.1.0": {
      "assert": "npm:assert@1.4.1"
    },
    "github:jspm/nodelibs-buffer@0.1.0": {
      "buffer": "npm:buffer@3.6.0"
    },
    "github:jspm/nodelibs-events@0.1.1": {
      "events": "npm:events@1.0.2"
    },
    "github:jspm/nodelibs-http@1.7.1": {
      "Base64": "npm:Base64@0.2.1",
      "events": "github:jspm/nodelibs-events@0.1.1",
      "inherits": "npm:inherits@2.0.1",
      "stream": "github:jspm/nodelibs-stream@0.1.0",
      "url": "github:jspm/nodelibs-url@0.1.0",
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "github:jspm/nodelibs-https@0.1.0": {
      "https-browserify": "npm:https-browserify@0.0.0"
    },
    "github:jspm/nodelibs-os@0.1.0": {
      "os-browserify": "npm:os-browserify@0.1.2"
    },
    "github:jspm/nodelibs-path@0.1.0": {
      "path-browserify": "npm:path-browserify@0.0.0"
    },
    "github:jspm/nodelibs-process@0.1.2": {
      "process": "npm:process@0.11.9"
    },
    "github:jspm/nodelibs-stream@0.1.0": {
      "stream-browserify": "npm:stream-browserify@1.0.0"
    },
    "github:jspm/nodelibs-url@0.1.0": {
      "url": "npm:url@0.10.3"
    },
    "github:jspm/nodelibs-util@0.1.0": {
      "util": "npm:util@0.10.3"
    },
    "github:jspm/nodelibs-vm@0.1.0": {
      "vm-browserify": "npm:vm-browserify@0.0.4"
    },
    "github:jzaefferer/jquery-validation@1.15.1": {
      "jquery": "npm:jquery@3.1.1"
    },
    "github:twbs/bootstrap@3.3.7": {
      "jquery": "npm:jquery@3.1.1"
    },
    "npm:amdefine@1.0.1": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "module": "github:jspm/nodelibs-module@0.1.0",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:angular-formly-templates-bootstrap@6.4.0": {
      "angular": "npm:angular@1.5.3",
      "angular-formly": "npm:angular-formly@7.5.2",
      "api-check": "npm:api-check@7.5.5",
      "bootstrap": "npm:bootstrap@3.3.7",
      "systemjs-json": "github:systemjs/plugin-json@0.1.2"
    },
    "npm:angular-formly@7.5.2": {
      "angular": "npm:angular@1.5.3",
      "systemjs-json": "github:systemjs/plugin-json@0.1.2"
    },
    "npm:angular-ui-router@0.2.18": {
      "angular": "npm:angular@1.5.3",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:assert@1.4.1": {
      "assert": "github:jspm/nodelibs-assert@0.1.0",
      "buffer": "github:jspm/nodelibs-buffer@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "util": "npm:util@0.10.3"
    },
    "npm:babel-runtime@5.8.38": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:bootstrap@3.3.7": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:buffer@3.6.0": {
      "base64-js": "npm:base64-js@0.0.8",
      "child_process": "github:jspm/nodelibs-child_process@0.1.0",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "ieee754": "npm:ieee754@1.1.8",
      "isarray": "npm:isarray@1.0.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:clean-css@3.4.20": {
      "buffer": "github:jspm/nodelibs-buffer@0.1.0",
      "commander": "npm:commander@2.8.1",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "http": "github:jspm/nodelibs-http@1.7.1",
      "https": "github:jspm/nodelibs-https@0.1.0",
      "os": "github:jspm/nodelibs-os@0.1.0",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "source-map": "npm:source-map@0.4.4",
      "url": "github:jspm/nodelibs-url@0.1.0",
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:commander@2.8.1": {
      "child_process": "github:jspm/nodelibs-child_process@0.1.0",
      "events": "github:jspm/nodelibs-events@0.1.1",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "graceful-readlink": "npm:graceful-readlink@1.0.1",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:core-js@1.2.7": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "systemjs-json": "github:systemjs/plugin-json@0.1.2"
    },
    "npm:core-util-is@1.0.2": {
      "buffer": "github:jspm/nodelibs-buffer@0.1.0"
    },
    "npm:debug@2.2.0": {
      "ms": "npm:ms@0.7.1"
    },
    "npm:font-awesome@4.7.0": {
      "css": "github:systemjs/plugin-css@0.1.32"
    },
    "npm:graceful-readlink@1.0.1": {
      "fs": "github:jspm/nodelibs-fs@0.1.2"
    },
    "npm:https-browserify@0.0.0": {
      "http": "github:jspm/nodelibs-http@1.7.1"
    },
    "npm:inherits@2.0.1": {
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:isarray@1.0.0": {
      "systemjs-json": "github:systemjs/plugin-json@0.1.2"
    },
    "npm:os-browserify@0.1.2": {
      "os": "github:jspm/nodelibs-os@0.1.0"
    },
    "npm:path-browserify@0.0.0": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:process@0.11.9": {
      "assert": "github:jspm/nodelibs-assert@0.1.0",
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "vm": "github:jspm/nodelibs-vm@0.1.0"
    },
    "npm:punycode@1.3.2": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:readable-stream@1.1.14": {
      "buffer": "github:jspm/nodelibs-buffer@0.1.0",
      "core-util-is": "npm:core-util-is@1.0.2",
      "events": "github:jspm/nodelibs-events@0.1.1",
      "inherits": "npm:inherits@2.0.1",
      "isarray": "npm:isarray@0.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "stream-browserify": "npm:stream-browserify@1.0.0",
      "string_decoder": "npm:string_decoder@0.10.31"
    },
    "npm:source-map@0.4.4": {
      "amdefine": "npm:amdefine@1.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:stream-browserify@1.0.0": {
      "events": "github:jspm/nodelibs-events@0.1.1",
      "inherits": "npm:inherits@2.0.1",
      "readable-stream": "npm:readable-stream@1.1.14"
    },
    "npm:string_decoder@0.10.31": {
      "buffer": "github:jspm/nodelibs-buffer@0.1.0"
    },
    "npm:typescript@1.8.9": {
      "os": "github:jspm/nodelibs-os@0.1.0"
    },
    "npm:url@0.10.3": {
      "assert": "github:jspm/nodelibs-assert@0.1.0",
      "punycode": "npm:punycode@1.3.2",
      "querystring": "npm:querystring@0.2.0",
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:util@0.10.3": {
      "inherits": "npm:inherits@2.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:vm-browserify@0.0.4": {
      "indexof": "npm:indexof@0.0.1"
    }
  }
});
