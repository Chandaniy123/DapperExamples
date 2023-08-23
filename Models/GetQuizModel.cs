namespace CQRsAndMEdiatorsEXample.Models
{
    public class GetQuizModel
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public int NumberToWin { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
