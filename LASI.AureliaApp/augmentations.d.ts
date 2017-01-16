import 'aurelia-framework';

declare module 'aurelia-framework' {
  export function computedFrom<K1 extends string, K2 extends string, K3 extends string>
    (prop1: K1, prop2: K2, prop3: K3): (target: {[P in K1 | K2 | K3]}, key: string | number | symbol) => void;

  export function computedFrom<K1 extends string, K2 extends string>
    (prop1: K1, prop2: K2): (target: {[P in K1 | K2]}, key: string | number | symbol) => void;

  export function computedFrom<K extends string>
    (prop: K): (target: {[P in K]}, key: string | number | symbol) => void;
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