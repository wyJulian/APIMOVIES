using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; } // Duration in minutes
        public string? Description { get; set; }
        [Required]
        public string Clasification { get; set; }
    }
}
