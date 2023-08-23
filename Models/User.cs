namespace CQRsAndMEdiatorsEXample.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Password{ get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }


    }
}
