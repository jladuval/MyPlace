var imageUpload = function(url, dropzoneId, resultId) {
    var _this = {};
    $(function () {
        $(dropzoneId).filedrop({
            url: url,
            paramname: 'files',
            maxFiles: 5,
            maxfilesize: 30,
            dragOver: function () {
                $(dropzoneId).css('background', 'blue');
            },
            dragLeave: function () {
                $(dropzoneId).css('background', 'gray');
            },
            drop: function () {
                $(dropzoneId).css('background', 'gray');
            },
            afterAll: function () {
                $(dropzoneId).html('The file(s) have been uploaded successfully!');
            },
            uploadFinished: function (i, file, response, time) {
                $(resultId).append('<li>' + file.name + '</li>');
            }
        });
    });

    return _this;
}