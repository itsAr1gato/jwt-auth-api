using System.ComponentModel.DataAnnotations;

namespace Webproj.Models.Requests
{
    public class SignUpRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string nickname { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string passwordConfirmation { get; set; }
    }
}