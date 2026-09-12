namespace Domain.Movies
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetAllMoviesAsync();
        public Task<Movie?> GetMovieByIdAsync(int id);
        public Task<Movie> AddMovieAsync(Movie movie);
        public Task<Movie> UpdateMovieAsync(Movie movie);
        public Task<bool> DeleteMovieAsync(int id);
        public Task<List<Movie>> GetMoviesByCategoryAsync(string category);
    }
}
