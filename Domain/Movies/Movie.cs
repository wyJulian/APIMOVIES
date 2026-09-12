namespace Domain.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public int? Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
