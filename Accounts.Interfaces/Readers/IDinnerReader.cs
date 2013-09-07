namespace Accounts.Interfaces.Readers
{
    using System;
    using Presentation.Dinner;

    public interface IDinnerReader
    {
        DinnerListDto GetDinnerList(double lat, double lng, int skip, int take);
        DinnerDto GetDinner(Guid id, Guid userId);
        DinnerConfirmDto DinnerCanBeConfirmedByPartner(string token);
    }
}
