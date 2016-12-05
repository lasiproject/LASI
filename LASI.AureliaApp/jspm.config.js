SystemJS.config({
  paths: {
    "github:": "jspm_packages/github/",
    "npm:": "jspm_packages/npm/",
    "app/": "src/"
  },
  meta: {
    "*.html": {
      "loader": "text"
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
        "app/**/*.html": {
          "defaultExtension": false,
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
    },
    "src/app": {
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
    "baseURL": "/"
  },
  devConfig: {
    "map": {
      "plugin-typescript": "github:frankwallis/plugin-typescript@5.3.1",
      "aurelia-testing": "npm:aurelia-testing@1.0.0-beta.2.0.0",
      "source-map-support": "npm:source-map-support@0.4.6",
      "tape": "npm:tape@4.6.3",
      "text": "github:systemjs/plugin-text@0.0.9",
      "@types/tape": "npm:@types/tape@4.2.28",
      "fsevents": "npm:fsevents@1.0.15",
      "bcrypt-pbkdf": "npm:bcrypt-pbkdf@1.0.0",
      "jsbn": "npm:jsbn@0.1.0",
      "ecc-jsbn": "npm:ecc-jsbn@0.1.1",
      "jodid25519": "npm:jodid25519@1.0.2",
      "tweetnacl": "npm:tweetnacl@0.14.4",
      "debug": "npm:debug@2.3.3",
      "timers": "npm:jspm-nodelibs-timers@0.2.0"
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
          "@types/node": "npm:@types/node@0.0.2"
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
      "npm:fsevents@1.0.15": {
        "map": {
          "nan": "npm:nan@2.4.0",
          "node-pre-gyp": "npm:node-pre-gyp@0.6.32"
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
      "npm:mkdirp@0.5.1": {
        "map": {
          "minimist": "npm:minimist@0.0.8"
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
          "sshpk": "npm:sshpk@1.10.1"
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
      "npm:sshpk@1.10.1": {
        "map": {
          "asn1": "npm:asn1@0.2.3",
          "assert-plus": "npm:assert-plus@1.0.0",
          "getpass": "npm:getpass@0.1.6",
          "dashdash": "npm:dashdash@1.14.1"
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
          "jsonpointer": "npm:jsonpointer@4.0.0",
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
          "tweetnacl": "npm:tweetnacl@0.14.4"
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
      "npm:source-map-support@0.4.6": {
        "map": {
          "source-map": "npm:source-map@0.5.6"
        }
      },
      "npm:form-data@2.1.2": {
        "map": {
          "combined-stream": "npm:combined-stream@1.0.5",
          "mime-types": "npm:mime-types@2.1.13",
          "asynckit": "npm:asynckit@0.4.0"
        }
      },
      "npm:debug@2.3.3": {
        "map": {
          "ms": "npm:ms@0.7.2"
        }
      },
      "github:frankwallis/plugin-typescript@5.3.1": {
        "map": {
          "typescript": "npm:typescript@next"
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
          "npmlog": "npm:npmlog@4.0.1",
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
      "npm:npmlog@4.0.1": {
        "map": {
          "gauge": "npm:gauge@2.7.2",
          "set-blocking": "npm:set-blocking@2.0.0",
          "console-control-strings": "npm:console-control-strings@1.1.0",
          "are-we-there-yet": "npm:are-we-there-yet@1.1.2"
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
          "mime-types": "npm:mime-types@2.1.13",
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
      "npm:mime-types@2.1.13": {
        "map": {
          "mime-db": "npm:mime-db@1.25.0"
        }
      },
      "npm:dashdash@1.14.1": {
        "map": {
          "assert-plus": "npm:assert-plus@1.0.0"
        }
      }
    }
  },
  transpiler: "plugin-typescript",
  typescriptOptions: {
    "typeCheck": false,
    "tsconfig": true
  },
  map: {
    "bootstrap-css": "github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css"
  }
});

SystemJS.config({
  packageConfigPaths: [
    "github:*/*.json",
    "npm:@*/*.json",
    "npm:*.json"
  ],
  map: {
    "aurelia-dialog": "npm:aurelia-dialog@1.0.0-beta.3.0.1",
    "@types/bootstrap": "npm:@types/bootstrap@3.3.32",
    "@types/whatwg-fetch": "npm:@types/whatwg-fetch@0.0.32",
    "aurelia-http-client": "npm:aurelia-http-client@1.0.3",
    "aurelia-fetch-client": "npm:aurelia-fetch-client@1.1.0",
    "aurelia-binding": "npm:aurelia-binding@1.0.9",
    "aurelia-bootstrapper": "npm:aurelia-bootstrapper@1.0.1",
    "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.2.0",
    "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.1",
    "aurelia-framework": "npm:aurelia-framework@1.0.7",
    "aurelia-history": "npm:aurelia-history@1.0.0",
    "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0",
    "aurelia-loader": "npm:aurelia-loader@1.0.0",
    "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0",
    "aurelia-logging": "npm:aurelia-logging@1.2.0",
    "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
    "aurelia-metadata": "npm:aurelia-metadata@1.0.2",
    "aurelia-pal": "npm:aurelia-pal@1.0.0",
    "aurelia-pal-browser": "npm:aurelia-pal-browser@1.0.0",
    "aurelia-path": "npm:aurelia-path@1.1.1",
    "aurelia-polyfills": "npm:aurelia-polyfills@1.1.1",
    "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.1.0",
    "aurelia-router": "npm:aurelia-router@1.1.0",
    "aurelia-task-queue": "npm:aurelia-task-queue@1.1.0",
    "aurelia-templating": "npm:aurelia-templating@1.1.4",
    "aurelia-templating-binding": "npm:aurelia-templating-binding@1.0.0",
    "aurelia-templating-resources": "npm:aurelia-templating-resources@1.1.1",
    "aurelia-templating-router": "npm:aurelia-templating-router@1.0.1",
    "assert": "npm:jspm-nodelibs-assert@0.2.0",
    "bootstrap": "github:twbs/bootstrap@3.3.7",
    "buffer": "npm:jspm-nodelibs-buffer@0.2.1",
    "child_process": "npm:jspm-nodelibs-child_process@0.2.0",
    "constants": "npm:jspm-nodelibs-constants@0.2.0",
    "core-js": "npm:core-js@2.4.1",
    "crypto": "npm:jspm-nodelibs-crypto@0.2.0",
    "css": "github:systemjs/plugin-css@0.1.32",
    "dgram": "npm:jspm-nodelibs-dgram@0.2.0",
    "dns": "npm:jspm-nodelibs-dns@0.2.0",
    "events": "npm:jspm-nodelibs-events@0.2.0",
    "font-awesome": "npm:font-awesome@4.7.0",
    "fs": "npm:jspm-nodelibs-fs@0.2.0",
    "graceful-fs": "npm:graceful-fs@4.1.11",
    "http": "npm:jspm-nodelibs-http@0.2.0",
    "https": "npm:jspm-nodelibs-https@0.2.1",
    "jquery": "npm:jquery@2.2.4",
    "jquery-contextmenu": "npm:jquery-contextmenu@2.3.0",
    "module": "npm:jspm-nodelibs-module@0.2.0",
    "net": "npm:jspm-nodelibs-net@0.2.0",
    "os": "npm:jspm-nodelibs-os@0.2.0",
    "path": "npm:jspm-nodelibs-path@0.2.1",
    "process": "npm:jspm-nodelibs-process@0.2.0",
    "punycode": "npm:jspm-nodelibs-punycode@0.2.0",
    "querystring": "npm:jspm-nodelibs-querystring@0.2.0",
    "readline": "npm:jspm-nodelibs-readline@0.2.0",
    "reflect-metadata": "npm:reflect-metadata@0.1.8",
    "sass": "github:mobilexag/plugin-sass@0.5.1",
    "stream": "npm:jspm-nodelibs-stream@0.2.0",
    "string_decoder": "npm:jspm-nodelibs-string_decoder@0.2.0",
    "tls": "npm:jspm-nodelibs-tls@0.2.0",
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
        "asn1.js": "npm:asn1.js@4.9.0"
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
    "npm:supports-color@3.1.2": {
      "map": {
        "has-flag": "npm:has-flag@1.0.0"
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
    "npm:browserslist@1.4.0": {
      "map": {
        "caniuse-db": "npm:caniuse-db@1.0.30000591"
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
        "ansi-regex": "npm:ansi-regex@2.0.0"
      }
    },
    "npm:strip-ansi@3.0.1": {
      "map": {
        "ansi-regex": "npm:ansi-regex@2.0.0"
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
    "npm:stream-http@2.5.0": {
      "map": {
        "readable-stream": "npm:readable-stream@2.2.2",
        "xtend": "npm:xtend@4.0.1",
        "inherits": "npm:inherits@2.0.3",
        "to-arraybuffer": "npm:to-arraybuffer@1.0.1",
        "builtin-status-codes": "npm:builtin-status-codes@2.0.0"
      }
    },
    "npm:jspm-nodelibs-http@0.2.0": {
      "map": {
        "http-browserify": "npm:stream-http@2.5.0"
      }
    },
    "npm:@types/whatwg-fetch@0.0.32": {
      "map": {
        "@types/whatwg-streams": "npm:@types/whatwg-streams@0.0.1"
      }
    },
    "npm:font-awesome@4.7.0": {
      "map": {
        "css": "github:systemjs/plugin-css@0.1.32"
      }
    },
    "npm:autoprefixer@6.5.3": {
      "map": {
        "postcss": "npm:postcss@5.2.6",
        "postcss-value-parser": "npm:postcss-value-parser@3.3.0",
        "normalize-range": "npm:normalize-range@0.1.2",
        "num2fraction": "npm:num2fraction@1.2.2",
        "browserslist": "npm:browserslist@1.4.0",
        "caniuse-db": "npm:caniuse-db@1.0.30000591"
      }
    },
    "npm:sha.js@2.4.8": {
      "map": {
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:asn1.js@4.9.0": {
      "map": {
        "bn.js": "npm:bn.js@4.11.6",
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
      }
    },
    "npm:@types/bootstrap@3.3.32": {
      "map": {
        "@types/jquery": "npm:@types/jquery@2.0.34"
      }
    },
    "github:twitter/typeahead.js@0.11.1": {
      "map": {
        "jquery": "npm:jquery@2.2.4"
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
    "npm:css-asset-copier@1.0.2": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.18.0",
        "fs-extra": "npm:fs-extra@0.30.0"
      }
    },
    "npm:css-url-rewriter-ex@1.0.6": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.18.0"
      }
    },
    "npm:babel-runtime@6.18.0": {
      "map": {
        "core-js": "npm:core-js@2.4.1",
        "regenerator-runtime": "npm:regenerator-runtime@0.9.6"
      }
    },
    "npm:fs-extra@0.30.0": {
      "map": {
        "rimraf": "npm:rimraf@2.5.4",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "jsonfile": "npm:jsonfile@2.4.0",
        "klaw": "npm:klaw@1.3.1",
        "graceful-fs": "npm:graceful-fs@4.1.11"
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
    "github:mobilexag/plugin-sass@0.5.1": {
      "map": {
        "postcss": "npm:postcss@5.2.6",
        "css-asset-copier": "npm:css-asset-copier@1.0.2",
        "autoprefixer": "npm:autoprefixer@6.5.3",
        "css-url-rewriter-ex": "npm:css-url-rewriter-ex@1.0.6",
        "sass.js": "npm:sass.js@0.9.13",
        "url": "npm:jspm-nodelibs-url@0.2.0",
        "path": "npm:jspm-nodelibs-path@0.2.0",
        "fs": "npm:jspm-nodelibs-fs@0.2.0",
        "reqwest": "github:ded/reqwest@2.0.5"
      }
    },
    "npm:postcss@5.2.6": {
      "map": {
        "source-map": "npm:source-map@0.5.6",
        "supports-color": "npm:supports-color@3.1.2",
        "js-base64": "npm:js-base64@2.1.9",
        "chalk": "npm:chalk@1.1.3"
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
    }
  }
});