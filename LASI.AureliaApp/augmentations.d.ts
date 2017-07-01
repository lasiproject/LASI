// Ensure this remains a module regardless of dependencies
export {}

declare module 'aurelia-pal' {
  namespace DOM {
    export type Element = typeof Element.prototype;
  }
}

declare module 'aurelia-dependency-injection' {
  interface Container {
    /**
     * Resolves a single instance based on the provided key.
     * @param key The key that identifies the object to resolve.
     * @return Returns the resolved instance.
     */
    get<T>(key: (new (...args: any[]) => T) | ((...args: any[]) => T)): T;
  }
}

declare global {
  interface Function {
    inject?: any[] | (() => any[]);
  }
}