import { Input as InputFactory, Injectable as InjectableFactory } from 'angular2/core';
// import { makePropDecorator, makeDecorator } from 'angular2/src/core/util/decorators';
/**
 * Shorthand decorator function for use when aliasing is not necessary.
 * @param target The property to decorate.
 * @param key They 
 */
export const Input: PropertyDecorator = (target, key) => {
    // call makePropDecorator passing the InputMetadata object resulting from calling the decorator factory, Input, on the key such that it has the name of the property.
    // this results in a decorator, not a decorator factory which must be manually called passing the arguments expected by a propery decorator, target and key.
    // this allows use to write
    // @input myProperty: string;
    // instead of
    // @Input() myProperty: string;
    // when we, as is most common, do not wish to alias the name, we do not incur the syntactic noise that comes from using a decorator factory.
    // What we have actually done is define a PropertyDecorator, input, by delegating to angular's Input decorator factory. 
    // Calling Input, a decorotor factory, actually returns what angular referers to as a decorator class which, when passed to makePropDecorator, returns a decorator to apply.
    // We then call this explicitely because we are not in decoration context;
    return InputFactory(key)(target, key);
}
export const Injectable: ClassDecorator = <TFunction extends (new (...args) => any)>(target: TFunction, chainFn?: (target: TFunction) => void) => InjectableFactory()(target, chainFn);