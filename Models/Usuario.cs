using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string NombresCompletos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
    }
}
