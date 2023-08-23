using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class CreateUserCommand:IRequest<User>
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }



        public CreateUserCommand(string userName, string userEmail, string address, int age,string password, string passwordHash, string salt)
        {
            UserName =userName;
            UserEmail = userEmail;
            Address = address;
            Age = age;
            Password = password;
            PasswordHash = passwordHash;
            Salt = salt;
          
        }
    }

}
