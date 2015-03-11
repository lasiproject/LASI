/// <binding ProjectOpened='default' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409

module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: true
                }
            }
        },
        jslint: {
            app: {
                src: [
                    'wwwroot/test/util-test.js',
                    'wwwroot/app/util.js',
                    'wwwroot/app/LASI.js',
                    'wwwroot/app/widgets/document-upload.js',
                    'wwwroot/app/account/manage.js',
                    'wwwroot/app/results/results.js',
                    'wwwroot/app/results/context-menu-provider.js'
                ],
                exclude: [],
                directives: {
                    vars: true,
                    browser: true,
                    predef: {
                        alert: false,
                        LASI: true,
                        $: false,
                        QUnit: false,
                        google: false,
                        require: false,
                        define: false,
                        module: false
                    }
                }
            }
        },
        qunit: {
            all: ['wwwroot/test/**/*.html']
        },
        'jsmin-sourcemap': {
            //app: {
            //    src: [
            //        'Scripts/app/util.js',
            //        'Scripts/app/LASI.js',
            //        'Scripts/app/widgets/document-upload.js',
            //        'Scripts/app/account/manage.js',
            //        'Scripts/app/results/results.js',
            //        'Scripts/app/results/context-menu-provider.js'
            //    ],
            //    srcRoot: 'dist/app',
            //    dest: 'wwwroot/dist/app/app.min.js'

            //},
            lib: {
                src: [
                    'wwwroot/lib/jquery/**/*.js',
                    'wwwroot/lib/jquery-validation/**/*.js',
                    'wwwroot/lib/jquery-validation-unobtrusive/**/*.js',
                    'wwwroot/lib/bootstrap/js/bootstrap.js',
                    'wwwroot/lib/bootstrap-contextmenu/bootstrap-contextmenu.js'
                ],
                dest: 'wwwroot/dist/lib/lib.min.js'
            }
        },
        cssmin: {
            options: {
                shorthandCompacting: false,
                roundingPrecision: -1,
                sourceMap: true,//want this to be on but it causes an error
                verbose: true
            },
            app: {
                files: {
                    'wwwroot/dist/app/app.min.css': ['wwwroot/css/**/*.css']
                }
            },
            lib: {
                files: {
                    'wwwroot/dist/lib/lib.min.css': ['wwwroot/lib/**/*.css']
                }
            }
        },
        watch: {
            appjs: {
                files: ['wwwroot/app/**/*.js'],
                tasks: ['jslint:app', 'qunit:all']
            }, test: {
                files: ['wwwroot/test/**/*.js'],
                tasks: ['qunit:all']
            },
            libjs: {
                files: ['wwwroot/lib/**/*.js'],
                tasks: ['jsmin-sourcemap:lib']
            },
            appcss: {
                files: ['wwwroot/css/**'],
                tasks: ['cssmin:app']
            },
            libcss: {
                files: ['wwwroot/lib/**/*.css'],
                tasks: ['cssmin:lib']
            }

        }
    });

    // This command registers the default task which installs bower packages into wwwroot/lib, and runs jslint.
    grunt.registerTask('default', ['bower:install', 'jsmin-sourcemap:lib']);
    grunt.loadNpmTasks('grunt-bower-task');
    // The following lines loads the grunt plugins.
    // these lines needs to be at the end of this file.
    // cannot use an array or varargs to load tasks from multiple plugins here. 
    // It seems that loadNpmTasks is a singular command which loads task(s) for a single plugin.
    // This api is a bit counter intuitive in that invocations cannot be chained.
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-jsmin-sourcemap');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-jslint');

};