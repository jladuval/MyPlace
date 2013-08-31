var imageUpload = function(url, dropzoneId, removeUrl, onImageClick, onDelete) {
    var _this = {};
    $(function() {
        $(dropzoneId).dropzone(
            {
                url: url,
                addRemoveLinks: true,
                init: function () {
                    this.on("removedfile", function(file) {
                        var dataUrl = $(file.previewElement).find('.dz-details .dz-dataUrl').html();
                        onDelete(file.name);
                    });
                    this.on("success", function (file, dataUrl) {
                        $(file.previewElement).find('.dz-details .dz-dataUrl').html(dataUrl);
                    });
                }
            });
        $(document).on("click", ".dz-details", onImageClick);
    });
    return _this;
}