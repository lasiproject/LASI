var LASI;
(function (LASI) {
    var utilities;
    (function (utilities) {
        var validateAndParseJson = function (data) {
            try {
                return JSON.parse(data);
            }
            catch (jsonParseException) {
                throw { toString: function () { return "Deserailization failed. Ensure response is valid a JSON structure."; } };
            }
        };
        function postJson(url, obj, success) {
            return $.ajax({
                type: "POST",
                // Note: The call to JSON.stringify is necessary. If a pojo is used directly, jQuery will url encode it.
                data: JSON.stringify(obj),
                url: url,
                contentType: 'application/json',
                success: success
            });
        }
        utilities.postJson = postJson;
        ;
        var perform$Post = function (value, uri) {
            return $.ajax({
                type: "POST",
                // Note: The call to JSON.stringify is necessary. 
                // If a pojo is used directly, jQuery will url encode it.
                data: JSON.stringify(value),
                url: "/api/jobs",
                contentType: "application/json"
            });
        };
        var perform$Get = function (uri) {
            return validateAndParseJson($.ajax({
                type: "GET",
                url: uri,
            }).responseJSON);
        };
        var perform$Delete = function (uri) {
            return $.ajax({
                type: "DELETE",
                url: "/api/jobs"
            });
        };
        ;
        ;
        utilities.ResourceFactory = {
            fromUri: function (uri) {
                var _this = this;
                return {
                    value: {},
                    location: uri,
                    load: function () {
                        _this.value = perform$Get(uri);
                        return _this.value;
                    },
                    save: function () { return perform$Post(_this.value, uri); },
                    delete: function () { return perform$Delete(uri); }
                };
            },
            fromValue: function (value) {
                throw { toString: function () { return "not supported"; } };
            }
        };
    })(utilities = LASI.utilities || (LASI.utilities = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=utilities.js.map