using MediatR;
using CQRsAndMEdiatorsEXample.Models;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public interface IPartcipentRepo
    {
        Task<Participant> InsertParticipantAsync(Participant participant);
     
        Task UpdateParticipantAsync(Participant participant);
    }
}
