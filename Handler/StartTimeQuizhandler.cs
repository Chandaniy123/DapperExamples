using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class StartTimeQuizhandler : IRequestHandler<StartTimeQuizCommand,int>
    {
        private readonly IQuizRepo _quizRepo;

        public StartTimeQuizhandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        public async Task<int> Handle(StartTimeQuizCommand request, CancellationToken cancellationToken)
        {
         


          

            return await _quizRepo.StartTimeQuizAsync();
        }
    }
}
