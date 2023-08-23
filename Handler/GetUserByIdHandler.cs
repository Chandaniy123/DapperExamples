using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class GetUserByIdHandler: IRequestHandler<GetUserByIdQueries, User>
    {
        private readonly IUserRepo _userRepo;

        public GetUserByIdHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> Handle(GetUserByIdQueries query, CancellationToken cancellationToken)
        {
            return await _userRepo.GetUserByIdAsync(query.Id);
        }
    }

}
