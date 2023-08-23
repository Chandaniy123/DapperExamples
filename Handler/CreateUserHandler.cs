using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;

using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepo _userRepo;

        public CreateUserHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userDetails = new User()
            {
               UserName = command.UserName,
                UserEmail = command.UserEmail,
                Address = command.Address,
                Age = command.Age,
                Password= command.Password,
                PasswordHash= command.PasswordHash,
                Salt= command.Salt
            };

            return await _userRepo.AddUserAsync(userDetails);
        }
    }
}
