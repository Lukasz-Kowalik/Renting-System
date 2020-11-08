const { series, src, dest, parallel, watch, task } = require('gulp');
const sass = require('gulp-sass');
const clean = require('gulp-clean-css');
const concat = require('gulp-concat');
const del = require("del");

const fontDir = './node_modules/@fortawesome/fontawesome-free/'
const destDir = './wwwroot';
const sassFiles = ['./src/scss/lib/*'];
const theme = './src/scss/main/**/*.scss'
const minjs = [
    "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js",
    "./node_modules/datatables/media/js/*.min.js"
];

function jq() {
    return src("./node_modules/jquery/dist/jquery.min.js")
        .pipe(dest(destDir + "/js"));
}
function libs() {
    return src(minjs)
        .pipe(dest(destDir + "/js"));
}

function copyFonts() {
    return src(fontDir + "webfonts/*")
        .pipe(dest(destDir + "/webfonts"));
}

function Styles() {
    return src(theme)
        .pipe(sass())
        .pipe(concat("style.css"))
        .pipe(clean())
        .pipe(dest(destDir + "/css"));
}

function scss() {
    return src(sassFiles)
        .pipe(sass())
        .pipe(concat("libs.css"))
        .pipe(clean())
        .pipe(dest(destDir + "/css"));
}

function svg() {
    src(fontDir + "svgs/brands/*").pipe(dest(destDir + "/svgs/brands/"));
    src(fontDir + "svgs/regular/*").pipe(dest(destDir + "/svgs/regular/"));
    src(fontDir + "svgs/solid/*").pipe(dest(destDir + "/svgs/solid/"));
    src(fontDir + "sprites/*").pipe(dest(destDir + "/sprites"));
    return src("./src/favicon.*")
        .pipe(dest(destDir));
}

exports.default = series(jq,
    parallel(copyFonts, scss, libs, Styles, svg));

function deleteFolserCss() {
    return del("./wwwroot/css/");
}

task('watch', function () {
    watch('./src/scss', series(deleteFolserCss, scss, Styles));
});