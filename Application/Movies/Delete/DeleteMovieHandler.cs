using Domain.Movies;
using FluentValidation;
using MediatR;

namespace Application.Movies.Delete
{
    public class DeleteMovieHandler : IRequestHandler<DeleteMovieCommand, bool>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<DeleteMovieCommand> _validator;

        public DeleteMovieHandler(IMovieRepository movieRepository, IValidator<DeleteMovieCommand> validator)
        {
            _movieRepository = movieRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _movieRepository.DeleteMovieAsync(request.Id);
        }
    }
}
