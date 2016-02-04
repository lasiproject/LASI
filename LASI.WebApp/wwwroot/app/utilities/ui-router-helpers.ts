/**
 * Highly Experimantal. Tries to synthesize a controller with the sole purpose of acting
 * as a container of the results of ui-router resolves without needing to create a class.
 * Not fully implemented.
 * @param ngDependencies names for $inject array.
 */
export default function CreateResolver(...ngDependencies: string[]): ((...args: { [k: string]: any; }[]) => (new (...args) => any)) {
    return function (...args) {
        return class {
            static $inject = [...ngDependencies];
            constructor(args: any[]) {
                var names = this.constructor.toString().split(',');
                names.forEach((name, index) => this[name] = args[index]); this
            }
        };
    }
}