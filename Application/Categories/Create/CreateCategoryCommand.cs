using Domain.Categories;
using MediatR;

namespace Application.Categories.Create
{
    public class CreateCategoryCommand : IRequest<CategoryDTO>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
