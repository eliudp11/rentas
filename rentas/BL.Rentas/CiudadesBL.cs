using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class CiudadesBL
    {
        Contexto _contexto;

        public BindingList<Ciudad> ListaCiudades { get; set; }

        public CiudadesBL()
        {
            _contexto = new Contexto();
            ListaCiudades = new BindingList<Ciudad>();
        }

        public BindingList<Ciudad> ObtenerCiudades()
        {
            _contexto.Ciudades.Load();
            ListaCiudades = _contexto.Ciudades.Local.ToBindingList();

            return ListaCiudades;
        }
    }

    public class Ciudad
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
