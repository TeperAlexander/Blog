using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Post> Posts { get; set; }
        public string TimeZoneId { get; set; }
    }
}
