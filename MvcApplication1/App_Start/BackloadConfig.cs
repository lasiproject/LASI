using Backload.Configuration;
using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(MvcExperimentation.BackloadConfig), "Initialize")]
 
namespace MvcExperimentation {
    public static class BackloadConfig
    {
        public static void Initialize()
        {
            // Use bundeling for client files (scripts (js) and styles (css)). 
            // Comment this out, if you manually include the files in your page
            registerBundles(BundleTable.Bundles);
        }


        private static void registerBundles(BundleCollection bundles)
        {
            var clientFiles = Bundles.GetClientFiles();
            if (clientFiles == null) clientFiles = new ClientFilesElement(); // Default values

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/fileUpload/bootstrap/Basic/js").Include(
                clientFiles.Scripts + "jqueryui/jquery.ui.widget.js",
                clientFiles.Scripts + "jquery.iframe-transport.js",
                clientFiles.Scripts + "jquery.fileupload.js")
            );
            bundles.Add(new StyleBundle("~/bundles/fileupload/bootstrap/Basic/css").Include(
                clientFiles.Styles + "bootstrap/bootstrap.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-responsive.debug.css",
                clientFiles.Styles + "jquery.fileupload-ui.css")
            );

            bundles.Add(new ScriptBundle("~/bundles/fileupload/bootstrap/BasicPlus/js").Include(
                clientFiles.Scripts + "jqueryui/jquery.ui.widget.js",
                clientFiles.Scripts + "load-image.debug.js",
                clientFiles.Scripts + "canvas-to-blob.debug.js",
                clientFiles.Scripts + "jquery.iframe-transport.js",
                clientFiles.Scripts + "jquery.fileupload.js",
                clientFiles.Scripts + "jquery.fileupload-process.js",
                clientFiles.Scripts + "jquery.fileupload-image.js",
                clientFiles.Scripts + "jquery.fileupload-audio.js",
                clientFiles.Scripts + "jquery.fileupload-video.js",
                clientFiles.Scripts + "jquery.fileupload-validate.js")
            );
            bundles.Add(new StyleBundle("~/bundles/fileupload/bootstrap/BasicPlus/css").Include(
                clientFiles.Styles + "bootstrap/bootstrap.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-responsive.debug.css",
                clientFiles.Styles + "jquery.fileupload-ui.css")
            );

            bundles.Add(new ScriptBundle("~/bundles/fileupload/bootstrap/BasicPlusUI/js").Include(
                clientFiles.Scripts + "jqueryui/jquery.ui.widget.js",
                clientFiles.Scripts + "tmpl.debug.js",
                clientFiles.Scripts + "load-image.debug.js",
                clientFiles.Scripts + "canvas-to-blob.debug.js",
                clientFiles.Scripts + "bootstrap/bootstrap.debug.js",
                clientFiles.Scripts + "bootstrap/bootstrap-image-gallery.debug.js",
                clientFiles.Scripts + "jquery.iframe-transport.js",
                clientFiles.Scripts + "jquery.fileupload.js",
                clientFiles.Scripts + "jquery.fileupload-process.js",
                clientFiles.Scripts + "jquery.fileupload-image.js",
                clientFiles.Scripts + "jquery.fileupload-audio.js",
                clientFiles.Scripts + "jquery.fileupload-video.js",
                clientFiles.Scripts + "jquery.fileupload-validate.js",
                clientFiles.Scripts + "jquery.fileupload-ui.js")
            );
            bundles.Add(new StyleBundle("~/bundles/fileupload/bootstrap/BasicPlusUI/css").Include(
                clientFiles.Styles + "bootstrap/bootstrap.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-responsive.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-image-gallery.debug.css",
                clientFiles.Styles + "jquery.fileupload-ui.css")
            );



            // Angular JS
            bundles.Add(new ScriptBundle("~/bundles/fileupload/angularjs/js").Include(
                clientFiles.Scripts + "angularjs/angular.debug.js",
                clientFiles.Scripts + "jqueryui/jquery.ui.widget.js",
                clientFiles.Scripts + "load-image.debug.js",
                clientFiles.Scripts + "canvas-to-blob.debug.js",
                clientFiles.Scripts + "bootstrap/bootstrap.debug.js",
                clientFiles.Scripts + "bootstrap/bootstrap-image-gallery.debug.js",
                clientFiles.Scripts + "jquery.iframe-transport.js",
                clientFiles.Scripts + "jquery.fileupload.js",
                clientFiles.Scripts + "jquery.fileupload-process.js",
                clientFiles.Scripts + "jquery.fileupload-image.js",
                clientFiles.Scripts + "jquery.fileupload-audio.js",
                clientFiles.Scripts + "jquery.fileupload-video.js",
                clientFiles.Scripts + "jquery.fileupload-validate.js",
                clientFiles.Scripts + "jquery.fileupload-angular.js")
            );
            bundles.Add(new StyleBundle("~/bundles/fileupload/angularjs/css").Include(
                clientFiles.Styles + "bootstrap/bootstrap.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-responsive.debug.css",
                clientFiles.Styles + "bootstrap/bootstrap-image-gallery.debug.css",
                clientFiles.Styles + "jquery.fileupload-ui.css")
            );


            // JQueryUI
            // jquery.fileupload.js: need special handling due to incopatibility with the bootstrap version
            // jquery.fileupload-ui.js: need special handling, relies on the special jqueryui version
            // jquery.fileupload-jui.js: need special handling, relies on the special jqueryui version
            bundles.Add(new ScriptBundle("~/bundles/fileupload/jqueryui/BasicPlusUI/js").Include(
                clientFiles.Scripts + "tmpl.debug.js",
                clientFiles.Scripts + "load-image.debug.js",
                clientFiles.Scripts + "canvas-to-blob.debug.js",
                clientFiles.Scripts + "jquery.image-gallery.debug.js",
                clientFiles.Scripts + "jquery.iframe-transport.js",
                clientFiles.Scripts + "jqueryui/jquery.fileupload.js",
                clientFiles.Scripts + "jquery.fileupload-fp.js",
                clientFiles.Scripts + "jqueryui/jquery.fileupload-ui.js",
                clientFiles.Scripts + "jqueryui/jquery.fileupload-jui.js")
            );

            // jquery.fileupload-jui.css: Put into special folder, because when the plugin was updated sometimes this does not work for jqueryui, so the older version has to be used.
            bundles.Add(new StyleBundle("~/bundles/fileupload/jqueryui/BasicPlusUI/css").Include(
                clientFiles.Styles + "jqueryui/jquery.fileupload-jui.css",
                clientFiles.Styles + "jquery.image-gallery.debug.css")
            );
        }
    }
}