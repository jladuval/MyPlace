namespace Accounts.Interfaces.Commands.Profile
{
    using System;

    public class SelectProfileImageCommand
    {
        public Guid UserId { get; set; }

        public string ImageName { get; set; }

        public SelectProfileImageCommand(Guid userId, string imageName)
        {
            UserId = userId;

            ImageName = imageName;
        }
    }
}
