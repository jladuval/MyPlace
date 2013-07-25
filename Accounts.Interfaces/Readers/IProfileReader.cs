namespace Accounts.Interfaces.Readers
{
    using System;
    using Presentation.Profile;

    public interface IProfileReader
    {
        string GetImageUrl(Guid userId, string fileName);
        PrivateProfileDto GetPrivateProfile(Guid userId);
    }
}
