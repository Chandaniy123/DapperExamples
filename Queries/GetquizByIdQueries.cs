using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Queries
{
    internal class GetquizByIdQueries : IRequest<QuizModel>
    {
        public int Id { get; set; }
    }
}