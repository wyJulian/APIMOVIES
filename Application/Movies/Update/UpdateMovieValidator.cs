using DAL.Interfaces;
using FluentValidation;

namespace Application.Movies.Update
{
    public class UpdateMovieValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Movie ID must be greater than 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100);

            RuleFor(x => x.Category)
                .MustAsync(async (category, cancellationToken) =>
                {
                    var categories = await categoryRepository.GetAllCategoriesAsync();
                    return categories.Any(c => c.Name == category);
                })
                .WithMessage(x => $"The category '{x.Category}' does not exist.")
                .When(x => x.Category != null);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Age.HasValue);
        }
    }
}
