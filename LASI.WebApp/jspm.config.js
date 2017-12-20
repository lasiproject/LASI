SystemJS.config({
  transpiler: 'plugin-babel',
  sourceMapContents: true,
  typescriptOptions: {
    'tsconfig': true
  },
  packages: {
    'app': {
      'format': 'esm',
      'main': 'main.ts',
      'defaultExtension': 'ts',
      'meta': {
        '*.ts': {
          'loader': 'plugin-typescript'
        },
        '*.js': {
          'loader': 'plugin-babel'
        }
      },
      'map': {
        'env': {
          'production': 'src/env/prod.js',
          '~production': 'src/env/dev.js'
        },
        'app': '.',
        'models': 'app/models'
      }
    },
    'test': {
      'format': 'esm',
      'main': 'test',
      'defaultExtension': 'ts',
      'meta': {
        '*.ts': {
          'loader': 'plugin-typescript'
        },
        '*.js': {
          'loader': 'plugin-typescript'
        }
      }
    }
  },
  meta: {
    '*.html': {
      'loader': 'text'
    },
    '*.css': {
      'loader': 'sass'
    },
    '*.scss': {
      'loader': 'sass'
    },
    '*.sass': {
      'loader': 'sass'
    }
  },
  map: {
    'bootstrap-css': 'github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css'
  }
});

SystemJS.config({
  packageConfigPaths: [
    'github:*/*.json',
    'npm:@*/*.json',
    'npm:*.json',
    'local:*.json'
  ],
  map: {
    '@types/react': 'npm:@types/react@latest',
    '@types/react-dom': 'npm:@types/react-dom@latest',
    '@types/jquery': 'npm:@types/jquery@latest',
    'aurelia-ui-virtualization': 'npm:aurelia-ui-virtualization@1.0.0-beta.3.1.2',
    'aurelia-validation': 'npm:aurelia-validation@1.1.2',
    '@types/deep-freeze': 'npm:@types/deep-freeze@0.1.1',
    '@types/jquery-contextmenu': 'npm:@types/jquery.contextmenu@1.7.33',
    '@types/typeahead': 'npm:@types/typeahead@0.11.31',
    'babel': 'npm:babel-core@6.26.0',
    'deep-freeze': 'npm:deep-freeze@0.0.1',
    'domain': 'npm:jspm-nodelibs-domain@0.2.1',
    'flat-map': 'github:aluanhaddad/flat-map@master',
    'font-awesome': 'npm:font-awesome@4.7.0',
    'aurelia-dialog': 'npm:aurelia-dialog@1.0.0-rc.2.0.0',
    '@types/bootstrap': 'npm:@types/bootstrap@3.3.36',
    'aurelia-http-client': 'npm:aurelia-http-client@1.2.1',
    'aurelia-fetch-client': 'npm:aurelia-fetch-client@1.1.3',
    'aurelia-binding': 'npm:aurelia-binding@1.5.0',
    'aurelia-bootstrapper': 'npm:aurelia-bootstrapper@2.1.1',
    'aurelia-dependency-injection': 'npm:aurelia-dependency-injection@1.3.2',
    'aurelia-event-aggregator': 'npm:aurelia-event-aggregator@1.0.1',
    'aurelia-framework': 'npm:aurelia-framework@1.1.5',
    'aurelia-history': 'npm:aurelia-history@1.1.0',
    'aurelia-history-browser': 'npm:aurelia-history-browser@1.1.0',
    'aurelia-loader': 'npm:aurelia-loader@1.0.0',
    'aurelia-loader-default': 'npm:aurelia-loader-default@1.0.3',
    'aurelia-logging': 'npm:aurelia-logging@1.3.1',
    'aurelia-logging-console': 'npm:aurelia-logging-console@1.0.0',
    'aurelia-metadata': 'npm:aurelia-metadata@1.0.3',
    'aurelia-pal': 'npm:aurelia-pal@1.4.0',
    'aurelia-pal-browser': 'npm:aurelia-pal-browser@1.3.0',
    'aurelia-path': 'npm:aurelia-path@1.1.1',
    'aurelia-polyfills': 'npm:aurelia-polyfills@1.2.2',
    'aurelia-route-recognizer': 'npm:aurelia-route-recognizer@1.1.1',
    'aurelia-router': 'npm:aurelia-router@1.4.0',
    'aurelia-task-queue': 'npm:aurelia-task-queue@1.2.1',
    'aurelia-templating': 'npm:aurelia-templating@1.6.0',
    'aurelia-templating-binding': 'npm:aurelia-templating-binding@1.4.0',
    'aurelia-templating-resources': 'npm:aurelia-templating-resources@1.5.4',
    'aurelia-templating-router': 'npm:aurelia-templating-router@1.2.0',
    'assert': 'npm:jspm-nodelibs-assert@0.2.1',
    'bootstrap': 'github:twbs/bootstrap@3.3.7',
    'buffer': 'npm:jspm-nodelibs-buffer@0.2.3',
    'child_process': 'npm:jspm-nodelibs-child_process@0.2.1',
    'constants': 'npm:jspm-nodelibs-constants@0.2.1',
    'core-js': 'npm:core-js@2.5.1',
    'crypto': 'npm:jspm-nodelibs-crypto@0.2.1',
    'dgram': 'npm:jspm-nodelibs-dgram@0.2.1',
    'dns': 'npm:jspm-nodelibs-dns@0.2.1',
    'events': 'npm:jspm-nodelibs-events@0.2.2',
    'fs': 'npm:jspm-nodelibs-fs@0.2.1',
    'graceful-fs': 'npm:graceful-fs@4.1.11',
    'http': 'npm:jspm-nodelibs-http@0.2.0',
    'https': 'npm:jspm-nodelibs-https@0.2.2',
    'jquery': 'npm:jquery@2.2.4',
    'jquery-contextmenu': 'npm:jquery-contextmenu@2.6.3',
    'kramed': 'npm:kramed@0.5.6',
    'lodash': 'npm:lodash@4.17.4',
    'module': 'npm:jspm-nodelibs-module@0.2.1',
    'moment': 'npm:moment@2.19.2',
    'net': 'npm:jspm-nodelibs-net@0.2.1',
    'numeral': 'npm:numeral@1.5.6',
    'os': 'npm:jspm-nodelibs-os@0.2.2',
    'path': 'npm:jspm-nodelibs-path@0.2.3',
    'process': 'npm:jspm-nodelibs-process@0.2.1',
    'punycode': 'npm:jspm-nodelibs-punycode@0.2.1',
    'querystring': 'npm:jspm-nodelibs-querystring@0.2.2',
    'react': 'npm:react@16.2.0',
    'react-dom': 'npm:react-dom@16.2.0',
    'readline': 'npm:jspm-nodelibs-readline@0.2.1',
    'reflect-metadata': 'npm:reflect-metadata@0.1.10',
    'sass': 'github:dougludlow/plugin-sass@0.6.0',
    'stream': 'npm:jspm-nodelibs-stream@0.2.1',
    'string_decoder': 'npm:jspm-nodelibs-string_decoder@0.2.2',
    'tls': 'npm:jspm-nodelibs-tls@0.2.1',
    'tslib': 'npm:tslib@1.8.0',
    'tty': 'npm:jspm-nodelibs-tty@0.2.1',
    'typeahead': 'github:twitter/typeahead.js@0.11.1',
    'typescript': 'npm:typescript@next',
    'url': 'npm:jspm-nodelibs-url@0.2.1',
    'util': 'npm:jspm-nodelibs-util@0.2.2',
    'vm': 'npm:jspm-nodelibs-vm@0.2.1',
    'whatwg-fetch': 'npm:whatwg-fetch@2.0.3',
    'zlib': 'npm:jspm-nodelibs-zlib@0.2.3'
  },
  packages: {
    'npm:create-ecdh@4.0.0': {
      'map': {
        'elliptic': 'npm:elliptic@6.4.0',
        'bn.js': 'npm:bn.js@4.11.8'
      }
    },
    'npm:public-encrypt@4.0.0': {
      'map': {
        'browserify-rsa': 'npm:browserify-rsa@4.0.1',
        'bn.js': 'npm:bn.js@4.11.8',
        'randombytes': 'npm:randombytes@2.0.5',
        'create-hash': 'npm:create-hash@1.1.3',
        'parse-asn1': 'npm:parse-asn1@5.1.0'
      }
    },
    'npm:browserify-cipher@1.0.0': {
      'map': {
        'evp_bytestokey': 'npm:evp_bytestokey@1.0.3',
        'browserify-aes': 'npm:browserify-aes@1.1.1',
        'browserify-des': 'npm:browserify-des@1.0.0'
      }
    },
    'npm:diffie-hellman@5.0.2': {
      'map': {
        'bn.js': 'npm:bn.js@4.11.8',
        'randombytes': 'npm:randombytes@2.0.5',
        'miller-rabin': 'npm:miller-rabin@4.0.1'
      }
    },
    'npm:browserify-rsa@4.0.1': {
      'map': {
        'bn.js': 'npm:bn.js@4.11.8',
        'randombytes': 'npm:randombytes@2.0.5'
      }
    },
    'npm:browserify-des@1.0.0': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'cipher-base': 'npm:cipher-base@1.0.4',
        'des.js': 'npm:des.js@1.0.0'
      }
    },
    'npm:des.js@1.0.0': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'minimalistic-assert': 'npm:minimalistic-assert@1.0.0'
      }
    },
    'npm:stream-browserify@2.0.1': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'readable-stream': 'npm:readable-stream@2.3.3'
      }
    },
    'npm:url@0.11.0': {
      'map': {
        'querystring': 'npm:querystring@0.2.0',
        'punycode': 'npm:punycode@1.3.2'
      }
    },
    'npm:browserify-zlib@0.1.4': {
      'map': {
        'readable-stream': 'npm:readable-stream@2.3.3',
        'pako': 'npm:pako@0.2.9'
      }
    },
    'github:twbs/bootstrap@3.3.7': {
      'map': {
        'jquery': 'npm:jquery@2.2.4'
      }
    },
    'npm:jspm-nodelibs-http@0.2.0': {
      'map': {
        'http-browserify': 'npm:stream-http@2.7.2'
      }
    },
    'npm:font-awesome@4.7.0': {
      'map': {
        'css': 'github:systemjs/plugin-css@0.1.36'
      }
    },
    'github:twitter/typeahead.js@0.11.1': {
      'map': {
        'jquery': 'npm:jquery@2.2.4'
      }
    },
    'npm:chalk@1.1.3': {
      'map': {
        'supports-color': 'npm:supports-color@2.0.0',
        'escape-string-regexp': 'npm:escape-string-regexp@1.0.5',
        'has-ansi': 'npm:has-ansi@2.0.0',
        'ansi-styles': 'npm:ansi-styles@2.2.1',
        'strip-ansi': 'npm:strip-ansi@3.0.1'
      }
    },
    'npm:has-ansi@2.0.0': {
      'map': {
        'ansi-regex': 'npm:ansi-regex@2.1.1'
      }
    },
    'npm:strip-ansi@3.0.1': {
      'map': {
        'ansi-regex': 'npm:ansi-regex@2.1.1'
      }
    },
    'npm:mkdirp@0.5.1': {
      'map': {
        'minimist': 'npm:minimist@0.0.8'
      }
    },
    'npm:home-or-tmp@2.0.0': {
      'map': {
        'os-homedir': 'npm:os-homedir@1.0.2',
        'os-tmpdir': 'npm:os-tmpdir@1.0.2'
      }
    },
    'npm:invariant@2.2.2': {
      'map': {
        'loose-envify': 'npm:loose-envify@1.3.1'
      }
    },
    'npm:detect-indent@4.0.0': {
      'map': {
        'repeating': 'npm:repeating@2.0.1'
      }
    },
    'npm:repeating@2.0.1': {
      'map': {
        'is-finite': 'npm:is-finite@1.0.2'
      }
    },
    'npm:is-finite@1.0.2': {
      'map': {
        'number-is-nan': 'npm:number-is-nan@1.0.1'
      }
    },
    'npm:loose-envify@1.3.1': {
      'map': {
        'js-tokens': 'npm:js-tokens@3.0.2'
      }
    },
    'npm:parse-asn1@5.1.0': {
      'map': {
        'create-hash': 'npm:create-hash@1.1.3',
        'evp_bytestokey': 'npm:evp_bytestokey@1.0.3',
        'pbkdf2': 'npm:pbkdf2@3.0.14',
        'browserify-aes': 'npm:browserify-aes@1.1.1',
        'asn1.js': 'npm:asn1.js@4.9.2'
      }
    },
    'npm:elliptic@6.4.0': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'bn.js': 'npm:bn.js@4.11.8',
        'minimalistic-crypto-utils': 'npm:minimalistic-crypto-utils@1.0.1',
        'hmac-drbg': 'npm:hmac-drbg@1.0.1',
        'minimalistic-assert': 'npm:minimalistic-assert@1.0.0',
        'hash.js': 'npm:hash.js@1.1.3',
        'brorand': 'npm:brorand@1.1.0'
      }
    },
    'npm:browserify-sign@4.0.4': {
      'map': {
        'create-hmac': 'npm:create-hmac@1.1.6',
        'create-hash': 'npm:create-hash@1.1.3',
        'inherits': 'npm:inherits@2.0.3',
        'elliptic': 'npm:elliptic@6.4.0',
        'browserify-rsa': 'npm:browserify-rsa@4.0.1',
        'parse-asn1': 'npm:parse-asn1@5.1.0',
        'bn.js': 'npm:bn.js@4.11.8'
      }
    },
    'npm:jspm-nodelibs-punycode@0.2.1': {
      'map': {
        'punycode': 'npm:punycode@1.4.1'
      }
    },
    'npm:jspm-nodelibs-stream@0.2.1': {
      'map': {
        'stream-browserify': 'npm:stream-browserify@2.0.1'
      }
    },
    'npm:jspm-nodelibs-crypto@0.2.1': {
      'map': {
        'crypto-browserify': 'npm:crypto-browserify@3.12.0'
      }
    },
    'npm:jspm-nodelibs-zlib@0.2.3': {
      'map': {
        'browserify-zlib': 'npm:browserify-zlib@0.1.4'
      }
    },
    'npm:jspm-nodelibs-domain@0.2.1': {
      'map': {
        'domain-browser': 'npm:domain-browser@1.1.7'
      }
    },
    'npm:jspm-nodelibs-url@0.2.1': {
      'map': {
        'url': 'npm:url@0.11.0'
      }
    },
    'npm:babel-messages@6.23.0': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0'
      }
    },
    'npm:babel-helpers@6.24.1': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'babel-template': 'npm:babel-template@6.26.0'
      }
    },
    'npm:hmac-drbg@1.0.1': {
      'map': {
        'hash.js': 'npm:hash.js@1.1.3',
        'minimalistic-crypto-utils': 'npm:minimalistic-crypto-utils@1.0.1',
        'minimalistic-assert': 'npm:minimalistic-assert@1.0.0'
      }
    },
    'npm:typescript@next': {
      'map': {
        'source-map-support': 'npm:source-map-support@0.4.18'
      }
    },
    'npm:jspm-nodelibs-buffer@0.2.3': {
      'map': {
        'buffer': 'npm:buffer@5.0.8'
      }
    },
    'npm:once@1.4.0': {
      'map': {
        'wrappy': 'npm:wrappy@1.0.2'
      }
    },
    'npm:inflight@1.0.6': {
      'map': {
        'once': 'npm:once@1.4.0',
        'wrappy': 'npm:wrappy@1.0.2'
      }
    },
    'npm:css-url-rewriter-ex@1.0.6': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0'
      }
    },
    'npm:css-asset-copier@1.0.2': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'fs-extra': 'npm:fs-extra@0.30.0'
      }
    },
    'npm:fs-extra@0.30.0': {
      'map': {
        'klaw': 'npm:klaw@1.3.1',
        'jsonfile': 'npm:jsonfile@2.4.0',
        'path-is-absolute': 'npm:path-is-absolute@1.0.1',
        'rimraf': 'npm:rimraf@2.6.2',
        'graceful-fs': 'npm:graceful-fs@4.1.11'
      }
    },
    'github:dougludlow/plugin-sass@0.6.0': {
      'map': {
        'css-url-rewriter-ex': 'npm:css-url-rewriter-ex@1.0.6',
        'css-asset-copier': 'npm:css-asset-copier@1.0.2',
        'autoprefixer': 'npm:autoprefixer@6.7.7',
        'postcss': 'npm:postcss@5.2.18',
        'sass.js': 'npm:sass.js@0.9.13',
        'reqwest': 'github:ded/reqwest@2.0.5'
      }
    },
    'npm:supports-color@3.2.3': {
      'map': {
        'has-flag': 'npm:has-flag@1.0.0'
      }
    },
    'npm:autoprefixer@6.7.7': {
      'map': {
        'postcss': 'npm:postcss@5.2.18',
        'browserslist': 'npm:browserslist@1.7.7',
        'num2fraction': 'npm:num2fraction@1.2.2',
        'caniuse-db': 'npm:caniuse-db@1.0.30000766',
        'normalize-range': 'npm:normalize-range@0.1.2',
        'postcss-value-parser': 'npm:postcss-value-parser@3.3.0'
      }
    },
    'npm:browserslist@1.7.7': {
      'map': {
        'caniuse-db': 'npm:caniuse-db@1.0.30000766',
        'electron-to-chromium': 'npm:electron-to-chromium@1.3.27'
      }
    },
    'npm:create-hash@1.1.3': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'cipher-base': 'npm:cipher-base@1.0.4',
        'ripemd160': 'npm:ripemd160@2.0.1',
        'sha.js': 'npm:sha.js@2.4.9'
      }
    },
    'npm:create-hmac@1.1.6': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'create-hash': 'npm:create-hash@1.1.3',
        'cipher-base': 'npm:cipher-base@1.0.4',
        'ripemd160': 'npm:ripemd160@2.0.1',
        'sha.js': 'npm:sha.js@2.4.9',
        'safe-buffer': 'npm:safe-buffer@5.1.1'
      }
    },
    'npm:ripemd160@2.0.1': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'hash-base': 'npm:hash-base@2.0.2'
      }
    },
    'npm:hash-base@2.0.2': {
      'map': {
        'inherits': 'npm:inherits@2.0.3'
      }
    },
    'npm:randombytes@2.0.5': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1'
      }
    },
    'npm:readable-stream@2.3.3': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'string_decoder': 'npm:string_decoder@1.0.3',
        'core-util-is': 'npm:core-util-is@1.0.2',
        'process-nextick-args': 'npm:process-nextick-args@1.0.7',
        'isarray': 'npm:isarray@1.0.0',
        'util-deprecate': 'npm:util-deprecate@1.0.2'
      }
    },
    'npm:string_decoder@1.0.3': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1'
      }
    },
    'npm:debug@2.6.9': {
      'map': {
        'ms': 'npm:ms@2.0.0'
      }
    },
    'npm:babel-core@6.26.0': {
      'map': {
        'lodash': 'npm:lodash@4.17.4',
        'debug': 'npm:debug@2.6.9',
        'babel-generator': 'npm:babel-generator@6.26.0',
        'babel-template': 'npm:babel-template@6.26.0',
        'slash': 'npm:slash@1.0.0',
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'path-is-absolute': 'npm:path-is-absolute@1.0.1',
        'babel-code-frame': 'npm:babel-code-frame@6.26.0',
        'private': 'npm:private@0.1.8',
        'source-map': 'npm:source-map@0.5.7',
        'babel-helpers': 'npm:babel-helpers@6.24.1',
        'babel-messages': 'npm:babel-messages@6.23.0',
        'babel-traverse': 'npm:babel-traverse@6.26.0',
        'babel-types': 'npm:babel-types@6.26.0',
        'babylon': 'npm:babylon@6.18.0',
        'minimatch': 'npm:minimatch@3.0.4',
        'babel-register': 'npm:babel-register@6.26.0',
        'convert-source-map': 'npm:convert-source-map@1.5.0',
        'json5': 'npm:json5@0.5.1'
      }
    },
    'npm:source-map-support@0.4.18': {
      'map': {
        'source-map': 'npm:source-map@0.5.7'
      }
    },
    'npm:@types/jquery.contextmenu@1.7.33': {
      'map': {
        '@types/jquery': 'npm:@types/jquery@latest'
      }
    },
    'npm:@types/typeahead@0.11.31': {
      'map': {
        '@types/jquery': 'npm:@types/jquery@latest'
      }
    },
    'npm:babel-generator@6.26.0': {
      'map': {
        'babel-messages': 'npm:babel-messages@6.23.0',
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'babel-types': 'npm:babel-types@6.26.0',
        'source-map': 'npm:source-map@0.5.7',
        'lodash': 'npm:lodash@4.17.4',
        'jsesc': 'npm:jsesc@1.3.0',
        'trim-right': 'npm:trim-right@1.0.1',
        'detect-indent': 'npm:detect-indent@4.0.0'
      }
    },
    'npm:babel-traverse@6.26.0': {
      'map': {
        'babel-messages': 'npm:babel-messages@6.23.0',
        'babel-code-frame': 'npm:babel-code-frame@6.26.0',
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'babel-types': 'npm:babel-types@6.26.0',
        'babylon': 'npm:babylon@6.18.0',
        'debug': 'npm:debug@2.6.9',
        'lodash': 'npm:lodash@4.17.4',
        'globals': 'npm:globals@9.18.0',
        'invariant': 'npm:invariant@2.2.2'
      }
    },
    'npm:babel-template@6.26.0': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'babel-traverse': 'npm:babel-traverse@6.26.0',
        'babel-types': 'npm:babel-types@6.26.0',
        'babylon': 'npm:babylon@6.18.0',
        'lodash': 'npm:lodash@4.17.4'
      }
    },
    'npm:babel-types@6.26.0': {
      'map': {
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'lodash': 'npm:lodash@4.17.4',
        'esutils': 'npm:esutils@2.0.2',
        'to-fast-properties': 'npm:to-fast-properties@1.0.3'
      }
    },
    'npm:babel-register@6.26.0': {
      'map': {
        'babel-core': 'npm:babel-core@6.26.0',
        'core-js': 'npm:core-js@2.5.1',
        'source-map-support': 'npm:source-map-support@0.4.18',
        'lodash': 'npm:lodash@4.17.4',
        'babel-runtime': 'npm:babel-runtime@6.26.0',
        'mkdirp': 'npm:mkdirp@0.5.1',
        'home-or-tmp': 'npm:home-or-tmp@2.0.0'
      }
    },
    'npm:jspm-nodelibs-os@0.2.2': {
      'map': {
        'os-browserify': 'npm:os-browserify@0.3.0'
      }
    },
    'npm:babel-runtime@6.26.0': {
      'map': {
        'core-js': 'npm:core-js@2.5.1',
        'regenerator-runtime': 'npm:regenerator-runtime@0.11.0'
      }
    },
    'npm:babel-code-frame@6.26.0': {
      'map': {
        'js-tokens': 'npm:js-tokens@3.0.2',
        'esutils': 'npm:esutils@2.0.2',
        'chalk': 'npm:chalk@1.1.3'
      }
    },
    'npm:jspm-nodelibs-string_decoder@0.2.2': {
      'map': {
        'string_decoder': 'npm:string_decoder@0.10.31'
      }
    },
    'npm:glob@7.1.2': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'minimatch': 'npm:minimatch@3.0.4',
        'path-is-absolute': 'npm:path-is-absolute@1.0.1',
        'once': 'npm:once@1.4.0',
        'fs.realpath': 'npm:fs.realpath@1.0.0',
        'inflight': 'npm:inflight@1.0.6'
      }
    },
    'npm:minimatch@3.0.4': {
      'map': {
        'brace-expansion': 'npm:brace-expansion@1.1.8'
      }
    },
    'npm:stream-http@2.7.2': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'readable-stream': 'npm:readable-stream@2.3.3',
        'builtin-status-codes': 'npm:builtin-status-codes@3.0.0',
        'to-arraybuffer': 'npm:to-arraybuffer@1.0.1',
        'xtend': 'npm:xtend@4.0.1'
      }
    },
    'npm:rimraf@2.6.2': {
      'map': {
        'glob': 'npm:glob@7.1.2'
      }
    },
    'npm:buffer@5.0.8': {
      'map': {
        'ieee754': 'npm:ieee754@1.1.8',
        'base64-js': 'npm:base64-js@1.2.1'
      }
    },
    'npm:brace-expansion@1.1.8': {
      'map': {
        'balanced-match': 'npm:balanced-match@1.0.0',
        'concat-map': 'npm:concat-map@0.0.1'
      }
    },
    'npm:postcss@5.2.18': {
      'map': {
        'chalk': 'npm:chalk@1.1.3',
        'source-map': 'npm:source-map@0.5.7',
        'supports-color': 'npm:supports-color@3.2.3',
        'js-base64': 'npm:js-base64@2.3.2'
      }
    },
    'npm:pbkdf2@3.0.14': {
      'map': {
        'create-hmac': 'npm:create-hmac@1.1.6',
        'create-hash': 'npm:create-hash@1.1.3',
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'ripemd160': 'npm:ripemd160@2.0.1',
        'sha.js': 'npm:sha.js@2.4.9'
      }
    },
    'npm:sha.js@2.4.9': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'inherits': 'npm:inherits@2.0.3'
      }
    },
    'npm:browserify-aes@1.1.1': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'cipher-base': 'npm:cipher-base@1.0.4',
        'create-hash': 'npm:create-hash@1.1.3',
        'evp_bytestokey': 'npm:evp_bytestokey@1.0.3',
        'inherits': 'npm:inherits@2.0.3',
        'buffer-xor': 'npm:buffer-xor@1.0.3'
      }
    },
    'npm:evp_bytestokey@1.0.3': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'md5.js': 'npm:md5.js@1.3.4'
      }
    },
    'npm:cipher-base@1.0.4': {
      'map': {
        'safe-buffer': 'npm:safe-buffer@5.1.1',
        'inherits': 'npm:inherits@2.0.3'
      }
    },
    'npm:miller-rabin@4.0.1': {
      'map': {
        'bn.js': 'npm:bn.js@4.11.8',
        'brorand': 'npm:brorand@1.1.0'
      }
    },
    'npm:md5.js@1.3.4': {
      'map': {
        'hash-base': 'npm:hash-base@3.0.4',
        'inherits': 'npm:inherits@2.0.3'
      }
    },
    'npm:hash.js@1.1.3': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'minimalistic-assert': 'npm:minimalistic-assert@1.0.0'
      }
    },
    'npm:hash-base@3.0.4': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'safe-buffer': 'npm:safe-buffer@5.1.1'
      }
    },
    'npm:crypto-browserify@3.12.0': {
      'map': {
        'inherits': 'npm:inherits@2.0.3',
        'randomfill': 'npm:randomfill@1.0.3',
        'diffie-hellman': 'npm:diffie-hellman@5.0.2',
        'create-hash': 'npm:create-hash@1.1.3',
        'pbkdf2': 'npm:pbkdf2@3.0.14',
        'create-hmac': 'npm:create-hmac@1.1.6',
        'randombytes': 'npm:randombytes@2.0.5',
        'public-encrypt': 'npm:public-encrypt@4.0.0',
        'create-ecdh': 'npm:create-ecdh@4.0.0',
        'browserify-sign': 'npm:browserify-sign@4.0.4',
        'browserify-cipher': 'npm:browserify-cipher@1.0.0'
      }
    },
    'npm:randomfill@1.0.3': {
      'map': {
        'randombytes': 'npm:randombytes@2.0.5',
        'safe-buffer': 'npm:safe-buffer@5.1.1'
      }
    },
    'npm:asn1.js@4.9.2': {
      'map': {
        'bn.js': 'npm:bn.js@4.11.8',
        'inherits': 'npm:inherits@2.0.3',
        'minimalistic-assert': 'npm:minimalistic-assert@1.0.0'
      }
    },
    'npm:@types/react-dom@latest': {
      'map': {
        '@types/react': 'npm:@types/react@latest',
        '@types/node': 'npm:@types/node@8.5.2'
      }
    },
    'npm:react-dom@16.2.0': {
      'map': {
        'loose-envify': 'npm:loose-envify@1.3.1',
        'fbjs': 'npm:fbjs@0.8.16',
        'object-assign': 'npm:object-assign@4.1.1',
        'prop-types': 'npm:prop-types@15.6.0'
      }
    },
    'npm:react@16.2.0': {
      'map': {
        'loose-envify': 'npm:loose-envify@1.3.1',
        'object-assign': 'npm:object-assign@4.1.1',
        'fbjs': 'npm:fbjs@0.8.16',
        'prop-types': 'npm:prop-types@15.6.0'
      }
    },
    'npm:fbjs@0.8.16': {
      'map': {
        'loose-envify': 'npm:loose-envify@1.3.1',
        'object-assign': 'npm:object-assign@4.1.1',
        'core-js': 'npm:core-js@1.2.7',
        'isomorphic-fetch': 'npm:isomorphic-fetch@2.2.1',
        'setimmediate': 'npm:setimmediate@1.0.5',
        'promise': 'npm:promise@7.3.1',
        'ua-parser-js': 'npm:ua-parser-js@0.7.17'
      }
    },
    'npm:prop-types@15.6.0': {
      'map': {
        'fbjs': 'npm:fbjs@0.8.16',
        'loose-envify': 'npm:loose-envify@1.3.1',
        'object-assign': 'npm:object-assign@4.1.1'
      }
    },
    'npm:promise@7.3.1': {
      'map': {
        'asap': 'npm:asap@2.0.6'
      }
    },
    'npm:isomorphic-fetch@2.2.1': {
      'map': {
        'whatwg-fetch': 'npm:whatwg-fetch@2.0.3',
        'node-fetch': 'npm:node-fetch@1.7.3'
      }
    },
    'npm:node-fetch@1.7.3': {
      'map': {
        'encoding': 'npm:encoding@0.1.12',
        'is-stream': 'npm:is-stream@1.1.0'
      }
    },
    'npm:encoding@0.1.12': {
      'map': {
        'iconv-lite': 'npm:iconv-lite@0.4.19'
      }
    }
  }
});
