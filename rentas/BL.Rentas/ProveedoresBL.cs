using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ProveedoresBL
    {
        Contexto _contexto;
        public BindingList<Proveedor> ListaProveedores { get; set; }

        public ProveedoresBL()
        {
            _contexto = new Contexto();

            ListaProveedores = new BindingList<Proveedor>();


        }

        public BindingList<Proveedor> ObtenerProveedores()
        {
            _contexto.Proveedores.Load();
            ListaProveedores = _contexto.Proveedores.Local.ToBindingList();

            return ListaProveedores;
        }

        public Resultado2 GuardarProveedor(Proveedor proveedor)
        {
            var resultado = Validar(proveedor);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();


            resultado.Exitoso = true;
            return resultado;
        }
        public void AgregarProveedor()
        {
            var nuevoProveedor = new Proveedor();
            ListaProveedores.Add(nuevoProveedor);
        }

        public bool EliminarProveedor(int id)
        {
            foreach (var proveedor in ListaProveedores)
            {
                if (proveedor.Id == id)
                {
                    ListaProveedores.Remove(proveedor);
                    _contexto.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        private Resultado2 Validar(Proveedor proveedor)
        {
            var resultado = new Resultado2();
            resultado.Exitoso = true;

            if (proveedor.Nombre == "")
            {
                resultado.Mensaje = "Ingrese un nombre";
                resultado.Exitoso = false;
            }
            if (proveedor.Telefono == "")
            {
                resultado.Mensaje = "Ingrese un telefono";
                resultado.Exitoso = false;
            }

            if (proveedor.Correo == "")
            {
                resultado.Mensaje = "Ingrese un correo";
                resultado.Exitoso = false;
            }
            if (proveedor.Direccion == "")
            {
                resultado.Mensaje = "Ingrese una direccion";
                resultado.Exitoso = false;
            }
            return resultado;
        }

    }
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }


        public Proveedor()
        {
            Activo = true;
        }
    }

        public class Resultado2
        {
            public bool Exitoso { get; set; }
            public string Mensaje { get; set; }
        }
    
}
