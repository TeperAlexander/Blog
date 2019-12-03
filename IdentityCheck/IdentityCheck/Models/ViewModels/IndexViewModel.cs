
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCheck.Utils;

namespace IdentityCheck.Models.ViewModels
{
    public class IndexViewModel
    {
        public ApplicationUser AppUser { get; set; }
        public PagingList<Post> PagingList{ get; set; }
        public string ActionName { get; set; }
        public QueryParams QueryParams { get; set; }


    }
}
