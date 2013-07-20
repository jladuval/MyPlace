var imageUpload = function(url, dropzoneId, removeUrl, onImageClick) {
    var _this = {};
    $(function() {
        $(dropzoneId).dropzone(
            {
                url: url,
                addRemoveLinks: true,
                init: function () {
                    this.on("removedfile", function(file) {
                         alert(file);
                    });
                    this.on("success", function (file, dataUrl) {
                        alert(dataUrl);
                    });
                }
            });
        $(document).on("click", ".dz-details", onImageClick)
    });
    return _this;
}