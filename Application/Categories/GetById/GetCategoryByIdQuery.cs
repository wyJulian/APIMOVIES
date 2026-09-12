using Domain.Categories;
using MediatR;

namespace Application.Categories.GetById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDTO?>
    {
        public required int Id { get; set; }
    }
}
