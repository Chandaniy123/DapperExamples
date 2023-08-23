using CQRsAndMEdiatorsEXample.Models;
using MediatR;

namespace CQRsAndMEdiatorsEXample.Queries
{
    internal class GetQuizListQueries : IRequest<List<GetQuizModel>>
    {
    }
}