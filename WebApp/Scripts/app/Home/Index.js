/*This file will contain all the javascript and jquery functions for Index.cshtml.
Keeping the functions will help with organization and will allow us to create classes
with collections of javascript functions and that way we can load those classes,
which will optimize page load time.
*/
var LASI;
(function (LASI) {
    (function (Index) {
        "use strict";

        (function () {
            // All top level functions should start with this directive. nested functions inherit it.
            "use strict";

            //This function disables submit button
            $(function () {
                $("input:submit").attr("disabled", "true");
                $("input:file").change(function () {
                    if ($(this).val()) {
                        $("input:submit").removeAttr("disabled");
                    } else {
                        $("input:submit").attr("disabled", "true");
                    }
                });
            });
        }());
    })(LASI.Index || (LASI.Index = {}));
    var Index = LASI.Index;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=Index.js.map
