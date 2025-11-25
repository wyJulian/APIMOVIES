using APIMOVIES.DAL.Models;
using APIMOVIES.DAL.Models.DTOs;
using APIMOVIES.Repository;
using APIMOVIES.Repository.IRepository;
using APIMOVIES.Services.IServices;
using AutoMapper;
using System.Net.WebSockets;

namespace APIMOVIES.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        
        public MovieService(IMovieRepository movieRepository,IMapper mapper)
        {
           _movieRepository = movieRepository;
           _mapper = mapper;
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto)
        {
            var movieExist = await _movieRepository.MovieExistByNameAsync(movieCreateUpdateDto.Name);
            if (movieExist)
            {
                throw new Exception("Movie already exists.");
            }

            var movie = _mapper.Map<Movie>(movieCreateUpdateDto);

            var movieCreated = await _movieRepository.CreateMovieAsync(movie);

            if (!movieCreated)
            {
                throw new Exception("Error creating movie.");
            }

            return _mapper.Map<MovieDto>(movie);
        } 

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movieExist = await _movieRepository.GetMovieAsync(id);
            if (movieExist == null)
            {
                throw new Exception("A movie with that id doesn't exist");
            }
            var movieDeleted = await _movieRepository.DeleteMovieAsync(id);
            return movieDeleted;
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<bool> MovieExistById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovieExistByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto, int id)
        {
            var movieExist = await _movieRepository.GetMovieAsync(id);
            if (movieExist == null)
            {
                throw new Exception("A movie with that id doesn't exist");
            }
            _mapper.Map(movieCreateUpdateDto, movieExist);

            var movieExistByName = await _movieRepository.MovieExistByNameAsync(movieCreateUpdateDto.Name);
            if (movieExistByName)
            {
                throw new Exception("A movie with that name already exists");
            }
            var movieUpdated = await _movieRepository.UpdateMovieAsync(movieExist);
            if (!movieUpdated)
            {
                throw new Exception("Error updating movie.");
            }

            return _mapper.Map<MovieDto>(movieExist);
        }
    }
}
