using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Service;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Ocsp;
using MediatR;
using System.Data.Common;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using CQRsAndMEdiatorsEXample.Commands;

namespace CQRsAndMEdiatorsEXample.Repository
{
    public class QuizRepo:IQuizRepo
    {
        private readonly string _connectionString;

        private readonly IConfiguration _configuration;
        public QuizRepo(IConfiguration configuration)
        {
            
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<QuestionModel> AddQuestionAsync(QuestionModel question)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string getMaxQuizIdQuery = "select isNull(Max(id),0) 'maxQuizId' from Questions";
                var maxQuestionId = await dbConnection.ExecuteScalarAsync<int>(getMaxQuizIdQuery, new { QuizId = question.QuizId });
                if (maxQuestionId == 0)
                {
                    maxQuestionId = 1;
                }
                else
                {
                    // Increment the maximum quiz ID by one to get the new quiz ID
                    maxQuestionId += 1;
                }
                question.Id = maxQuestionId;
                string insertQuestionQuery = @"
                    INSERT INTO Questions (Id,QuizId, Question)
                    VALUES (@Id,@NewQuizId, @QuestionText)";

                int affectedRows = await dbConnection.ExecuteAsync(insertQuestionQuery, new { Id = question.Id, NewQuizId = question.QuizId, QuestionText = question.Question });
                if (affectedRows == 0)
                {
                    throw new Exception("Unable to craete Question");
                }
                else
                {
                    foreach (var ans in question.Answers)
                    {
                        string insertAnswerQuery = @"
                            INSERT INTO Answer (QuestionId, Text, IsCorrect)
                            VALUES (@QuestionId, @AnswerText, @IsCorrect)";

                        int affectedAnswerRows = await dbConnection.ExecuteAsync(insertAnswerQuery, new { QuestionId = maxQuestionId, AnswerText = ans.Text, IsCorrect = ans.IsCorrect });
                        if (affectedAnswerRows == 0)
                        {
                            throw new Exception("Unable to insert answer");
                        }
                    }
                }
                return question;

            }
        }
        public async Task<QuizModel> CreateQuizAsync(QuizModel quizDetails)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "INSERT INTO Quizs ( QuizName,NumberToWin) VALUES( @QuizName,@NumberToWin)";
                int count = await dbConnection.ExecuteAsync(sQuery, quizDetails);

                dbConnection.Close();
                return quizDetails;
                
            }
        }

     

        public async Task<int> DeleteQuizAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string deleteQuizQuery = "DELETE FROM Quizs WHERE Id = @Id";
                await dbConnection.ExecuteAsync(deleteQuizQuery, new { Id = id });
              
                string selectQuestionsQuery = "SELECT * FROM Questions WHERE QuizId = @QuizId";
                var questionList = await dbConnection.QueryAsync<QuestionModel>(selectQuestionsQuery, new { QuizId = id });
                foreach (var question in questionList)
                {
                   
                    string deleteAnswerQuery = "DELETE FROM Answer WHERE QuestionId = @QuestionId";
                    await dbConnection.ExecuteAsync(deleteAnswerQuery, new { QuestionId = question.Id });
                }

               
                string deleteQuestionsQuery = "DELETE FROM Questions WHERE QuizId = @QuizId";
                await dbConnection.ExecuteAsync(deleteQuestionsQuery, new { QuizId = id });

                return id;
            }
        }
        public async Task<QuizModel> GetQuizByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM Quizs WHERE Id = @Id";
               QuizModel  AllQuiz = (await dbConnection.QueryFirstOrDefaultAsync<QuizModel>(sQuery, new { Id = id }));
                dbConnection.Close();
                return AllQuiz;
            }
        }

        public async Task<List<GetQuizModel>> GetQuizListAsync()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                List<GetQuizModel> quizModels = new List<GetQuizModel>();
                string quizQuery = "select * from Quizs";
                var quizList = await dbConnection.QueryAsync<QuizModel>(quizQuery);
                foreach (var quizModel in quizList)
                {
                    GetQuizModel getQuizModel = new GetQuizModel();
                    getQuizModel.Id = quizModel.Id;
                    getQuizModel.QuizName = quizModel.QuizName;
                    string questionQuery = "select * from Questions where QuizId = @QuizId";
                    var questionList = await dbConnection.QueryAsync<QuestionModel>(questionQuery, new { QuizId = getQuizModel.Id });
                    if (questionList != null)
                    {
                        foreach (var question in questionList)
                        {

                            string answerQuery = "select * from Answer where QuestionId =@QuestionId ";
                            var answerList = await dbConnection.QueryAsync<AnswerModel>(answerQuery, new { QuestionId = question.Id });
                            question.Answers = answerList.ToList();
                        }
                        getQuizModel.Questions = questionList.ToList();

                    }
                    quizModels.Add(getQuizModel);
                };
                return quizModels;
            }
        }

        public async Task<int> StartTimeQuizAsync()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string updatesQuery = "UPDATE QuizTimers SET StartTime = @StartTime, EndTime = @EndTime WHERE QuizId = @QuizId";


                int count = await dbConnection.ExecuteAsync(updatesQuery);
                dbConnection.Close();
                if (count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }




        public async Task<int> UpdateAllQuizsAsync(object quizsDetails)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string updateQuery = @"
            UPDATE Quizs
            SET NumberToWin = @NumberToWin
            WHERE QuizId = @QuizId";
                

                int count = await dbConnection.ExecuteAsync(updateQuery, quizsDetails);
                dbConnection.Close();
                if (count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public async Task<int> UpdateQuizAsync(object quizDetails)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "UPDATE Quizs SET  QuizName = @QuizName,NumberToWin=@NumberToWin WHERE Id = @Id";
                int count = await dbConnection.ExecuteAsync(sQuery, quizDetails);
                dbConnection.Close();
                if (count > 0)
                {
                    return 1;
                }
                return 0;
            }
        }

       

        async Task<QuestionModel> IQuizRepo.GetByQuizIdAsync(int questionId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                var query = @"
            SELECT q.*, a.*
            FROM Questions q
            LEFT JOIN Answer a ON q.Id = a.QuestionId
            WHERE q.Id = @QuestionId";

                var questionDictionary = new Dictionary<int, QuestionModel>();
                var questionWithAnswers = await dbConnection.QueryAsync<QuestionModel, AnswerModel, QuestionModel>(
                    query,
                    (question, answer) =>
                    {
                        if (!questionDictionary.TryGetValue(question.Id, out var questionEntry))
                        {
                            questionEntry = question;
                            questionEntry.Answers = new List<AnswerModel>();
                            questionDictionary.Add(questionEntry.Id, questionEntry);
                        }
                        questionEntry.Answers.Add(answer);
                        return questionEntry;
                    },
                    new { QuestionId = questionId },
                    splitOn: "Id");

                return questionWithAnswers.FirstOrDefault();
               
               
            }
        }
    }
}
