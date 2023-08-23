using CQRsAndMEdiatorsEXample.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public class PartcipentRepo : IPartcipentRepo
    {
        private readonly string _connectionString;

        private readonly IConfiguration _configuration;
        public PartcipentRepo(IConfiguration configuration)
        {

            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Participant> InsertParticipantAsync(Participant participant)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "INSERT INTO Participants (Name, Score) VALUES (@Name, @Score); SELECT CAST(SCOPE_IDENTITY() as int)";
                int count = await dbConnection.ExecuteAsync(sQuery, participant);

                dbConnection.Close();
                return participant;

            }
        }

        public async Task UpdateParticipantAsync(Participant participant)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string updateQuery = @"
            UPDATE Participants SET Name = @Name, Score = @Score WHERE ParticipantId = @ParticipantId";


                await dbConnection.ExecuteAsync(updateQuery, participant);
                dbConnection.Close();
              
              
               
            }
        }
    }
}
