﻿namespace Accounts.Interfaces.Presentation.Comments
{
    using System;

    public class CommentDto
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
