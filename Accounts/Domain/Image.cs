using Base.DDD.Domain;

namespace Accounts.Domain
{
    public class Image : Entity
    {
        public string Url { get; set; }

        public string FolderPath { get; set; }

        public string ImageName { get; set; }

        public Image(string url)
        {
            Url = url;
        }
    }
}
