using CQRsAndMEdiatorsEXample.Commands;
using CQRsAndMEdiatorsEXample.Models;
using CQRsAndMEdiatorsEXample.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRsAndMEdiatorsEXample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;
       

        public QuizController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetQuiz")]
  
        public async Task<List<GetQuizModel>> GetQuizListAsync()
        {
          
            var quizinfo = await _mediator.Send(new GetQuizListQueries());

            return quizinfo;
        }

        [HttpPost]
        [Route("AddQuiz")]
        public async Task<QuizModel> CreateQuizAsync(QuizModel quizDetails)
        {
            var quizDetail = await _mediator.Send(new CreateQuizCommand(
             
                quizDetails.QuizName,
                quizDetails.NumberToWin
                
               
               ));
           
            return quizDetail;
        }

        [HttpPut]
        [Route("UpdateQuiz")]
        public async Task<int> UpdateQuizAsync(QuizModel quizDetails)
        {
            var isuserDetailUpdated = await _mediator.Send(new UpdateQuizCommand(
               quizDetails.Id,
              quizDetails.QuizName
              
               ));
            return isuserDetailUpdated;
        }


        [HttpGet]
        [Route("GetQuizById")]
        public async Task<QuizModel> GetQuizByIdAsync(int Id)
        {
            var quizGetById = await _mediator.Send(new GetquizByIdQueries() { Id = Id });

            return quizGetById;
        }
        [HttpGet]
        [Route("GetQuestionById/{QuestionId}")]
        public async Task<QuestionModel> GetQuizsByQuizId(int QuestionId)
        {

            var quizGetById = await _mediator.Send(new GetQuestionByIdQuery() { QuestionId = QuestionId });

            return (QuestionModel)quizGetById;


        }

        [HttpPost("StartQuiz/{quizId}")]
        public async Task<IActionResult> StartQuiz(int quizId)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddMinutes(30); // Set the duration as needed

            var updateCommand = new StartTimeQuizCommand
            {
                QuizId = quizId,
                StartTime = startTime,
                EndTime = endTime
            };

            await _mediator.Send(updateCommand);

            return Ok("Quiz timer started.");
        }
        [HttpDelete]
        [Route("DeleteQuizs")]
        public async Task<int> DeleteQuizAsync(int Id)
        {
            return await _mediator.Send(new DeletequizCommand() { Id = Id });
        }

      

        [HttpPost("{quizId}/Question")]
        public async Task<QuestionModel> AddQuestion(int quizId, QuestionModel questionModel)
        {
            questionModel.QuizId = quizId;
            return await _mediator.Send(new AddQuestionCommand { Question = questionModel });
        }


        [HttpPut("UpdateNumberToWin/{QuizId}")]
        public async Task<int> UpdateNumberToWinAsync(int quizId, [FromBody] GetQuizModel updateDto)
        {

            var isuserDetailUpdated = await _mediator.Send(new UpdateNumberToWinCommand(
                updateDto.Id,
               updateDto.NumberToWin

                ));
            return isuserDetailUpdated;

        }

       


    }

   
}
