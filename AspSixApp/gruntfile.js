/// <binding Clean='bower:install' ProjectOpened='watch:test' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409

module.exports = function (grunt) {
    var scriptDir = 'Scripts/**/*.js';
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: 'wwwroot/lib/',
                    layout: 'byComponent',
                    cleanTargetDir: true
                }
            }
        },
        jslint: {
            app: {
                src: [scriptDir],
                exclude: [],
                directives: {
                    browser: true,
                    predef: ['jQuery', 'QUnit', '$', 'require']
                }
            }
        }, 'jsmin-sourcemap': {
            app: {
                src: [scriptDir],
                dest: 'wwwroot/dist/app/app.min.js',
                destMap: 'wwwroot/dist/app/app.min.js.map'
            },
            lib: {
                src: ['wwwroot/lib/**/*.js'],
                dest: 'wwwroot/dist/lib/lib.min.js',
                destMap: 'wwwroot/dist/lib/lib.min.js.map'
            }
        },
        qunit: {
            all: ['Scripts/test/**/*.html']
        },
        watch: {
            app: {
                files: [scriptDir],
                tasks: ['jslint:app']
            },
            test: {
                files: ['Scripts/app/util.js', 'Scripts/test/**/*.js'],
                tasks: ['qunit:all']
            }
        }
    });

    // This command registers the default task which installs bower packages into wwwroot/lib, and runs jslint.
    grunt.registerTask('default', ['bower:install', 'jsmin-sourcemap:lib', 'jsmin-sourcemap:app']);
    grunt.loadNpmTasks('grunt-jsmin-sourcemap');
    grunt.loadNpmTasks('grunt-bower-task');
    // The following lines loads the grunt plugins.
    // these lines needs to be at the end of this file.
    // cannot use an array or varargs to load tasks from multiple plugins here. 
    // It seems that loadNpmTasks is a singular command which loads task(s) for a single plugin.
    // This api is a bit counter intuitive in that invokations cannot be chained.
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-jslint');

};