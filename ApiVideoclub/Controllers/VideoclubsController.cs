using ApiVideoclub.DTOs;
using ApiVideoclub.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVideoclub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoclubsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public VideoclubsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VideoclubDTOConPeliculas>> Get(int id)
        {
            var videoclub = await dbContext.Videoclubs
                .Include(peliculaDB=>peliculaDB.PeliculaVideoclub)
                .ThenInclude(peliculaVideoclubDB=>peliculaVideoclubDB.Pelicula)
                .FirstOrDefaultAsync(x => x.Id == id);
            //var videoclub = await dbContext.Videoclubs.Include(videoclubBD => videoclubBD.Reseñas).FirstOrDefaultAsync(x => x.Id == id);

            videoclub.PeliculaVideoclub = videoclub.PeliculaVideoclub.OrderBy(x => x.Orden).ToList();
            
            return mapper.Map<VideoclubDTOConPeliculas>(videoclub);
        }

        [HttpPost]
        public async Task<ActionResult> Post(VideoclubCreacionDTO videoclubCreacionDTO)
        {

            if(videoclubCreacionDTO.PeliculasIds == null)
            {
                return BadRequest("No se puede abrir un videoclub sin peliculas");
            }

            var peliculasIds = await dbContext.Peliculas
                .Where(peliculaBD => videoclubCreacionDTO.PeliculasIds.Contains(peliculaBD.Id)).Select(x => x.Id).ToListAsync();

            if(videoclubCreacionDTO.PeliculasIds.Count != peliculasIds.Count)
            {
                return BadRequest("No existe una de las peliculas enviadas");
            }

            var videoclub= mapper.Map<Videoclub>(videoclubCreacionDTO);

            if(videoclub.PeliculaVideoclub != null)
            {
                for (int i = 0; i < videoclub.PeliculaVideoclub.Count; i++)
                {
                    videoclub.PeliculaVideoclub[i].Orden = i;
                }
            }

            dbContext.Add(videoclub);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
