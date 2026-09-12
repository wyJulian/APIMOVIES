using FluentValidation;

namespace Application.Movies.Delete
{
    public class DeleteMovieValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Movie ID must be greater than 0.");
        }
    }
}
