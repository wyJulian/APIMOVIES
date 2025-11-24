using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;

namespace APIMOVIES.Services.IServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(int id);
        Task<bool> CategoryExistByIdAsync(int id);
        Task<bool> CategoryExistByNameAsync(string name);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
    }
}
