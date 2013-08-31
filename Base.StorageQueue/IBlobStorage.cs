using System.Web;

namespace Base.AzureStorage
{
    public interface IBlobStorage
    {
        string SaveImage(HttpPostedFileBase image, string folderName);

        string DeleteImage(string folderPath, string fileName);
    }
}
