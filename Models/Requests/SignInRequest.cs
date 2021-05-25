using System.ComponentModel.DataAnnotations;

namespace Webproj.Models.Requests
{
    public class SignInRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}