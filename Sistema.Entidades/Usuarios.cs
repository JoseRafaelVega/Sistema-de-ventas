using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public int IdRol {  get; set; }
        public string Nombre { get; set; }
        public string Tipo_Documento { get; set; }
        public string Num_Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set;}
        public string Clave { get; set; }
        public string Email { get; set; }
    }
}
