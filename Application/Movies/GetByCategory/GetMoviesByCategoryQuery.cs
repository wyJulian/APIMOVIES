using Domain.Movies;
using MediatR;

namespace Application.Movies.GetByCategory
{
    public class GetMoviesByCategoryQuery : IRequest<List<MovieDTO>>
    {
        public required string Category { get; set; }
    }
}
