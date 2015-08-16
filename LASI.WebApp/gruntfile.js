/// <binding AfterBuild='typescript' ProjectOpened='watch' />
// This file in the main entry point for defining grunt tasks and using grunt plugins.

module.exports = function (grunt) {
    'use strict';
    grunt.initConfig({
        bower: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/grunt-bower-task.json'),
        typescript: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/grunt-typescript.json'),
        tslint: {
            options: { configuration: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/tslint.json') },
            app: { src: ['wwwroot/app/**/*.ts'] },
            test: { src: ['wwwroot/test/**/*.ts'] }
        },
        cssmin: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/grunt-cssmin-task.json'),
        qunit: { all: ['wwwroot/test/**/*.html'] },
        watch: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/grunt-watch-task.json'),
        clean: grunt.file.readJSON('TaskRunnerConfiguration/Grunt/grunt-clean-task.json')
    });
    // This command registers the default task which installs bower packages into wwwroot/lib.
    grunt.registerTask('default', ['bower:install']);
    // register an alias for qunit tests called 'test'.
    grunt.registerTask('test', ['qunit:all']);
    // register an alias for qunit tests called 'test'.
    grunt.registerTask('tslintandbuild', ['tslint', 'typescript']);

    // The following lines loads the grunt plugins.
    // these lines needs to be at the end of this file.
    // cannot use an array or varargs to load tasks from multiple plugins here. 
    // It seems that loadNpmTasks is a singular command which loads task(s) for a single plugin.
    // This api is a bit counter intuitive in that invocations cannot be chained.
    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-typescript');
    grunt.loadNpmTasks('grunt-tslint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-clean');
};