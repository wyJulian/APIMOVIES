using DAL.Interfaces;
using Domain.Movies;
using FluentValidation;
using MediatR;

namespace Application.Movies.Update
{
    public class UpdateMovieHandler : IRequestHandler<UpdateMovieCommand, MovieDTO?>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<UpdateMovieCommand> _validator;

        public UpdateMovieHandler(IMovieRepository movieRepository, IValidator<UpdateMovieCommand> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<MovieDTO?> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
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

            if (request.Name != null)
            {
                movie.Name = request.Name;
            }

            if (request.Category != null)
            {
                movie.Category = request.Category;
            }

            if (request.Description != null)
            {
                movie.Description = request.Description;
            }

            if (request.Age.HasValue)
            {
                movie.Age = request.Age;
            }

            movie.ModifiedDate = DateTime.UtcNow;

            var updated = await _movieRepository.UpdateMovieAsync(movie);

            return new MovieDTO
            {
                Id = updated.Id,
                Name = updated.Name,
                Category = updated.Category,
                Description = updated.Description,
                Age = updated.Age,
                CreatedDate = updated.CreatedDate,
                ModifiedDate = updated.ModifiedDate
            };
        }
    }
}
