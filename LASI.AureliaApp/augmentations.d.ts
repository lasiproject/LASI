import 'jspm_packages/npm/aurelia-framework@1.0.7';
declare module 'jspm_packages/npm/aurelia-framework@1.0.7' {
  export function computedFrom<K1 extends string, K2 extends string, K3 extends string>
    (prop1: K1, prop2: K2, prop3: K3): (target: {[P in K1 | K2 | K3]}, key: string | number | symbol) => void;

  export function computedFrom<K1 extends string, K2 extends string>
    (prop1: K1, prop2: K2): (target: {[P in K1 | K2]}, key: string | number | symbol) => void;

  export function computedFrom<K extends string>
    (prop: K): (target: {[P in K]}, key: string | number | symbol) => void;
}