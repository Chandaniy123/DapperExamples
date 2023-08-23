using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public interface IQuizRepo
    {
        Task<QuestionModel> AddQuestionAsync(QuestionModel question);

        Task<QuizModel> CreateQuizAsync(QuizModel quizDetails);
       
        Task<int> DeleteQuizAsync(int id);
       
        Task<QuestionModel> GetByQuizIdAsync(int questionId);
        Task<QuizModel> GetQuizByIdAsync(int id);
        Task<List<GetQuizModel>> GetQuizListAsync();
   
        Task<int> StartTimeQuizAsync();
        public  Task<int> UpdateAllQuizsAsync(object quizsDetails);
        public Task<int> UpdateQuizAsync(object quizDetails);
    }
}
