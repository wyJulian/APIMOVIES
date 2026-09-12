using Domain.Movies;
using MediatR;

namespace Application.Movies.GetById
{
    public class GetMovieByIdQuery : IRequest<MovieDTO?>
    {
        public required int Id { get; set; }
    }
}
