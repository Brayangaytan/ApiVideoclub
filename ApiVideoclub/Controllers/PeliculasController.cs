using ApiVideoclub.Entidades;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ApiVideoclub.Controllers
{
    [ApiController]
    [Route("peliculas")]
    public class PeliculasController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;

        public PeliculasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            return await dbContext.Peliculas.Include(x => x.videoclubs).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pelicula pelicula)
        {
            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pelicula pelicula, int id)
        {
            if(pelicula.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido url.");
            }

            dbContext.Update(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Pelicula()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        
    }
}
