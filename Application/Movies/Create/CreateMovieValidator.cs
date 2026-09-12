using Domain.Categories;
using FluentValidation;

namespace Application.Movies.Create
{
    public class CreateMovieValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Category)
                .NotEmpty()
                .MustAsync(async (category, cancellationToken) =>
                {
                    var categories = await categoryRepository.GetAllCategoriesAsync();
                    return categories.Any(c => c.Name == category);
                })
                .WithMessage(x => $"The category '{x.Category}' does not exist.");

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Age.HasValue);
        }
    }
}
