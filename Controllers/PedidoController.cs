using MaestroDetalleCRUD.Models;
using MaestroDetalleCRUD.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaestroDetalleCRUD.Controllers
{
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<ActionResult> Index()
        {
            var pedidos= await _context.Pedidos
            .Include(p=>p.Cliente)
            .ToListAsync();

            return View(pedidos);
        }
        
        public async Task<ActionResult> Create()
        {
            ViewBag.Clientes=await _context.Clientes.ToListAsync();
            ViewBag.Productos=await _context.Productos.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pedido pedido, int[] productoIds, int[] cantidades)
        {
            
            var cli= await _context.Clientes.FirstOrDefaultAsync(m=>m.ClienteId==pedido.ClienteId);

            if (cli!=null)
                pedido.Cliente=cli;

            foreach (var item in productoIds)
            {
                var producto=await _context.Productos.FindAsync(item);
                if (producto!=null)
                {
                    pedido.Detalles.Add(new PedidoDetalle
                    {
                        ProductoId=item,
                        Cantidad=cantidades[Array.IndexOf(productoIds, item)],
                        Producto=producto
                    });
                }   
            }
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details (int id)
        {
            var pedido= await _context.Pedidos
            .Include(p=>p.Detalles)
                .ThenInclude(d=>d.Producto)
            .Include(p=>p.Cliente)
            .FirstOrDefaultAsync(p=>p.PedidoId==id);

            if (pedido==null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        public async Task<IActionResult> Edit (int id)
        {
            var pedido= await _context.Pedidos.FindAsync(id);
            if (pedido== null)
                return NotFound();
            
            return View(pedido);
        }
    }
}