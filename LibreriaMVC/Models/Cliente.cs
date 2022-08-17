using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaMVC.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Genero { get; set; }
        public string Estado { get; set; }
    }
}
