using ApiEstudio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstudio.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        }
        
        //Aqui pasar todas las entidades (modelos)

        public DbSet<Categoria> Categoria { get; set; }
            
     
    }
}
