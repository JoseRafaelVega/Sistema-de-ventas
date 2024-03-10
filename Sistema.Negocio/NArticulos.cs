using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NArticulos
    {
        public static DataTable Listar()
        {
            DArticulos Datos = new DArticulos();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Buscar(valor);
        }

        public static string Insertar(int idCategoria, string codigo, string nombre, decimal precio_Venta, int stock, string descripcion, string imagen)
        {
            DArticulos Datos = new DArticulos();

            string existe = Datos.Existe(nombre);
            if (existe == "1")
            {
                return "El articulo ya no existe";
            }
            else
            {
                Articulos obj = new Articulos();
                obj.IdCategoria = idCategoria;
                obj.Codigo = codigo;
                obj.Nombre = nombre;
                obj.Precio_Venta = precio_Venta;
                obj.Stock = stock;
                obj.Descripcion = descripcion;
                obj.Imagen = imagen;
                return Datos.Insertar(obj);
            }
        }

        public static string Actualizar(int idarticulo,int idCategoria, string codigo, string nombre, decimal precio_Venta, int stock, string descripcion, string imagen)
        {
            DArticulos Datos = new DArticulos();

            Articulos obj = new Articulos();
            obj.IdCategoria = idCategoria;
            obj.Codigo = codigo;
            obj.Nombre = nombre;
            obj.Precio_Venta = precio_Venta;
            obj.Stock = stock;
            obj.Descripcion = descripcion;
            obj.Imagen = imagen;
            obj.IdArticulo = idarticulo;
            return Datos.Actualizar(obj);
        }

        public static string Eliminar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Eliminar(idArticulo);
        }

        public static string Activar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Activar(idArticulo);
        }

        public static string Desactivar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Desactivar(idArticulo);
        }

        
        public static DataTable categorialistar() 
        {
            DArticulos Datos = new DArticulos();
            return Datos.ArticuloCategoria();
        }
        
    }
}

