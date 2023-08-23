using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class UpdateQuizHandler : IRequestHandler<UpdateQuizCommand, int>
    {
        private readonly IQuizRepo _quizRepo;

        public UpdateQuizHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }

        
     
     

        async Task<int> IRequestHandler<UpdateQuizCommand, int>.Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            var quizDetails = await _quizRepo.GetQuizByIdAsync(request.Id);
            if (quizDetails == null)
                return default;

           
            quizDetails.QuizName = request.QuizName;


            return await _quizRepo.UpdateQuizAsync(quizDetails);
        }
    }
}
