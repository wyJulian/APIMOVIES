using Domain.Categories;
using FluentValidation;

namespace Application.Categories.Update
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Category ID must be greater than 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .MustAsync(async (command, name, cancellationToken) =>
                {
                    var categories = await categoryRepository.GetAllCategoriesAsync();
                    return !categories.Any(c => c.Name == name && c.Id != command.Id);
                })
                .WithMessage(x => $"A category with the name '{x.Name}' already exists.")
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
