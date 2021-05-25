using System;

namespace Webproj.Models.Responses
{
    public class SignInResponse
    {
        public SignInResponse(User user, string jwtToken)
        {
            _id = user.Id;
            email = user.Email;
            nickname = user.Nickname;
            created_at = user.CreatedAt;
            token = jwtToken;
        }

        public string _id { get; set; }

        public string email { get; set; }

        public string nickname { get; set; }

        public string token { get; set; }

        public DateTime created_at { get; set; }
    }
}