namespace CQRsAndMEdiatorsEXample.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Question { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
