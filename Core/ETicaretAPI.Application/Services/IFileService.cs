using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        //  Task<List<(string fileName, string path)>> UploadAsync(string path,IFormFileCollection files);
        //Task<string> FileRenameAndFilterAsync(string fileName,IFormFile file);
        Task<bool> UploadAsync(string path, IFormFileCollection filess);
      //  Task<bool> CopyFileAsync(string path,IFormFile file);
        Task<bool> BuKalsorYoluYoksaOlustur(string path);
    }
}
