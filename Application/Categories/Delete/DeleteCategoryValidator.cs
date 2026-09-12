using FluentValidation;

namespace Application.Categories.Delete
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
        }
    }
}
