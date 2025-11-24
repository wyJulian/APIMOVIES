using APIMOVIES.DAL.Models;

namespace APIMOVIES.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<bool> CategoryExistById(int id);
        Task<bool> CategoryExistByName(string name);
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
