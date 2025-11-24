using APIMOVIES.DAL;
using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;
using APIMOVIES.Repository.IRepository;
using APIMOVIES.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistByNameAsync(string name)
        {
           throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryUpdateCreateDto categoryCreateDto)
        {
            var categoryExist = await _categoryRepository.CategoryExistByNameAsync(categoryCreateDto.Name);
            if (categoryExist)
            {
                throw new Exception("Category already exists.");
            }

            var category = _mapper.Map<Category>(categoryCreateDto);

            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Error creating category.");
            }

            return _mapper.Map<CategoryDto>(category);
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
            var category = await _categoryRepository.GetCategoryAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<CategoryDto> UpdateCategoryAsync(CategoryUpdateCreateDto dto, int id)
        {
            var categoryExist = await _categoryRepository.GetCategoryAsync(id);
            if (categoryExist == null)
            {
                throw new Exception("A category with that id doesn't exist");
            }

            var categoryExistByName =  await _categoryRepository.CategoryExistByNameAsync(dto.Name);
            if (categoryExistByName)
            {
                throw new Exception("A category with that name already exists.");
            }

            _mapper.Map(dto, categoryExist);
            
            var categoryUpdated = await _categoryRepository.UpdateCategoryAsync(categoryExist);

            if (!categoryUpdated)
            {
                throw new Exception("Error updating category.");
            }

            return _mapper.Map<CategoryDto>(categoryExist);
        }
    }
}
