using ApiVideoclub.DTOs;
using ApiVideoclub.Entidades;
using ApiVideoclub.Filtros;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ApiVideoclub.Controllers
{
    [ApiController]
    [Route("peliculas")]
    public class PeliculasController : ControllerBase
    {
        
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]//api/peliculas
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            var peliculas = await dbContext.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDTOConVideoclubs>> Get(int id)
        {
            var pelicula = await dbContext.Peliculas
                .Include(peliculaDB=>peliculaDB.PeliculaVideoclub)
                .ThenInclude(peliculaVideoclubDB=>peliculaVideoclubDB.Videoclub)
                .FirstOrDefaultAsync(peliculaBD => peliculaBD.Id == id);

            if(pelicula == null)
            {
                return NotFound();
            }

            return mapper.Map<PeliculaDTOConVideoclubs>(pelicula);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<PeliculaDTO>>> Get([FromRoute]string nombre)
        {
            var peliculas = await dbContext.Peliculas.Where(peliculaBD => peliculaBD.Name.Contains(nombre)).ToListAsync();
           
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var existePeliculaMismoNombre = await dbContext.Peliculas.AnyAsync(x => x.Name == peliculaCreacionDTO.Name);

            if (existePeliculaMismoNombre)
            {
                return BadRequest("Ya existe una pelicula con el nombre");
            }

            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);

            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pelicula pelicula, int id)
        {
            if(pelicula.Id != id)
            {
                return BadRequest("El id de la pelicula no coincide con el establecido url.");
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
