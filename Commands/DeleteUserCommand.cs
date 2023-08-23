using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class DeleteUserCommand:IRequest<int>
    {
        public int Id { get; set; }
    }

}
