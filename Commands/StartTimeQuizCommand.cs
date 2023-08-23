using MediatR;

namespace CQRsAndMEdiatorsEXample.Commands
{
    public class StartTimeQuizCommand : IRequest<int>
    {
        public int QuizId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
