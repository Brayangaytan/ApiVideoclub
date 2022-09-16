using ApiVideoclub.Entidades;
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

        public VideoclubsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Videoclub>>> GetAll()
        {
            return await dbContext.Videoclubs.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Videoclub>> GetById(int id)
        {
            return await dbContext.Videoclubs.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Videoclub videoclub)
        {
            var existePelicula = await dbContext.Peliculas.AnyAsync(x => x.Id == videoclub.PeliculaId);

            if (!existePelicula)
            {
                return BadRequest($"No existe pelicula con el id: {videoclub.PeliculaId} ");
            }

            dbContext.Add(videoclub);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Videoclub videoclub, int id)
        {
            var exist = await dbContext.Videoclubs.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("La clase especifica no existe");
            }

            if (videoclub.Id != id)
            {
                return BadRequest("El id de la clase no coincide con el establecido en la url");
            }

            dbContext.Update(videoclub);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Videoclubs.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            //var validateRelation = await dbContext.PeliculaVideoclub.AnyAsync

            dbContext.Remove(new Videoclub
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
