namespace Accounts.Interfaces.Commands.Profile
{
    using System;

    public class DeleteProfileImageCommand
    {

        public readonly Guid UserId;

        public readonly string ImageUrl;

        public readonly string FolderPath;

        public readonly string ImageName;

        public DeleteProfileImageCommand(Guid userId, string imageUrl, string folderPath = null, string imageName = null)
        {
            UserId = userId;
            ImageUrl = imageUrl;
            FolderPath = folderPath;
            ImageName = imageName;
        }
    }
}

