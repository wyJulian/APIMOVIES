using DAL.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Categories.Delete
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<DeleteCategoryCommand> _validator;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IValidator<DeleteCategoryCommand> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _categoryRepository.DeleteCategoryAsync(request.Id);
        }
    }
}
