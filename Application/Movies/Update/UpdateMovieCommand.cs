using Domain.Movies;
using MediatR;

namespace Application.Movies.Update
{
    public class UpdateMovieCommand : IRequest<MovieDTO?>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int? Age { get; set; }
    }
}
