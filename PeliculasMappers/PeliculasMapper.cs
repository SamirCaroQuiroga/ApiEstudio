using ApiEstudio.Models;
using ApiEstudio.Models.Dtos;
using AutoMapper;

namespace ApiEstudio.PeliculasMappers
{
    public class PeliculasMapper : Profile
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<Pelicula, CrearPeliculaDto>().ReverseMap();
        }
    }
}
