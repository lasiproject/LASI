/// <binding />
var gulp = require('gulp'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    sourcemaps = require('gulp-sourcemaps'),
    minifyCss = require('gulp-minify-css'),
    typescript = require('gulp-typescript'),
    tslint = require('gulp-tslint');

var paths = {
    appts: [
        'wwwroot/app/utilities/augmentations.ts',
        'wwwroot/app/LASI.ts',
        'wwwroot/app/**/*.module.ts',
        'wwwroot/app/**/*.ts',
        'wwwroot/main.ts'
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

gulp.task('typescript', function () {
    return gulp
        .src(paths.appts)
        .pipe(sourcemaps.init())
        .pipe(typescript({
            noImplicitAny: false,
            noEmitOnError: false,
            removeComments: false,
            target: 'es5',
            watch: false,
            module: 'amd'
        }))
        .pipe(sourcemaps.write());
});

gulp.task('appcss', function () {
    return gulp
        .src(paths.css.app.src)
        .pipe(sourcemaps.init())
        .pipe(concat(paths.css.app.dest.name))
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest(paths.css.app.dest.dir))
        .pipe(minifyCss())
        .pipe(rename('app.min.css'))
        .pipe(gulp.dest(paths.css.app.dest.dir));
});

gulp.task('tslint', function () {
    return gulp
        .src(paths.appts)
        .pipe(tslint(require('./wwwroot/tslint.json')))
        .pipe(tslint.report('verbose'));
});

gulp.task('watch', function () {
    var onError = console.error.bind(console);
    gulp.watch(paths.appts, ['typescript', 'tslint'])
        .on('error', onError);
    gulp.watch(paths.appcss, ['appcss'])
        .on('error', onError);
});