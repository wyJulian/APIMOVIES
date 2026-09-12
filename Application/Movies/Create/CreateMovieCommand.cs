using Domain.Movies;
using MediatR;

namespace Application.Movies.Create
{
    public class CreateMovieCommand : IRequest<MovieDTO>
    {
        public required string Name { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public int? Age { get; set; }
    }
}
