using Sistema.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema.Negocio
{
    public class Nrol
    {
        public static DataTable Listar()
        {
            Drol datos = new Drol();
            return datos.Listar();
        }
    }
}
