using System.Collections.Generic;

namespace Base.Mailing
{
    public interface IMailMessage<T> where T : class
    {
        string TemplateName { get; set; }

        ICollection<string> Recipients { get; set; }

        T MessageParameters { get; set; }
    }
}
