using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class UserLoginHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepo _userRepository;
        public UserLoginHandler(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }
        Task<string> IRequestHandler<LoginUserCommand, string>.Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.LogInUserAsync(request.LoginUser);
        }
    }

}
