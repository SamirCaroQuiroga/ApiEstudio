using ApiEstudio.Data;
using ApiEstudio.Models;
using ApiEstudio.Repository.IRepository;

namespace ApiEstudio.Repository
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _bd.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            categoria.FechaCreacion= DateTime.Now;
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(int CategoriaId)
        {
            return _bd.Categoria.Any(c => c.Id == CategoriaId);
        }

        public bool ExisteCategoria(string CategoriaNombre)
        {
            return _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == CategoriaNombre.ToLower().Trim());
        }

        public Categoria GetCategoria(int CategoriaId)
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true: false;
        }
    }
}
