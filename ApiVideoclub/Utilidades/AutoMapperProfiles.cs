using ApiVideoclub.DTOs;
using ApiVideoclub.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;

namespace ApiVideoclub.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PeliculaCreacionDTO, Pelicula>();
            CreateMap<Pelicula, PeliculaDTO>();
            CreateMap<Pelicula, PeliculaDTOConVideoclubs>()
                .ForMember(peliculaDTO => peliculaDTO.Videoclubs, opciones => opciones.MapFrom(MapPeliculaDTOVideoclub));

            CreateMap<VideoclubCreacionDTO, Videoclub>()
                .ForMember(videoclub => videoclub.PeliculaVideoclub, opciones => opciones.MapFrom(MapPeliculaVideoclub));
            CreateMap<Videoclub, VideoclubDTO>();
            CreateMap<Videoclub, VideoclubDTOConPeliculas>()
                .ForMember(videoclubDTO => videoclubDTO.Peliculas, opciones => opciones.MapFrom(MapVideoclubDTOPelicula));
            
            CreateMap<ReseñaCreacionDTO, Reseña>();
            CreateMap<Reseña, ReseñaDTO>();
        }

        private List<VideoclubDTO> MapPeliculaDTOVideoclub(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<VideoclubDTO>();

            if(pelicula.PeliculaVideoclub == null) { return resultado; }

            foreach( var peliculaVideoclub in pelicula.PeliculaVideoclub)
            {
                resultado.Add(new VideoclubDTO()
                {
                    Id = peliculaVideoclub.VideoclubId,
                    Name = peliculaVideoclub.Videoclub.Name
                });
            }

            return resultado;
        }

        private List<PeliculaDTO> MapVideoclubDTOPelicula(Videoclub videoclub, VideoclubDTO videoclubDTO)
        {
            var resultado = new List<PeliculaDTO>();

            if(videoclub.PeliculaVideoclub == null) { return resultado; }

            foreach(var peliculaVideoclub in videoclub.PeliculaVideoclub)
            {
                resultado.Add(new PeliculaDTO()
                {
                    Id = peliculaVideoclub.PeliculaId,
                    Name = peliculaVideoclub.Pelicula.Name
                });
            }

            return resultado;
        }

        private List<PeliculaVideoclub> MapPeliculaVideoclub(VideoclubCreacionDTO videoclubCreacionDTO, Videoclub videoclub)
        {
            var resultado = new List<PeliculaVideoclub>();

            if(videoclubCreacionDTO.PeliculasIds == null) { return resultado; }

            foreach (var peliculaid in videoclubCreacionDTO.PeliculasIds)
            {
                resultado.Add(new PeliculaVideoclub() { PeliculaId = peliculaid });
            }

            return resultado;
        }

    }
}
