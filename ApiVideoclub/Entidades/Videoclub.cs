namespace ApiVideoclub.Entidades
{
    public class Videoclub
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genero { get; set; }

        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }
    }
}
