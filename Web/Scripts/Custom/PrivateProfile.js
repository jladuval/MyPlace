SelectLocation.init();
imageUpload("@Url.Action("ProfileImage", "Upload")",
            "#dropZone",
            "@Url.Action("RemoveProfileImage", "Upload")",
function(element) {
    var fileName = $(element.target).attr('alt');
    $.post("@Url.Action("SelectImage", "Profile")", {fileName : fileName})
.done(function(response) {
    $('#profileImage').html('<img src="' + response + '" alt=' + fileName + '/>');
});
});
$('.dz-remove-razor').click(function() {

});