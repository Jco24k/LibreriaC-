using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaMVC.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
