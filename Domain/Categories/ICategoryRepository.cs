namespace Domain.Categories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<Category?> GetCategoryByIdAsync(int id);
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
