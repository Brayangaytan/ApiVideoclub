namespace ApiVideoclub.Entidades
{
    public class Reseña
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int VideoclubId { get; set; }
        public Videoclub Videoclub { get; set; }
    }
}
