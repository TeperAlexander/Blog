using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models.RequestModels
{
    public class PostRequest
    {
        [Required(ErrorMessage = "Title required")]
        [MinLength(3, ErrorMessage = "The title must contain at least 3 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description required")]
        [MinLength(3, ErrorMessage = "Description must contain at least 3 characters")]
        public string Description { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
    }
}
