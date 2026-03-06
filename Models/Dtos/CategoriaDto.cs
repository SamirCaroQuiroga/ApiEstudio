using System.ComponentModel.DataAnnotations;

namespace ApiEstudio.Models.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre es de maximo 100 caracteres")]
        public string Nombre { get; set; }    
        public DateTime FechaCreacion { get; set; }
    }
}
