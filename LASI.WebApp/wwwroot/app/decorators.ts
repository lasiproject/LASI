export var inject: ClassDecorator = (target: FunctionConstructor) => {
    console.log(target);
};