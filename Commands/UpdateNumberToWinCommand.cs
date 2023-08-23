using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    internal class UpdateNumberToWinCommand : IRequest<int>
    {
       public int QuizId { get; set; }  
      public int NumberToWin { get; set; }

        public UpdateNumberToWinCommand(int quizId, int numberToWin)
        {
           QuizId = quizId;
            NumberToWin = numberToWin;
        }
    }
}