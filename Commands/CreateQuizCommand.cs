using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class CreateQuizCommand : IRequest<QuizModel>
    {
      
        public string QuizName { get; set; }

        public int NumberToWin { get; set; }
        public CreateQuizCommand(string quizName, int numberToWin)
        {

            QuizName = quizName;
            NumberToWin = numberToWin;
        }


    }
}