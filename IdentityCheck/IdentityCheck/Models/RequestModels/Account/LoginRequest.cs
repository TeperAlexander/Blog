using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Models.RequestModels.Account
{
    public class LoginRequest
    {

        [Required(ErrorMessage ="The Email field is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<string> ErrorMessages { get; set; } = new List<string>();

    }
}
