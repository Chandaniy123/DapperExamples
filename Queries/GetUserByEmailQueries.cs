using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Queries
{
    public class GetUserByEmailQueries : IRequest<User>
    {
        public int UserEmail { get; set; }
    }
}
