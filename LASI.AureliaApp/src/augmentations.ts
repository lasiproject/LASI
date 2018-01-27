// Ensure this remains a module regardless of dependencies
export {}
import {FrameworkConfiguration} from 'aurelia-framework';

declare module 'aurelia-framework' {
  interface FrameworkConfiguration {
    singleton(type: {}, factory: Function, deps: (Function | string)[]): FrameworkConfiguration;
  }
}

const {singleton} = FrameworkConfiguration.prototype;
FrameworkConfiguration.prototype.singleton = function (type: {}, implementationOrFactory: () => {}, deps?: {}) {
  if (Array.isArray(deps) && deps.length) {
    implementationOrFactory.inject = [...deps];
  }
  return singleton.call(this, type, implementationOrFactory);
}

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
    get<T>(key: (new (...args: {}[]) => T) | ((...args: {}[]) => T)): T;
  }
}

declare global {
  interface Function {
    inject?: {}[] | (() => {}[]);
  }
}