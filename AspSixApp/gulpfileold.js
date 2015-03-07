(function (config) {
    /*
     * package json
     * "gulp": "3.8.11",
        "gulp-qunit": "1.2.1",
        "gulp-concat": "2.5.2",
        "gulp-cssmin": "0.1.6",
        "gulp-rename": "1.2.0",
        "gulp-jslint": "^0.2.2",
        "gulp-jsmin": "0.1.4",
        "gulp-sourcemaps": "1.5.0",
        "gulp-watch": "4.1.1",
        "gulp-bower": "0.0.10",
        "gulp-filter": "2.0.2",
        "gulp-clean": "0.3.1",
        "qunitjs": "1.17.1"
     * 
     */
    var gulp = require('gulp');
    var bower = require('gulp-bower');
    var qunit = require('gulp-qunit');
    var jslint = require('gulp-jslint');
    var cssmin = require('gulp-cssmin');
    var jsmin = require('gulp-jsmin');
    var sourcemap = require('gulp-sourcemaps');
    var concat = require('gulp-concat');
    var watch = require('gulp-watch');
    var clean = require('gulp-clean');
    var rename = require('gulp-rename');

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
    gulp.task('jslintWatcher', [], function () {
        var srcs = [
            'wwwroot/test/util-test.js',
            'wwwroot/app/util.js',
            'wwwroot/app/LASI.js',
            'wwwroot/app/widgets/document-upload.js',
            'wwwroot/app/account/manage.js',
            'wwwroot/app/results/results.js',
            'wwwroot/app/results/context-menu-provider.js'
        ];
        return watch(srcs, function () {
            gulp.src(srcs)
                .pipe(jslint({
                    vars: true,
                    browser: true,
                    predef: {
                        alert: false,
                        app: true,
                        LASI: true,
                        $: false,
                        QUnit: false,
                        google: false,
                        require: false
                    },
                    version: 'latest'
                }));
        });
    });
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