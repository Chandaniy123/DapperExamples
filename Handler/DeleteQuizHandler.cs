using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Repository;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Handler
{
    public class DeleteQuizHandler : IRequestHandler<DeletequizCommand, int>
    {
        private readonly IQuizRepo _quizRepo;

        public DeleteQuizHandler(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }
        public async Task<int> Handle(DeletequizCommand request, CancellationToken cancellationToken)
        {
            return await _quizRepo.DeleteQuizAsync(request.Id);
        }
    }

}
