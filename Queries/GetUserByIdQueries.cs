using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Queries
{
    public class GetUserByIdQueries: IRequest<User>
    {
        public int Id { get; set; }
    }
}
 