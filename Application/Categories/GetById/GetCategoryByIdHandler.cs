using Domain.Categories;
using FluentValidation;
using MediatR;

namespace Application.Categories.GetById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO?>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<GetCategoryByIdQuery> _validator;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IValidator<GetCategoryByIdQuery> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CategoryDTO?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
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

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
                Description = category.Description
            };
        }
    }
}
