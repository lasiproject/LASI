SystemJS.config({
  transpiler: "plugin-babel",
  typescriptOptions: {
    "typeCheck": false,
    "tsconfig": true
  },
  paths: {
    "github:": "jspm_packages/github/",
    "npm:": "jspm_packages/npm/",
    "app/": "src/"
  },
  meta: {
    "*.ts": {
      "loader": "plugin-typescript"
    },
    "*.html": {
      "loader": "text"
    },
    "*.css": {
      "loader": "css"
    }
  },
  packages: {
    "src": {
      "format": "esm",
      "main": "main",
      "defaultExtension": "ts",
      "meta": {
        "*.ts": {
          "loader": "plugin-typescript"
        },
        "*.js": {
          "loader": "plugin-typescript"
        },
        "*.css": {
          "loader": "css"
        },
        "**/*.html": {
          "loader": "text"
        }
      }
    },
    "test": {
      "format": "esm",
      "main": "test",
      "defaultExtension": "ts",
      "meta": {
        "*.ts": {
          "loader": "plugin-typescript"
        },
        "*.js": {
          "loader": "plugin-typescript"
        }
      }
    },
    "shared": {
      "format": "esm",
      "defaultExtension": "ts",
      "meta": {
        "*.ts": {
          "loader": "plugin-typescript"
        },
        "*.js": {
          "loader": "plugin-typescript"
        }
      }
    }
  },
  browserConfig: {
    "baseURL": "/",
    "bundles": {
      "build.js": [
        "app/main.ts",
        "app/app/services/token.ts",
        "app/app/services/storage.ts",
        "npm:aurelia-framework@1.0.8/aurelia-framework.js",
        "npm:aurelia-framework@1.0.8.json",
        "npm:aurelia-logging@1.2.0/aurelia-logging.js",
        "npm:aurelia-logging@1.2.0.json",
        "npm:aurelia-pal@1.2.0/aurelia-pal.js",
        "npm:aurelia-pal@1.2.0.json",
        "npm:aurelia-path@1.1.1/aurelia-path.js",
        "npm:aurelia-path@1.1.1.json",
        "npm:aurelia-task-queue@1.1.0/aurelia-task-queue.js",
        "npm:aurelia-task-queue@1.1.0.json",
        "npm:aurelia-loader@1.0.0/aurelia-loader.js",
        "npm:aurelia-loader@1.0.0.json",
        "npm:aurelia-metadata@1.0.3/aurelia-metadata.js",
        "npm:aurelia-metadata@1.0.3.json",
        "npm:aurelia-templating@1.2.0/aurelia-templating.js",
        "npm:aurelia-templating@1.2.0.json",
        "npm:aurelia-binding@1.1.1/aurelia-binding.js",
        "npm:aurelia-binding@1.1.1.json",
        "npm:aurelia-dependency-injection@1.3.0/aurelia-dependency-injection.js",
        "npm:aurelia-dependency-injection@1.3.0.json",
        "npm:tslib@1.5.0/tslib.js",
        "npm:tslib@1.5.0.json",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/createClass.js",
        "npm:systemjs-plugin-babel@0.0.17.json",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/classCallCheck.js",
        "github:frankwallis/plugin-typescript@6.0.0.json",
        "app/helpers.ts",
        "npm:aurelia-fetch-client@1.1.0/aurelia-fetch-client.js",
        "npm:aurelia-fetch-client@1.1.0.json",
        "npm:core-js@2.4.1/client/core.js",
        "npm:core-js@2.4.1.json",
        "npm:jspm-nodelibs-process@0.2.0/process.js",
        "npm:jspm-nodelibs-process@0.2.0.json",
        "github:twbs/bootstrap@3.3.7/dist/js/bootstrap.js",
        "github:twbs/bootstrap@3.3.7.json",
        "npm:jquery@2.2.4/dist/jquery.js",
        "npm:jquery@2.2.4.json",
        "app/styles/lexical.css",
        "app/styles/site.css",
        "github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css",
        "npm:font-awesome@4.7.0/css/font-awesome.min.css",
        "npm:font-awesome@4.7.0.json",
        "app/enhance-array.ts",
        "npm:jspm-nodelibs-path@0.2.1/path.js",
        "npm:jspm-nodelibs-path@0.2.1.json",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/slicedToArray.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/asyncToGenerator.js",
        "npm:systemjs-plugin-babel@0.0.17/regenerator-runtime.js",
        "app/configuration/typeahead.ts",
        "app/configuration/http.ts",
        "app/configuration/dialog.ts",
        "app/app/signout.ts",
        "app/app/signin.ts",
        "app/app/services/user.ts",
        "app/app/services/http-utilities.ts",
        "npm:aurelia-router@1.1.1/aurelia-router.js",
        "npm:aurelia-router@1.1.1.json",
        "npm:aurelia-event-aggregator@1.0.1/aurelia-event-aggregator.js",
        "npm:aurelia-event-aggregator@1.0.1.json",
        "npm:aurelia-history@1.0.0/aurelia-history.js",
        "npm:aurelia-history@1.0.0.json",
        "npm:aurelia-route-recognizer@1.1.0/aurelia-route-recognizer.js",
        "npm:aurelia-route-recognizer@1.1.0.json",
        "app/app/services/results-service.ts",
        "app/app/services/build-menus.ts",
        "app/app/services/result-chart-builder.ts",
        "app/app/modal.ts",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/index.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1.json",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/aurelia-dialog.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-result.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-controller.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/lifecycle.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-service.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-options.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/renderer.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/attach-focus.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/ai-dialog-footer.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/ai-dialog-body.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/ai-dialog-header.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/ai-dialog.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-configuration.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-renderer.js",
        "app/app/documents.ts",
        "app/models.ts",
        "app/app/services/document-model-service.ts",
        "app/app/document-viewer/search.ts",
        "github:twitter/typeahead.js@0.11.1/dist/typeahead.bundle.js",
        "github:twitter/typeahead.js@0.11.1.json",
        "app/app/document-viewer/phrase.ts",
        "app/app/services/lexical-menu-builder.ts",
        "app/app/document-viewer/document-viewer.ts",
        "app/app/app.ts",
        "app/app/account.ts"
      ]
    }
  },
  devConfig: {
    "map": {
      "plugin-typescript": "github:frankwallis/plugin-typescript@6.0.0",
      "aurelia-testing": "npm:aurelia-testing@1.0.0-beta.2.0.1",
      "source-map-support": "npm:source-map-support@0.4.9",
      "tape": "npm:tape@4.6.3",
      "text": "github:systemjs/plugin-text@0.0.9",
      "@types/tape": "npm:@types/tape@4.2.28",
      "fsevents": "npm:fsevents@1.0.17",
      "bcrypt-pbkdf": "npm:bcrypt-pbkdf@1.0.0",
      "jsbn": "npm:jsbn@0.1.0",
      "ecc-jsbn": "npm:ecc-jsbn@0.1.1",
      "jodid25519": "npm:jodid25519@1.0.2",
      "tweetnacl": "npm:tweetnacl@0.14.5",
      "debug": "npm:debug@2.6.0",
      "timers": "npm:jspm-nodelibs-timers@0.2.0",
      "request": "npm:request@2.79.0",
      "plugin-babel": "npm:systemjs-plugin-babel@0.0.17",
      "aurelia-types-installer": "npm:aurelia-types-installer@0.1.8"
    },
    "packages": {
      "npm:resumer@0.0.0": {
        "map": {
          "through": "npm:through@2.3.8"
        }
      },
      "npm:string.prototype.trim@1.1.2": {
        "map": {
          "function-bind": "npm:function-bind@1.1.0",
          "define-properties": "npm:define-properties@1.1.2",
          "es-abstract": "npm:es-abstract@1.6.1"
        }
      },
      "npm:define-properties@1.1.2": {
        "map": {
          "foreach": "npm:foreach@2.0.5",
          "object-keys": "npm:object-keys@1.0.11"
        }
      },
      "npm:es-abstract@1.6.1": {
        "map": {
          "function-bind": "npm:function-bind@1.1.0",
          "is-regex": "npm:is-regex@1.0.3",
          "es-to-primitive": "npm:es-to-primitive@1.1.1",
          "is-callable": "npm:is-callable@1.1.3"
        }
      },
      "npm:es-to-primitive@1.1.1": {
        "map": {
          "is-callable": "npm:is-callable@1.1.3",
          "is-symbol": "npm:is-symbol@1.0.1",
          "is-date-object": "npm:is-date-object@1.0.1"
        }
      },
      "npm:has@1.0.1": {
        "map": {
          "function-bind": "npm:function-bind@1.1.0"
        }
      },
      "npm:@types/tape@4.2.28": {
        "map": {
          "@types/node": "npm:@types/node@7.0.0"
        }
      },
      "npm:pinkie-promise@2.0.1": {
        "map": {
          "pinkie": "npm:pinkie@2.0.4"
        }
      },
      "npm:debug@2.2.0": {
        "map": {
          "ms": "npm:ms@0.7.1"
        }
      },
      "npm:commander@2.9.0": {
        "map": {
          "graceful-readlink": "npm:graceful-readlink@1.0.1"
        }
      },
      "npm:string-width@1.0.2": {
        "map": {
          "strip-ansi": "npm:strip-ansi@3.0.1",
          "code-point-at": "npm:code-point-at@1.1.0",
          "is-fullwidth-code-point": "npm:is-fullwidth-code-point@1.0.0"
        }
      },
      "npm:nopt@3.0.6": {
        "map": {
          "abbrev": "npm:abbrev@1.0.9"
        }
      },
      "npm:is-fullwidth-code-point@1.0.0": {
        "map": {
          "number-is-nan": "npm:number-is-nan@1.0.1"
        }
      },
      "npm:har-validator@2.0.6": {
        "map": {
          "chalk": "npm:chalk@1.1.3",
          "commander": "npm:commander@2.9.0",
          "pinkie-promise": "npm:pinkie-promise@2.0.1",
          "is-my-json-valid": "npm:is-my-json-valid@2.15.0"
        }
      },
      "npm:tar-pack@3.3.0": {
        "map": {
          "once": "npm:once@1.3.3",
          "readable-stream": "npm:readable-stream@2.1.5",
          "debug": "npm:debug@2.2.0",
          "rimraf": "npm:rimraf@2.5.4",
          "tar": "npm:tar@2.2.1",
          "uid-number": "npm:uid-number@0.0.6",
          "fstream-ignore": "npm:fstream-ignore@1.0.5",
          "fstream": "npm:fstream@1.0.10"
        }
      },
      "npm:tar@2.2.1": {
        "map": {
          "inherits": "npm:inherits@2.0.3",
          "block-stream": "npm:block-stream@0.0.9",
          "fstream": "npm:fstream@1.0.10"
        }
      },
      "npm:combined-stream@1.0.5": {
        "map": {
          "delayed-stream": "npm:delayed-stream@1.0.0"
        }
      },
      "npm:http-signature@1.1.1": {
        "map": {
          "assert-plus": "npm:assert-plus@0.2.0",
          "jsprim": "npm:jsprim@1.3.1",
          "sshpk": "npm:sshpk@1.10.2"
        }
      },
      "npm:rc@1.1.6": {
        "map": {
          "minimist": "npm:minimist@1.2.0",
          "ini": "npm:ini@1.3.4",
          "strip-json-comments": "npm:strip-json-comments@1.0.4",
          "deep-extend": "npm:deep-extend@0.4.1"
        }
      },
      "npm:once@1.3.3": {
        "map": {
          "wrappy": "npm:wrappy@1.0.2"
        }
      },
      "npm:tough-cookie@2.3.2": {
        "map": {
          "punycode": "npm:punycode@1.4.1"
        }
      },
      "npm:hawk@3.1.3": {
        "map": {
          "sntp": "npm:sntp@1.0.9",
          "cryptiles": "npm:cryptiles@2.0.5",
          "hoek": "npm:hoek@2.16.3",
          "boom": "npm:boom@2.10.1"
        }
      },
      "npm:block-stream@0.0.9": {
        "map": {
          "inherits": "npm:inherits@2.0.3"
        }
      },
      "npm:fstream-ignore@1.0.5": {
        "map": {
          "inherits": "npm:inherits@2.0.3",
          "minimatch": "npm:minimatch@3.0.3",
          "fstream": "npm:fstream@1.0.10"
        }
      },
      "npm:sntp@1.0.9": {
        "map": {
          "hoek": "npm:hoek@2.16.3"
        }
      },
      "npm:cryptiles@2.0.5": {
        "map": {
          "boom": "npm:boom@2.10.1"
        }
      },
      "npm:jsprim@1.3.1": {
        "map": {
          "extsprintf": "npm:extsprintf@1.0.2",
          "json-schema": "npm:json-schema@0.2.3",
          "verror": "npm:verror@1.3.6"
        }
      },
      "npm:fstream@1.0.10": {
        "map": {
          "inherits": "npm:inherits@2.0.3",
          "rimraf": "npm:rimraf@2.5.4",
          "graceful-fs": "npm:graceful-fs@4.1.11",
          "mkdirp": "npm:mkdirp@0.5.1"
        }
      },
      "npm:are-we-there-yet@1.1.2": {
        "map": {
          "readable-stream": "npm:readable-stream@1.1.14",
          "delegates": "npm:delegates@1.0.0"
        }
      },
      "npm:is-my-json-valid@2.15.0": {
        "map": {
          "xtend": "npm:xtend@4.0.1",
          "generate-object-property": "npm:generate-object-property@1.2.0",
          "jsonpointer": "npm:jsonpointer@4.0.1",
          "generate-function": "npm:generate-function@2.0.0"
        }
      },
      "npm:boom@2.10.1": {
        "map": {
          "hoek": "npm:hoek@2.16.3"
        }
      },
      "npm:readable-stream@1.1.14": {
        "map": {
          "core-util-is": "npm:core-util-is@1.0.2",
          "inherits": "npm:inherits@2.0.3",
          "string_decoder": "npm:string_decoder@0.10.31",
          "isarray": "npm:isarray@0.0.1",
          "stream-browserify": "npm:stream-browserify@1.0.0"
        }
      },
      "npm:verror@1.3.6": {
        "map": {
          "extsprintf": "npm:extsprintf@1.0.2"
        }
      },
      "npm:getpass@0.1.6": {
        "map": {
          "assert-plus": "npm:assert-plus@1.0.0"
        }
      },
      "npm:ecc-jsbn@0.1.1": {
        "map": {
          "jsbn": "npm:jsbn@0.1.0"
        }
      },
      "npm:bcrypt-pbkdf@1.0.0": {
        "map": {
          "tweetnacl": "npm:tweetnacl@0.14.5"
        }
      },
      "npm:jodid25519@1.0.2": {
        "map": {
          "jsbn": "npm:jsbn@0.1.0"
        }
      },
      "npm:generate-object-property@1.2.0": {
        "map": {
          "is-property": "npm:is-property@1.0.2"
        }
      },
      "npm:wide-align@1.1.0": {
        "map": {
          "string-width": "npm:string-width@1.0.2"
        }
      },
      "npm:stream-browserify@1.0.0": {
        "map": {
          "readable-stream": "npm:readable-stream@1.1.14",
          "inherits": "npm:inherits@2.0.3"
        }
      },
      "npm:jspm-nodelibs-timers@0.2.0": {
        "map": {
          "timers-browserify": "npm:timers-browserify@1.4.2"
        }
      },
      "npm:timers-browserify@1.4.2": {
        "map": {
          "process": "npm:process@0.11.9"
        }
      },
      "npm:readable-stream@2.1.5": {
        "map": {
          "inherits": "npm:inherits@2.0.3",
          "core-util-is": "npm:core-util-is@1.0.2",
          "string_decoder": "npm:string_decoder@0.10.31",
          "process-nextick-args": "npm:process-nextick-args@1.0.7",
          "isarray": "npm:isarray@1.0.0",
          "util-deprecate": "npm:util-deprecate@1.0.2",
          "buffer-shims": "npm:buffer-shims@1.0.0"
        }
      },
      "npm:form-data@2.1.2": {
        "map": {
          "combined-stream": "npm:combined-stream@1.0.5",
          "mime-types": "npm:mime-types@2.1.14",
          "asynckit": "npm:asynckit@0.4.0"
        }
      },
      "npm:tape@4.6.3": {
        "map": {
          "defined": "npm:defined@1.0.0",
          "object-inspect": "npm:object-inspect@1.2.1",
          "function-bind": "npm:function-bind@1.1.0",
          "deep-equal": "npm:deep-equal@1.0.1",
          "inherits": "npm:inherits@2.0.3",
          "resolve": "npm:resolve@1.1.7",
          "resumer": "npm:resumer@0.0.0",
          "has": "npm:has@1.0.1",
          "through": "npm:through@2.3.8",
          "glob": "npm:glob@7.1.1",
          "minimist": "npm:minimist@1.2.0",
          "string.prototype.trim": "npm:string.prototype.trim@1.1.2",
          "for-each": "npm:for-each@0.3.2"
        }
      },
      "npm:node-pre-gyp@0.6.32": {
        "map": {
          "npmlog": "npm:npmlog@4.0.2",
          "request": "npm:request@2.79.0",
          "tar": "npm:tar@2.2.1",
          "mkdirp": "npm:mkdirp@0.5.1",
          "rimraf": "npm:rimraf@2.5.4",
          "rc": "npm:rc@1.1.6",
          "tar-pack": "npm:tar-pack@3.3.0",
          "semver": "npm:semver@5.3.0",
          "nopt": "npm:nopt@3.0.6"
        }
      },
      "npm:for-each@0.3.2": {
        "map": {
          "is-function": "npm:is-function@1.0.1"
        }
      },
      "npm:gauge@2.7.2": {
        "map": {
          "supports-color": "npm:supports-color@0.2.0",
          "console-control-strings": "npm:console-control-strings@1.1.0",
          "strip-ansi": "npm:strip-ansi@3.0.1",
          "object-assign": "npm:object-assign@4.1.0",
          "wide-align": "npm:wide-align@1.1.0",
          "aproba": "npm:aproba@1.0.4",
          "string-width": "npm:string-width@1.0.2",
          "signal-exit": "npm:signal-exit@3.0.2",
          "has-unicode": "npm:has-unicode@2.0.1"
        }
      },
      "npm:request@2.79.0": {
        "map": {
          "tunnel-agent": "npm:tunnel-agent@0.4.3",
          "extend": "npm:extend@3.0.0",
          "combined-stream": "npm:combined-stream@1.0.5",
          "http-signature": "npm:http-signature@1.1.1",
          "caseless": "npm:caseless@0.11.0",
          "tough-cookie": "npm:tough-cookie@2.3.2",
          "mime-types": "npm:mime-types@2.1.14",
          "forever-agent": "npm:forever-agent@0.6.1",
          "aws4": "npm:aws4@1.5.0",
          "qs": "npm:qs@6.3.0",
          "stringstream": "npm:stringstream@0.0.5",
          "oauth-sign": "npm:oauth-sign@0.8.2",
          "isstream": "npm:isstream@0.1.2",
          "form-data": "npm:form-data@2.1.2",
          "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
          "hawk": "npm:hawk@3.1.3",
          "har-validator": "npm:har-validator@2.0.6",
          "aws-sign2": "npm:aws-sign2@0.6.0",
          "is-typedarray": "npm:is-typedarray@1.0.0",
          "uuid": "npm:uuid@3.0.1"
        }
      },
      "npm:dashdash@1.14.1": {
        "map": {
          "assert-plus": "npm:assert-plus@1.0.0"
        }
      },
      "npm:npmlog@4.0.2": {
        "map": {
          "set-blocking": "npm:set-blocking@2.0.0",
          "console-control-strings": "npm:console-control-strings@1.1.0",
          "are-we-there-yet": "npm:are-we-there-yet@1.1.2",
          "gauge": "npm:gauge@2.7.2"
        }
      },
      "npm:fsevents@1.0.17": {
        "map": {
          "nan": "npm:nan@2.5.0",
          "node-pre-gyp": "npm:node-pre-gyp@0.6.32"
        }
      },
      "npm:mime-types@2.1.14": {
        "map": {
          "mime-db": "npm:mime-db@1.26.0"
        }
      },
      "npm:sshpk@1.10.2": {
        "map": {
          "assert-plus": "npm:assert-plus@1.0.0",
          "dashdash": "npm:dashdash@1.14.1",
          "asn1": "npm:asn1@0.2.3",
          "getpass": "npm:getpass@0.1.6"
        }
      },
      "github:frankwallis/plugin-typescript@6.0.0": {
        "map": {
          "typescript": "npm:typescript@next"
        }
      },
      "npm:aurelia-types-installer@0.1.8": {
        "map": {
          "minimist": "npm:minimist@1.2.0",
          "request": "npm:request@2.79.0",
          "mz": "npm:mz@2.6.0",
          "@types/request-promise": "npm:@types/request-promise@4.1.33",
          "@types/mz": "npm:@types/mz@0.0.30",
          "request-promise": "npm:request-promise@4.1.1",
          "@types/minimist": "npm:@types/minimist@1.2.0",
          "@types/request": "npm:@types/request@0.0.34",
          "async": "npm:async@2.1.4"
        }
      },
      "npm:@types/request-promise@4.1.33": {
        "map": {
          "@types/request": "npm:@types/request@0.0.39",
          "@types/bluebird": "npm:@types/bluebird@3.0.37"
        }
      },
      "npm:request-promise@4.1.1": {
        "map": {
          "stealthy-require": "npm:stealthy-require@1.0.0",
          "request-promise-core": "npm:request-promise-core@1.1.1",
          "bluebird": "npm:bluebird@3.4.7"
        }
      },
      "npm:@types/request@0.0.39": {
        "map": {
          "@types/form-data": "npm:@types/form-data@0.0.33",
          "@types/node": "npm:@types/node@7.0.0"
        }
      },
      "npm:@types/request@0.0.34": {
        "map": {
          "@types/form-data": "npm:@types/form-data@0.0.33",
          "@types/node": "npm:@types/node@7.0.0"
        }
      },
      "npm:mz@2.6.0": {
        "map": {
          "thenify-all": "npm:thenify-all@1.6.0",
          "any-promise": "npm:any-promise@1.3.0",
          "object-assign": "npm:object-assign@4.1.0"
        }
      },
      "npm:@types/mz@0.0.30": {
        "map": {
          "@types/node": "npm:@types/node@7.0.0"
        }
      },
      "npm:@types/form-data@0.0.33": {
        "map": {
          "@types/node": "npm:@types/node@7.0.0"
        }
      },
      "npm:async@2.1.4": {
        "map": {
          "lodash": "npm:lodash@4.17.4"
        }
      },
      "npm:request-promise-core@1.1.1": {
        "map": {
          "lodash": "npm:lodash@4.17.4"
        }
      },
      "npm:thenify-all@1.6.0": {
        "map": {
          "thenify": "npm:thenify@3.2.1"
        }
      },
      "npm:thenify@3.2.1": {
        "map": {
          "any-promise": "npm:any-promise@1.3.0"
        }
      }
    }
  },
  map: {
    "bootstrap-css": "github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css",
    "typescript": "npm:typescript@next"
  }
});

SystemJS.config({
  packageConfigPaths: [
    "github:*/*.json",
    "npm:@*/*.json",
    "npm:*.json",
    "local:*.json"
  ],
  map: {
    "@types/typeahead": "npm:@types/typeahead@0.11.27",
    "babel": "npm:babel-core@6.21.0",
    "css": "github:systemjs/plugin-css@0.1.32",
    "domain": "npm:jspm-nodelibs-domain@0.2.0",
    "font-awesome": "npm:font-awesome@4.7.0",
    "aurelia-dialog": "npm:aurelia-dialog@1.0.0-beta.3.0.1",
    "@types/bootstrap": "npm:@types/bootstrap@3.3.32",
    "@types/whatwg-fetch": "npm:@types/whatwg-fetch@0.0.33",
    "aurelia-http-client": "npm:aurelia-http-client@1.0.4",
    "aurelia-fetch-client": "npm:aurelia-fetch-client@1.1.0",
    "aurelia-binding": "npm:aurelia-binding@1.1.1",
    "aurelia-bootstrapper": "npm:aurelia-bootstrapper@1.0.1",
    "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.3.0",
    "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.1",
    "aurelia-framework": "npm:aurelia-framework@1.0.8",
    "aurelia-history": "npm:aurelia-history@1.0.0",
    "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0",
    "aurelia-loader": "npm:aurelia-loader@1.0.0",
    "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0",
    "aurelia-logging": "npm:aurelia-logging@1.2.0",
    "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
    "aurelia-metadata": "npm:aurelia-metadata@1.0.3",
    "aurelia-pal": "npm:aurelia-pal@1.2.0",
    "aurelia-pal-browser": "npm:aurelia-pal-browser@1.1.0",
    "aurelia-path": "npm:aurelia-path@1.1.1",
    "aurelia-polyfills": "npm:aurelia-polyfills@1.1.1",
    "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.1.0",
    "aurelia-router": "npm:aurelia-router@1.1.1",
    "aurelia-task-queue": "npm:aurelia-task-queue@1.1.0",
    "aurelia-templating": "npm:aurelia-templating@1.2.0",
    "aurelia-templating-binding": "npm:aurelia-templating-binding@1.2.0",
    "aurelia-templating-resources": "npm:aurelia-templating-resources@1.2.0",
    "aurelia-templating-router": "npm:aurelia-templating-router@1.0.1",
    "assert": "npm:jspm-nodelibs-assert@0.2.0",
    "bootstrap": "github:twbs/bootstrap@3.3.7",
    "buffer": "npm:jspm-nodelibs-buffer@0.2.1",
    "child_process": "npm:jspm-nodelibs-child_process@0.2.0",
    "constants": "npm:jspm-nodelibs-constants@0.2.0",
    "core-js": "npm:core-js@2.4.1",
    "crypto": "npm:jspm-nodelibs-crypto@0.2.0",
    "dgram": "npm:jspm-nodelibs-dgram@0.2.0",
    "dns": "npm:jspm-nodelibs-dns@0.2.0",
    "events": "npm:jspm-nodelibs-events@0.2.0",
    "fs": "npm:jspm-nodelibs-fs@0.2.0",
    "graceful-fs": "npm:graceful-fs@4.1.11",
    "http": "npm:jspm-nodelibs-http@0.2.0",
    "https": "npm:jspm-nodelibs-https@0.2.1",
    "jquery": "npm:jquery@2.2.4",
    "jquery-contextmenu": "npm:jquery-contextmenu@2.4.2",
    "module": "npm:jspm-nodelibs-module@0.2.0",
    "moment": "npm:moment@2.17.1",
    "net": "npm:jspm-nodelibs-net@0.2.0",
    "os": "npm:jspm-nodelibs-os@0.2.0",
    "path": "npm:jspm-nodelibs-path@0.2.1",
    "process": "npm:jspm-nodelibs-process@0.2.0",
    "punycode": "npm:jspm-nodelibs-punycode@0.2.0",
    "querystring": "npm:jspm-nodelibs-querystring@0.2.0",
    "readline": "npm:jspm-nodelibs-readline@0.2.0",
    "reflect-metadata": "npm:reflect-metadata@0.1.9",
    "rxjs": "npm:rxjs-es@5.0.0-beta.12",
    "sass": "github:mobilexag/plugin-sass@0.5.1",
    "stream": "npm:jspm-nodelibs-stream@0.2.0",
    "string_decoder": "npm:jspm-nodelibs-string_decoder@0.2.0",
    "tls": "npm:jspm-nodelibs-tls@0.2.0",
    "tslib": "npm:tslib@1.5.0",
    "tty": "npm:jspm-nodelibs-tty@0.2.0",
    "typeahead": "github:twitter/typeahead.js@0.11.1",
    "url": "npm:jspm-nodelibs-url@0.2.0",
    "util": "npm:jspm-nodelibs-util@0.2.1",
    "vm": "npm:jspm-nodelibs-vm@0.2.0",
    "zlib": "npm:jspm-nodelibs-zlib@0.2.2"
  },
  packages: {
    "npm:crypto-browserify@3.11.0": {
      "map": {
        "create-ecdh": "npm:create-ecdh@4.0.0",
        "inherits": "npm:inherits@2.0.3",
        "public-encrypt": "npm:public-encrypt@4.0.0",
        "pbkdf2": "npm:pbkdf2@3.0.9",
        "browserify-cipher": "npm:browserify-cipher@1.0.0",
        "create-hmac": "npm:create-hmac@1.1.4",
        "diffie-hellman": "npm:diffie-hellman@5.0.2",
        "randombytes": "npm:randombytes@2.0.3",
        "create-hash": "npm:create-hash@1.1.2",
        "browserify-sign": "npm:browserify-sign@4.0.0"
      }
    },
    "npm:create-hmac@1.1.4": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "create-hash": "npm:create-hash@1.1.2"
      }
    },
    "npm:create-ecdh@4.0.0": {
      "map": {
        "elliptic": "npm:elliptic@6.3.2",
        "bn.js": "npm:bn.js@4.11.6"
      }
    },
    "npm:public-encrypt@4.0.0": {
      "map": {
        "browserify-rsa": "npm:browserify-rsa@4.0.1",
        "bn.js": "npm:bn.js@4.11.6",
        "randombytes": "npm:randombytes@2.0.3",
        "create-hash": "npm:create-hash@1.1.2",
        "parse-asn1": "npm:parse-asn1@5.0.0"
      }
    },
    "npm:browserify-cipher@1.0.0": {
      "map": {
        "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
        "browserify-aes": "npm:browserify-aes@1.0.6",
        "browserify-des": "npm:browserify-des@1.0.0"
      }
    },
    "npm:diffie-hellman@5.0.2": {
      "map": {
        "bn.js": "npm:bn.js@4.11.6",
        "randombytes": "npm:randombytes@2.0.3",
        "miller-rabin": "npm:miller-rabin@4.0.0"
      }
    },
    "npm:browserify-rsa@4.0.1": {
      "map": {
        "bn.js": "npm:bn.js@4.11.6",
        "randombytes": "npm:randombytes@2.0.3"
      }
    },
    "npm:browserify-aes@1.0.6": {
      "map": {
        "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
        "inherits": "npm:inherits@2.0.3",
        "create-hash": "npm:create-hash@1.1.2",
        "cipher-base": "npm:cipher-base@1.0.3",
        "buffer-xor": "npm:buffer-xor@1.0.3"
      }
    },
    "npm:browserify-des@1.0.0": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "cipher-base": "npm:cipher-base@1.0.3",
        "des.js": "npm:des.js@1.0.0"
      }
    },
    "npm:hash.js@1.0.3": {
      "map": {
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:evp_bytestokey@1.0.0": {
      "map": {
        "create-hash": "npm:create-hash@1.1.2"
      }
    },
    "npm:create-hash@1.1.2": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "cipher-base": "npm:cipher-base@1.0.3",
        "ripemd160": "npm:ripemd160@1.0.1",
        "sha.js": "npm:sha.js@2.4.8"
      }
    },
    "npm:des.js@1.0.0": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
      }
    },
    "npm:browserify-sign@4.0.0": {
      "map": {
        "bn.js": "npm:bn.js@4.11.6",
        "browserify-rsa": "npm:browserify-rsa@4.0.1",
        "create-hash": "npm:create-hash@1.1.2",
        "create-hmac": "npm:create-hmac@1.1.4",
        "elliptic": "npm:elliptic@6.3.2",
        "inherits": "npm:inherits@2.0.3",
        "parse-asn1": "npm:parse-asn1@5.0.0"
      }
    },
    "npm:parse-asn1@5.0.0": {
      "map": {
        "browserify-aes": "npm:browserify-aes@1.0.6",
        "create-hash": "npm:create-hash@1.1.2",
        "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
        "pbkdf2": "npm:pbkdf2@3.0.9",
        "asn1.js": "npm:asn1.js@4.9.1"
      }
    },
    "npm:stream-browserify@2.0.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "readable-stream": "npm:readable-stream@2.2.2"
      }
    },
    "npm:miller-rabin@4.0.0": {
      "map": {
        "bn.js": "npm:bn.js@4.11.6",
        "brorand": "npm:brorand@1.0.6"
      }
    },
    "npm:url@0.11.0": {
      "map": {
        "querystring": "npm:querystring@0.2.0",
        "punycode": "npm:punycode@1.3.2"
      }
    },
    "npm:browserify-zlib@0.1.4": {
      "map": {
        "readable-stream": "npm:readable-stream@2.2.2",
        "pako": "npm:pako@0.2.9"
      }
    },
    "github:twbs/bootstrap@3.3.7": {
      "map": {
        "jquery": "npm:jquery@2.2.4"
      }
    },
    "npm:buffer@4.9.1": {
      "map": {
        "base64-js": "npm:base64-js@1.2.0",
        "ieee754": "npm:ieee754@1.1.8",
        "isarray": "npm:isarray@1.0.0"
      }
    },
    "npm:cipher-base@1.0.3": {
      "map": {
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:elliptic@6.3.2": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "bn.js": "npm:bn.js@4.11.6",
        "brorand": "npm:brorand@1.0.6",
        "hash.js": "npm:hash.js@1.0.3"
      }
    },
    "npm:jspm-nodelibs-stream@0.2.0": {
      "map": {
        "stream-browserify": "npm:stream-browserify@2.0.1"
      }
    },
    "npm:jspm-nodelibs-url@0.2.0": {
      "map": {
        "url-browserify": "npm:url@0.11.0"
      }
    },
    "npm:jspm-nodelibs-string_decoder@0.2.0": {
      "map": {
        "string_decoder-browserify": "npm:string_decoder@0.10.31"
      }
    },
    "npm:jspm-nodelibs-crypto@0.2.0": {
      "map": {
        "crypto-browserify": "npm:crypto-browserify@3.11.0"
      }
    },
    "npm:jspm-nodelibs-os@0.2.0": {
      "map": {
        "os-browserify": "npm:os-browserify@0.2.1"
      }
    },
    "npm:jspm-nodelibs-punycode@0.2.0": {
      "map": {
        "punycode-browserify": "npm:punycode@1.4.1"
      }
    },
    "npm:pbkdf2@3.0.9": {
      "map": {
        "create-hmac": "npm:create-hmac@1.1.4"
      }
    },
    "npm:jspm-nodelibs-http@0.2.0": {
      "map": {
        "http-browserify": "npm:stream-http@2.6.2"
      }
    },
    "npm:font-awesome@4.7.0": {
      "map": {
        "css": "github:systemjs/plugin-css@0.1.32"
      }
    },
    "npm:sha.js@2.4.8": {
      "map": {
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:@types/bootstrap@3.3.32": {
      "map": {
        "@types/jquery": "npm:@types/jquery@2.0.39"
      }
    },
    "github:twitter/typeahead.js@0.11.1": {
      "map": {
        "jquery": "npm:jquery@2.2.4"
      }
    },
    "npm:jspm-nodelibs-buffer@0.2.1": {
      "map": {
        "buffer": "npm:buffer@4.9.1"
      }
    },
    "npm:jspm-nodelibs-zlib@0.2.2": {
      "map": {
        "browserify-zlib": "npm:browserify-zlib@0.1.4"
      }
    },
    "npm:readable-stream@2.2.2": {
      "map": {
        "isarray": "npm:isarray@1.0.0",
        "inherits": "npm:inherits@2.0.3",
        "string_decoder": "npm:string_decoder@0.10.31",
        "buffer-shims": "npm:buffer-shims@1.0.0",
        "process-nextick-args": "npm:process-nextick-args@1.0.7",
        "core-util-is": "npm:core-util-is@1.0.2",
        "util-deprecate": "npm:util-deprecate@1.0.2"
      }
    },
    "npm:asn1.js@4.9.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0",
        "bn.js": "npm:bn.js@4.11.6"
      }
    },
    "npm:chalk@1.1.3": {
      "map": {
        "supports-color": "npm:supports-color@2.0.0",
        "escape-string-regexp": "npm:escape-string-regexp@1.0.5",
        "has-ansi": "npm:has-ansi@2.0.0",
        "ansi-styles": "npm:ansi-styles@2.2.1",
        "strip-ansi": "npm:strip-ansi@3.0.1"
      }
    },
    "npm:has-ansi@2.0.0": {
      "map": {
        "ansi-regex": "npm:ansi-regex@2.1.1"
      }
    },
    "npm:strip-ansi@3.0.1": {
      "map": {
        "ansi-regex": "npm:ansi-regex@2.1.1"
      }
    },
    "npm:once@1.4.0": {
      "map": {
        "wrappy": "npm:wrappy@1.0.2"
      }
    },
    "npm:minimatch@3.0.3": {
      "map": {
        "brace-expansion": "npm:brace-expansion@1.1.6"
      }
    },
    "npm:brace-expansion@1.1.6": {
      "map": {
        "balanced-match": "npm:balanced-match@0.4.2",
        "concat-map": "npm:concat-map@0.0.1"
      }
    },
    "npm:rimraf@2.5.4": {
      "map": {
        "glob": "npm:glob@7.1.1"
      }
    },
    "npm:glob@7.1.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimatch": "npm:minimatch@3.0.3",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "fs.realpath": "npm:fs.realpath@1.0.0",
        "once": "npm:once@1.4.0",
        "inflight": "npm:inflight@1.0.6"
      }
    },
    "npm:inflight@1.0.6": {
      "map": {
        "once": "npm:once@1.4.0",
        "wrappy": "npm:wrappy@1.0.2"
      }
    },
    "github:mobilexag/plugin-sass@0.5.1": {
      "map": {
        "css-asset-copier": "npm:css-asset-copier@1.0.2",
        "css-url-rewriter-ex": "npm:css-url-rewriter-ex@1.0.6",
        "postcss": "npm:postcss@5.2.10",
        "sass.js": "npm:sass.js@0.9.13",
        "autoprefixer": "npm:autoprefixer@6.6.1",
        "fs": "npm:jspm-nodelibs-fs@0.2.0",
        "url": "npm:jspm-nodelibs-url@0.2.0",
        "path": "npm:jspm-nodelibs-path@0.2.0",
        "reqwest": "github:ded/reqwest@2.0.5"
      }
    },
    "npm:css-url-rewriter-ex@1.0.6": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0"
      }
    },
    "npm:css-asset-copier@1.0.2": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "fs-extra": "npm:fs-extra@0.30.0"
      }
    },
    "npm:babel-runtime@6.20.0": {
      "map": {
        "regenerator-runtime": "npm:regenerator-runtime@0.10.1",
        "core-js": "npm:core-js@2.4.1"
      }
    },
    "npm:fs-extra@0.30.0": {
      "map": {
        "klaw": "npm:klaw@1.3.1",
        "jsonfile": "npm:jsonfile@2.4.0",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "rimraf": "npm:rimraf@2.5.4",
        "graceful-fs": "npm:graceful-fs@4.1.11"
      }
    },
    "npm:mkdirp@0.5.1": {
      "map": {
        "minimist": "npm:minimist@0.0.8"
      }
    },
    "npm:babel-core@6.21.0": {
      "map": {
        "babylon": "npm:babylon@6.15.0",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "source-map": "npm:source-map@0.5.6",
        "babel-messages": "npm:babel-messages@6.8.0",
        "babel-template": "npm:babel-template@6.16.0",
        "babel-code-frame": "npm:babel-code-frame@6.20.0",
        "babel-generator": "npm:babel-generator@6.21.0",
        "babel-helpers": "npm:babel-helpers@6.16.0",
        "babel-register": "npm:babel-register@6.18.0",
        "babel-traverse": "npm:babel-traverse@6.21.0",
        "babel-types": "npm:babel-types@6.21.0",
        "minimatch": "npm:minimatch@3.0.3",
        "debug": "npm:debug@2.6.0",
        "private": "npm:private@0.1.6",
        "convert-source-map": "npm:convert-source-map@1.3.0",
        "slash": "npm:slash@1.0.0",
        "json5": "npm:json5@0.5.1",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-messages@6.8.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0"
      }
    },
    "npm:babel-template@6.16.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "babylon": "npm:babylon@6.15.0",
        "babel-traverse": "npm:babel-traverse@6.21.0",
        "babel-types": "npm:babel-types@6.21.0",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-generator@6.21.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "source-map": "npm:source-map@0.5.6",
        "babel-messages": "npm:babel-messages@6.8.0",
        "babel-types": "npm:babel-types@6.21.0",
        "detect-indent": "npm:detect-indent@4.0.0",
        "lodash": "npm:lodash@4.17.4",
        "jsesc": "npm:jsesc@1.3.0"
      }
    },
    "npm:babel-helpers@6.16.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "babel-template": "npm:babel-template@6.16.0"
      }
    },
    "npm:babel-register@6.18.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "core-js": "npm:core-js@2.4.1",
        "babel-core": "npm:babel-core@6.21.0",
        "home-or-tmp": "npm:home-or-tmp@2.0.0",
        "mkdirp": "npm:mkdirp@0.5.1",
        "source-map-support": "npm:source-map-support@0.4.9",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-traverse@6.21.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "babel-code-frame": "npm:babel-code-frame@6.20.0",
        "babel-messages": "npm:babel-messages@6.8.0",
        "babel-types": "npm:babel-types@6.21.0",
        "babylon": "npm:babylon@6.15.0",
        "debug": "npm:debug@2.6.0",
        "invariant": "npm:invariant@2.2.2",
        "globals": "npm:globals@9.14.0",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-types@6.21.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.20.0",
        "esutils": "npm:esutils@2.0.2",
        "to-fast-properties": "npm:to-fast-properties@1.0.2",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-code-frame@6.20.0": {
      "map": {
        "esutils": "npm:esutils@2.0.2",
        "js-tokens": "npm:js-tokens@2.0.0",
        "chalk": "npm:chalk@1.1.3"
      }
    },
    "npm:debug@2.6.0": {
      "map": {
        "ms": "npm:ms@0.7.2"
      }
    },
    "npm:home-or-tmp@2.0.0": {
      "map": {
        "os-homedir": "npm:os-homedir@1.0.2",
        "os-tmpdir": "npm:os-tmpdir@1.0.2"
      }
    },
    "npm:invariant@2.2.2": {
      "map": {
        "loose-envify": "npm:loose-envify@1.3.1"
      }
    },
    "npm:detect-indent@4.0.0": {
      "map": {
        "repeating": "npm:repeating@2.0.1"
      }
    },
    "npm:repeating@2.0.1": {
      "map": {
        "is-finite": "npm:is-finite@1.0.2"
      }
    },
    "npm:is-finite@1.0.2": {
      "map": {
        "number-is-nan": "npm:number-is-nan@1.0.1"
      }
    },
    "npm:@types/typeahead@0.11.27": {
      "map": {
        "@types/jquery": "npm:@types/jquery@2.0.39"
      }
    },
    "npm:@types/whatwg-fetch@0.0.33": {
      "map": {
        "@types/whatwg-streams": "npm:@types/whatwg-streams@0.0.2"
      }
    },
    "npm:jspm-nodelibs-domain@0.2.0": {
      "map": {
        "domain-browserify": "npm:domain-browser@1.1.7"
      }
    },
    "npm:autoprefixer@6.6.1": {
      "map": {
        "postcss": "npm:postcss@5.2.10",
        "normalize-range": "npm:normalize-range@0.1.2",
        "num2fraction": "npm:num2fraction@1.2.2",
        "postcss-value-parser": "npm:postcss-value-parser@3.3.0",
        "browserslist": "npm:browserslist@1.5.2",
        "caniuse-db": "npm:caniuse-db@1.0.30000611"
      }
    },
    "npm:browserslist@1.5.2": {
      "map": {
        "caniuse-db": "npm:caniuse-db@1.0.30000611"
      }
    },
    "npm:rxjs-es@5.0.0-beta.12": {
      "format": "esm",
      "map": {
        "symbol-observable": "npm:symbol-observable@1.0.4"
      }
    },
    "npm:source-map-support@0.4.9": {
      "map": {
        "source-map": "npm:source-map@0.5.6"
      }
    },
    "npm:loose-envify@1.3.1": {
      "map": {
        "js-tokens": "npm:js-tokens@3.0.0"
      }
    },
    "npm:postcss@5.2.10": {
      "map": {
        "chalk": "npm:chalk@1.1.3",
        "source-map": "npm:source-map@0.5.6",
        "js-base64": "npm:js-base64@2.1.9",
        "supports-color": "npm:supports-color@3.2.3"
      }
    },
    "npm:supports-color@3.2.3": {
      "map": {
        "has-flag": "npm:has-flag@1.0.0"
      }
    },
    "npm:stream-http@2.6.2": {
      "map": {
        "readable-stream": "npm:readable-stream@2.2.2",
        "inherits": "npm:inherits@2.0.3",
        "to-arraybuffer": "npm:to-arraybuffer@1.0.1",
        "builtin-status-codes": "npm:builtin-status-codes@3.0.0",
        "xtend": "npm:xtend@4.0.1"
      }
    }
  }
});
