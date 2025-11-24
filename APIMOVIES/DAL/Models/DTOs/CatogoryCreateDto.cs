using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models.DTOs
{
    public class CatogoryCreateDto
    {
        [Required(ErrorMessage = "Category name is mandatory")]
        [MaxLength(100, ErrorMessage = "Category name can't exceed 100 characters")]
        public string Name { get; set; }
    }
}
