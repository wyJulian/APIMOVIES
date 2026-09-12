using MediatR;

namespace Application.Movies.Delete
{
    public class DeleteMovieCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
