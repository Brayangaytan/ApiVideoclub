using ApiVideoclub.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiVideoclub
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        } 

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Videoclub> Videoclubs { get; set; }

    }
}
