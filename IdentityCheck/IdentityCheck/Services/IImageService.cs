using IdentityCheck.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services
{
    public interface IImageService
    {
        Task<List<ImageDetails>> GetImageListAsync(long postId);
        Task<List<IFormFile>> UploadImagesAsync(List<IFormFile> files, long postId);
        Task DeleteFileAsync(string fileName);
        Task DeleteAllFileAsync(long postId);
    }
}
