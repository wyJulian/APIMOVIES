using Domain.Movies;
using MediatR;

namespace Application.Movies.GetAll
{
    public class GetAllMoviesQuery : IRequest<List<MovieDTO>>
    {
    }
}
