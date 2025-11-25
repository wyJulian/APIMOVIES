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
            CreateMap<CategoryUpdateCreateDto, Category>().ReverseMap();

            CreateMap <Movie, MovieDto>().ReverseMap();
            CreateMap<MovieCreateUpdateDto, Movie>().ReverseMap();
        }
    }
}
