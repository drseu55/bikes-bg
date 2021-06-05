using bikes_bg.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.Utils
{
    public class FileUpload
    {
        public static string ProcessUploadedFile(IFormFile photo, IHostingEnvironment hostingEnvironment, 
            string folder)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, folder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
