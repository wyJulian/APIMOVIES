using Domain.Movies;
using MediatR;

namespace Application.Movies.GetAll
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, List<MovieDTO>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieDTO>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Name = m.Name,
                Category = m.Category,
                Description = m.Description,
                Age = m.Age,
                CreatedDate = m.CreatedDate,
                ModifiedDate = m.ModifiedDate
            }).ToList();
        }
    }
}
