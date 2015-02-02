module LASI.utilities {
    var validateAndParseJson = <T>(data: string) => {
        try { return <T>JSON.parse(data); } catch (jsonParseException) {
            throw { toString: () => "Deserailization failed. Ensure response is valid a JSON structure." };
        }
    };
    export function postJson(url: string, obj: any, success: (data: any) => any): JQueryXHR {
        return $.ajax({
            type: "POST",
            // Note: The call to JSON.stringify is necessary. If a pojo is used directly, jQuery will url encode it.
            data: JSON.stringify(obj),
            url: url,
            contentType: 'application/json',
            success: success
        });
    };
    var perform$Post = <TResource>(value: TResource, uri: string) => {
        return $.ajax({
            type: "POST",
            // Note: The call to JSON.stringify is necessary. 
            // If a pojo is used directly, jQuery will url encode it.
            data: JSON.stringify(value),
            url: "/api/jobs",
            contentType: "application/json"
        });
    };
    var perform$Get = <TResource>(uri: string) => {
        return validateAndParseJson<TResource>($.ajax({
            type: "GET",
            url: uri,
            //accepts: "application/json"
        }).responseJSON);
    };
    var perform$Delete = (uri: string) => {
        return $.ajax({
            type: "DELETE",
            url: "/api/jobs"
        });
    };
    export interface HttpResource<TResource> {
        location: string;
        load: () => TResource;
        save: () => JQueryXHR;
        delete: () => JQueryXHR;
    };
    export interface ResourceFactory {
        fromUri<TResource>(location: string): HttpResource<TResource>;
        fromValue<TResource>(value: TResource): HttpResource<TResource>;
    };
    export var ResourceFactory: ResourceFactory = {
        fromUri<TResource>(uri: string): HttpResource<TResource> {
            return {
                value: <TResource>{},
                location: uri,
                load: () => { this.value = perform$Get<TResource>(uri); return this.value; },
                save: () => perform$Post(this.value, uri),
                delete: () => perform$Delete(uri)
            };
        },
        fromValue<TResource>(value: TResource): HttpResource<TResource> {
            throw { toString: () => "not supported" };
        }
    };
}