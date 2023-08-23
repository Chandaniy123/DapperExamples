using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class GetQuizByIdHandler : IRequestHandler<GetquizByIdQueries, QuizModel>
    {
        private readonly IQuizRepo _quizRepo;

        public GetQuizByIdHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        async Task<QuizModel> IRequestHandler<GetquizByIdQueries, QuizModel>.Handle(GetquizByIdQueries request, CancellationToken cancellationToken)
        {
            return await _quizRepo.GetQuizByIdAsync(request.Id);
        }
    }


}
