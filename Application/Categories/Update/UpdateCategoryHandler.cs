using DAL.Interfaces;
using Domain.Categories;
using FluentValidation;
using MediatR;

namespace Application.Categories.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO?>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<UpdateCategoryCommand> _validator;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IValidator<UpdateCategoryCommand> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CategoryDTO?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            if (category == null)
            {
                return null;
            }

            if (request.Name != null)
            {
                category.Name = request.Name;
            }

            if (request.Description != null)
            {
                category.Description = request.Description;
            }

            category.ModifiedDate = DateTime.UtcNow;

            var updated = await _categoryRepository.UpdateCategoryAsync(category);

            return new CategoryDTO
            {
                Id = updated.Id,
                Name = updated.Name,
                CreatedDate = updated.CreatedDate,
                ModifiedDate = updated.ModifiedDate,
                Description = updated.Description
            };
        }
    }
}
