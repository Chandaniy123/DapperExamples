using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepo _userRepo;

        public UpdateUserHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var userDetails = await _userRepo.GetUserByIdAsync(command.Id);
            if (userDetails == null)
                return default;

            userDetails.UserName = command.UserName;
            userDetails.UserEmail = command.UserEmail;
            userDetails.Address = command.Address;
            userDetails.Age = command.Age;
            userDetails.Password= command.Password;
            userDetails.PasswordHash= command.PasswordHash;
            userDetails.Salt= command.Salt;

            return await _userRepo.UpdateUserAsync(userDetails);
        }
    }

}
