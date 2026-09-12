using Domain.Movies;
using FluentValidation;
using MediatR;

namespace Application.Movies.GetById
{
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, MovieDTO?>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<GetMovieByIdQuery> _validator;

        public GetMovieByIdHandler(IMovieRepository movieRepository, IValidator<GetMovieByIdQuery> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<MovieDTO?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var movie = await _movieRepository.GetMovieByIdAsync(request.Id);
            if (movie == null)
            {
                return null;
            }

            return new MovieDTO
            {
                Id = movie.Id,
                Name = movie.Name,
                Category = movie.Category,
                Description = movie.Description,
                Age = movie.Age,
                CreatedDate = movie.CreatedDate,
                ModifiedDate = movie.ModifiedDate
            };
        }
    }
}
