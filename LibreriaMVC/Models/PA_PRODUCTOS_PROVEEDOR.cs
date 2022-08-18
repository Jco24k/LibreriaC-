using System.ComponentModel.DataAnnotations;

namespace LibreriaMVC.Models
{
    public class PA_PRODUCTOS_PROVEEDOR
    {
        [Key]
        public int id { get; set; }
        public string marca { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
    }
}
