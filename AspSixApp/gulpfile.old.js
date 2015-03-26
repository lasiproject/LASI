(function (config) {

    var gulp = require('gulp');
    var plumber = require('gulp-plumber');
    var bower = require('gulp-bower');
    var qunit = require('gulp-qunit');
    var jslint = require('gulp-jslint');
    var cssmin = require('gulp-cssmin');
    var jsmin = require('gulp-jsmin');
    var sourcemap = require('gulp-sourcemaps');
    var concat = require('gulp-concat');
    var watch = require('gulp-watch');
    var clean = require('gulp-clean');

    gulp.task('cleanBowerInstallDir', [], function () {
        return gulp
            .src(config.bowerInstallDir + '*')
            .pipe(clean());
    });
    gulp.task('bower', ['cleanBowerInstallDir'], function () {
        return bower().pipe(gulp.dest(config.bowerInstallDir));

    });
    gulp.task('remLibJSFile', [], function () {
        return gulp
            .src(config.libJSDir + config.libJSFile)
            .pipe(clean());
    });
    gulp.task('buildLib', ['remLibJSFile'], function () {
        return gulp
            .src(['wwwroot/lib/jquery/jquery.js', 'wwwroot/lib/bootstrap/js/bootstrap.js', 'wwwroot/lib/**/*js', '!wwwroot/lib/**/*min.js'])
            .pipe(concat(config.libJSFile))
            .pipe(gulp.dest(config.libJSDir));
    });
    gulp.task('buildMinLib', [], function () {
        return gulp
            .src(config.libJSDir + config.libJSFile)
            .pipe(jsmin())
            .pipe(gulp.dest(config.libJSDir));
    });
    gulp.task('watchBowerJSON', [], function () {
        return watch('bower.json', function () {
            gulp.start('bower');
        });
    });
    gulp.task('minCss', [], function () {
        return gulp.src(['wwwroot/lib/**/*.css', '!wwwroot/lib/**/*.min.css'])
          .pipe(cssmin())
          .pipe(rename({ suffix: '.min' }))
          .pipe(concat('lib.min.css'))
          .pipe(gulp.dest('wwwroot/dist/lib'));
    });

    gulp.src('wwwroot/app/**/*.js')
          .pipe(plumber())
          .pipe(jslint({
              vars: true,
              browser: true, todo: true,
              predef: {
                  alert: false,
                  console: false,
                  app: true,
                  LASI: true,
                  $: false,
                  QUnit: false,
                  google: false,
                  require: false,
                  define: false
              },
              version: 'latest'
          }));


    gulp.task('test', [], function () {
        return gulp
            .src('wwwroot/test/**/*.html')
            .pipe(qunit());
    });

}({
    libJSFile: 'lib.js',
    libMinJSFile: 'lib.min.js',
    libJSDir: 'wwwroot/dist/lib/',
    bowerInstallDir: 'wwwroot/lib/'
}));