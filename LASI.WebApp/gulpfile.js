/// <binding ProjectOpened='watch' />
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
    appcss: 'wwwroot/css/**/*.css',
    appcssDest: 'wwwroot/dist/app.min.css',
    libcss: 'wwwroot/lib/**/*.css',
    libcssDest: 'wwwroot/dist/lib/lib.min.css'
};
gulp.task('typescript', function () {
    return gulp.src(paths.appts)
        .pipe(sourcemaps.init())
        .pipe(typescript({
            noImplicitAny: false,
            noEmitOnError: false,
            removeComments: false,
            target: 'es5',
            watch: false,
            module:'amd',
        }))
        .pipe(concat('app.js'))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/dist/app'));
});
gulp.task('appcss', function () {
    return gulp.src(paths.appcss)
        .pipe(sourcemaps.init())
        .pipe(concat('app.min.css'))
        .pipe(minifyCss())
        .pipe(sourcemaps.write('/'))
        .pipe(gulp.dest('wwwroot/dist'));
});

gulp.task('watch', function () {
    var onError = console.error.bind(console);
    gulp.watch(paths.appts, ['typescript'])
        .on('error', onError);
    gulp.watch(paths.appcss, ['appcss'])
        .on('error', onError);
});