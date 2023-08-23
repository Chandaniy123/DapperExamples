using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class GetquizListHandler : IRequestHandler<GetQuizListQueries, List<GetQuizModel>>
    {
        private readonly IQuizRepo _quizRepo;

        public GetquizListHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }

     async Task<List<GetQuizModel>> IRequestHandler<GetQuizListQueries, List<GetQuizModel>>.Handle(GetQuizListQueries request, CancellationToken cancellationToken)
        {
            return await _quizRepo.GetQuizListAsync();
        }
    }

}
