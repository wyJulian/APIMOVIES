namespace Domain.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Description { get; set; }
    }
}
