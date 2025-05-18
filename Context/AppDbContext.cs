using Microsoft.EntityFrameworkCore;
using WebApiProyect.Models;

namespace WebApiProyect.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            
        }
        public DbSet<Inventario> Inventarios { get; set; } 
    }
}
