/// <binding ProjectOpened='watch' />
const gulp = require('gulp'),
    debug = require('gulp-debug'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    sourcemaps = require('gulp-sourcemaps'),
    cssNano = require('gulp-cssnano'),
    tslint = require('tslint'),
    gulpTslint = require('gulp-tslint'),
    uglify = require('gulp-uglify'),
    qunit = require('gulp-qunit'),
    build = require('./system.build.js');

var paths = {
    appts: [
        //'wwwroot/app/utilities/augmentations.ts',
        //'wwwroot/app/LASI.ts',
        //'wwwroot/app/**/*.module.ts',
        'wwwroot/app/**/*.ts',
        //'wwwroot/main.ts'
    ],
    test: [
        'wwwroot/test/**/*.html'
    ],
    css: {
        app: {
            src: 'wwwroot/css/**/*.css',
            dest: {
                dir: 'wwwroot/dist',
                name: 'app.css'
            }
        },
        lib: {
            src: 'wwwroot/lib/**/*.css',
            dest: {
                dir: 'wwwroot/dist/lib',
                name: 'lib.min.css'
            }
        }
    }
};

gulp.task('qunit', function () {
    return gulp.src(paths.test).pipe(qunit());
});

gulp.task('build', build);

gulp.task('appcss', function () {
    return gulp.src(paths.css.app.src)
        .pipe(sourcemaps.init())
        .pipe(concat(paths.css.app.dest.name))
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest(paths.css.app.dest.dir))
        .pipe(cssNano())
        .pipe(rename('app.min.css'))
        .pipe(gulp.dest(paths.css.app.dest.dir));
});

gulp.task('tslint', function () {
    return gulp.src(paths.appts)
        .pipe(gulpTslint({
            emitError: true,
            rulesDirectory: './wwwroot',
            tslint: tslint
        }))
        .pipe(gulpTslint.report('verbose'));
});

gulp.task('watch', function () {
    var onError = console.error.bind(console);
    gulp.watch(paths.appts, [/*'typescript',*/ 'tslint'])
        .on('error', onError);
    gulp.watch(paths.css.app, ['appcss'])
        .on('error', onError);
});
