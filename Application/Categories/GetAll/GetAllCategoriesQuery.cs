using Domain.Categories;
using MediatR;

namespace Application.Categories.GetAll
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDTO>>
    {
    }
}
