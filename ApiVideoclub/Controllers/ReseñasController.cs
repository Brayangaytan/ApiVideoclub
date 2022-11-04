using ApiVideoclub.DTOs;
using ApiVideoclub.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVideoclub.Controllers
{
    [ApiController]
    [Route("api/videoclubs/{videoclubId:int}/reseñas")]
    public class ReseñasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ReseñasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReseñaDTO>>> Get(int videoclubId)
        {
            var existeVideoclub = await context.Videoclubs.AnyAsync(videoclubDB => videoclubDB.Id == videoclubId);

            if (!existeVideoclub)
            {
                return NotFound();
            }

            var reseñas = await context.Reseñas.
                Where(reseñaDB => reseñaDB.VideoclubId == videoclubId).ToListAsync();

            return mapper.Map<List<ReseñaDTO>>(reseñas);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int videoclubId, ReseñaCreacionDTO reseñaCreacionDTO)
        {
            var existeVideoclub = await context.Videoclubs.AnyAsync(videoclubDB => videoclubDB.Id == videoclubId);

            if (!existeVideoclub)
            {
                return NotFound();
            }

            var reseña = mapper.Map<Reseña>(reseñaCreacionDTO);
            reseña.VideoclubId = videoclubId;
            context.Add(reseña);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
