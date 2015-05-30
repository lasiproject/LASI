/// <binding ProjectOpened='watch' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409

module.exports = function (grunt) {
    'use strict';
    /**
     * prefixor factory
     * @param {string} pathPrefix - the prefix
     */
    function prefix(prefix) {
        return function (path) {
            return prefix + path;
        };
    };
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
            test: {
                src: 'wwwroot/test/**/*.js',
                verbose: true,
                maxparams: 4,
                undef: true,
                unused: true
            },
        },
        tslint: {
            options: {
                configuration: grunt.file.readJSON('tslint.json')
            },
            app: {
                src: [
                    'wwwroot/app/widgets/**/*.ts',
                    'wwwroot/app/document-viewer/**/*.ts',
                    'wwwroot/app/*.ts',
                ]
            }
        },
        qunit: { all: ['wwwroot/test/**/*.html'] },

        concat: {
            dist: {
                src: [
                    'LASI.js',
                    'debug-panel/debug-panel.js',
                    'utilities/augmentations.js',
                    'account/manage.js',
                    'results/context-menu-provider.js',
                    'results/result-chart-provider.js',
                    'document-viewer/**/*.js',
                    'widgets/document-upload.js',
                    'widgets/document-list.js',
                    'widgets/document-list/section.js',
                    'widgets/document-list/app.js',
                    'widgets/document-list/document-list-service-provider.js',
                    'widgets/document-list/documents-service.js',
                    'widgets/document-list/delete-document-modal-controller.js',
                    'widgets/document-list/results-service.js',
                    'widgets/document-list/document-list-menu-item.js',
                    'widgets/document-list/document-list-tabset-item.js',
                    'widgets/document-list/tasks-list-service-provider.js',
                    'widgets/document-list/list-controller.js',
                    'document-viewer/section.js'
                ].map(prefix('wwwroot/app/')),
                dest: 'wwwroot/dist/app/app.js',
            }, options: {
                sourceMap: true,
                stripBanners: {
                    block: true,
                    line: true
                }
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
            appts: {
                files: ['wwwroot/app/**/*ts'],
                tasks: ['tslint:app']
            },
            appjs: {
                files: ['wwwroot/app/**/*.js'],
                tasks: ['qunit:all', 'concat']
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
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-tslint');
};