using System;

namespace Accounts.Interfaces.Commands
{
    public class SetProfileImage
    {
        public readonly Guid UserId;

        public readonly string ImageUrl;

        public SetProfileImage(Guid userId, string imageUrl)
        {
            UserId = userId;
            ImageUrl = imageUrl;
        }
    }
}
