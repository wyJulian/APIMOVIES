using APIMOVIES.DAL;
using APIMOVIES.DAL.Models;
using APIMOVIES.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace APIMOVIES.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateMovieAsync(Movie movie)
        {
            movie.CreatedDate = DateTime.UtcNow;
            await _context.Movies.AddAsync(movie);
            return await SaveAsync();
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);

            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            return await SaveAsync();
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Movie>> GetMoviesAsync()
        {
            var movies = await _context.Movies.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
            return movies;
        }

        public async Task<bool> MovieExistByIdAsync(int id)
        {
            return await _context.Movies.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> MovieExistByNameAsync(string name)
        {
            return await _context.Movies.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            movie.ModifiedDate = DateTime.UtcNow;
            _context.Movies.Update(movie);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
