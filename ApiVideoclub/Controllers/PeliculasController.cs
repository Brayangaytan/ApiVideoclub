using ApiVideoclub.Entidades;
using ApiVideoclub.Services;
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
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<PeliculasController> logger;

        public PeliculasController(ApplicationDbContext dbContext, IService service, 
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<PeliculasController> logger)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        public ActionResult ObtenerGUID()
        {
            return Ok(new
            {
                PeliculasControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                PeliculasControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                PeliculasControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]//api/peliculas
        [HttpGet("listado")]//api/peliculas/listado
        [HttpGet("/listado")]// /listado
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.ejecutarJob();
            return await dbContext.Peliculas.Include(x => x.videoclubs).ToListAsync();
        }

        [HttpGet("primero")]//api/peliculas/primero
        public async Task<ActionResult<Pelicula>> PrimerPelicula([FromHeader] int valor, [FromQuery] string pelicula,
            [FromQuery] int peliculaid)
        {
            return await dbContext.Peliculas.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]//api/peliculas/primero2
        public ActionResult<Pelicula> PrimerPeliculaD()
        {
            return new Pelicula() { Name = "DOS" };
        }

        [HttpGet("{id:int}/{param=Star Wars}")]
        public async Task<ActionResult<Pelicula>> Get(int id, string param)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if(pelicula == null)
            {
                return NotFound();
            }
            return pelicula;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Pelicula>> Get([FromRoute]string nombre)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Name.Contains(nombre));
            if (pelicula == null)
            {
                logger.LogError("No se encuentra el alumno");
                return NotFound();
            }
            return pelicula;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Pelicula pelicula)
        {
            var existeAlumnoMismoNombre = await dbContext.Peliculas.AnyAsync(x => x.Name == pelicula.Name);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest("Ya existe un autor con el nombre");
            }

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
