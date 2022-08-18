using System.ComponentModel.DataAnnotations;

namespace LibreriaMVC.Models
{
    public class PA_PRODUCTOS_STOCK
    {
        [Key]
        public int id { get; set; }
        public string marca { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public string nombre { get; set; }


    }
}
