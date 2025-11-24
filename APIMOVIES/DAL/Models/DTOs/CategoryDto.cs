using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models.DTOs
{
    public class CategoryDto 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is mandatory")]
        [MaxLength(100, ErrorMessage = "Category name can't exceed 100 characters")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
