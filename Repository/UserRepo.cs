using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Service;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly string _connectionString;
     
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerateService _tokenGenerator;
        public UserRepo(IConfiguration configuration,ITokenGenerateService tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "DELETE FROM UserData WHERE Id = @Id";
                int count = await dbConnection.ExecuteAsync(sQuery, new { Id = id });

                dbConnection.Close();
                if (count > 0)
                {
                    return 1;
                }
                return 0;

            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM UserData WHERE Id = @Id";
                User AllEmployee = (await dbConnection.QueryFirstOrDefaultAsync<User>(sQuery, new { Id = id }));
                dbConnection.Close();
                return AllEmployee;
            }
        }
        public async Task<User> GetUserByEmail(string userEmail)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM UserData WHERE UserEmail =@UserEmail";
                User AllUsers = (await dbConnection.QueryFirstOrDefaultAsync<User>(sQuery, new { UserEmail = userEmail}));
                dbConnection.Close();
                return AllUsers;
            }
               
        }
        public async Task<string> LogInUserAsync(LoginUser loginUser)
        {
            string token = "";

           
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM UserData WHERE UserEmail =@UserEmail";
                dbConnection.Open();
                User user = await dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { UserEmail = loginUser.UserEmail });
                if (user != null)
                {
                    bool checkPassword =VerifyPassword(loginUser.Password, user.PasswordHash, user.Salt);
                    if (checkPassword)
                    {
                        var userSecretKey = _configuration["JwtValidationDetails:UserAppSecretKey"];
                        var userIssuer = _configuration["JwtValidationDetails:UserIssuer"];
                        var userAudience = _configuration["JwtValidationDetails:UserAudience"];

                        token = _tokenGenerator.GenerateToken(user.UserName , user.UserEmail, userSecretKey, userIssuer, userAudience);
                        if (token != null)
                        {
                            return token;
                        }
                        else
                        {
                            token = string.Empty;
                        }

                    }
                    else
                    {
                        throw new Exception("Username or Password Incorret");
                    }
                }
                else
                {
                    throw new Exception("User Not Foundn or Deactive");
                }
            }
            return token;
        }

      

        private bool VerifyPassword(string password, string passwordHash, string salt)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            byte[] saltBytes = Convert.FromBase64String(salt);
            var sha256 = SHA256.Create();
            byte[] combinedBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] newHashBytes = sha256.ComputeHash(combinedBytes);
                
            
            

            // Compare the generated hash with the stored hash
            return SlowEquals(hashBytes, newHashBytes);
        }
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        public async Task<int> UpdateUserAsync(object userDetails)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "UPDATE UserData SET UserName = @UserName, UserEmail = @UserEmail, Address = @Address, Age = @Age,PasswordHash =@PasswordHash,Salt=@salt WHERE Id = @Id";
                int count = await dbConnection.ExecuteAsync(sQuery, userDetails);
                dbConnection.Close();
                if (count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public async Task<User> AddUserAsync(User userDetails)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {

                userDetails.Salt = GenerateSalt();
                userDetails.PasswordHash = HashPassword(userDetails.PasswordHash, userDetails.Salt);


                dbConnection.Open();
                string sQuery = "INSERT INTO  UserData (UserName, UserEmail, Address, Age,PasswordHash, Salt) VALUES(@UserName, @UserEmail, @Address, @Age,@PasswordHash, @Salt)";
                int count = await dbConnection.ExecuteAsync(sQuery, userDetails);
                dbConnection.Close();
                return userDetails;
                
            }
        }

        private string HashPassword(string passwordHash, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] combinedBytes = Encoding.UTF8.GetBytes(passwordHash + salt);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
            throw new NotImplementedException();
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

     

        public async Task<List<User>> GetUserListAsync()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM UserData";
                List<User> AllEmployee = (await dbConnection.QueryAsync<User>(sQuery)).ToList();
                dbConnection.Close();
                return AllEmployee;
            }
        }

       
    }
}
