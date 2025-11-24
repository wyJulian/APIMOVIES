using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;
using AutoMapper;

namespace APIMOVIES.MoviesMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CatogoryCreateDto, Category>().ReverseMap();
        }
    }
}
