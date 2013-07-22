using System.Web;

namespace Base.AzureStorage
{
    public interface IBlobStorage
    {
        string SaveImage(HttpPostedFileBase image, string folderName);
    }
}
