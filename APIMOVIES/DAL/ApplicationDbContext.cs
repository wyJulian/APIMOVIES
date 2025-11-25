using APIMOVIES.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMOVIES.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Crear db set de cada modelo aqui abajo
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
