using Core.Interfaces.FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration configuration)
        {
            _imagePath = configuration["Path:Images"];
        }

        public string SaveImage(IFormFile file)
        {
            if(!Directory.Exists(_imagePath))
            {
                // if path dosen't exist => Create one 
                Directory.CreateDirectory(_imagePath);
            }

            // Get image Name
            string fileName = file.FileName;

            // Copy image to storage 
            var filestream = new FileStream(Path.Combine(_imagePath, fileName),
                                        FileMode.Create);

            file.CopyTo(filestream);

            // return imageName
            return fileName;
        }
    }
}
