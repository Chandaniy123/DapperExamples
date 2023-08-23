using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, QuestionModel>
    {
        private readonly IQuizRepo _quizRepo;

        public AddQuestionHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        public Task<QuestionModel> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            return _quizRepo.AddQuestionAsync(request.Question);
        }
    }
}
