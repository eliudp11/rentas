using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ClientesBL

    {
       public BindingList<Cliente> ListaClientes { get; set; }

        public ClientesBL()
        {
            ListaClientes = new BindingList<Cliente>();

            var cliente1 = new Cliente();
            cliente1.id = 1;
            cliente1.nombre = "Juan";
            cliente1.correo = "juan@gmail.com";
            cliente1.telefono = "99999999";
            cliente1.direccion = "Bo. san fernando, sps";
            cliente1.activo = true;

            ListaClientes.Add(cliente1);

            var cliente2 = new Cliente();
            cliente2.id = 2;
            cliente2.nombre = "Maria";
            cliente2.correo = "maria@gmail.com";
            cliente2.telefono = "55555555";
            cliente2.direccion = "La Ceiba";
            cliente2.activo = true;

            ListaClientes.Add(cliente2);

            var cliente3 = new Cliente();
            cliente3.id = 3;
            cliente3.nombre = "Carlos";
            cliente3.correo = "carlos@gmail.com";
            cliente3.telefono = "22222222";
            cliente3.direccion = "Tegucigalpa";
            cliente3.activo = true;

            ListaClientes.Add(cliente3);

            var cliente4 = new Cliente();
            cliente4.id = 4;
            cliente4.nombre = "Victoria";
            cliente4.correo = "victoria@gmail.com";
            cliente4.telefono = "78457845";
            cliente4.direccion = "SPS";
            cliente4.activo = true;

            ListaClientes.Add(cliente4);

            var cliente5 = new Cliente();
            cliente5.id = 5;
            cliente5.nombre = "Pedro";
            cliente5.correo = "pedro@gmail.com";
            cliente5.telefono = "12561256";
            cliente5.direccion = "Santa Barbara";
            cliente5.activo = true;

            ListaClientes.Add(cliente5);
        }

        public BindingList<Cliente> ObtenerClientes()
        {
            return ListaClientes;
        }

        public Resultado2 GuardarCliente(Cliente cliente)
        {
            var resultado = validar(cliente);
            if(resultado.exitoso=false)
            {
                return resultado;
            }
            if (cliente.id==0)
            {
                cliente.id = ListaClientes.Max(item => item.id) + 1;
            }
            resultado.exitoso = true;
            return resultado;
        }
        public void agregarcliente()
        {
            var nuevocliente = new Cliente();
            ListaClientes.Add(nuevocliente);
        }

        public bool eliminarcliente(int Id)
        {
            foreach (var cliente in ListaClientes)
            {
                if(cliente.id==Id)
                {
                    ListaClientes.Remove(cliente);
                    return true;
                }

            }

            return false;
        }

        private Resultado2 validar(Cliente cliente)
        {
            var resultado = new Resultado2();
            resultado.exitoso = true;

            if(cliente.nombre=="")
            {
                resultado.mensaje = "Ingrese un nombre";
                resultado.exitoso = false;
            }
            if (cliente.correo == "")
            {
                resultado.mensaje = "Ingrese un correo";
                resultado.exitoso = false;
            }

            if (cliente.telefono == "")
            {
                resultado.mensaje = "Ingrese un telefono";
                resultado.exitoso = false;
            }
            if (cliente.direccion == "")
            {
                resultado.mensaje = "Ingrese una direccion";
                resultado.exitoso = false;
            }
            return resultado;
        }
    }



    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public bool activo { get; set; }

    }

    public class Resultado2
    {
        public bool exitoso { get; set; }
        public string mensaje { get; set; }
    }
}
