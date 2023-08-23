using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class GetUserListHandler :IRequestHandler<GetUserListQueries, List<User>>
    {
        private readonly IUserRepo _userRepo;

    public GetUserListHandler(IUserRepo userRepo)
    {
            _userRepo = userRepo;
    }
    public async Task<List<User>> Handle(GetUserListQueries request, CancellationToken cancellationToken)
    {
            return await _userRepo.GetUserListAsync();
        }
   }
}





