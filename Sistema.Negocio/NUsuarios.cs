using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NUsuarios
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(valor);
        }

        public static string Insertar(int idrol, string nombre, string tipodocumento, string numdocumento, string direccion, string telefono, string email, string clave)
        {
            DUsuario Datos = new DUsuario();

            string existe = Datos.Existe(nombre);
            if (existe == "1")
            {
                return "El articulo ya no existe";
            }
            else
            {
                Usuarios obj = new Usuarios();
                obj.IdRol = idrol;
                obj.Nombre = nombre;
                obj.Tipo_Documento = tipodocumento;
                obj.Num_Documento = numdocumento;
                obj.Direccion = direccion;
                obj.Telefono = telefono;
                obj.Email = email;
                obj.Clave = clave;
                
                return Datos.Insertar(obj);
            }
        }

        public static string Actualizar(int idusuario, int idrol, string nombre, string tipodocumento, string numdocumento, string direccion, string telefono, string email, string clave)
        {
            DUsuario Datos = new DUsuario();

            Usuarios obj = new Usuarios();
            obj.IdUsuario = idusuario;
            obj.IdRol = idrol;
            obj.Nombre = nombre;
            obj.Tipo_Documento = tipodocumento;
            obj.Num_Documento = numdocumento;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Email = email;
            obj.Clave = clave;
            return Datos.Actualizar(obj);
        }

        public static string Eliminar(int idUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(idUsuario);
        }

        public static string Activar(int idUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(idUsuario);
        }

        public static string Desactivar(int IdUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(IdUsuario);
        }
    }
}
