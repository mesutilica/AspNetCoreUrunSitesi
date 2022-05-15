using System.IO;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreUrunSitesi.Utils
{
    public class FileHelper
    {
        public static string FileLoader(IFormFile formFile, string filePath = "/Img/")
        {
            var fileName = "";

            if (formFile != null && formFile.Length > 0)
            {
                fileName = formFile.FileName;
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;
                using var stream = new FileStream(directory, FileMode.Create);
                formFile.CopyTo(stream);
            }

            return fileName;
        }
    }
}
