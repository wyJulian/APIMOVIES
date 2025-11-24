using APIMOVIES.DAL;
using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;
using APIMOVIES.Repository.IRepository;
using APIMOVIES.Services.IServices;
using AutoMapper;

namespace APIMOVIES.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> CategoryExistById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
