using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionModel>
    {
        private readonly IQuizRepo _quizRepo;

        public GetQuestionByIdQueryHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        async Task<QuestionModel> IRequestHandler<GetQuestionByIdQuery, QuestionModel>.Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _quizRepo.GetByQuizIdAsync(request.QuestionId);
        }
    }
}
