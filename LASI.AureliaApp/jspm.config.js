SystemJS.config({
    paths: {
        "github:": "jspm_packages/github/",
        "npm:": "jspm_packages/npm/"
    },
    packages: {
        "src": {
            "main": "main",
            "defaultExtension": "ts",
            "meta": {
                "*.ts": {
                    "loader": "plugin-typescript"
                }
            }
        },
        "shared": {
            "defaultExtension": "ts",
            "meta": {
                "*.ts": {
                    "loader": "plugin-typescript"
                }
            }
        },
        "src/app": {
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
                }
            }
        }
    },
    browserConfig: {
        "baseURL": "/"
    },
    devConfig: {
        "map": {
            "typescript": "npm:typescript@1.9.0-dev.20160609-1.0",
            "plugin-typescript": "github:frankwallis/plugin-typescript@4.0.16"
        },
        "packages": {
            "github:frankwallis/plugin-typescript@4.0.16": {
                "map": {
                    "typescript": "npm:typescript@1.9.0-dev.20160609-1.0"
                }
            }
        }
    },
    transpiler: "plugin-typescript",
    typescriptOptions: {
        "target": "es6",
        "module": "system",
        "typeCheck": false,
        "emitDecoratorMetadata": true,
        "experimentalDecorators": true,
        "experimentalAsyncFunctions": true
    },
    map: {
        "bootstrap-css": "github:twbs/bootstrap@3.3.6/dist/css/bootstrap.css"
    }
});

SystemJS.config({
    packageConfigPaths: [
        "github:*/*.json",
        "npm:@*/*.json",
        "npm:*.json"
    ],
    map: {
        "aurelia-dialog": "npm:aurelia-dialog@0.6.2",
        "assert": "github:jspm/nodelibs-assert@0.2.0-alpha",
        "aurelia-binding": "npm:aurelia-binding@1.0.0-beta.1.3.6",
        "aurelia-bootstrapper": "npm:aurelia-bootstrapper@1.0.0-beta.1.2.1",
        "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.0.0-beta.1.2.3",
        "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.0-beta.1.2.1",
        "aurelia-fetch-client": "npm:aurelia-fetch-client@1.0.0-beta.1.2.5",
        "aurelia-framework": "npm:aurelia-framework@1.0.0-beta.1.2.5",
        "aurelia-history": "npm:aurelia-history@1.0.0-beta.1.2.1",
        "aurelia-history-browser": "npm:aurelia-history-browser@1.0.0-beta.1.2.1",
        "aurelia-http-client": "npm:aurelia-http-client@1.0.0-beta.1.2.2",
        "aurelia-loader": "npm:aurelia-loader@1.0.0-beta.1.2.0",
        "aurelia-loader-default": "npm:aurelia-loader-default@1.0.0-beta.1.2.2",
        "aurelia-logging": "npm:aurelia-logging@1.0.0-beta.1.2.1",
        "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0-beta.1.2.2",
        "aurelia-metadata": "npm:aurelia-metadata@1.0.0-beta.1.2.1",
        "aurelia-pal": "npm:aurelia-pal@1.0.0-beta.1.2.2",
        "aurelia-pal-browser": "npm:aurelia-pal-browser@1.0.0-beta.2.0.1",
        "aurelia-path": "npm:aurelia-path@1.0.0-beta.1.2.2",
        "aurelia-polyfills": "npm:aurelia-polyfills@1.0.0-beta.1.1.6",
        "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.0.0-beta.1.2.1",
        "aurelia-router": "npm:aurelia-router@1.0.0-beta.1.2.4",
        "aurelia-task-queue": "npm:aurelia-task-queue@1.0.0-beta.1.2.1",
        "aurelia-templating": "npm:aurelia-templating@1.0.0-beta.1.2.7",
        "aurelia-templating-binding": "npm:aurelia-templating-binding@1.0.0-beta.1.2.4",
        "aurelia-templating-resources": "npm:aurelia-templating-resources@1.0.0-beta.1.2.6",
        "aurelia-templating-router": "npm:aurelia-templating-router@1.0.0-beta.1.2.1",
        "aurelia-typeahead": "npm:aurelia-typeahead@1.0.5",
        "bootstrap": "github:twbs/bootstrap@3.3.6",
        "buffer": "github:jspm/nodelibs-buffer@0.2.0-alpha",
        "child_process": "github:jspm/nodelibs-child_process@0.2.0-alpha",
        "constants": "github:jspm/nodelibs-constants@0.2.0-alpha",
        "crypto": "github:jspm/nodelibs-crypto@0.2.0-alpha",
        "css": "github:systemjs/plugin-css@0.1.22",
        "dgram": "github:jspm/nodelibs-dgram@0.2.0-alpha",
        "dns": "github:jspm/nodelibs-dns@0.2.0-alpha",
        "events": "github:jspm/nodelibs-events@0.2.0-alpha",
        "font-awesome": "npm:font-awesome@4.6.3",
        "fs": "github:jspm/nodelibs-fs@0.2.0-alpha",
        "http": "github:jspm/nodelibs-http@0.2.0-alpha",
        "https": "github:jspm/nodelibs-https@0.2.0-alpha",
        "jquery": "npm:jquery@2.2.4",
        "module": "github:jspm/nodelibs-module@0.2.0-alpha",
        "net": "github:jspm/nodelibs-net@0.2.0-alpha",
        "os": "github:jspm/nodelibs-os@0.2.0-alpha",
        "path": "github:jspm/nodelibs-path@0.2.0-alpha",
        "process": "github:jspm/nodelibs-process@0.2.0-alpha",
        "punycode": "github:jspm/nodelibs-punycode@0.2.0-alpha",
        "querystring": "github:jspm/nodelibs-querystring@0.2.0-alpha",
        "readline": "github:jspm/nodelibs-readline@0.2.0-alpha",
        "stream": "github:jspm/nodelibs-stream@0.2.0-alpha",
        "string_decoder": "github:jspm/nodelibs-string_decoder@0.2.0-alpha",
        "tls": "github:jspm/nodelibs-tls@0.2.0-alpha",
        "tty": "github:jspm/nodelibs-tty@0.2.0-alpha",
        "url": "github:jspm/nodelibs-url@0.2.0-alpha",
        "util": "github:jspm/nodelibs-util@0.2.0-alpha",
        "vm": "github:jspm/nodelibs-vm@0.2.0-alpha",
        "zlib": "github:jspm/nodelibs-zlib@0.2.0-alpha"
    },
    packages: {
        "github:twbs/bootstrap@3.3.6": {
            "meta": {
                "*.css": {
                    "loader": "css"
                }
            },
            "map": {
                "jquery": "npm:jquery@2.2.4"
            }
        },
        "npm:font-awesome@4.6.3": {
            "meta": {
                "*.css": {
                    "loader": "css"
                }
            },
            "map": {
                "css": "github:systemjs/plugin-css@0.1.22"
            }
        },
        "github:jspm/nodelibs-os@0.2.0-alpha": {
            "map": {
                "os-browserify": "npm:os-browserify@0.2.1"
            }
        },
        "github:jspm/nodelibs-crypto@0.2.0-alpha": {
            "map": {
                "crypto-browserify": "npm:crypto-browserify@3.11.0"
            }
        },
        "npm:crypto-browserify@3.11.0": {
            "map": {
                "create-ecdh": "npm:create-ecdh@4.0.0",
                "inherits": "npm:inherits@2.0.1",
                "public-encrypt": "npm:public-encrypt@4.0.0",
                "pbkdf2": "npm:pbkdf2@3.0.4",
                "browserify-cipher": "npm:browserify-cipher@1.0.0",
                "create-hmac": "npm:create-hmac@1.1.4",
                "diffie-hellman": "npm:diffie-hellman@5.0.2",
                "randombytes": "npm:randombytes@2.0.3",
                "create-hash": "npm:create-hash@1.1.2",
                "browserify-sign": "npm:browserify-sign@4.0.0"
            }
        },
        "npm:pbkdf2@3.0.4": {
            "map": {
                "create-hmac": "npm:create-hmac@1.1.4"
            }
        },
        "npm:create-hmac@1.1.4": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "create-hash": "npm:create-hash@1.1.2"
            }
        },
        "npm:create-ecdh@4.0.0": {
            "map": {
                "elliptic": "npm:elliptic@6.2.8",
                "bn.js": "npm:bn.js@4.11.4"
            }
        },
        "npm:public-encrypt@4.0.0": {
            "map": {
                "browserify-rsa": "npm:browserify-rsa@4.0.1",
                "bn.js": "npm:bn.js@4.11.4",
                "randombytes": "npm:randombytes@2.0.3",
                "create-hash": "npm:create-hash@1.1.2",
                "parse-asn1": "npm:parse-asn1@5.0.0"
            }
        },
        "npm:elliptic@6.2.8": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "bn.js": "npm:bn.js@4.11.4",
                "hash.js": "npm:hash.js@1.0.3",
                "brorand": "npm:brorand@1.0.5"
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
                "bn.js": "npm:bn.js@4.11.4",
                "randombytes": "npm:randombytes@2.0.3",
                "miller-rabin": "npm:miller-rabin@4.0.0"
            }
        },
        "npm:browserify-rsa@4.0.1": {
            "map": {
                "bn.js": "npm:bn.js@4.11.4",
                "randombytes": "npm:randombytes@2.0.3"
            }
        },
        "npm:browserify-aes@1.0.6": {
            "map": {
                "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
                "inherits": "npm:inherits@2.0.1",
                "create-hash": "npm:create-hash@1.1.2",
                "cipher-base": "npm:cipher-base@1.0.2",
                "buffer-xor": "npm:buffer-xor@1.0.3"
            }
        },
        "npm:browserify-des@1.0.0": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "cipher-base": "npm:cipher-base@1.0.2",
                "des.js": "npm:des.js@1.0.0"
            }
        },
        "npm:hash.js@1.0.3": {
            "map": {
                "inherits": "npm:inherits@2.0.1"
            }
        },
        "npm:evp_bytestokey@1.0.0": {
            "map": {
                "create-hash": "npm:create-hash@1.1.2"
            }
        },
        "npm:create-hash@1.1.2": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "cipher-base": "npm:cipher-base@1.0.2",
                "ripemd160": "npm:ripemd160@1.0.1",
                "sha.js": "npm:sha.js@2.4.5"
            }
        },
        "npm:cipher-base@1.0.2": {
            "map": {
                "inherits": "npm:inherits@2.0.1"
            }
        },
        "npm:des.js@1.0.0": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
            }
        },
        "npm:browserify-sign@4.0.0": {
            "map": {
                "bn.js": "npm:bn.js@4.11.4",
                "browserify-rsa": "npm:browserify-rsa@4.0.1",
                "create-hash": "npm:create-hash@1.1.2",
                "create-hmac": "npm:create-hmac@1.1.4",
                "elliptic": "npm:elliptic@6.2.8",
                "inherits": "npm:inherits@2.0.1",
                "parse-asn1": "npm:parse-asn1@5.0.0"
            }
        },
        "npm:parse-asn1@5.0.0": {
            "map": {
                "browserify-aes": "npm:browserify-aes@1.0.6",
                "create-hash": "npm:create-hash@1.1.2",
                "evp_bytestokey": "npm:evp_bytestokey@1.0.0",
                "pbkdf2": "npm:pbkdf2@3.0.4",
                "asn1.js": "npm:asn1.js@4.6.2"
            }
        },
        "npm:sha.js@2.4.5": {
            "map": {
                "inherits": "npm:inherits@2.0.1"
            }
        },
        "npm:asn1.js@4.6.2": {
            "map": {
                "bn.js": "npm:bn.js@4.11.4",
                "inherits": "npm:inherits@2.0.1",
                "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
            }
        },
        "github:jspm/nodelibs-stream@0.2.0-alpha": {
            "map": {
                "stream-browserify": "npm:stream-browserify@2.0.1"
            }
        },
        "npm:stream-browserify@2.0.1": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "readable-stream": "npm:readable-stream@2.1.4"
            }
        },
        "npm:readable-stream@2.1.4": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "isarray": "npm:isarray@1.0.0",
                "util-deprecate": "npm:util-deprecate@1.0.2",
                "process-nextick-args": "npm:process-nextick-args@1.0.7",
                "core-util-is": "npm:core-util-is@1.0.2",
                "buffer-shims": "npm:buffer-shims@1.0.0",
                "string_decoder": "npm:string_decoder@0.10.31"
            }
        },
        "github:jspm/nodelibs-string_decoder@0.2.0-alpha": {
            "map": {
                "string_decoder-browserify": "npm:string_decoder@0.10.31"
            }
        },
        "github:jspm/nodelibs-buffer@0.2.0-alpha": {
            "map": {
                "buffer-browserify": "npm:buffer@4.6.0"
            }
        },
        "npm:buffer@4.6.0": {
            "map": {
                "isarray": "npm:isarray@1.0.0",
                "base64-js": "npm:base64-js@1.1.2",
                "ieee754": "npm:ieee754@1.1.6"
            }
        },
        "npm:miller-rabin@4.0.0": {
            "map": {
                "bn.js": "npm:bn.js@4.11.4",
                "brorand": "npm:brorand@1.0.5"
            }
        },
        "npm:aurelia-typeahead@1.0.5": {
            "map": {
                "jquery": "npm:jquery@2.2.4",
                "bower": "npm:bower@1.7.9",
                "wamda-typeahead": "npm:wamda-typeahead@0.11.1",
                "typeahead.js-jspm": "npm:typeahead.js-jspm@1.2.0"
            }
        },
        "npm:wamda-typeahead@0.11.1": {
            "map": {
                "jquery": "npm:jquery@2.2.4"
            }
        },
        "npm:typeahead.js-jspm@1.2.0": {
            "map": {
                "jquery": "npm:jquery@2.2.4"
            }
        },
        "github:jspm/nodelibs-url@0.2.0-alpha": {
            "map": {
                "url-browserify": "npm:url@0.11.0"
            }
        },
        "npm:url@0.11.0": {
            "map": {
                "querystring": "npm:querystring@0.2.0",
                "punycode": "npm:punycode@1.3.2"
            }
        },
        "github:jspm/nodelibs-http@0.2.0-alpha": {
            "map": {
                "http-browserify": "npm:stream-http@2.3.0"
            }
        },
        "npm:stream-http@2.3.0": {
            "map": {
                "inherits": "npm:inherits@2.0.1",
                "readable-stream": "npm:readable-stream@2.1.4",
                "xtend": "npm:xtend@4.0.1",
                "builtin-status-codes": "npm:builtin-status-codes@2.0.0",
                "to-arraybuffer": "npm:to-arraybuffer@1.0.1"
            }
        },
        "github:jspm/nodelibs-punycode@0.2.0-alpha": {
            "map": {
                "punycode-browserify": "npm:punycode@1.4.1"
            }
        },
        "github:jspm/nodelibs-zlib@0.2.0-alpha": {
            "map": {
                "zlib-browserify": "npm:browserify-zlib@0.1.4"
            }
        },
        "npm:browserify-zlib@0.1.4": {
            "map": {
                "readable-stream": "npm:readable-stream@2.1.4",
                "pako": "npm:pako@0.2.8"
            }
        }
    }
});
