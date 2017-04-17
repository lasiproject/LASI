export default function<T extends Notifier<T, K>, K extends keyof T>(target: T, key: K) {
  let storedValue = target[key];

  const get = () => storedValue;

  const set = (value: T[K]) => {
    if (storedValue !== value) {
      storedValue = value;
      target.notifyPropertyChange(key, storedValue);
    }
  };

  Object.defineProperty(target, key, {
    get,
    set,
    enumerable: true,
    configurable: false
  });
}

export interface Notifier<T, K extends keyof T> {
  notifyPropertyChange(key: K, value: T[K]): void;
}