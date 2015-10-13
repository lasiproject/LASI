/**
 @function A trait which specifies whose $inject property is required.
*/
type Annotated = {};

type BlockFunction = { (...args): any } & Annotated;
type AnnotatedFactory = { (...args): any } & Annotated;
type DirectiveFactory = ng.IDirectiveFactory & Annotated;
type AnnotatedConstructor = { new (...args): any } & Annotated;
type AnnotatedProviderClass = ng.IServiceProviderClass & Annotated;
type AnnotatedProviderFactory = ng.IServiceProviderFactory & Annotated;

interface AngularModuleOptions {
    name: string;
    requires: string[];
    configFn?: BlockFunction;
    runFn?: BlockFunction;
    values?: { [name: string]: any };
    constants?: { [name: string]: any };
    filters?: {};
    controllers?: {};
    directives?: {};
    factories?: {};
    services?: { [name: string]: AnnotatedConstructor };
    providers?: {}
}