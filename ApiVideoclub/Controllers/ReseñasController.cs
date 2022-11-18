using ApiVideoclub.DTOs;
using ApiVideoclub.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> userManager;

        public ReseñasController(ApplicationDbContext context, IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int videoclubId, ReseñaCreacionDTO reseñaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            var existeVideoclub = await context.Videoclubs.AnyAsync(videoclubDB => videoclubDB.Id == videoclubId);

            if (!existeVideoclub)
            {
                return NotFound();
            }

            var reseña = mapper.Map<Reseña>(reseñaCreacionDTO);
            reseña.VideoclubId = videoclubId;
            reseña.UsuarioId = usuarioId;
            context.Add(reseña);
            await context.SaveChangesAsync();
            return Ok();

        }

    }
}
