using Base.DDD.Domain;

namespace Accounts.Domain
{
    public class Image : Entity
    {
        public string Url { get; set; }

        public string FolderPath { get; set; }

        public string ImageName { get; set; }

        private Image()
        {
        }

        public Image(string url, string folderPath = null, string imageName = null)
        {
            Url = url;
            FolderPath = folderPath;
            ImageName = imageName;
        }
    }
}
