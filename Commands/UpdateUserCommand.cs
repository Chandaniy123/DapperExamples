using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class UpdateUserCommand:IRequest<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }


        public UpdateUserCommand(int id, string userName, string userEmail, string address, int age,string password, string passwordHash, string salt)
        {
            Id = id;
            UserName = userName;
            UserEmail = userEmail;
            Address = address;
            Age = age;
          Password= password;
            PasswordHash = passwordHash;
            Salt = salt;
        }
    }
   
}
