using ApiEstudio.Models;
using ApiEstudio.Models.Dtos;
using ApiEstudio.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstudio.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepositorio _pelRepo;
        private readonly IMapper _mapper;
        public PeliculasController(IPeliculaRepositorio pelRepo, IMapper mapper)
        {
            _pelRepo = pelRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public IActionResult GetPeliculas()
        {
            var peliculas = _pelRepo.GetPeliculas();
            var peliculasDto = new List<PeliculaDto>();
            foreach (var pelicula in peliculas)
            {
                peliculasDto.Add(_mapper.Map<PeliculaDto>(pelicula));
            }
            return Ok(peliculasDto);
        }

        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = _pelRepo.GetPelicula(peliculaId);
            if (pelicula == null)
            {
                return NotFound();
            }

            var peliculaDto = new PeliculaDto();
            peliculaDto = _mapper.Map<PeliculaDto>(pelicula);
            return Ok(peliculaDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] CrearPeliculaDto crearPeliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearPeliculaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_pelRepo.ExistePelicula(crearPeliculaDto.Nombre))
            {
                ModelState.AddModelError("", "La pelicula ya existe");
                return StatusCode(404, ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(crearPeliculaDto);


            if (!_pelRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPelicula", new {peliculaId = pelicula.Id }, pelicula);
        }

        [HttpPut("{peliculaId:int}", Name = "ActualizarPelicula")]
        public IActionResult ActualizarPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        {
            if (peliculaDto == null || peliculaId != peliculaDto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pelicula = _mapper.Map<Pelicula>(peliculaDto);

            if (!_pelRepo.ActualizarPelicula(pelicula))
            {
                ModelState.AddModelError("", "Algo salió mal actualizando el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        //[HttpPatch("{peliculaId:int}", Name = "ActualizarPelicula")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult ActualizarPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (peliculaDto == null || peliculaId != peliculaDto.Id)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (_pelRepo.ExistePelicula(peliculaDto.Nombre))
        //    {
        //        ModelState.AddModelError("", "La pelicula ya existe");
        //        return StatusCode(400, ModelState);
        //    }

        //    var pelicula = _mapper.Map<Pelicula>(peliculaDto);


        //    if (!_pelRepo.ActualizarPelicula(pelicula))
        //    {
        //        ModelState.AddModelError("", $"Algo salio mal actualizando el registro {pelicula.Nombre}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();
        //}
    }
}
