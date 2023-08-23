using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserRepo _userRepo;

        public DeleteUserHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepo.DeleteUserAsync(request.Id);
        }
    }


}
