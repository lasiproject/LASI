/// <reference path="typings/gulp/gulp.d.ts" />
const gulp = require("gulp"),
    tslint = require('tslint'),
    gulpTslint = require('gulp-tslint');

var onError = console.error.bind(console);

const appts = './src/**/*.ts';

gulp.task('tslint', function () {
    return gulp.src(appts)
        .pipe(gulpTslint({
            emitError: true,
            tslint: tslint
        }))
        .pipe(gulpTslint.report('verbose'));
}); 

gulp.task('watch', function () {
    gulp.watch(appts, ['tslint'])
        .on('error', onError);
});

gulp.task('default', ['watch']);