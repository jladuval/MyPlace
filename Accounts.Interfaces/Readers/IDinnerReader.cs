namespace Accounts.Interfaces.Readers
{
    using Presentation.Dinner;

    public interface IDinnerReader
    {
        DinnerListDto GetDinnerList(double lat, double lng);
    }
}
