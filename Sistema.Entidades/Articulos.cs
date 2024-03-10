﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Articulos
    {
        public int IdArticulo {  get; set; }
        public int IdCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_Venta { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public string Imagen { get; set; }  
    }
}
