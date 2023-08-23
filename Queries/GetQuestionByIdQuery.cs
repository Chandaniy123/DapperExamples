using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Queries
{
    internal class GetQuestionByIdQuery : IRequest<QuestionModel>
    {
        public int QuestionId { get; set; }
    
    }
}