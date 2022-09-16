namespace ApiVideoclub.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Videoclub> videoclubs { get; set; }
    }
}
