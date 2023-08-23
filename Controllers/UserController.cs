using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CQRsAndMEdiatorsEXample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpGet]
        [Route("Get")]
        // get all User

        public async Task<List<User>> GetUserListAsync()
        {
            var userinfo = await _mediator.Send(new GetUserListQueries());

            return userinfo;
        }
        [HttpGet("Id")]
        public async Task<User> GetUserByIdAsync(int Id)
        {
            var userGetById = await _mediator.Send(new GetUserByIdQueries() { Id = Id });

            return userGetById;
        }

        [HttpPost]
        [Route("AddUsers")]
        public async Task<User> AddUserAsync(User userDetails)
        {
            var studentDetail = await _mediator.Send(new CreateUserCommand(
               userDetails.UserName,
                userDetails.UserEmail,
                userDetails.Address,
                userDetails.Age,
                userDetails.Password,
                userDetails.PasswordHash,
                userDetails.Salt
               ));
            studentDetail.Password = "";
            return studentDetail;
        }




        [HttpPut]
        public async Task<int> UpdateUserAsync(User userDetails)
        {
            var isuserDetailUpdated = await _mediator.Send(new UpdateUserCommand(
               userDetails.Id,
              userDetails.UserName,
              userDetails.UserEmail,
              userDetails.Address,
               userDetails.Age,
                userDetails.Password,
                userDetails.PasswordHash,
                userDetails.Salt
               ));
            return isuserDetailUpdated;
        }
        [HttpDelete]
        public async Task<int> DeleteUsertAsync(int Id)
        {
            return await _mediator.Send(new DeleteUserCommand() { Id = Id });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<string> LoginUser(LoginUser loginUser)
        {


            string token = await _mediator.Send(new LoginUserCommand() { LoginUser = loginUser });
            return token;
        }

     



    }
}