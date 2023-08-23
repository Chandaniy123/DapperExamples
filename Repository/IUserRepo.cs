using CQRsAndMEdiatorsEXample.Models;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public interface IUserRepo
    {
      public Task<User> AddUserAsync(User userDetails);
        
        public Task<int> DeleteUserAsync(int id);
        Task<User> GetUserByEmail(string userEmail);
        public  Task<User> GetUserByIdAsync(int id);
      Task<List<User>> GetUserListAsync();

        Task<string> LogInUserAsync(LoginUser loginUser);
        public Task<int> UpdateUserAsync(object userDetails);
    }
}
