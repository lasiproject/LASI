/// <binding ProjectOpened='watch' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
module.exports = function (grunt) {
    'use strict';
    grunt.initConfig({
        typescript: {
            base: {
                src: [
                    'wwwroot/app/utilities/augmentations.ts',
                    'wwwroot/app/lasi.ts',
                    'wwwroot/app/**/*.module.ts',
                    'wwwroot/app/**/*.ts'
                ],
                dest: 'wwwroot/dist/app/app.js',
                options: {
                    references: [
                        'typings/**/*.d.ts'
                    ],
                    version: '1.5.0-beta',
                    noImplicitAny: false,
                    noEmitOnError: false,
                    removeComments: true,
                    sourceMap: true,
                    target: "es5",
                    declaration: false,
                    watch: false
                }
            }
        },
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: true
                }
            }
        },
        tslint: {
            options: {
                // Load the tslint rules from ./tslint.json. 
                // Keep this file checked in adhering to all defined rules and conventions.
                configuration: grunt.file.readJSON('tslint.json')
            },
            app: {
                src: [
                    'wwwroot/app/widgets/**/*.ts',
                    'wwwroot/app/document-viewer/**/*.ts',
                    'wwwroot/app/*.ts',
                ]
            },
            test: {
                src: ['wwwroot/test/**/*.ts']
            }
        },
        qunit: {
            all: ['wwwroot/test/**/*.html']
        },
        cssmin: {
            options: {
                shorthandCompacting: false,
                roundingPrecision: -1,
                sourceMap: true,
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
                tasks: ['tslint:app', 'qunit:all', 'typescript']
            },
            test: {
                files: ['wwwroot/test/**/*.js'],
                tasks: ['tslint:test', 'qunit:all']
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
    grunt.registerTask('default', ['bower:install', 'typescript:base', 'qunit:all']);
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
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-typescript');
    grunt.loadNpmTasks('grunt-tslint');
};
