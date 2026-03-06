using ApiEstudio.Data;
using ApiEstudio.Models;
using ApiEstudio.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEstudio.Repository
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public PeliculaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            var peliculaExistente = _bd.Pelicula.Find(pelicula.Id);
            if(peliculaExistente != null)
            {
                _bd.Entry(peliculaExistente).CurrentValues.SetValues(pelicula);
            } else
            {
                _bd.Pelicula.Update(pelicula);
            }
               
            return Guardar();
        }

        public bool BorrarPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Remove(pelicula);
            return Guardar();
        }

        public IEnumerable<Pelicula> BuscarPelicula(string nombre)
        {
            IQueryable<Pelicula> query = _bd.Pelicula;
            if (string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre) || p.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion= DateTime.Now;
            _bd.Pelicula.Add(pelicula);
            return Guardar();
        }

        public bool ExistePelicula(int peliculaId)
        {
            return _bd.Pelicula.Any(p => p.Id == peliculaId);
        }

        public bool ExistePelicula(string peliculaNombre)
        {
            return _bd.Pelicula.Any(p => p.Nombre.ToLower().Trim() == peliculaNombre.ToLower().Trim());
        }

        public Pelicula GetPelicula(int peliculaId)
        {
            return _bd.Pelicula.FirstOrDefault(p => p.Id == peliculaId);
        }

        //public ICollection<Pelicula> GetPelicula()
        //{
        //    return _bd.Pelicula.OrderBy(p => p.Nombre).ToList();
        //}

        public ICollection<Pelicula> GetPeliculas()
        {
            return _bd.Pelicula.OrderBy(p => p.Nombre).ToList();
        }

        public ICollection<Pelicula> GetPeliculasEnCategorias(int categoriaId)
        {
            return _bd.Pelicula.Include(c => c.Categoria).Where(c => c.CategoriaId ==categoriaId).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true: false;
        }
    }
}
