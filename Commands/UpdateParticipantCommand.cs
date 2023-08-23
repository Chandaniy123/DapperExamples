using MediatR;
using CQRsAndMEdiatorsEXample.Models;

namespace CQRsAndMEdiatorsEXample.features.Command
{
    public class UpdateParticipantCommand: IRequest
    {
        public int ParticipantId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
