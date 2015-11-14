/**
 @function A trait which decsribes a functions whose $inject property is required.
*/
type FunctionMap = { [name: string]: Function };
type ConstructorMap = { [name: string]: new (...args) => any };
type FunctionOrConstructorMap = { [name: string]: Function | (new (...args) => any) };

interface AngularModuleOptions {
    name: string;
    requires: (string | AngularModuleOptions)[];
    configFn?: Function;
    runFn?: Function;
    values?: FunctionMap;
    constants?: FunctionMap;
    filters?: FunctionMap;
    controllers?: ConstructorMap;
    directives?: FunctionMap;
    factories?: FunctionMap;
    services?: ConstructorMap;
    providers?: FunctionOrConstructorMap;
}