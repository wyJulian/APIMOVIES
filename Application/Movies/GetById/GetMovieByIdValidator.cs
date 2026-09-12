using FluentValidation;

namespace Application.Movies.GetById
{
    public class GetMovieByIdValidator : AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Movie ID must be greater than 0.");
        }
    }
}
