using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class AddQuestionCommand : IRequest<QuestionModel>
    {
        public QuestionModel Question { get; set; }
 
    }
}