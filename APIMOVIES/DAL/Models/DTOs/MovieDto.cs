using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Movie name is mandatory")]
        [MaxLength(100, ErrorMessage ="Movie name can't exceed 100 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is mandatory")]
        public int Duration { get; set; } // Duration in minutes

        [Required(ErrorMessage = "Clasification is mandatory")]
        [MaxLength(10, ErrorMessage = "Clasification can't exceed 10 characters")]
        public string Clasification { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
