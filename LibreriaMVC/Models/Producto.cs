using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaMVC.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public int IdProveedor { get; set; }

        public virtual Proveedor IdProveedorNavigation { get; set; }
    }
}
