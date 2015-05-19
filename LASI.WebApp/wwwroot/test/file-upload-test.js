$(function () {
    var testData = {
        singletonTextFileSet: null,
        singleTextFile: null
    };
    var postData = function (formData) {
        return $.ajax({
            type: "POST",
            url: "/Documents/Upload",
            'Content-Type': 'multipart/form-data; charset=UTF-8; type="text/plain"',
            processData: false,
            data: formData
        }).success(function (s) {
            alert(s);
        });
    };
});