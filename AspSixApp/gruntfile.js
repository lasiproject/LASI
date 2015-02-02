/// <binding AfterBuild='default' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409

module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: false
                }
            }
        },
        jslint: {
            client: {
                src: [
                  'wwwroot/**/*.js'
                ], exclude: [
                    'wwwroot/dist/js/*.j*',
                    'wwwroot/lib/**/*.js'
                ], directives: {
                    browser: true,
                    predef: [
                      'jQuery', '$', // jQuery
                      'test', 'ok', 'deepEqual', 'notStrictEqual' // qunit
                    ]
                }
            }
        }, 'jsmin-sourcemap': {
            app: {
                src: ['wwwroot/src/app/*.js'],
                dest: 'wwwroot/dist/js/app.min.js',
                destMap: 'wwwroot/dist/js/app.min.js.map'
            }, lib: {
                src: [
                    'wwwroot/lib/jquery/**/*.js',
                    'wwwroot/lib/jquery-validation/**/*.js',
                    'wwwroot/lib/jquery-validation-unobtrusive/**/*.js',
                    'wwwroot/lib/bootstrap/**/*.js',
                    'wwwroot/lib/**/*.js'
                ],
                dest: 'wwwroot/dist/js/lib.min.js',
                destMap: 'wwwroot/dist/js/lib.min.js.map'
            },
        }
    });

    // This command registers the default task which installs bower packages into wwwroot/lib, and runs jslint.
    grunt.registerTask('default', ['bower:install', 'jslint', 'jsmin-sourcemap:app']);
    grunt.registerTask('build-dist', ['jsmin-sourcemap:lib']);
    // The following lines loads the grunt plugins.
    // these lines needs to be at the end of this file.
    // cannot use an array or varargs to load tasks from multiple plugins here. 
    // It seems that loadNpmTasks is a singular command which loads task(s) for a single plugin.
    // This api is a bit counter intuitive in that invokations cannot be chained.
    grunt.loadNpmTasks("grunt-bower-task");
    grunt.loadNpmTasks('grunt-jslint');
    grunt.loadNpmTasks('grunt-jsmin-sourcemap');
};