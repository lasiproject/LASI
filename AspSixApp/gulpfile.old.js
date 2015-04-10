(function () {
    var dirs = function () {
        project = require('./project.json');
        return {
            lib: project.webroot + '/lib',
            app: project.webroot + '/app',
            css: project.webroot + '/css',
            test: project.webroot + '/test'
        };
    }();

    var gulp = require('gulp'),
        bower = require('gulp-bower'),
        jshint = require('gulp-jshint'),
        qunit = require('gulp-qunit'),
        del = require('del');

    gulp.task('clean', function (done) {
        del(dirs.lib, done);
    });

    gulp.task('bower:install', ['clean'], function () {
        return bower({
            directory: dirs.lib
        });
    });

    gulp.task('test', function () {
        return gulp.src(dirs.test + '/**/*.html')
            .pipe(qunit());
    });

    gulp.task('default', ['bower:install'], function () {
        return;
    });

    ////var plumber = require('gulp-plumber');

    ////var qunit = require('gulp-qunit');
    ////var cssmin = require('gulp-cssmin');
    ////var jsmin = require('gulp-jsmin');
    ////var sourcemap = require('gulp-sourcemaps');
    ////var concat = require('gulp-concat');
    ////var watch = require('gulp-watch');
    ////var clean = require('gulp-clean');
    ////var jshint = require('grulp-jshint');
    //gulp.task('cleanBowerInstallDir', [], function () {
    //    return gulp
    //        .src(config.bowerInstallDir + '*')
    //        .pipe(clean());
    //});
    //gulp.task('bower', ['cleanBowerInstallDir'], function () {
    //    return bower().pipe(gulp.dest(config.bowerInstallDir));

    //});
    //gulp.task('remLibJSFile', [], function () {
    //    return gulp
    //        .src(config.libJSDir + config.libJSFile)
    //        .pipe(clean());
    //});
    //gulp.task('buildLib', ['remLibJSFile'], function () {
    //    return gulp
    //        .src(['wwwroot/lib/jquery/jquery.js', 'wwwroot/lib/bootstrap/js/bootstrap.js', 'wwwroot/lib/**/*js', '!wwwroot/lib/**/*min.js'])
    //        .pipe(concat(config.libJSFile))
    //        .pipe(gulp.dest(config.libJSDir));
    //});
    //gulp.task('buildMinLib', [], function () {
    //    return gulp
    //        .src(config.libJSDir + config.libJSFile)
    //        .pipe(jsmin())
    //        .pipe(gulp.dest(config.libJSDir));
    //});
    //gulp.task('watchBowerJSON', [], function () {
    //    return watch('bower.json', function () {
    //        gulp.start('bower');
    //    });
    //});
    //gulp.task('minCss', [], function () {
    //    return gulp.src(['wwwroot/lib/**/*.css', '!wwwroot/lib/**/*.min.css'])
    //      .pipe(cssmin())
    //      .pipe(rename({ suffix: '.min' }))
    //      .pipe(concat('lib.min.css'))
    //      .pipe(gulp.dest('wwwroot/dist/lib'));
    //});



    //gulp.task('test', [], function () {
    //    return gulp
    //        .src('wwwroot/test/**/*.html')
    //        .pipe(qunit());
    //});

}());