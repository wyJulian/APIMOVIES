using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;

namespace APIMOVIES.Services.IServices
{
    public interface IMovieService
    {
        Task<ICollection<MovieDto>> GetMoviesAsync();
        Task<MovieDto> GetMovieAsync(int id);
        Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto);
        Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto, int id);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> MovieExistById(int id);
        Task<bool> MovieExistByName(string name);
    }
}

