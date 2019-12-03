using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models.ViewModels
{
    public class ImageViewModel
    {
        public long PostId { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> WrongFiles { get; set; } = new List<IFormFile>();
    }
}
