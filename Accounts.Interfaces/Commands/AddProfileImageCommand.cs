using System;

namespace Accounts.Interfaces.Commands
{
    public class AddProfileImageCommand
    {
        public readonly Guid UserId;

        public readonly string ImageUrl;

        public  readonly string FolderPath;

        public readonly string ImageName;

        public AddProfileImageCommand(Guid userId, string imageUrl, string folderPath = null, string imageName = null)
        {
            UserId = userId;
            ImageUrl = imageUrl;
            FolderPath = folderPath;
            ImageName = imageName;
        }
    }
}
