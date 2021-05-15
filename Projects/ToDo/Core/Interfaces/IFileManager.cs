using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.FileManager
{
    public interface IFileManager
    {
        string SaveImage(IFormFile file);
    }
}
