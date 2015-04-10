/// <binding AfterBuild='default' ProjectOpened='watch' />
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
        jshint: {
            app: {
                src: 'wwwroot/app/**/*.js',
                verbose: true,
                maxparams: 4,
                undef: true,
                unused: true
            },
            test: {
                src: 'wwwroot/test/**/*.js',
                verbose: true,
                maxparams: 4,
                undef: true,
                unused: true
            },
        },
        qunit: {
            all: ['wwwroot/test/**/*.html']
        },
        'jsmin-sourcemap': {
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
                tasks: ['jshint:app', 'qunit:all']
            },
            test: {
                files: ['wwwroot/test/**/*.js'],
                tasks: ['jshint:test', 'qunit:all']
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

    // This command registers the default task which installs bower packages into wwwroot/lib.
    grunt.registerTask('default', ['bower:install']);

    // register an alias for qunit tests called 'test'.
    grunt.registerTask('test', ['qunit:all']);


    // The following lines loads the grunt plugins.
    // these lines needs to be at the end of this file.
    // cannot use an array or varargs to load tasks from multiple plugins here. 
    // It seems that loadNpmTasks is a singular command which loads task(s) for a single plugin.
    // This api is a bit counter intuitive in that invocations cannot be chained.
    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-jsmin-sourcemap');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-contrib-jshint');
};