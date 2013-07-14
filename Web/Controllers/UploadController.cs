using System.Web.Mvc;

namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class UploadController : Controller
    {
        private const string TempPath = @"C:\Temp";

        [HttpPost]
        public ActionResult Images(HttpPostedFileBase file)
        {

            var filePath = Path.Combine(TempPath, file.FileName);
            System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}
