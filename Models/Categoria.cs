using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiEstudio.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name="Fecha Creacion")]
        public DateTime FechaCreacion { get; set; }
    }
}
