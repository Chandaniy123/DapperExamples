using System.ComponentModel.DataAnnotations;

namespace CQRsAndMEdiatorsEXample.Models
{
    public class LoginUser
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public static implicit operator string(LoginUser v)
        {
            throw new NotImplementedException();
        }
    }
}
