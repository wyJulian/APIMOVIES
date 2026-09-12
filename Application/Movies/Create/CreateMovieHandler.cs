using DAL.Interfaces;
using Domain.Movies;
using FluentValidation;
using MediatR;

namespace Application.Movies.Create
{
    public class CreateMovieHandler : IRequestHandler<CreateMovieCommand, MovieDTO>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<CreateMovieCommand> _validator;

        public CreateMovieHandler(IMovieRepository movieRepository, IValidator<CreateMovieCommand> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<MovieDTO> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var movie = new Movie
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                Age = request.Age,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _movieRepository.AddMovieAsync(movie);

            return new MovieDTO
            {
                Id = created.Id,
                Name = created.Name,
                Category = created.Category,
                Description = created.Description,
                Age = created.Age,
                CreatedDate = created.CreatedDate,
                ModifiedDate = created.ModifiedDate
            };
        }
    }
}
