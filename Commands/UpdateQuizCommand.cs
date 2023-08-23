using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    internal class UpdateQuizCommand : IRequest<int>
    {
        public UpdateQuizCommand(int id, string quizName)
        {
            Id = id;
            QuizName = quizName;
        }

        public int Id { get; set; }
        public string QuizName { get; set; }
    }
}