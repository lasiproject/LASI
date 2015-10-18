System.config({
  baseURL: "/",
  defaultJSExtensions: true,
  transpiler: "none",
  paths: {
    "github:*": "jspm_packages/github/*",
    "npm:*": "jspm_packages/npm/*"
  },

  packages: {
    "app": {
      "main": "app.js",
      "defaultExtension": "js",
      "modules": {
        "*.html": {
          "loader": "text"
        }
      }
    },
    "bootstrap-css": {
      "css": "github:systemjs/plugin-css@0.1.18",
      "bootstrap-css": "github:twbs/bootstrap@3.3.5/css/bootstrap.css!"
    }
  },

  map: {
    "angular": "github:angular/bower-angular@1.4.7",
    "angular-bootstrap": "github:angular-ui/bootstrap-bower@0.14.2",
    "angular-bootstrap-contextmenu": "github:Templarian/ui.bootstrap.contextMenu@0.9.4",
    "angular-file-upload": "github:danialfarid/ng-file-upload-bower@9.0.13",
    "angular-resource": "github:angular/bower-angular-resource@1.4.7",
    "bootstrap": "github:twbs/bootstrap@3.3.5",
    "css": "github:systemjs/plugin-css@0.1.18",
    "font-awesome": "npm:font-awesome@4.4.0",
    "jquery": "github:components/jquery@2.1.4",
    "jquery-validation": "github:jzaefferer/jquery-validation@1.14.0",
    "jquery-validation-unobtrusive": "github:aspnet/jquery-validation-unobtrusive@3.2.3",
    "text": "github:systemjs/plugin-text@0.0.2",
    "github:aspnet/jquery-validation-unobtrusive@3.2.3": {
      "jquery-validation": "github:jzaefferer/jquery-validation@1.14.0"
    },
    "github:jzaefferer/jquery-validation@1.14.0": {
      "jquery": "github:components/jquery@2.1.4"
    },
    "github:twbs/bootstrap@3.3.5": {
      "jquery": "github:components/jquery@2.1.4"
    },
    "npm:font-awesome@4.4.0": {
      "css": "github:systemjs/plugin-css@0.1.18"
    }
  }
});
