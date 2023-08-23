using CQRsAndMEdiatorsEXample.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public LoginUser LoginUser { get; set; }
    }
}
