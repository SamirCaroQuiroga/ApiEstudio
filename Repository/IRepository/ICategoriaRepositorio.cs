using ApiEstudio.Models;

namespace ApiEstudio.Repository.IRepository
{
    public interface ICategoriaRepositorio
    {
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int CategoriaId);
        bool ExisteCategoria(int CategoriaId);
        bool ExisteCategoria(string CategoriaNombre);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();

    }
}
