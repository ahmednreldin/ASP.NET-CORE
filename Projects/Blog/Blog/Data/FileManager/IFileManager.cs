using Microsoft.AspNetCore.Http;

namespace Blog.Data.FileManager
{
    public interface IFileManager
    {
        string SaveImage(IFormFile file);
    }
}
