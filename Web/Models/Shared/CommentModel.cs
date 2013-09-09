namespace Web.Models.Shared
{
    using System;

    public class CommentModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public string CreatedDate { get; set; }

        public string Name { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}