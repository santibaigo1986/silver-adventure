using System.ComponentModel.DataAnnotations;

namespace MaestroDetalleCRUD.Models
{
    public class Cliente
    {
        public int ClienteId {get ; set;}
        [Required]
        public string Nombre { get; set; } = null!;
    }
}