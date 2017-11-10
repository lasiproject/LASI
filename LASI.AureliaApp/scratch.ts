function foo(baz: (boolean | (() => boolean))) {
    switch (true) {
        case typeof baz === 'function':
            return baz();
        default: return baz;
    }
}