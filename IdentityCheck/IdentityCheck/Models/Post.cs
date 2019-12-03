using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace IdentityCheck.Models
{
    public class Post
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
