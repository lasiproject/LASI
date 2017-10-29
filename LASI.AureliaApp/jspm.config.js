SystemJS.config({
  transpiler: "plugin-babel",
  sourceMapContents: true,
  typescriptOptions: {
    "typeCheck": false,
    "tsconfig": true
  },
  packages: {
    "app": {
      "format": "esm",
      "main": "main.ts",
      "defaultExtension": "ts",
      "meta": {
        "*.ts": {
          "loader": "plugin-typescript"
        },
        "*.js": {
          "loader": "plugin-typescript"
        }
      },
      "map": {
        "env": {
          "production": "src/env/prod.js",
          "~production": "src/env/dev.js"
        },
        "models": "src/models.ts"
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
    }
  },
  paths: {
    "github:": "jspm_packages/github/",
    "npm:": "jspm_packages/npm/",
    "app/": "src/"
  },
  meta: {
    "*.html": {
      "loader": "text"
    },
    "*.css": {
      "loader": "sass"
    },
    "*.scss": {
      "loader": "sass"
    },
    "*.sass": {
      "loader": "sass"
    }
  },
  browserConfig: {
    "baseURL": "/",
    "bundles": {
      "transpilers.bundle.js": [
        "npm:systemjs-plugin-babel@0.0.17/plugin-babel.js",
        "npm:systemjs-plugin-babel@0.0.17.json",
        "npm:systemjs-plugin-babel@0.0.17/systemjs-babel-browser.js",
        "github:systemjs/plugin-text@0.0.9/text.js",
        "github:systemjs/plugin-text@0.0.9.json",
        "github:frankwallis/plugin-typescript@7.0.6/plugin.js",
        "github:frankwallis/plugin-typescript@7.0.6.json",
        "npm:typescript@next/lib/typescript.js",
        "npm:typescript@next.json",
        "npm:source-map-support@0.4.14/source-map-support.js",
        "npm:source-map-support@0.4.14.json",
        "npm:jspm-nodelibs-buffer@0.2.3/global.js",
        "npm:jspm-nodelibs-buffer@0.2.3.json",
        "npm:buffer@5.0.6/index.js",
        "npm:buffer@5.0.6.json",
        "npm:ieee754@1.1.8/index.js",
        "npm:ieee754@1.1.8.json",
        "npm:base64-js@1.2.1/index.js",
        "npm:base64-js@1.2.1.json",
        "npm:jspm-nodelibs-process@0.2.1/process.js",
        "npm:jspm-nodelibs-process@0.2.1.json",
        "npm:jspm-nodelibs-module@0.2.1.json",
        "npm:jspm-nodelibs-fs@0.2.1/fs.js",
        "npm:jspm-nodelibs-fs@0.2.1.json",
        "npm:jspm-nodelibs-path@0.2.3/path.js",
        "npm:jspm-nodelibs-path@0.2.3.json",
        "npm:source-map@0.5.6/source-map.js",
        "npm:source-map@0.5.6.json",
        "npm:source-map@0.5.6/lib/source-node.js",
        "npm:source-map@0.5.6/lib/util.js",
        "npm:source-map@0.5.6/lib/source-map-generator.js",
        "npm:source-map@0.5.6/lib/mapping-list.js",
        "npm:source-map@0.5.6/lib/array-set.js",
        "npm:source-map@0.5.6/lib/base64-vlq.js",
        "npm:source-map@0.5.6/lib/base64.js",
        "npm:source-map@0.5.6/lib/source-map-consumer.js",
        "npm:source-map@0.5.6/lib/quick-sort.js",
        "npm:source-map@0.5.6/lib/binary-search.js",
        "npm:crypto-browserify@3.11.0/index.js",
        "npm:crypto-browserify@3.11.0.json",
        "npm:public-encrypt@4.0.0/browser.js",
        "npm:public-encrypt@4.0.0.json",
        "npm:public-encrypt@4.0.0/privateDecrypt.js",
        "npm:public-encrypt@4.0.0/withPublic.js",
        "npm:bn.js@4.11.7/lib/bn.js",
        "npm:bn.js@4.11.7.json",
        "npm:create-hash@1.1.3/browser.js",
        "npm:create-hash@1.1.3.json",
        "npm:cipher-base@1.0.3/index.js",
        "npm:cipher-base@1.0.3.json",
        "npm:string_decoder@0.10.31/index.js",
        "npm:string_decoder@0.10.31.json",
        "npm:jspm-nodelibs-string_decoder@0.2.1.json",
        "npm:inherits@2.0.3/inherits_browser.js",
        "npm:inherits@2.0.3.json",
        "npm:stream-browserify@2.0.1/index.js",
        "npm:stream-browserify@2.0.1.json",
        "npm:readable-stream@2.3.3/passthrough.js",
        "npm:readable-stream@2.3.3.json",
        "npm:readable-stream@2.3.3/readable-browser.js",
        "npm:readable-stream@2.3.3/lib/_stream_passthrough.js",
        "npm:core-util-is@1.0.2/lib/util.js",
        "npm:core-util-is@1.0.2.json",
        "npm:readable-stream@2.3.3/lib/_stream_transform.js",
        "npm:readable-stream@2.3.3/lib/_stream_duplex.js",
        "npm:readable-stream@2.3.3/lib/_stream_writable.js",
        "npm:readable-stream@2.3.3/lib/internal/streams/destroy.js",
        "npm:process-nextick-args@1.0.7/index.js",
        "npm:process-nextick-args@1.0.7.json",
        "npm:safe-buffer@5.1.1/index.js",
        "npm:safe-buffer@5.1.1.json",
        "npm:readable-stream@2.3.3/lib/internal/streams/stream-browser.js",
        "npm:jspm-nodelibs-events@0.2.2/events.js",
        "npm:jspm-nodelibs-events@0.2.2.json",
        "npm:util-deprecate@1.0.2/browser.js",
        "npm:util-deprecate@1.0.2.json",
        "npm:readable-stream@2.3.3/lib/_stream_readable.js",
        "npm:string_decoder@1.0.3/lib/string_decoder.js",
        "npm:string_decoder@1.0.3.json",
        "npm:readable-stream@2.3.3/lib/internal/streams/BufferList.js",
        "npm:isarray@1.0.0/index.js",
        "npm:isarray@1.0.0.json",
        "npm:readable-stream@2.3.3/transform.js",
        "npm:readable-stream@2.3.3/duplex-browser.js",
        "npm:readable-stream@2.3.3/writable-browser.js",
        "npm:jspm-nodelibs-stream@0.2.1.json",
        "npm:sha.js@2.4.8/index.js",
        "npm:sha.js@2.4.8.json",
        "npm:sha.js@2.4.8/sha512.js",
        "npm:sha.js@2.4.8/hash.js",
        "npm:sha.js@2.4.8/sha384.js",
        "npm:sha.js@2.4.8/sha256.js",
        "npm:sha.js@2.4.8/sha224.js",
        "npm:sha.js@2.4.8/sha1.js",
        "npm:sha.js@2.4.8/sha.js",
        "npm:ripemd160@2.0.1/index.js",
        "npm:ripemd160@2.0.1.json",
        "npm:hash-base@2.0.2/index.js",
        "npm:hash-base@2.0.2.json",
        "npm:create-hash@1.1.3/md5.js",
        "npm:create-hash@1.1.3/make-hash.js",
        "npm:browserify-rsa@4.0.1/index.js",
        "npm:browserify-rsa@4.0.1.json",
        "npm:randombytes@2.0.5/browser.js",
        "npm:randombytes@2.0.5.json",
        "npm:public-encrypt@4.0.0/xor.js",
        "npm:public-encrypt@4.0.0/mgf.js",
        "npm:parse-asn1@5.1.0/index.js",
        "npm:parse-asn1@5.1.0.json",
        "npm:pbkdf2@3.0.12/browser.js",
        "npm:pbkdf2@3.0.12.json",
        "npm:pbkdf2@3.0.12/lib/sync-browser.js",
        "npm:pbkdf2@3.0.12/lib/default-encoding.js",
        "npm:pbkdf2@3.0.12/lib/precondition.js",
        "npm:pbkdf2@3.0.12/lib/async.js",
        "npm:browserify-aes@1.0.6/browser.js",
        "npm:browserify-aes@1.0.6.json",
        "npm:browserify-aes@1.0.6/modes.js",
        "npm:browserify-aes@1.0.6/decrypter.js",
        "npm:browserify-aes@1.0.6/modes/ctr.js",
        "npm:buffer-xor@1.0.3/index.js",
        "npm:buffer-xor@1.0.3.json",
        "npm:browserify-aes@1.0.6/modes/ofb.js",
        "npm:browserify-aes@1.0.6/modes/cfb1.js",
        "npm:browserify-aes@1.0.6/modes/cfb8.js",
        "npm:browserify-aes@1.0.6/modes/cfb.js",
        "npm:browserify-aes@1.0.6/modes/cbc.js",
        "npm:browserify-aes@1.0.6/modes/ecb.js",
        "npm:evp_bytestokey@1.0.0/index.js",
        "npm:evp_bytestokey@1.0.0.json",
        "npm:browserify-aes@1.0.6/authCipher.js",
        "npm:browserify-aes@1.0.6/ghash.js",
        "npm:browserify-aes@1.0.6/aes.js",
        "npm:browserify-aes@1.0.6/streamCipher.js",
        "npm:browserify-aes@1.0.6/encrypter.js",
        "npm:parse-asn1@5.1.0/fixProc.js",
        "npm:parse-asn1@5.1.0/aesid.json",
        "npm:parse-asn1@5.1.0/asn1.js",
        "npm:parse-asn1@5.1.0/certificate.js",
        "npm:asn1.js@4.9.1/lib/asn1.js",
        "npm:asn1.js@4.9.1.json",
        "npm:asn1.js@4.9.1/lib/asn1/encoders/index.js",
        "npm:asn1.js@4.9.1/lib/asn1/encoders/pem.js",
        "npm:asn1.js@4.9.1/lib/asn1/encoders/der.js",
        "npm:asn1.js@4.9.1/lib/asn1/decoders/index.js",
        "npm:asn1.js@4.9.1/lib/asn1/decoders/pem.js",
        "npm:asn1.js@4.9.1/lib/asn1/decoders/der.js",
        "npm:asn1.js@4.9.1/lib/asn1/constants/index.js",
        "npm:asn1.js@4.9.1/lib/asn1/constants/der.js",
        "npm:asn1.js@4.9.1/lib/asn1/base/index.js",
        "npm:asn1.js@4.9.1/lib/asn1/base/node.js",
        "npm:minimalistic-assert@1.0.0/index.js",
        "npm:minimalistic-assert@1.0.0.json",
        "npm:asn1.js@4.9.1/lib/asn1/base/buffer.js",
        "npm:asn1.js@4.9.1/lib/asn1/base/reporter.js",
        "npm:asn1.js@4.9.1/lib/asn1/api.js",
        "npm:jspm-nodelibs-vm@0.2.1/vm.js",
        "npm:jspm-nodelibs-vm@0.2.1.json",
        "npm:public-encrypt@4.0.0/publicEncrypt.js",
        "npm:create-ecdh@4.0.0/browser.js",
        "npm:create-ecdh@4.0.0.json",
        "npm:elliptic@6.4.0/lib/elliptic.js",
        "npm:elliptic@6.4.0.json",
        "npm:elliptic@6.4.0/lib/elliptic/eddsa/index.js",
        "npm:elliptic@6.4.0/lib/elliptic/eddsa/signature.js",
        "npm:elliptic@6.4.0/lib/elliptic/eddsa/key.js",
        "npm:hash.js@1.1.2/lib/hash.js",
        "npm:hash.js@1.1.2.json",
        "npm:hash.js@1.1.2/lib/hash/hmac.js",
        "npm:hash.js@1.1.2/lib/hash/utils.js",
        "npm:hash.js@1.1.2/lib/hash/ripemd.js",
        "npm:hash.js@1.1.2/lib/hash/common.js",
        "npm:hash.js@1.1.2/lib/hash/sha.js",
        "npm:hash.js@1.1.2/lib/hash/sha/512.js",
        "npm:hash.js@1.1.2/lib/hash/sha/384.js",
        "npm:hash.js@1.1.2/lib/hash/sha/256.js",
        "npm:hash.js@1.1.2/lib/hash/sha/common.js",
        "npm:hash.js@1.1.2/lib/hash/sha/224.js",
        "npm:hash.js@1.1.2/lib/hash/sha/1.js",
        "npm:elliptic@6.4.0/lib/elliptic/ec/index.js",
        "npm:elliptic@6.4.0/lib/elliptic/ec/signature.js",
        "npm:elliptic@6.4.0/lib/elliptic/ec/key.js",
        "npm:hmac-drbg@1.0.1/lib/hmac-drbg.js",
        "npm:hmac-drbg@1.0.1.json",
        "npm:minimalistic-crypto-utils@1.0.1/lib/utils.js",
        "npm:minimalistic-crypto-utils@1.0.1.json",
        "npm:elliptic@6.4.0/lib/elliptic/curves.js",
        "npm:elliptic@6.4.0/lib/elliptic/precomputed/secp256k1.js",
        "npm:elliptic@6.4.0/lib/elliptic/curve/index.js",
        "npm:elliptic@6.4.0/lib/elliptic/curve/edwards.js",
        "npm:elliptic@6.4.0/lib/elliptic/curve/mont.js",
        "npm:elliptic@6.4.0/lib/elliptic/curve/short.js",
        "npm:elliptic@6.4.0/lib/elliptic/curve/base.js",
        "npm:brorand@1.1.0/index.js",
        "npm:brorand@1.1.0.json",
        "npm:elliptic@6.4.0/lib/elliptic/utils.js",
        "npm:elliptic@6.4.0/package.json",
        "npm:browserify-sign@4.0.4/browser/index.js",
        "npm:browserify-sign@4.0.4.json",
        "npm:browserify-sign@4.0.4/browser/algorithms.json",
        "npm:browserify-sign@4.0.4/browser/verify.js",
        "npm:browserify-sign@4.0.4/browser/curves.json",
        "npm:browserify-sign@4.0.4/browser/sign.js",
        "npm:create-hmac@1.1.6/browser.js",
        "npm:create-hmac@1.1.6.json",
        "npm:create-hmac@1.1.6/legacy.js",
        "npm:diffie-hellman@5.0.2/browser.js",
        "npm:diffie-hellman@5.0.2.json",
        "npm:diffie-hellman@5.0.2/lib/dh.js",
        "npm:diffie-hellman@5.0.2/lib/generatePrime.js",
        "npm:miller-rabin@4.0.0/lib/mr.js",
        "npm:miller-rabin@4.0.0.json",
        "npm:diffie-hellman@5.0.2/lib/primes.json",
        "npm:browserify-cipher@1.0.0/browser.js",
        "npm:browserify-cipher@1.0.0.json",
        "npm:browserify-des@1.0.0/modes.js",
        "npm:browserify-des@1.0.0.json",
        "npm:browserify-des@1.0.0/index.js",
        "npm:des.js@1.0.0/lib/des.js",
        "npm:des.js@1.0.0.json",
        "npm:des.js@1.0.0/lib/des/ede.js",
        "npm:des.js@1.0.0/lib/des/cbc.js",
        "npm:des.js@1.0.0/lib/des/des.js",
        "npm:des.js@1.0.0/lib/des/cipher.js",
        "npm:des.js@1.0.0/lib/des/utils.js",
        "npm:browserify-sign@4.0.4/algos.js",
        "npm:jspm-nodelibs-crypto@0.2.1.json",
        "npm:os-browserify@0.2.1/browser.js",
        "npm:os-browserify@0.2.1.json",
        "npm:jspm-nodelibs-os@0.2.1.json",
        "github:dougludlow/plugin-sass@0.6.0/index.js",
        "github:dougludlow/plugin-sass@0.6.0.json",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/asyncToGenerator.js",
        "npm:systemjs-plugin-babel@0.0.17/regenerator-runtime.js"
      ],
      "deps.bundle.js": [
        "npm:tslib@1.7.1/tslib.es6.js",
        "npm:tslib@1.7.1.json",
        "npm:systemjs-plugin-babel@0.0.17/regenerator-runtime.js",
        "npm:systemjs-plugin-babel@0.0.17.json",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/toConsumableArray.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/slicedToArray.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/extends.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/defineProperty.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/asyncToGenerator.js",
        "npm:rxjs@5.3.0/add/operator/throttle.js",
        "npm:rxjs@5.3.0.json",
        "npm:rxjs@5.3.0/operator/throttle.js",
        "npm:rxjs@5.3.0/util/subscribeToResult.js",
        "npm:rxjs@5.3.0/symbol/observable.js",
        "npm:rxjs@5.3.0/util/root.js",
        "npm:rxjs@5.3.0/InnerSubscriber.js",
        "npm:rxjs@5.3.0/Subscriber.js",
        "npm:rxjs@5.3.0/symbol/rxSubscriber.js",
        "npm:rxjs@5.3.0/Observer.js",
        "npm:rxjs@5.3.0/Subscription.js",
        "npm:rxjs@5.3.0/util/UnsubscriptionError.js",
        "npm:rxjs@5.3.0/util/errorObject.js",
        "npm:rxjs@5.3.0/util/tryCatch.js",
        "npm:rxjs@5.3.0/util/isFunction.js",
        "npm:rxjs@5.3.0/util/isObject.js",
        "npm:rxjs@5.3.0/util/isArray.js",
        "npm:rxjs@5.3.0/symbol/iterator.js",
        "npm:rxjs@5.3.0/Observable.js",
        "npm:rxjs@5.3.0/util/toSubscriber.js",
        "npm:rxjs@5.3.0/util/isPromise.js",
        "npm:rxjs@5.3.0/util/isArrayLike.js",
        "npm:rxjs@5.3.0/OuterSubscriber.js",
        "npm:rxjs@5.3.0/add/operator/switchMap.js",
        "npm:rxjs@5.3.0/operator/switchMap.js",
        "npm:rxjs@5.3.0/add/operator/mergeMap.js",
        "npm:rxjs@5.3.0/operator/mergeMap.js",
        "npm:rxjs@5.3.0/add/operator/map.js",
        "npm:rxjs@5.3.0/operator/map.js",
        "npm:rxjs@5.3.0/add/operator/distinctUntilChanged.js",
        "npm:rxjs@5.3.0/operator/distinctUntilChanged.js",
        "npm:rxjs@5.3.0/add/operator/debounceTime.js",
        "npm:rxjs@5.3.0/operator/debounceTime.js",
        "npm:rxjs@5.3.0/scheduler/async.js",
        "npm:rxjs@5.3.0/scheduler/AsyncScheduler.js",
        "npm:rxjs@5.3.0/Scheduler.js",
        "npm:rxjs@5.3.0/scheduler/AsyncAction.js",
        "npm:rxjs@5.3.0/scheduler/Action.js",
        "npm:rxjs@5.3.0/add/operator/debounce.js",
        "npm:rxjs@5.3.0/operator/debounce.js",
        "npm:rxjs@5.3.0/add/operator/bufferWhen.js",
        "npm:rxjs@5.3.0/operator/bufferWhen.js",
        "npm:rxjs@5.3.0/add/operator/buffer.js",
        "npm:rxjs@5.3.0/operator/buffer.js",
        "npm:rxjs@5.3.0/add/observable/of.js",
        "npm:rxjs@5.3.0/observable/of.js",
        "npm:rxjs@5.3.0/observable/ArrayObservable.js",
        "npm:rxjs@5.3.0/util/isScheduler.js",
        "npm:rxjs@5.3.0/observable/EmptyObservable.js",
        "npm:rxjs@5.3.0/observable/ScalarObservable.js",
        "npm:rxjs@5.3.0/add/observable/from.js",
        "npm:rxjs@5.3.0/observable/from.js",
        "npm:rxjs@5.3.0/observable/FromObservable.js",
        "npm:rxjs@5.3.0/operator/observeOn.js",
        "npm:rxjs@5.3.0/Notification.js",
        "npm:rxjs@5.3.0/observable/ArrayLikeObservable.js",
        "npm:rxjs@5.3.0/observable/IteratorObservable.js",
        "npm:rxjs@5.3.0/observable/PromiseObservable.js",
        "npm:jquery-contextmenu@2.4.4/dist/jquery.contextMenu.js",
        "npm:jquery-contextmenu@2.4.4.json",
        "npm:jquery@2.2.4/dist/jquery.js",
        "npm:jquery@2.2.4.json",
        "npm:deep-freeze@0.0.1/index.js",
        "npm:deep-freeze@0.0.1.json",
        "npm:core-js@2.4.1/index.js",
        "npm:core-js@2.4.1.json",
        "npm:jspm-nodelibs-process@0.2.1/process.js",
        "npm:jspm-nodelibs-process@0.2.1.json",
        "npm:core-js@2.4.1/modules/_core.js",
        "npm:core-js@2.4.1/modules/core.string.unescape-html.js",
        "npm:core-js@2.4.1/modules/_replacer.js",
        "npm:core-js@2.4.1/modules/_export.js",
        "npm:core-js@2.4.1/modules/_ctx.js",
        "npm:core-js@2.4.1/modules/_a-function.js",
        "npm:core-js@2.4.1/modules/_redefine.js",
        "npm:core-js@2.4.1/modules/_uid.js",
        "npm:core-js@2.4.1/modules/_has.js",
        "npm:core-js@2.4.1/modules/_hide.js",
        "npm:core-js@2.4.1/modules/_descriptors.js",
        "npm:core-js@2.4.1/modules/_fails.js",
        "npm:core-js@2.4.1/modules/_property-desc.js",
        "npm:core-js@2.4.1/modules/_object-dp.js",
        "npm:core-js@2.4.1/modules/_to-primitive.js",
        "npm:core-js@2.4.1/modules/_is-object.js",
        "npm:core-js@2.4.1/modules/_ie8-dom-define.js",
        "npm:core-js@2.4.1/modules/_dom-create.js",
        "npm:core-js@2.4.1/modules/_global.js",
        "npm:core-js@2.4.1/modules/_an-object.js",
        "npm:core-js@2.4.1/modules/core.string.escape-html.js",
        "npm:core-js@2.4.1/modules/core.regexp.escape.js",
        "npm:core-js@2.4.1/modules/core.number.iterator.js",
        "npm:core-js@2.4.1/modules/_iter-define.js",
        "npm:core-js@2.4.1/modules/_wks.js",
        "npm:core-js@2.4.1/modules/_shared.js",
        "npm:core-js@2.4.1/modules/_object-gpo.js",
        "npm:core-js@2.4.1/modules/_shared-key.js",
        "npm:core-js@2.4.1/modules/_to-object.js",
        "npm:core-js@2.4.1/modules/_defined.js",
        "npm:core-js@2.4.1/modules/_set-to-string-tag.js",
        "npm:core-js@2.4.1/modules/_iter-create.js",
        "npm:core-js@2.4.1/modules/_object-create.js",
        "npm:core-js@2.4.1/modules/_html.js",
        "npm:core-js@2.4.1/modules/_enum-bug-keys.js",
        "npm:core-js@2.4.1/modules/_object-dps.js",
        "npm:core-js@2.4.1/modules/_object-keys.js",
        "npm:core-js@2.4.1/modules/_object-keys-internal.js",
        "npm:core-js@2.4.1/modules/_array-includes.js",
        "npm:core-js@2.4.1/modules/_to-index.js",
        "npm:core-js@2.4.1/modules/_to-integer.js",
        "npm:core-js@2.4.1/modules/_to-length.js",
        "npm:core-js@2.4.1/modules/_to-iobject.js",
        "npm:core-js@2.4.1/modules/_iobject.js",
        "npm:core-js@2.4.1/modules/_cof.js",
        "npm:core-js@2.4.1/modules/_iterators.js",
        "npm:core-js@2.4.1/modules/_library.js",
        "npm:core-js@2.4.1/modules/core.object.make.js",
        "npm:core-js@2.4.1/modules/_object-define.js",
        "npm:core-js@2.4.1/modules/_own-keys.js",
        "npm:core-js@2.4.1/modules/_object-gops.js",
        "npm:core-js@2.4.1/modules/_object-gopn.js",
        "npm:core-js@2.4.1/modules/_object-gopd.js",
        "npm:core-js@2.4.1/modules/_object-pie.js",
        "npm:core-js@2.4.1/modules/core.object.define.js",
        "npm:core-js@2.4.1/modules/core.object.classof.js",
        "npm:core-js@2.4.1/modules/_classof.js",
        "npm:core-js@2.4.1/modules/core.object.is-object.js",
        "npm:core-js@2.4.1/modules/core.function.part.js",
        "npm:core-js@2.4.1/modules/_partial.js",
        "npm:core-js@2.4.1/modules/_invoke.js",
        "npm:core-js@2.4.1/modules/_path.js",
        "npm:core-js@2.4.1/modules/core.delay.js",
        "npm:core-js@2.4.1/modules/core.is-iterable.js",
        "npm:core-js@2.4.1/modules/core.get-iterator.js",
        "npm:core-js@2.4.1/modules/core.get-iterator-method.js",
        "npm:core-js@2.4.1/modules/core.dict.js",
        "npm:core-js@2.4.1/modules/_iter-step.js",
        "npm:core-js@2.4.1/modules/_for-of.js",
        "npm:core-js@2.4.1/modules/_is-array-iter.js",
        "npm:core-js@2.4.1/modules/_iter-call.js",
        "npm:core-js@2.4.1/modules/_keyof.js",
        "npm:core-js@2.4.1/modules/_object-assign.js",
        "npm:core-js@2.4.1/shim.js",
        "npm:core-js@2.4.1/modules/web.dom.iterable.js",
        "npm:core-js@2.4.1/modules/es6.array.iterator.js",
        "npm:core-js@2.4.1/modules/_add-to-unscopables.js",
        "npm:core-js@2.4.1/modules/web.immediate.js",
        "npm:core-js@2.4.1/modules/_task.js",
        "npm:core-js@2.4.1/modules/web.timers.js",
        "npm:core-js@2.4.1/modules/es7.observable.js",
        "npm:core-js@2.4.1/modules/_set-species.js",
        "npm:core-js@2.4.1/modules/_redefine-all.js",
        "npm:core-js@2.4.1/modules/_an-instance.js",
        "npm:core-js@2.4.1/modules/_microtask.js",
        "npm:core-js@2.4.1/modules/es7.asap.js",
        "npm:core-js@2.4.1/modules/es7.reflect.metadata.js",
        "npm:core-js@2.4.1/modules/_metadata.js",
        "npm:core-js@2.4.1/modules/es6.weak-map.js",
        "npm:core-js@2.4.1/modules/_collection.js",
        "npm:core-js@2.4.1/modules/_inherit-if-required.js",
        "npm:core-js@2.4.1/modules/_set-proto.js",
        "npm:core-js@2.4.1/modules/_iter-detect.js",
        "npm:core-js@2.4.1/modules/_meta.js",
        "npm:core-js@2.4.1/modules/_collection-weak.js",
        "npm:core-js@2.4.1/modules/_array-methods.js",
        "npm:core-js@2.4.1/modules/_array-species-create.js",
        "npm:core-js@2.4.1/modules/_array-species-constructor.js",
        "npm:core-js@2.4.1/modules/_is-array.js",
        "npm:core-js@2.4.1/modules/es6.map.js",
        "npm:core-js@2.4.1/modules/_collection-strong.js",
        "npm:core-js@2.4.1/modules/es7.reflect.has-own-metadata.js",
        "npm:core-js@2.4.1/modules/es7.reflect.has-metadata.js",
        "npm:core-js@2.4.1/modules/es7.reflect.get-own-metadata-keys.js",
        "npm:core-js@2.4.1/modules/es7.reflect.get-own-metadata.js",
        "npm:core-js@2.4.1/modules/es7.reflect.get-metadata-keys.js",
        "npm:core-js@2.4.1/modules/_array-from-iterable.js",
        "npm:core-js@2.4.1/modules/es6.set.js",
        "npm:core-js@2.4.1/modules/es7.reflect.get-metadata.js",
        "npm:core-js@2.4.1/modules/es7.reflect.delete-metadata.js",
        "npm:core-js@2.4.1/modules/es7.reflect.define-metadata.js",
        "npm:core-js@2.4.1/modules/es7.math.umulh.js",
        "npm:core-js@2.4.1/modules/es7.math.imulh.js",
        "npm:core-js@2.4.1/modules/es7.math.isubh.js",
        "npm:core-js@2.4.1/modules/es7.math.iaddh.js",
        "npm:core-js@2.4.1/modules/es7.error.is-error.js",
        "npm:core-js@2.4.1/modules/es7.system.global.js",
        "npm:core-js@2.4.1/modules/es7.set.to-json.js",
        "npm:core-js@2.4.1/modules/_collection-to-json.js",
        "npm:core-js@2.4.1/modules/es7.map.to-json.js",
        "npm:core-js@2.4.1/modules/es7.object.lookup-setter.js",
        "npm:core-js@2.4.1/modules/_object-forced-pam.js",
        "npm:core-js@2.4.1/modules/es7.object.lookup-getter.js",
        "npm:core-js@2.4.1/modules/es7.object.define-setter.js",
        "npm:core-js@2.4.1/modules/es7.object.define-getter.js",
        "npm:core-js@2.4.1/modules/es7.object.entries.js",
        "npm:core-js@2.4.1/modules/_object-to-array.js",
        "npm:core-js@2.4.1/modules/es7.object.values.js",
        "npm:core-js@2.4.1/modules/es7.object.get-own-property-descriptors.js",
        "npm:core-js@2.4.1/modules/_create-property.js",
        "npm:core-js@2.4.1/modules/es7.symbol.observable.js",
        "npm:core-js@2.4.1/modules/_wks-define.js",
        "npm:core-js@2.4.1/modules/_wks-ext.js",
        "npm:core-js@2.4.1/modules/es7.symbol.async-iterator.js",
        "npm:core-js@2.4.1/modules/es7.string.match-all.js",
        "npm:core-js@2.4.1/modules/_flags.js",
        "npm:core-js@2.4.1/modules/_is-regexp.js",
        "npm:core-js@2.4.1/modules/es7.string.trim-right.js",
        "npm:core-js@2.4.1/modules/_string-trim.js",
        "npm:core-js@2.4.1/modules/_string-ws.js",
        "npm:core-js@2.4.1/modules/es7.string.trim-left.js",
        "npm:core-js@2.4.1/modules/es7.string.pad-end.js",
        "npm:core-js@2.4.1/modules/_string-pad.js",
        "npm:core-js@2.4.1/modules/_string-repeat.js",
        "npm:core-js@2.4.1/modules/es7.string.pad-start.js",
        "npm:core-js@2.4.1/modules/es7.string.at.js",
        "npm:core-js@2.4.1/modules/_string-at.js",
        "npm:core-js@2.4.1/modules/es7.array.includes.js",
        "npm:core-js@2.4.1/modules/es6.reflect.set-prototype-of.js",
        "npm:core-js@2.4.1/modules/es6.reflect.set.js",
        "npm:core-js@2.4.1/modules/es6.reflect.prevent-extensions.js",
        "npm:core-js@2.4.1/modules/es6.reflect.own-keys.js",
        "npm:core-js@2.4.1/modules/es6.reflect.is-extensible.js",
        "npm:core-js@2.4.1/modules/es6.reflect.has.js",
        "npm:core-js@2.4.1/modules/es6.reflect.get-prototype-of.js",
        "npm:core-js@2.4.1/modules/es6.reflect.get-own-property-descriptor.js",
        "npm:core-js@2.4.1/modules/es6.reflect.get.js",
        "npm:core-js@2.4.1/modules/es6.reflect.enumerate.js",
        "npm:core-js@2.4.1/modules/es6.reflect.delete-property.js",
        "npm:core-js@2.4.1/modules/es6.reflect.define-property.js",
        "npm:core-js@2.4.1/modules/es6.reflect.construct.js",
        "npm:core-js@2.4.1/modules/_bind.js",
        "npm:core-js@2.4.1/modules/es6.reflect.apply.js",
        "npm:core-js@2.4.1/modules/es6.typed.float64-array.js",
        "npm:core-js@2.4.1/modules/_typed-array.js",
        "npm:core-js@2.4.1/modules/_array-copy-within.js",
        "npm:core-js@2.4.1/modules/_array-fill.js",
        "npm:core-js@2.4.1/modules/_species-constructor.js",
        "npm:core-js@2.4.1/modules/_same-value.js",
        "npm:core-js@2.4.1/modules/_typed-buffer.js",
        "npm:core-js@2.4.1/modules/_typed.js",
        "npm:core-js@2.4.1/modules/es6.typed.float32-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.uint32-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.int32-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.uint16-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.int16-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.uint8-clamped-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.uint8-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.int8-array.js",
        "npm:core-js@2.4.1/modules/es6.typed.data-view.js",
        "npm:core-js@2.4.1/modules/es6.typed.array-buffer.js",
        "npm:core-js@2.4.1/modules/es6.weak-set.js",
        "npm:core-js@2.4.1/modules/es6.promise.js",
        "npm:core-js@2.4.1/modules/es6.regexp.split.js",
        "npm:core-js@2.4.1/modules/_fix-re-wks.js",
        "npm:core-js@2.4.1/modules/es6.regexp.search.js",
        "npm:core-js@2.4.1/modules/es6.regexp.replace.js",
        "npm:core-js@2.4.1/modules/es6.regexp.match.js",
        "npm:core-js@2.4.1/modules/es6.regexp.flags.js",
        "npm:core-js@2.4.1/modules/es6.regexp.to-string.js",
        "npm:core-js@2.4.1/modules/es6.regexp.constructor.js",
        "npm:core-js@2.4.1/modules/es6.array.species.js",
        "npm:core-js@2.4.1/modules/es6.array.find-index.js",
        "npm:core-js@2.4.1/modules/es6.array.find.js",
        "npm:core-js@2.4.1/modules/es6.array.fill.js",
        "npm:core-js@2.4.1/modules/es6.array.copy-within.js",
        "npm:core-js@2.4.1/modules/es6.array.last-index-of.js",
        "npm:core-js@2.4.1/modules/_strict-method.js",
        "npm:core-js@2.4.1/modules/es6.array.index-of.js",
        "npm:core-js@2.4.1/modules/es6.array.reduce-right.js",
        "npm:core-js@2.4.1/modules/_array-reduce.js",
        "npm:core-js@2.4.1/modules/es6.array.reduce.js",
        "npm:core-js@2.4.1/modules/es6.array.every.js",
        "npm:core-js@2.4.1/modules/es6.array.some.js",
        "npm:core-js@2.4.1/modules/es6.array.filter.js",
        "npm:core-js@2.4.1/modules/es6.array.map.js",
        "npm:core-js@2.4.1/modules/es6.array.for-each.js",
        "npm:core-js@2.4.1/modules/es6.array.sort.js",
        "npm:core-js@2.4.1/modules/es6.array.slice.js",
        "npm:core-js@2.4.1/modules/es6.array.join.js",
        "npm:core-js@2.4.1/modules/es6.array.of.js",
        "npm:core-js@2.4.1/modules/es6.array.from.js",
        "npm:core-js@2.4.1/modules/es6.array.is-array.js",
        "npm:core-js@2.4.1/modules/es6.date.to-primitive.js",
        "npm:core-js@2.4.1/modules/_date-to-primitive.js",
        "npm:core-js@2.4.1/modules/es6.date.to-string.js",
        "npm:core-js@2.4.1/modules/es6.date.to-iso-string.js",
        "npm:core-js@2.4.1/modules/es6.date.to-json.js",
        "npm:core-js@2.4.1/modules/es6.date.now.js",
        "npm:core-js@2.4.1/modules/es6.string.sup.js",
        "npm:core-js@2.4.1/modules/_string-html.js",
        "npm:core-js@2.4.1/modules/es6.string.sub.js",
        "npm:core-js@2.4.1/modules/es6.string.strike.js",
        "npm:core-js@2.4.1/modules/es6.string.small.js",
        "npm:core-js@2.4.1/modules/es6.string.link.js",
        "npm:core-js@2.4.1/modules/es6.string.italics.js",
        "npm:core-js@2.4.1/modules/es6.string.fontsize.js",
        "npm:core-js@2.4.1/modules/es6.string.fontcolor.js",
        "npm:core-js@2.4.1/modules/es6.string.fixed.js",
        "npm:core-js@2.4.1/modules/es6.string.bold.js",
        "npm:core-js@2.4.1/modules/es6.string.blink.js",
        "npm:core-js@2.4.1/modules/es6.string.big.js",
        "npm:core-js@2.4.1/modules/es6.string.anchor.js",
        "npm:core-js@2.4.1/modules/es6.string.starts-with.js",
        "npm:core-js@2.4.1/modules/_fails-is-regexp.js",
        "npm:core-js@2.4.1/modules/_string-context.js",
        "npm:core-js@2.4.1/modules/es6.string.repeat.js",
        "npm:core-js@2.4.1/modules/es6.string.includes.js",
        "npm:core-js@2.4.1/modules/es6.string.ends-with.js",
        "npm:core-js@2.4.1/modules/es6.string.code-point-at.js",
        "npm:core-js@2.4.1/modules/es6.string.iterator.js",
        "npm:core-js@2.4.1/modules/es6.string.trim.js",
        "npm:core-js@2.4.1/modules/es6.string.raw.js",
        "npm:core-js@2.4.1/modules/es6.string.from-code-point.js",
        "npm:core-js@2.4.1/modules/es6.math.trunc.js",
        "npm:core-js@2.4.1/modules/es6.math.tanh.js",
        "npm:core-js@2.4.1/modules/_math-expm1.js",
        "npm:core-js@2.4.1/modules/es6.math.sinh.js",
        "npm:core-js@2.4.1/modules/es6.math.sign.js",
        "npm:core-js@2.4.1/modules/_math-sign.js",
        "npm:core-js@2.4.1/modules/es6.math.log2.js",
        "npm:core-js@2.4.1/modules/es6.math.log1p.js",
        "npm:core-js@2.4.1/modules/_math-log1p.js",
        "npm:core-js@2.4.1/modules/es6.math.log10.js",
        "npm:core-js@2.4.1/modules/es6.math.imul.js",
        "npm:core-js@2.4.1/modules/es6.math.hypot.js",
        "npm:core-js@2.4.1/modules/es6.math.fround.js",
        "npm:core-js@2.4.1/modules/es6.math.expm1.js",
        "npm:core-js@2.4.1/modules/es6.math.cosh.js",
        "npm:core-js@2.4.1/modules/es6.math.clz32.js",
        "npm:core-js@2.4.1/modules/es6.math.cbrt.js",
        "npm:core-js@2.4.1/modules/es6.math.atanh.js",
        "npm:core-js@2.4.1/modules/es6.math.asinh.js",
        "npm:core-js@2.4.1/modules/es6.math.acosh.js",
        "npm:core-js@2.4.1/modules/es6.number.parse-int.js",
        "npm:core-js@2.4.1/modules/_parse-int.js",
        "npm:core-js@2.4.1/modules/es6.number.parse-float.js",
        "npm:core-js@2.4.1/modules/_parse-float.js",
        "npm:core-js@2.4.1/modules/es6.number.min-safe-integer.js",
        "npm:core-js@2.4.1/modules/es6.number.max-safe-integer.js",
        "npm:core-js@2.4.1/modules/es6.number.is-safe-integer.js",
        "npm:core-js@2.4.1/modules/_is-integer.js",
        "npm:core-js@2.4.1/modules/es6.number.is-nan.js",
        "npm:core-js@2.4.1/modules/es6.number.is-integer.js",
        "npm:core-js@2.4.1/modules/es6.number.is-finite.js",
        "npm:core-js@2.4.1/modules/es6.number.epsilon.js",
        "npm:core-js@2.4.1/modules/es6.number.to-precision.js",
        "npm:core-js@2.4.1/modules/_a-number-value.js",
        "npm:core-js@2.4.1/modules/es6.number.to-fixed.js",
        "npm:core-js@2.4.1/modules/es6.number.constructor.js",
        "npm:core-js@2.4.1/modules/es6.parse-float.js",
        "npm:core-js@2.4.1/modules/es6.parse-int.js",
        "npm:core-js@2.4.1/modules/es6.function.has-instance.js",
        "npm:core-js@2.4.1/modules/es6.function.name.js",
        "npm:core-js@2.4.1/modules/es6.function.bind.js",
        "npm:core-js@2.4.1/modules/es6.object.to-string.js",
        "npm:core-js@2.4.1/modules/es6.object.set-prototype-of.js",
        "npm:core-js@2.4.1/modules/es6.object.is.js",
        "npm:core-js@2.4.1/modules/es6.object.assign.js",
        "npm:core-js@2.4.1/modules/es6.object.is-extensible.js",
        "npm:core-js@2.4.1/modules/_object-sap.js",
        "npm:core-js@2.4.1/modules/es6.object.is-sealed.js",
        "npm:core-js@2.4.1/modules/es6.object.is-frozen.js",
        "npm:core-js@2.4.1/modules/es6.object.prevent-extensions.js",
        "npm:core-js@2.4.1/modules/es6.object.seal.js",
        "npm:core-js@2.4.1/modules/es6.object.freeze.js",
        "npm:core-js@2.4.1/modules/es6.object.get-own-property-names.js",
        "npm:core-js@2.4.1/modules/_object-gopn-ext.js",
        "npm:core-js@2.4.1/modules/es6.object.keys.js",
        "npm:core-js@2.4.1/modules/es6.object.get-prototype-of.js",
        "npm:core-js@2.4.1/modules/es6.object.get-own-property-descriptor.js",
        "npm:core-js@2.4.1/modules/es6.object.define-properties.js",
        "npm:core-js@2.4.1/modules/es6.object.define-property.js",
        "npm:core-js@2.4.1/modules/es6.object.create.js",
        "npm:core-js@2.4.1/modules/es6.symbol.js",
        "npm:core-js@2.4.1/modules/_enum-keys.js",
        "npm:aurelia-templating-router@1.1.0/aurelia-templating-router.js",
        "npm:aurelia-templating-router@1.1.0.json",
        "npm:aurelia-templating-router@1.1.0/route-href.js",
        "npm:aurelia-logging@1.3.1/aurelia-logging.js",
        "npm:aurelia-logging@1.3.1.json",
        "npm:aurelia-pal@1.3.0/aurelia-pal.js",
        "npm:aurelia-pal@1.3.0.json",
        "npm:aurelia-router@1.3.0/aurelia-router.js",
        "npm:aurelia-router@1.3.0.json",
        "npm:aurelia-event-aggregator@1.0.1/aurelia-event-aggregator.js",
        "npm:aurelia-event-aggregator@1.0.1.json",
        "npm:aurelia-history@1.0.0/aurelia-history.js",
        "npm:aurelia-history@1.0.0.json",
        "npm:aurelia-dependency-injection@1.3.1/aurelia-dependency-injection.js",
        "npm:aurelia-dependency-injection@1.3.1.json",
        "npm:aurelia-metadata@1.0.3/aurelia-metadata.js",
        "npm:aurelia-metadata@1.0.3.json",
        "npm:aurelia-route-recognizer@1.1.0/aurelia-route-recognizer.js",
        "npm:aurelia-route-recognizer@1.1.0.json",
        "npm:aurelia-path@1.1.1/aurelia-path.js",
        "npm:aurelia-path@1.1.1.json",
        "npm:aurelia-templating@1.4.2/aurelia-templating.js",
        "npm:aurelia-templating@1.4.2.json",
        "npm:aurelia-task-queue@1.2.0/aurelia-task-queue.js",
        "npm:aurelia-task-queue@1.2.0.json",
        "npm:aurelia-binding@1.2.1/aurelia-binding.js",
        "npm:aurelia-binding@1.2.1.json",
        "npm:aurelia-loader@1.0.0/aurelia-loader.js",
        "npm:aurelia-loader@1.0.0.json",
        "npm:aurelia-templating-router@1.1.0/router-view.js",
        "npm:aurelia-templating-router@1.1.0/route-loader.js",
        "npm:aurelia-templating-resources@1.4.0/aurelia-templating-resources.js",
        "npm:aurelia-templating-resources@1.4.0.json",
        "npm:aurelia-templating-resources@1.4.0/aurelia-hide-style.js",
        "npm:aurelia-templating-resources@1.4.0/analyze-view-factory.js",
        "npm:aurelia-templating-resources@1.4.0/repeat-utilities.js",
        "npm:aurelia-templating-resources@1.4.0/number-repeat-strategy.js",
        "npm:aurelia-templating-resources@1.4.0/set-repeat-strategy.js",
        "npm:aurelia-templating-resources@1.4.0/map-repeat-strategy.js",
        "npm:aurelia-templating-resources@1.4.0/array-repeat-strategy.js",
        "npm:aurelia-templating-resources@1.4.0/null-repeat-strategy.js",
        "npm:aurelia-templating-resources@1.4.0/html-resource-plugin.js",
        "npm:aurelia-templating-resources@1.4.0/dynamic-element.js",
        "npm:aurelia-templating-resources@1.4.0/repeat-strategy-locator.js",
        "npm:aurelia-templating-resources@1.4.0/abstract-repeater.js",
        "npm:aurelia-templating-resources@1.4.0/update-trigger-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/binding-signaler.js",
        "npm:aurelia-templating-resources@1.4.0/signal-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/self-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/debounce-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/throttle-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/binding-mode-behaviors.js",
        "npm:aurelia-templating-resources@1.4.0/attr-binding-behavior.js",
        "npm:aurelia-templating-resources@1.4.0/html-sanitizer.js",
        "npm:aurelia-templating-resources@1.4.0/css-resource.js",
        "npm:aurelia-templating-resources@1.4.0/focus.js",
        "npm:aurelia-templating-resources@1.4.0/replaceable.js",
        "npm:aurelia-templating-resources@1.4.0/sanitize-html.js",
        "npm:aurelia-templating-resources@1.4.0/hide.js",
        "npm:aurelia-templating-resources@1.4.0/show.js",
        "npm:aurelia-templating-resources@1.4.0/repeat.js",
        "npm:aurelia-templating-resources@1.4.0/with.js",
        "npm:aurelia-templating-resources@1.4.0/if.js",
        "npm:aurelia-templating-resources@1.4.0/compose.js",
        "npm:aurelia-templating-binding@1.3.0/aurelia-templating-binding.js",
        "npm:aurelia-templating-binding@1.3.0.json",
        "npm:aurelia-logging-console@1.0.0/aurelia-logging-console.js",
        "npm:aurelia-logging-console@1.0.0.json",
        "npm:aurelia-loader-default@1.0.2/aurelia-loader-default.js",
        "npm:aurelia-loader-default@1.0.2.json",
        "npm:aurelia-history-browser@1.0.0/aurelia-history-browser.js",
        "npm:aurelia-history-browser@1.0.0.json",
        "npm:aurelia-framework@1.1.2/aurelia-framework.js",
        "npm:aurelia-framework@1.1.2.json",
        "npm:aurelia-fetch-client@1.1.2/aurelia-fetch-client.js",
        "npm:aurelia-fetch-client@1.1.2.json",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/index.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1.json",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/aurelia-dialog.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-result.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/classCallCheck.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/dialog-controller.js",
        "npm:aurelia-dialog@1.0.0-beta.3.0.1/dist/es2015/lifecycle.js",
        "npm:systemjs-plugin-babel@0.0.17/babel-helpers/createClass.js",
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
        "npm:aurelia-bootstrapper@1.0.1/aurelia-bootstrapper.js",
        "npm:aurelia-bootstrapper@1.0.1.json",
        "npm:aurelia-polyfills@1.2.1/aurelia-polyfills.js",
        "npm:aurelia-polyfills@1.2.1.json",
        "npm:aurelia-pal-browser@1.2.1/aurelia-pal-browser.js",
        "npm:aurelia-pal-browser@1.2.1.json",
        "github:twitter/typeahead.js@0.11.1/dist/typeahead.bundle.js",
        "github:twitter/typeahead.js@0.11.1.json",
        "github:twbs/bootstrap@3.3.7/dist/js/bootstrap.js",
        "github:twbs/bootstrap@3.3.7.json",
        "github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css",
        "github:dougludlow/plugin-sass@0.6.0.json",
        "github:aluanhaddad/flat-map@master/index.js",
        "github:aluanhaddad/flat-map@master.json",
        "github:aluanhaddad/flat-map@master/src/flat-map.js",
        "github:aluanhaddad/flat-map@master/src/array-species-create.js",
        "github:aluanhaddad/flat-map@master/src/flatten-into-array.js",
        "github:aluanhaddad/flat-map@master/src/flatten.js"
      ],
      "app.bundle.js": [
        "app/type-ahead/type-ahead.ts",
        "app/type-ahead/type-ahead.html",
        "app/type-ahead/type-ahead.css",
        "app/styles/widgets/document-upload.css",
        "app/signout.html",
        "app/signin.ts",
        "app/services/user.ts",
        "app/services/http-utilities.ts",
        "app/services/token.ts",
        "app/services/storage.ts",
        "app/signin.html",
        "app/services/results.ts",
        "app/services/build-menus.ts",
        "app/services/chart-builder.ts",
        "app/navbar.html",
        "app/models/verbal-menu.ts",
        "app/models/user.ts",
        "app/models/text-fragment-model.ts",
        "app/models/paragraph-model.ts",
        "app/models/lexical-model-core.ts",
        "app/models/document-list-item.ts",
        "app/models/credentials.ts",
        "app/models/authentication-result.ts",
        "app/modal.ts",
        "app/modal.html",
        "app/main.ts",
        "app/styles/lexical.css",
        "app/styles/site.css",
        "app/styles/styles.scss",
        "app/enhance-array.ts",
        "app/augmentations.ts",
        "app/env/prod.js",
        "app/env/dev.js",
        "app/documents.ts",
        "app/models/document-model.ts",
        "app/services/document-model-service.ts",
        "app/documents.html",
        "app/document-viewer/sentence.html",
        "app/document-viewer/search.ts",
        "app/document-viewer/search.html",
        "app/document-viewer/phrase.ts",
        "app/services/lexical-menu-builder.ts",
        "app/models.ts",
        "app/document-viewer/phrase.html",
        "app/document-viewer/paragraph.html",
        "app/document-viewer/page.html",
        "app/document-viewer/document-viewer.ts",
        "app/document-viewer/document-viewer.html",
        "app/decorators.ts",
        "app/context-menu/context-menu.ts",
        "app/context-menu/context-menu.html",
        "app/configuration/typeahead.ts",
        "app/configuration/http.ts",
        "app/configuration/dialog.ts",
        "app/app.ts",
        "app/app.html",
        "app/account.ts",
        "app/account.html"
      ]
    }
  },
  devConfig: {
    "map": {
      "plugin-typescript": "github:frankwallis/plugin-typescript@7.1.0",
      "aurelia-testing": "npm:aurelia-testing@1.0.0-beta.3.0.1",
      "source-map-support": "npm:source-map-support@0.4.18",
      "tape": "npm:tape@4.8.0",
      "@types/tape": "npm:@types/tape@4.2.31",
      "fsevents": "npm:fsevents@1.1.2",
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
      "text": "github:systemjs/plugin-text@0.0.9"
    },
    "packages": {
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
      "npm:fsevents@1.1.2": {
        "map": {
          "node-pre-gyp": "npm:node-pre-gyp@0.6.38",
          "nan": "npm:nan@2.7.0"
        }
      },
      "npm:@types/tape@4.2.31": {
        "map": {
          "@types/node": "npm:@types/node@8.0.47"
        }
      },
      "npm:node-pre-gyp@0.6.38": {
        "map": {
          "request": "npm:request@2.81.0",
          "tar-pack": "npm:tar-pack@3.4.1",
          "hawk": "npm:hawk@3.1.3",
          "mkdirp": "npm:mkdirp@0.5.1",
          "rc": "npm:rc@1.2.2",
          "rimraf": "npm:rimraf@2.6.2",
          "semver": "npm:semver@5.4.1",
          "nopt": "npm:nopt@4.0.1",
          "npmlog": "npm:npmlog@4.1.2",
          "tar": "npm:tar@2.2.1"
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
      }
    }
  },
  map: {
    "bootstrap-css": "github:twbs/bootstrap@3.3.7/dist/css/bootstrap.css"
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
    "aurelia-ui-virtualization": "npm:aurelia-ui-virtualization@1.0.0-beta.3.1.2",
    "aurelia-validation": "npm:aurelia-validation@1.1.2",
    "@types/deep-freeze": "npm:@types/deep-freeze@0.1.1",
    "aurelia-ui-framework": "github:adarshpastakia/aurelia-ui-framework@master",
    "@types/jquery-contextmenu": "npm:@types/jquery.contextmenu@1.7.33",
    "@types/typeahead": "npm:@types/typeahead@0.11.31",
    "babel": "npm:babel-core@6.26.0",
    "deep-freeze": "npm:deep-freeze@0.0.1",
    "domain": "npm:jspm-nodelibs-domain@0.2.1",
    "flat-map": "github:aluanhaddad/flat-map@master",
    "font-awesome": "npm:font-awesome@4.7.0",
    "aurelia-dialog": "npm:aurelia-dialog@1.0.0-rc.1.0.3",
    "@types/bootstrap": "npm:@types/bootstrap@3.3.36",
    "aurelia-http-client": "npm:aurelia-http-client@1.2.1",
    "aurelia-fetch-client": "npm:aurelia-fetch-client@1.1.3",
    "aurelia-binding": "npm:aurelia-binding@1.5.0",
    "aurelia-bootstrapper": "npm:aurelia-bootstrapper@2.1.1",
    "aurelia-dependency-injection": "npm:aurelia-dependency-injection@1.3.2",
    "aurelia-event-aggregator": "npm:aurelia-event-aggregator@1.0.1",
    "aurelia-framework": "npm:aurelia-framework@1.1.5",
    "aurelia-history": "npm:aurelia-history@1.1.0",
    "aurelia-history-browser": "npm:aurelia-history-browser@1.1.0",
    "aurelia-loader": "npm:aurelia-loader@1.0.0",
    "aurelia-loader-default": "npm:aurelia-loader-default@1.0.3",
    "aurelia-logging": "npm:aurelia-logging@1.3.1",
    "aurelia-logging-console": "npm:aurelia-logging-console@1.0.0",
    "aurelia-metadata": "npm:aurelia-metadata@1.0.3",
    "aurelia-pal": "npm:aurelia-pal@1.4.0",
    "aurelia-pal-browser": "npm:aurelia-pal-browser@1.3.0",
    "aurelia-path": "npm:aurelia-path@1.1.1",
    "aurelia-polyfills": "npm:aurelia-polyfills@1.2.2",
    "aurelia-route-recognizer": "npm:aurelia-route-recognizer@1.1.1",
    "aurelia-router": "npm:aurelia-router@1.4.0",
    "aurelia-task-queue": "npm:aurelia-task-queue@1.2.1",
    "aurelia-templating": "npm:aurelia-templating@1.6.0",
    "aurelia-templating-binding": "npm:aurelia-templating-binding@1.4.0",
    "aurelia-templating-resources": "npm:aurelia-templating-resources@1.5.4",
    "aurelia-templating-router": "npm:aurelia-templating-router@1.2.0",
    "assert": "npm:jspm-nodelibs-assert@0.2.1",
    "bootstrap": "github:twbs/bootstrap@3.3.7",
    "buffer": "npm:jspm-nodelibs-buffer@0.2.3",
    "child_process": "npm:jspm-nodelibs-child_process@0.2.1",
    "constants": "npm:jspm-nodelibs-constants@0.2.1",
    "core-js": "npm:core-js@2.5.1",
    "crypto": "npm:jspm-nodelibs-crypto@0.2.1",
    "dgram": "npm:jspm-nodelibs-dgram@0.2.1",
    "dns": "npm:jspm-nodelibs-dns@0.2.1",
    "events": "npm:jspm-nodelibs-events@0.2.2",
    "fs": "npm:jspm-nodelibs-fs@0.2.1",
    "graceful-fs": "npm:graceful-fs@4.1.11",
    "http": "npm:jspm-nodelibs-http@0.2.0",
    "https": "npm:jspm-nodelibs-https@0.2.2",
    "jquery": "npm:jquery@2.2.4",
    "jquery-contextmenu": "npm:jquery-contextmenu@2.6.2",
    "kramed": "npm:kramed@0.5.6",
    "lodash": "npm:lodash@4.17.4",
    "module": "npm:jspm-nodelibs-module@0.2.1",
    "moment": "npm:moment@2.19.1",
    "net": "npm:jspm-nodelibs-net@0.2.1",
    "numeral": "npm:numeral@1.5.6",
    "os": "npm:jspm-nodelibs-os@0.2.2",
    "path": "npm:jspm-nodelibs-path@0.2.3",
    "process": "npm:jspm-nodelibs-process@0.2.1",
    "punycode": "npm:jspm-nodelibs-punycode@0.2.1",
    "querystring": "npm:jspm-nodelibs-querystring@0.2.2",
    "readline": "npm:jspm-nodelibs-readline@0.2.1",
    "reflect-metadata": "npm:reflect-metadata@0.1.10",
    "rxjs": "npm:rxjs@5.5.2",
    "sass": "github:dougludlow/plugin-sass@0.6.0",
    "stream": "npm:jspm-nodelibs-stream@0.2.1",
    "string_decoder": "npm:jspm-nodelibs-string_decoder@0.2.2",
    "tls": "npm:jspm-nodelibs-tls@0.2.1",
    "tslib": "npm:tslib@1.8.0",
    "tty": "npm:jspm-nodelibs-tty@0.2.1",
    "typeahead": "github:twitter/typeahead.js@0.11.1",
    "typescript": "npm:typescript@next",
    "url": "npm:jspm-nodelibs-url@0.2.1",
    "util": "npm:jspm-nodelibs-util@0.2.2",
    "vm": "npm:jspm-nodelibs-vm@0.2.1",
    "whatwg-fetch": "npm:whatwg-fetch@2.0.3",
    "zlib": "npm:jspm-nodelibs-zlib@0.2.3"
  },
  packages: {
    "npm:create-ecdh@4.0.0": {
      "map": {
        "elliptic": "npm:elliptic@6.4.0",
        "bn.js": "npm:bn.js@4.11.8"
      }
    },
    "npm:public-encrypt@4.0.0": {
      "map": {
        "browserify-rsa": "npm:browserify-rsa@4.0.1",
        "bn.js": "npm:bn.js@4.11.8",
        "randombytes": "npm:randombytes@2.0.5",
        "create-hash": "npm:create-hash@1.1.3",
        "parse-asn1": "npm:parse-asn1@5.1.0"
      }
    },
    "npm:browserify-cipher@1.0.0": {
      "map": {
        "evp_bytestokey": "npm:evp_bytestokey@1.0.3",
        "browserify-aes": "npm:browserify-aes@1.1.1",
        "browserify-des": "npm:browserify-des@1.0.0"
      }
    },
    "npm:diffie-hellman@5.0.2": {
      "map": {
        "bn.js": "npm:bn.js@4.11.8",
        "randombytes": "npm:randombytes@2.0.5",
        "miller-rabin": "npm:miller-rabin@4.0.1"
      }
    },
    "npm:browserify-rsa@4.0.1": {
      "map": {
        "bn.js": "npm:bn.js@4.11.8",
        "randombytes": "npm:randombytes@2.0.5"
      }
    },
    "npm:browserify-des@1.0.0": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "cipher-base": "npm:cipher-base@1.0.4",
        "des.js": "npm:des.js@1.0.0"
      }
    },
    "npm:des.js@1.0.0": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
      }
    },
    "npm:stream-browserify@2.0.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "readable-stream": "npm:readable-stream@2.3.3"
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
        "readable-stream": "npm:readable-stream@2.3.3",
        "pako": "npm:pako@0.2.9"
      }
    },
    "github:twbs/bootstrap@3.3.7": {
      "map": {
        "jquery": "npm:jquery@2.2.4"
      }
    },
    "npm:jspm-nodelibs-http@0.2.0": {
      "map": {
        "http-browserify": "npm:stream-http@2.7.2"
      }
    },
    "npm:font-awesome@4.7.0": {
      "map": {
        "css": "github:systemjs/plugin-css@0.1.36"
      }
    },
    "github:twitter/typeahead.js@0.11.1": {
      "map": {
        "jquery": "npm:jquery@2.2.4"
      }
    },
    "npm:asn1.js@4.9.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0",
        "bn.js": "npm:bn.js@4.11.8"
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
    "npm:mkdirp@0.5.1": {
      "map": {
        "minimist": "npm:minimist@0.0.8"
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
    "npm:loose-envify@1.3.1": {
      "map": {
        "js-tokens": "npm:js-tokens@3.0.2"
      }
    },
    "npm:parse-asn1@5.1.0": {
      "map": {
        "create-hash": "npm:create-hash@1.1.3",
        "evp_bytestokey": "npm:evp_bytestokey@1.0.3",
        "pbkdf2": "npm:pbkdf2@3.0.14",
        "browserify-aes": "npm:browserify-aes@1.1.1",
        "asn1.js": "npm:asn1.js@4.9.1"
      }
    },
    "npm:elliptic@6.4.0": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "bn.js": "npm:bn.js@4.11.8",
        "minimalistic-crypto-utils": "npm:minimalistic-crypto-utils@1.0.1",
        "hmac-drbg": "npm:hmac-drbg@1.0.1",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0",
        "hash.js": "npm:hash.js@1.1.3",
        "brorand": "npm:brorand@1.1.0"
      }
    },
    "npm:browserify-sign@4.0.4": {
      "map": {
        "create-hmac": "npm:create-hmac@1.1.6",
        "create-hash": "npm:create-hash@1.1.3",
        "inherits": "npm:inherits@2.0.3",
        "elliptic": "npm:elliptic@6.4.0",
        "browserify-rsa": "npm:browserify-rsa@4.0.1",
        "parse-asn1": "npm:parse-asn1@5.1.0",
        "bn.js": "npm:bn.js@4.11.8"
      }
    },
    "npm:jspm-nodelibs-punycode@0.2.1": {
      "map": {
        "punycode": "npm:punycode@1.4.1"
      }
    },
    "npm:jspm-nodelibs-stream@0.2.1": {
      "map": {
        "stream-browserify": "npm:stream-browserify@2.0.1"
      }
    },
    "npm:jspm-nodelibs-crypto@0.2.1": {
      "map": {
        "crypto-browserify": "npm:crypto-browserify@3.11.1"
      }
    },
    "npm:jspm-nodelibs-zlib@0.2.3": {
      "map": {
        "browserify-zlib": "npm:browserify-zlib@0.1.4"
      }
    },
    "npm:jspm-nodelibs-domain@0.2.1": {
      "map": {
        "domain-browser": "npm:domain-browser@1.1.7"
      }
    },
    "npm:jspm-nodelibs-url@0.2.1": {
      "map": {
        "url": "npm:url@0.11.0"
      }
    },
    "npm:babel-messages@6.23.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0"
      }
    },
    "npm:babel-helpers@6.24.1": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "babel-template": "npm:babel-template@6.26.0"
      }
    },
    "npm:hmac-drbg@1.0.1": {
      "map": {
        "hash.js": "npm:hash.js@1.1.3",
        "minimalistic-crypto-utils": "npm:minimalistic-crypto-utils@1.0.1",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
      }
    },
    "npm:typescript@next": {
      "map": {
        "source-map-support": "npm:source-map-support@0.4.18"
      }
    },
    "npm:jspm-nodelibs-buffer@0.2.3": {
      "map": {
        "buffer": "npm:buffer@5.0.8"
      }
    },
    "npm:once@1.4.0": {
      "map": {
        "wrappy": "npm:wrappy@1.0.2"
      }
    },
    "npm:inflight@1.0.6": {
      "map": {
        "once": "npm:once@1.4.0",
        "wrappy": "npm:wrappy@1.0.2"
      }
    },
    "npm:css-url-rewriter-ex@1.0.6": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0"
      }
    },
    "npm:css-asset-copier@1.0.2": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "fs-extra": "npm:fs-extra@0.30.0"
      }
    },
    "npm:fs-extra@0.30.0": {
      "map": {
        "klaw": "npm:klaw@1.3.1",
        "jsonfile": "npm:jsonfile@2.4.0",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "rimraf": "npm:rimraf@2.6.2",
        "graceful-fs": "npm:graceful-fs@4.1.11"
      }
    },
    "github:dougludlow/plugin-sass@0.6.0": {
      "map": {
        "css-url-rewriter-ex": "npm:css-url-rewriter-ex@1.0.6",
        "css-asset-copier": "npm:css-asset-copier@1.0.2",
        "autoprefixer": "npm:autoprefixer@6.7.7",
        "postcss": "npm:postcss@5.2.18",
        "sass.js": "npm:sass.js@0.9.13",
        "reqwest": "github:ded/reqwest@2.0.5"
      }
    },
    "npm:supports-color@3.2.3": {
      "map": {
        "has-flag": "npm:has-flag@1.0.0"
      }
    },
    "npm:autoprefixer@6.7.7": {
      "map": {
        "postcss": "npm:postcss@5.2.18",
        "browserslist": "npm:browserslist@1.7.7",
        "num2fraction": "npm:num2fraction@1.2.2",
        "caniuse-db": "npm:caniuse-db@1.0.30000755",
        "normalize-range": "npm:normalize-range@0.1.2",
        "postcss-value-parser": "npm:postcss-value-parser@3.3.0"
      }
    },
    "npm:browserslist@1.7.7": {
      "map": {
        "caniuse-db": "npm:caniuse-db@1.0.30000755",
        "electron-to-chromium": "npm:electron-to-chromium@1.3.27"
      }
    },
    "github:adarshpastakia/aurelia-ui-framework@master": {
      "map": {
        "aurelia-validation": "npm:aurelia-validation@1.1.2",
        "kramed": "npm:kramed@0.5.6",
        "aurelia-ui-virtualization": "npm:aurelia-ui-virtualization@1.0.0-beta.3.1.2",
        "aurelia-bootstrapper": "npm:aurelia-bootstrapper@1.0.1",
        "lodash": "npm:lodash@4.17.4",
        "moment": "npm:moment@2.18.1",
        "aurelia-fetch-client": "npm:aurelia-fetch-client@1.1.3",
        "numeral": "npm:numeral@1.5.6",
        "whatwg-fetch": "npm:whatwg-fetch@2.0.3",
        "aurelia-animator-css": "npm:aurelia-animator-css@1.0.4"
      }
    },
    "npm:create-hash@1.1.3": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "cipher-base": "npm:cipher-base@1.0.4",
        "ripemd160": "npm:ripemd160@2.0.1",
        "sha.js": "npm:sha.js@2.4.9"
      }
    },
    "npm:create-hmac@1.1.6": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "create-hash": "npm:create-hash@1.1.3",
        "cipher-base": "npm:cipher-base@1.0.4",
        "ripemd160": "npm:ripemd160@2.0.1",
        "sha.js": "npm:sha.js@2.4.9",
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    },
    "npm:ripemd160@2.0.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "hash-base": "npm:hash-base@2.0.2"
      }
    },
    "npm:hash-base@2.0.2": {
      "map": {
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:randombytes@2.0.5": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    },
    "npm:readable-stream@2.3.3": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "string_decoder": "npm:string_decoder@1.0.3",
        "core-util-is": "npm:core-util-is@1.0.2",
        "process-nextick-args": "npm:process-nextick-args@1.0.7",
        "isarray": "npm:isarray@1.0.0",
        "util-deprecate": "npm:util-deprecate@1.0.2"
      }
    },
    "npm:string_decoder@1.0.3": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    },
    "npm:debug@2.6.9": {
      "map": {
        "ms": "npm:ms@2.0.0"
      }
    },
    "npm:babel-core@6.26.0": {
      "map": {
        "lodash": "npm:lodash@4.17.4",
        "debug": "npm:debug@2.6.9",
        "babel-generator": "npm:babel-generator@6.26.0",
        "babel-template": "npm:babel-template@6.26.0",
        "slash": "npm:slash@1.0.0",
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "babel-code-frame": "npm:babel-code-frame@6.26.0",
        "private": "npm:private@0.1.8",
        "source-map": "npm:source-map@0.5.7",
        "babel-helpers": "npm:babel-helpers@6.24.1",
        "babel-messages": "npm:babel-messages@6.23.0",
        "babel-traverse": "npm:babel-traverse@6.26.0",
        "babel-types": "npm:babel-types@6.26.0",
        "babylon": "npm:babylon@6.18.0",
        "minimatch": "npm:minimatch@3.0.4",
        "babel-register": "npm:babel-register@6.26.0",
        "convert-source-map": "npm:convert-source-map@1.5.0",
        "json5": "npm:json5@0.5.1"
      }
    },
    "npm:source-map-support@0.4.18": {
      "map": {
        "source-map": "npm:source-map@0.5.7"
      }
    },
    "npm:@types/jquery.contextmenu@1.7.33": {
      "map": {
        "@types/jquery": "npm:@types/jquery@3.2.15"
      }
    },
    "npm:@types/typeahead@0.11.31": {
      "map": {
        "@types/jquery": "npm:@types/jquery@3.2.15"
      }
    },
    "npm:rxjs@5.5.2": {
      "map": {
        "symbol-observable": "npm:symbol-observable@1.0.4"
      }
    },
    "npm:babel-generator@6.26.0": {
      "map": {
        "babel-messages": "npm:babel-messages@6.23.0",
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "babel-types": "npm:babel-types@6.26.0",
        "source-map": "npm:source-map@0.5.7",
        "lodash": "npm:lodash@4.17.4",
        "jsesc": "npm:jsesc@1.3.0",
        "trim-right": "npm:trim-right@1.0.1",
        "detect-indent": "npm:detect-indent@4.0.0"
      }
    },
    "npm:babel-traverse@6.26.0": {
      "map": {
        "babel-messages": "npm:babel-messages@6.23.0",
        "babel-code-frame": "npm:babel-code-frame@6.26.0",
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "babel-types": "npm:babel-types@6.26.0",
        "babylon": "npm:babylon@6.18.0",
        "debug": "npm:debug@2.6.9",
        "lodash": "npm:lodash@4.17.4",
        "globals": "npm:globals@9.18.0",
        "invariant": "npm:invariant@2.2.2"
      }
    },
    "npm:babel-template@6.26.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "babel-traverse": "npm:babel-traverse@6.26.0",
        "babel-types": "npm:babel-types@6.26.0",
        "babylon": "npm:babylon@6.18.0",
        "lodash": "npm:lodash@4.17.4"
      }
    },
    "npm:babel-types@6.26.0": {
      "map": {
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "lodash": "npm:lodash@4.17.4",
        "esutils": "npm:esutils@2.0.2",
        "to-fast-properties": "npm:to-fast-properties@1.0.3"
      }
    },
    "npm:babel-register@6.26.0": {
      "map": {
        "babel-core": "npm:babel-core@6.26.0",
        "core-js": "npm:core-js@2.5.1",
        "source-map-support": "npm:source-map-support@0.4.18",
        "lodash": "npm:lodash@4.17.4",
        "babel-runtime": "npm:babel-runtime@6.26.0",
        "mkdirp": "npm:mkdirp@0.5.1",
        "home-or-tmp": "npm:home-or-tmp@2.0.0"
      }
    },
    "npm:jspm-nodelibs-os@0.2.2": {
      "map": {
        "os-browserify": "npm:os-browserify@0.3.0"
      }
    },
    "npm:babel-runtime@6.26.0": {
      "map": {
        "core-js": "npm:core-js@2.5.1",
        "regenerator-runtime": "npm:regenerator-runtime@0.11.0"
      }
    },
    "npm:babel-code-frame@6.26.0": {
      "map": {
        "js-tokens": "npm:js-tokens@3.0.2",
        "esutils": "npm:esutils@2.0.2",
        "chalk": "npm:chalk@1.1.3"
      }
    },
    "npm:jspm-nodelibs-string_decoder@0.2.2": {
      "map": {
        "string_decoder": "npm:string_decoder@0.10.31"
      }
    },
    "npm:glob@7.1.2": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimatch": "npm:minimatch@3.0.4",
        "path-is-absolute": "npm:path-is-absolute@1.0.1",
        "once": "npm:once@1.4.0",
        "fs.realpath": "npm:fs.realpath@1.0.0",
        "inflight": "npm:inflight@1.0.6"
      }
    },
    "npm:minimatch@3.0.4": {
      "map": {
        "brace-expansion": "npm:brace-expansion@1.1.8"
      }
    },
    "npm:stream-http@2.7.2": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "readable-stream": "npm:readable-stream@2.3.3",
        "builtin-status-codes": "npm:builtin-status-codes@3.0.0",
        "to-arraybuffer": "npm:to-arraybuffer@1.0.1",
        "xtend": "npm:xtend@4.0.1"
      }
    },
    "npm:rimraf@2.6.2": {
      "map": {
        "glob": "npm:glob@7.1.2"
      }
    },
    "npm:crypto-browserify@3.11.1": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "pbkdf2": "npm:pbkdf2@3.0.14",
        "create-ecdh": "npm:create-ecdh@4.0.0",
        "public-encrypt": "npm:public-encrypt@4.0.0",
        "create-hmac": "npm:create-hmac@1.1.6",
        "browserify-sign": "npm:browserify-sign@4.0.4",
        "randombytes": "npm:randombytes@2.0.5",
        "browserify-cipher": "npm:browserify-cipher@1.0.0",
        "diffie-hellman": "npm:diffie-hellman@5.0.2",
        "create-hash": "npm:create-hash@1.1.3"
      }
    },
    "npm:buffer@5.0.8": {
      "map": {
        "ieee754": "npm:ieee754@1.1.8",
        "base64-js": "npm:base64-js@1.2.1"
      }
    },
    "npm:brace-expansion@1.1.8": {
      "map": {
        "balanced-match": "npm:balanced-match@1.0.0",
        "concat-map": "npm:concat-map@0.0.1"
      }
    },
    "npm:postcss@5.2.18": {
      "map": {
        "chalk": "npm:chalk@1.1.3",
        "source-map": "npm:source-map@0.5.7",
        "supports-color": "npm:supports-color@3.2.3",
        "js-base64": "npm:js-base64@2.3.2"
      }
    },
    "npm:pbkdf2@3.0.14": {
      "map": {
        "create-hmac": "npm:create-hmac@1.1.6",
        "create-hash": "npm:create-hash@1.1.3",
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "ripemd160": "npm:ripemd160@2.0.1",
        "sha.js": "npm:sha.js@2.4.9"
      }
    },
    "npm:sha.js@2.4.9": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:browserify-aes@1.1.1": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "cipher-base": "npm:cipher-base@1.0.4",
        "create-hash": "npm:create-hash@1.1.3",
        "evp_bytestokey": "npm:evp_bytestokey@1.0.3",
        "inherits": "npm:inherits@2.0.3",
        "buffer-xor": "npm:buffer-xor@1.0.3"
      }
    },
    "npm:evp_bytestokey@1.0.3": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "md5.js": "npm:md5.js@1.3.4"
      }
    },
    "npm:cipher-base@1.0.4": {
      "map": {
        "safe-buffer": "npm:safe-buffer@5.1.1",
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:miller-rabin@4.0.1": {
      "map": {
        "bn.js": "npm:bn.js@4.11.8",
        "brorand": "npm:brorand@1.1.0"
      }
    },
    "npm:md5.js@1.3.4": {
      "map": {
        "hash-base": "npm:hash-base@3.0.4",
        "inherits": "npm:inherits@2.0.3"
      }
    },
    "npm:hash.js@1.1.3": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "minimalistic-assert": "npm:minimalistic-assert@1.0.0"
      }
    },
    "npm:hash-base@3.0.4": {
      "map": {
        "inherits": "npm:inherits@2.0.3",
        "safe-buffer": "npm:safe-buffer@5.1.1"
      }
    }
  }
});
