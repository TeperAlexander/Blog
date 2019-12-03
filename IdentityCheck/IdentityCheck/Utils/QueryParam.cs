using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Utils
{
    public class QueryParams
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int Page { get; set; } = 1;
    }
}
