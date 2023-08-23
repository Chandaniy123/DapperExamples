using CQRsAndMEdiatorsEXample.Models;
using MediatR;
namespace CQRsAndMEdiatorsEXample.Queries
{
    public class GetUserListQueries : IRequest<List<User>>
    {
    }
}
