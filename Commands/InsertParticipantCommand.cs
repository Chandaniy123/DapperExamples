using MediatR;
using CQRsAndMEdiatorsEXample.Models;

namespace CQRsAndMEdiatorsEXample.features.Command
{
    public class InsertParticipantCommand : IRequest<Participant>
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
