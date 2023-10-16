using Microsoft.EntityFrameworkCore;

namespace MaestroDetalleCRUD.Models.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option) : base(option)
        {

        }  
        public DbSet<Cliente> Clientes {get;set;} = null!;
        public DbSet<Producto> Productos {get;set;} = null!;
        public DbSet<Pedido> Pedidos {get;set;} = null!;
        public DbSet<PedidoDetalle> PedidoDetalles {get;set;} = null!;
    }
}