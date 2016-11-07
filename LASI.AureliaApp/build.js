System.register("app/app/account.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n    <div class=\"col-md-10\">\r\n        <form class=\"form form-horizontal\">\r\n            <div class=\"form-group\">\r\n                <input type=\"text\" value.bind=\"userName\" class=\"form-control\">\r\n                <input type=\"text\" value.bind=\"fieldOfStudy\" class=\"form-control\">\r\n                <input type=\"date\" value.bind=\"birthDate\" class=\"form-control\">\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <div repeat.for=\"email of emails\">\r\n                    <input type=\"email\" value.bind=\"email.address\" class=\"form-control\">\r\n                    <input type=\"checkbox\" checked.bind=\"email.default\" class=\"form-control\">\r\n                </div>\r\n            </div>\r\n        </form>\r\n    </div>\r\n</template>");
        }
    };
});
System.register("app/app/app.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n  <require from=\"./navbar.html\"></require>\r\n  <navbar router.bind=\"router\"></navbar>\r\n  <div class=\"page-host\">\r\n    <router-view></router-view>\r\n  </div>\r\n</template>");
        }
    };
});
System.register("app/app/documents.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n    <require from=\"./document-viewer/document-viewer\"></require>\r\n    <document-viewer document.bind=\"document\"></document-viewer>\r\n</template>");
        }
    };
});
System.register("app/app/modal.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n    <require from=\"${component}\"></require>\r\n    <ai-dialog>\r\n    <ai-dialog-header>\r\n    </ai-dialog-header>\r\n    <ai-dialog-body>\r\n<div class=\"col-md-10\">\r\n    <form novalidate name=\"login-form\" submit.trigger=\"login()\" method=\"post\" class=\"form\" role=\"form\">\r\n    <div class=\"form-group\">\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Username: </label>\r\n            <input type=\"text\" name=\"email\" id=\"email\" placeholder=\"email\" value.bind=\"username\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Password: </label>\r\n            <input type=\"password\" name=\"password\" id=\"password\" value.bind=\"password\" class=\"form-control\" />\r\n\r\n            <input name=\"__RequestVerificationToken\" type=\"hidden\">\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Remeber Username?: </label>\r\n            <input type=\"checkbox\" name=\"remember-me\" id=\"remember-me\" value.bind=\"rememberMe\" class=\"form-control\" />\r\n        </div>\r\n    </div>\r\n    <button type=\"submit\" class=\"btn btn-primary\">Login</button>\r\n</form>\r\n</div>\r\n    </ai-dialog-body>\r\n    <ai-dialog-footer>\r\n\r\n    </ai-dialog-footer>\r\n    </ai-dialog>\r\n</template>");
        }
    };
});
System.register("app/app/navbar.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template bindable=\"router\">\r\n  <nav class=\"navbar navbar-default navbar-fixed-top navbar-inverse\" role=\"navigation\">\r\n    <div class=\"navbar-header\">\r\n      <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#bs-example-navbar-collapse-1\">\r\n        <span class=\"sr-only\">Toggle Navigation</span>\r\n        <span class=\"icon-bar\"></span>\r\n        <span class=\"icon-bar\"></span>\r\n        <span class=\"icon-bar\"></span>\r\n      </button>\r\n      <a class=\"navbar-brand\" href=\"#\">\r\n        <i class=\"fa fa-home\"></i>\r\n        <span>${router.title}</span>\r\n      </a>\r\n    </div>\r\n\r\n    <div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">\r\n      <ul class=\"nav navbar-nav\">\r\n        <li repeat.for=\"row of router.navigation\" class=\"${row.isActive ? 'active' : ''}\">\r\n          <a href.bind=\"row.href\">${row.title}</a>\r\n        </li>\r\n      </ul>\r\n\r\n      <ul class=\"nav navbar-nav navbar-right\">\r\n        <li class=\"loader\" if.bind=\"router.isNavigating\">\r\n          <i class=\"fa fa-spinner fa-spin fa-2x\"></i>\r\n        </li>\r\n      </ul>\r\n    </div>\r\n  </nav>\r\n</template>");
        }
    };
});
System.register("app/app/signin.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n<div class=\"col-md-10\">\r\n    <form novalidate name=\"login-form\" submit.trigger=\"login()\" method=\"post\" class=\"form\" role=\"form\">\r\n    <div class=\"form-group\">\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Username: </label>\r\n            <input type=\"text\" name=\"email\" id=\"email\" placeholder=\"email\" value.bind=\"username\" class=\"form-control\" />\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Password: </label>\r\n            <input type=\"password\" name=\"password\" id=\"password\" value.bind=\"password\" class=\"form-control\" />\r\n\r\n            <input name=\"__RequestVerificationToken\" type=\"hidden\">\r\n        </div>\r\n        <div class=\"form-group\">\r\n            <label class=\"control-label\">Remeber Username?: </label>\r\n            <input type=\"checkbox\" name=\"remember-me\" id=\"remember-me\" value.bind=\"rememberMe\" class=\"form-control\" />\r\n        </div>\r\n    </div>\r\n    <button type=\"submit\" class=\"btn btn-primary\">Login</button>\r\n</form>\r\n</div>\r\n</template>");
        }
    };
});
System.registerDynamic("github:frankwallis/plugin-typescript@5.2.9.json", [], false, function() {
  return {
    "main": "plugin",
    "format": "register"
  };
});

System.register("app/app/signout.html", [], function (exports_1, context_1) {
    "use strict";

    var __moduleName = context_1 && context_1.id;
    var __useDefault;
    return {
        setters: [],
        execute: function () {
            exports_1("__useDefault", __useDefault = true);
            exports_1("default", "<template>\r\n    <p>Signout?</p>\r\n    <div class=\"btn-group btn-group-lg btn-group-btn-group-vertical\">\r\n        <button class=\"btn btn-lg btn-primary\">Yes</button>\r\n        <button class=\"btn btn-lgbtn-link\">No</button>\r\n    </div>\r\n</template>");
        }
    };
});
//# sourceMappingURL=build.js.map