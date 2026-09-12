using MediatR;

namespace Application.Categories.Delete
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
