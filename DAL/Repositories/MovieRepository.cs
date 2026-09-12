using Domain.Movies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            var result = await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Movie>> GetAllMoviesAsync() => _context.Movie.ToList();
        public async Task<Movie?> GetMovieByIdAsync(int id) => await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);  
        public async Task<List<Movie>> GetMoviesByCategoryAsync(string category) => await _context.Movie.Where(m => m.Category == category).ToListAsync();
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            _context.Movie.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
