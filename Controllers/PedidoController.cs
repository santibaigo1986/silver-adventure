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
    }
}