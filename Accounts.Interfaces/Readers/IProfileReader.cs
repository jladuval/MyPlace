﻿namespace Accounts.Interfaces.Readers
{
    using System;
    using Presentation;
    using Presentation.Profile;

    public interface IProfileReader
    {
        string GetImageUrl(Guid userId, string fileName);

        PrivateProfileDto GetPrivateProfile(Guid userId);

        LatLngDto GetLatLong(Guid userId);

        string GetLocationString(Guid userId);

        PublicProfileDto GetPublicProfile(Guid id);
    }
}
