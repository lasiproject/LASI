SystemJS.config({
  map: {
    "plugin-typescript": "github:frankwallis/plugin-typescript@7.1.1",
    "aurelia-testing": "npm:aurelia-testing@1.0.0-beta.4.0.0",
    "source-map-support": "npm:source-map-support@0.4.18",
    "tape": "npm:tape@4.8.0",
    "fsevents": "npm:fsevents@1.1.3",
    "bcrypt-pbkdf": "npm:bcrypt-pbkdf@1.0.1",
    "jsbn": "npm:jsbn@0.1.1",
    "ecc-jsbn": "npm:ecc-jsbn@0.1.1",
    "jodid25519": "npm:jodid25519@1.0.2",
    "tweetnacl": "npm:tweetnacl@0.14.5",
    "debug": "npm:debug@2.6.9",
    "timers": "npm:jspm-nodelibs-timers@0.2.1",
    "request": "npm:request@2.83.0",
    "plugin-babel": "npm:systemjs-plugin-babel@0.0.25",
    "css": "github:systemjs/plugin-css@0.1.36",
    "text": "github:systemjs/plugin-text@0.0.9",
    "@types/blue-tape": "npm:@types/blue-tape@latest",
    "@types/tape": "npm:@types/tape@latest"
  },
  packages: {
    "npm:resumer@0.0.0": {
      "map": {
        "through": "npm:through@2.3.8"
      }
    },
    "npm:string.prototype.trim@1.1.2": {
      "map": {
        "function-bind": "npm:function-bind@1.1.1",
        "define-properties": "npm:define-properties@1.1.2",
        "es-abstract": "npm:es-abstract@1.9.0"
      }
    },
    "npm:define-properties@1.1.2": {
      "map": {
        "foreach": "npm:foreach@2.0.5",
        "object-keys": "npm:object-keys@1.0.11"
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
        "function-bind": "npm:function-bind@1.1.1"
      }
    },
    "npm:string-width@1.0.2": {
      "map": {
        "strip-ansi": "npm:strip-ansi@3.0.1",
        "code-point-at": "npm:code-point-at@1.1.0",
        "is-fullwidth-code-point": "npm:is-fullwidth-code-point@1.0.0"
      }
    },
    "npm:is-fullwidth-code-point@1.0.0": {
      "map": {
        "number-is-nan": "npm:number-is-nan@1.0.1"
      }
    },
    "npm:tar@2.2.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "block-stream": "npm:block-stream@0.0.9",
        "fstream": "npm:fstream@1.0.11"
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
        "jsprim": "npm:jsprim@1.4.1",
        "sshpk": "npm:sshpk@1.13.1"
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
        "minimatch": "npm:minimatch@3.0.4",
        "fstream": "npm:fstream@1.0.11"
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
    "npm:boom@2.10.1": {
      "map": {
        "hoek": "npm:hoek@2.16.3"
      }
    },
    "npm:ecc-jsbn@0.1.1": {
      "map": {
        "jsbn": "npm:jsbn@0.1.1"
      }
    },
    "npm:jodid25519@1.0.2": {
      "map": {
        "jsbn": "npm:jsbn@0.1.1"
      }
    },
    "npm:timers-browserify@1.4.2": {
      "map": {
        "process": "npm:process@0.11.10"
      }
    },
    "npm:for-each@0.3.2": {
      "map": {
        "is-function": "npm:is-function@1.0.1"
      }
    },
    "npm:dashdash@1.14.1": {
      "map": {
        "assert-plus": "npm:assert-plus@1.0.0"
      }
    },
    "npm:bcrypt-pbkdf@1.0.1": {
      "map": {
        "tweetnacl": "npm:tweetnacl@0.14.5"
      }
    },
    "npm:request@2.81.0": {
      "map": {
        "isstream": "npm:isstream@0.1.2",
        "form-data": "npm:form-data@2.1.4",
        "aws4": "npm:aws4@1.6.0",
        "http-signature": "npm:http-signature@1.1.1",
        "stringstream": "npm:stringstream@0.0.5",
        "mime-types": "npm:mime-types@2.1.17",
        "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
        "oauth-sign": "npm:oauth-sign@0.8.2",
        "tough-cookie": "npm:tough-cookie@2.3.3",
        "har-validator": "npm:har-validator@4.2.1",
        "caseless": "npm:caseless@0.12.0",
        "tunnel-agent": "npm:tunnel-agent@0.6.0",
        "qs": "npm:qs@6.4.0",
        "combined-stream": "npm:combined-stream@1.0.5",
        "extend": "npm:extend@3.0.1",
        "forever-agent": "npm:forever-agent@0.6.1",
        "aws-sign2": "npm:aws-sign2@0.6.0",
        "is-typedarray": "npm:is-typedarray@1.0.0",
        "hawk": "npm:hawk@3.1.3",
        "uuid": "npm:uuid@3.1.0",
        "performance-now": "npm:performance-now@0.2.0",
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    },
    "npm:tunnel-agent@0.6.0": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    },
    "npm:har-validator@4.2.1": {
      "map": {
        "har-schema": "npm:har-schema@1.0.5",
        "ajv": "npm:ajv@4.11.8"
      }
    },
    "npm:jspm-nodelibs-timers@0.2.1": {
      "map": {
        "timers-browserify": "npm:timers-browserify@1.4.2"
      }
    },
    "npm:nopt@4.0.1": {
      "map": {
        "abbrev": "npm:abbrev@1.1.1",
        "osenv": "npm:osenv@0.1.4"
      }
    },
    "npm:fstream@1.0.11": {
      "map": {
        "graceful-fs": "npm:graceful-fs@4.1.11",
        "inherits": "npm:inherits@2.0.3",
        "rimraf": "npm:rimraf@2.6.2",
        "mkdirp": "npm:mkdirp@0.5.1"
      }
    },
    "npm:osenv@0.1.4": {
      "map": {
        "os-homedir": "npm:os-homedir@1.0.2",
        "os-tmpdir": "npm:os-tmpdir@1.0.2"
      }
    },
    "npm:is-regex@1.0.4": {
      "map": {
        "has": "npm:has@1.0.1"
      }
    },
    "npm:json-stable-stringify@1.0.1": {
      "map": {
        "jsonify": "npm:jsonify@0.0.0"
      }
    },
    "npm:form-data@2.1.4": {
      "map": {
        "combined-stream": "npm:combined-stream@1.0.5",
        "asynckit": "npm:asynckit@0.4.0",
        "mime-types": "npm:mime-types@2.1.17"
      }
    },
    "npm:tape@4.8.0": {
      "map": {
        "glob": "npm:glob@7.1.2",
        "for-each": "npm:for-each@0.3.2",
        "has": "npm:has@1.0.1",
        "minimist": "npm:minimist@1.2.0",
        "resolve": "npm:resolve@1.4.0",
        "defined": "npm:defined@1.0.0",
        "inherits": "npm:inherits@2.0.3",
        "deep-equal": "npm:deep-equal@1.0.1",
        "resumer": "npm:resumer@0.0.0",
        "string.prototype.trim": "npm:string.prototype.trim@1.1.2",
        "through": "npm:through@2.3.8",
        "object-inspect": "npm:object-inspect@1.3.0",
        "function-bind": "npm:function-bind@1.1.1"
      }
    },
    "npm:request@2.83.0": {
      "map": {
        "is-typedarray": "npm:is-typedarray@1.0.0",
        "mime-types": "npm:mime-types@2.1.17",
        "http-signature": "npm:http-signature@1.2.0",
        "caseless": "npm:caseless@0.12.0",
        "stringstream": "npm:stringstream@0.0.5",
        "form-data": "npm:form-data@2.3.1",
        "tough-cookie": "npm:tough-cookie@2.3.3",
        "qs": "npm:qs@6.5.1",
        "aws-sign2": "npm:aws-sign2@0.7.0",
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "combined-stream": "npm:combined-stream@1.0.5",
        "tunnel-agent": "npm:tunnel-agent@0.6.0",
        "forever-agent": "npm:forever-agent@0.6.1",
        "har-validator": "npm:har-validator@5.0.3",
        "aws4": "npm:aws4@1.6.0",
        "hawk": "npm:hawk@6.0.2",
        "extend": "npm:extend@3.0.1",
        "performance-now": "npm:performance-now@2.1.0",
        "uuid": "npm:uuid@3.1.0",
        "json-stringify-safe": "npm:json-stringify-safe@5.0.1",
        "isstream": "npm:isstream@0.1.2",
        "oauth-sign": "npm:oauth-sign@0.8.2"
      }
    },
    "npm:tar-pack@3.4.1": {
      "map": {
        "debug": "npm:debug@2.6.9",
        "rimraf": "npm:rimraf@2.6.2",
        "tar": "npm:tar@2.2.1",
        "uid-number": "npm:uid-number@0.0.6",
        "fstream-ignore": "npm:fstream-ignore@1.0.5",
        "fstream": "npm:fstream@1.0.11",
        "once": "npm:once@1.4.0",
        "readable-stream": "npm:readable-stream@2.3.3"
      }
    },
    "npm:tough-cookie@2.3.3": {
      "map": {
        "punycode": "npm:punycode@1.4.1"
      }
    },
    "npm:rc@1.2.2": {
      "map": {
        "minimist": "npm:minimist@1.2.0",
        "deep-extend": "npm:deep-extend@0.4.2",
        "ini": "npm:ini@1.3.4",
        "strip-json-comments": "npm:strip-json-comments@2.0.1"
      }
    },
    "npm:form-data@2.3.1": {
      "map": {
        "combined-stream": "npm:combined-stream@1.0.5",
        "mime-types": "npm:mime-types@2.1.17",
        "asynckit": "npm:asynckit@0.4.0"
      }
    },
    "npm:mime-types@2.1.17": {
      "map": {
        "mime-db": "npm:mime-db@1.30.0"
      }
    },
    "npm:http-signature@1.2.0": {
      "map": {
        "sshpk": "npm:sshpk@1.13.1",
        "assert-plus": "npm:assert-plus@1.0.0",
        "jsprim": "npm:jsprim@1.4.1"
      }
    },
    "npm:npmlog@4.1.2": {
      "map": {
        "set-blocking": "npm:set-blocking@2.0.0",
        "console-control-strings": "npm:console-control-strings@1.1.0",
        "gauge": "npm:gauge@2.7.4",
        "are-we-there-yet": "npm:are-we-there-yet@1.1.4"
      }
    },
    "npm:hawk@6.0.2": {
      "map": {
        "cryptiles": "npm:cryptiles@3.1.2",
        "hoek": "npm:hoek@4.2.0",
        "sntp": "npm:sntp@2.1.0",
        "boom": "npm:boom@4.3.1"
      }
    },
    "npm:resolve@1.4.0": {
      "map": {
        "path-parse": "npm:path-parse@1.0.5"
      }
    },
    "npm:har-validator@5.0.3": {
      "map": {
        "ajv": "npm:ajv@5.3.0",
        "har-schema": "npm:har-schema@2.0.0"
      }
    },
    "npm:gauge@2.7.4": {
      "map": {
        "console-control-strings": "npm:console-control-strings@1.1.0",
        "strip-ansi": "npm:strip-ansi@3.0.1",
        "aproba": "npm:aproba@1.2.0",
        "has-unicode": "npm:has-unicode@2.0.1",
        "object-assign": "npm:object-assign@4.1.1",
        "wide-align": "npm:wide-align@1.1.2",
        "string-width": "npm:string-width@1.0.2",
        "signal-exit": "npm:signal-exit@3.0.2"
      }
    },
    "npm:are-we-there-yet@1.1.4": {
      "map": {
        "readable-stream": "npm:readable-stream@2.3.3",
        "delegates": "npm:delegates@1.0.0"
      }
    },
    "npm:jsprim@1.4.1": {
      "map": {
        "assert-plus": "npm:assert-plus@1.0.0",
        "extsprintf": "npm:extsprintf@1.3.0",
        "verror": "npm:verror@1.10.0",
        "json-schema": "npm:json-schema@0.2.3"
      }
    },
    "npm:sshpk@1.13.1": {
      "map": {
        "assert-plus": "npm:assert-plus@1.0.0",
        "dashdash": "npm:dashdash@1.14.1",
        "getpass": "npm:getpass@0.1.7",
        "asn1": "npm:asn1@0.2.3"
      }
    },
    "npm:es-abstract@1.9.0": {
      "map": {
        "function-bind": "npm:function-bind@1.1.1",
        "has": "npm:has@1.0.1",
        "is-regex": "npm:is-regex@1.0.4",
        "is-callable": "npm:is-callable@1.1.3",
        "es-to-primitive": "npm:es-to-primitive@1.1.1"
      }
    },
    "npm:ajv@4.11.8": {
      "map": {
        "json-stable-stringify": "npm:json-stable-stringify@1.0.1",
        "co": "npm:co@4.6.0"
      }
    },
    "npm:ajv@5.3.0": {
      "map": {
        "co": "npm:co@4.6.0",
        "json-schema-traverse": "npm:json-schema-traverse@0.3.1",
        "fast-deep-equal": "npm:fast-deep-equal@1.0.0",
        "fast-json-stable-stringify": "npm:fast-json-stable-stringify@2.0.0"
      }
    },
    "npm:cryptiles@3.1.2": {
      "map": {
        "boom": "npm:boom@5.2.0"
      }
    },
    "npm:wide-align@1.1.2": {
      "map": {
        "string-width": "npm:string-width@1.0.2"
      }
    },
    "npm:sntp@2.1.0": {
      "map": {
        "hoek": "npm:hoek@4.2.0"
      }
    },
    "npm:boom@4.3.1": {
      "map": {
        "hoek": "npm:hoek@4.2.0"
      }
    },
    "npm:boom@5.2.0": {
      "map": {
        "hoek": "npm:hoek@4.2.0"
      }
    },
    "npm:verror@1.10.0": {
      "map": {
        "core-util-is": "npm:core-util-is@1.0.2",
        "extsprintf": "npm:extsprintf@1.3.0",
        "assert-plus": "npm:assert-plus@1.0.0"
      }
    },
    "npm:getpass@0.1.7": {
      "map": {
        "assert-plus": "npm:assert-plus@1.0.0"
      }
    },
    "npm:fsevents@1.1.3": {
      "map": {
        "node-pre-gyp": "npm:node-pre-gyp@0.6.39",
        "nan": "npm:nan@2.8.0"
      }
    },
    "npm:node-pre-gyp@0.6.39": {
      "map": {
        "request": "npm:request@2.81.0",
        "hawk": "npm:hawk@3.1.3",
        "detect-libc": "npm:detect-libc@1.0.2",
        "mkdirp": "npm:mkdirp@0.5.1",
        "semver": "npm:semver@5.4.1",
        "tar": "npm:tar@2.2.1",
        "nopt": "npm:nopt@4.0.1",
        "rimraf": "npm:rimraf@2.6.2",
        "tar-pack": "npm:tar-pack@3.4.1",
        "rc": "npm:rc@1.2.2",
        "npmlog": "npm:npmlog@4.1.2"
      }
    },
    "npm:@types/blue-tape@latest": {
      "map": {
        "@types/tape": "npm:@types/tape@latest",
        "@types/node": "npm:@types/node@8.0.53"
      }
    },
    "npm:@types/tape@latest": {
      "map": {
        "@types/node": "npm:@types/node@8.0.53"
      }
    }
  }
});
