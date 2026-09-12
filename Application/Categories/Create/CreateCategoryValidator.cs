using DAL.Interfaces;
using FluentValidation;

namespace Application.Categories.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (name, cancellationToken) =>
                {
                    var categories = await categoryRepository.GetAllCategoriesAsync();
                    return !categories.Any(c => c.Name == name);
                })
                .WithMessage(x => $"A category with the name '{x.Name}' already exists.");

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
