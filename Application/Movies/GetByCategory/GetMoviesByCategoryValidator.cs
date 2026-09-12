using FluentValidation;

namespace Application.Movies.GetByCategory
{
    public class GetMoviesByCategoryValidator : AbstractValidator<GetMoviesByCategoryQuery>
    {
        public GetMoviesByCategoryValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
