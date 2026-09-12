using Domain.Categories;
using MediatR;

namespace Application.Categories.Update
{
    public class UpdateCategoryCommand : IRequest<CategoryDTO?>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
