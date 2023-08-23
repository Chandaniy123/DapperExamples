using MediatR;

using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;
using CQRsAndMEdiatorsEXample.features.Command;

namespace CQRsAndMEdiatorsEXample.features.Handler
{
    public class UpdateParticipanthandler : IRequestHandler<UpdateParticipantCommand>
    {
        private readonly IPartcipentRepo _partcipentRepo;

        public UpdateParticipanthandler(IPartcipentRepo partcipentRepo)
        {
            _partcipentRepo = partcipentRepo;
        }

        

        public async Task<Unit> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            var participant = new Participant { ParticipantId = request.ParticipantId, Name = request.Name, Score = request.Score };
            await _partcipentRepo.UpdateParticipantAsync(participant);
            return Unit.Value;
        }
    }
}
