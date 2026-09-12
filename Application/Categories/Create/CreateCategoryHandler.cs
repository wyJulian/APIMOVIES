using Domain.Categories;
using FluentValidation;
using MediatR;

namespace Application.Categories.Create
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryCommand> _validator;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IValidator<CreateCategoryCommand> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _categoryRepository.AddCategoryAsync(category);

            return new CategoryDTO
            {
                Id = created.Id,
                Name = created.Name,
                CreatedDate = created.CreatedDate,
                ModifiedDate = created.ModifiedDate,
                Description = created.Description
            };
        }
    }
}
