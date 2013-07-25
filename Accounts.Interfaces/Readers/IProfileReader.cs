namespace Accounts.Interfaces.Readers
{
    using System;

    public interface IProfileReader
    {
        string GetImageUrl(Guid userId, string fileName);
    }
}
