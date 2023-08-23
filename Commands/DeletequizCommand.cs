using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class DeletequizCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
