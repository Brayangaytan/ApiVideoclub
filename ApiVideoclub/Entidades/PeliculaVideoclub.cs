namespace ApiVideoclub.Entidades
{
    public class PeliculaVideoclub
    {
        public int VideoclubId { get; set; }
        public int PeliculaId { get; set; }
        public int Orden { get; set; }
        public Videoclub Videoclub { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
