using DAL.Interfaces;
using Domain.Movies;
using FluentValidation;
using MediatR;

namespace Application.Movies.GetByCategory
{
    public class GetMoviesByCategoryHandler : IRequestHandler<GetMoviesByCategoryQuery, List<MovieDTO>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<GetMoviesByCategoryQuery> _validator;

        public GetMoviesByCategoryHandler(IMovieRepository movieRepository, IValidator<GetMoviesByCategoryQuery> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<List<MovieDTO>> Handle(GetMoviesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var movies = await _movieRepository.GetMoviesByCategoryAsync(request.Category);

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
