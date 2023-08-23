using MediatR;
using CQRsAndMEdiatorsEXample.features.Command;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;

namespace CQRsAndMEdiatorsEXample.features.Handler
{
    public class InsertParticipantHandler : IRequestHandler<InsertParticipantCommand, Participant>
    {
        private readonly IPartcipentRepo _partcipentRepo;

        public InsertParticipantHandler(IPartcipentRepo partcipentRepo)
        {
            _partcipentRepo = partcipentRepo;
        }

        public async Task<Participant> Handle(InsertParticipantCommand request, CancellationToken cancellationToken)
        {
            var participant = new Participant { Name = request.Name, Score = request.Score };
            var insertedParticipant = await _partcipentRepo.InsertParticipantAsync(participant);
            return insertedParticipant;
        }
    }
}
