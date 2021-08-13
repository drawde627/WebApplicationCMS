using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Login
    {
        public Usuario Usuario { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}
