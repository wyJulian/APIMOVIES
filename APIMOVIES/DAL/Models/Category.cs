using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models
{
    public class Category : AuditBase
    {
        [Required]
        [Display(Name = "Category name is mandatory")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
