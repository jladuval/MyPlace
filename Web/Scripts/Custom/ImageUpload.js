var imageUpload = function(url, dropzoneId, resultId) {
    var _this = {};
    $(function() {
        $(dropzoneId).dropzone({ url: url });
    });
    return _this;
}