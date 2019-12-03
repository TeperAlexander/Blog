using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models.ViewModels
{
    public class PostViewModel
    {
        public List<ImageDetails> Images { get; set; }
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
