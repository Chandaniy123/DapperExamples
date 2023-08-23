namespace CQRsAndMEdiatorsEXample.Service
{
    public interface ITokenGenerateService
    {
        string GenerateToken(string userName, string userEmail, string userSecretKey, string userIssuer, string userAudience);
        
    }
}
