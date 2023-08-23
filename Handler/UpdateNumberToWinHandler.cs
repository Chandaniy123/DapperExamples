using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class UpdateNumberToWinHandler : IRequestHandler<UpdateNumberToWinCommand, int>
    {

        private readonly IQuizRepo _quizRepo;

        public UpdateNumberToWinHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }

        async Task<int> IRequestHandler<UpdateNumberToWinCommand, int>.Handle(UpdateNumberToWinCommand request, CancellationToken cancellationToken)
        {
           var quizsDetails = await _quizRepo.GetQuizByIdAsync(request.QuizId);
            if (quizsDetails == null)
                return default;


            quizsDetails.NumberToWin = request.NumberToWin;


            return await _quizRepo.UpdateAllQuizsAsync(quizsDetails);
        }
    }
}
