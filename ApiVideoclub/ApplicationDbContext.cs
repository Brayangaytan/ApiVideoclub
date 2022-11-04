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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PeliculaVideoclub>()
                .HasKey(al => new { al.PeliculaId, al.VideoclubId });
        }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Videoclub> Videoclubs { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }
        public DbSet<PeliculaVideoclub> PeliculaVideoclub { get; set; }

    }
}
