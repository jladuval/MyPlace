function CommentsVM(dinnerId) {
    var _this = {};

    _this.Comments = ko.observableArray();

    _this.loadDetails = function () {
        $.post('/Dinner/GetComments', { DinnerId: dinnerId })
            .done(function(response) {
                var comments = response;
                _.each(comments, function(comment) {
                    _this.Comments.push(Comment(comment));
                });
            });
    };

    _this.NewCommentVisible = ko.observable(false);

    _this.ToggleNewComment = function () {
        _this.NewComment('');
        _this.NewCommentVisible(!_this.NewCommentVisible());
    };

    _this.NewComment = ko.observable(null);

    _this.Save = function () {
        var text = _this.NewComment();
        _this.ToggleNewComment();
        $.post('/Dinner/SaveNewComment', { DinnerId: dinnerId, Text: text })
            .done(function() {
                _this.Comments.push(Comment({
                    Name: "You",
                    CreatedDate: "Now",
                    Text: text
                }));
            });
    };

    _this.loadDetails();

    return _this;
}

function Comment(commentData) {
    var _this = {};

    _this.Name = ko.observable(commentData.Name);

    _this.Text = ko.observable(commentData.Text);

    _this.CreatedDate = ko.observable(commentData.CreatedDate);

    _this.ProfileImageUrl = ko.observable(commentData.ProfileImageUrl);

    return _this;
}
