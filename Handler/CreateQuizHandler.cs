using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class CreateQuizHandler : IRequestHandler<CreateQuizCommand, QuizModel>
    {
        private readonly IQuizRepo _quizRepo;

        public CreateQuizHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        public async Task<QuizModel> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var quizDetails = new  QuizModel()
            {
               
               QuizName = request.QuizName,
                NumberToWin = request.NumberToWin,

            };

            return await _quizRepo.CreateQuizAsync(quizDetails);
        }
    }
}
