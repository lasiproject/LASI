declare module LASI.utilities {
    function postJson(url: string, obj: any, success: (data: any) => any): JQueryXHR;
    interface HttpResource<TResource> {
        location: string;
        load: () => TResource;
        save: () => JQueryXHR;
        delete: () => JQueryXHR;
    }
    interface ResourceFactory {
        fromUri<TResource>(location: string): HttpResource<TResource>;
        fromValue<TResource>(value: TResource): HttpResource<TResource>;
    }
    var ResourceFactory: ResourceFactory;
}
