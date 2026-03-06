using ApiEstudio.Models;

namespace ApiEstudio.Repository.IRepository
{
    public interface IPeliculaRepositorio
    {
        ICollection<Pelicula> GetPeliculas();
        ICollection<Pelicula> GetPeliculasEnCategorias(int categoriaId);
        IEnumerable<Pelicula> BuscarPelicula(string nombre);
        Pelicula GetPelicula(int peliculaId);
        bool ExistePelicula(int peliculaId);
        bool ExistePelicula(string peliculaNombre);
        bool CrearPelicula(Pelicula pelicula);
        bool ActualizarPelicula(Pelicula pelicula);
        bool BorrarPelicula(Pelicula pelicula);
        bool Guardar();

    }
}
