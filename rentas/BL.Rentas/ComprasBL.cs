using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ComprasBL
    {
        Contexto _contexto;

        public BindingList<Compra> ListaCompras { get; set; }

        public ComprasBL()
        {
            _contexto = new Contexto();
        }

        public BindingList<Compra> ObtenerCompras()
        {
            _contexto.Compras.Include("CompraDetalle").Load();
            ListaCompras = _contexto.Compras.Local.ToBindingList();

            return ListaCompras;
        }

        public void AgregarCompra()
        {
            var nuevaCompra = new Compra();
            _contexto.Compras.Add(nuevaCompra);
        }
        public void AgregarCompraDetalle (Compra compra)
        {
            if(compra != null)
            {
                var nuevoDetalle = new CompraDetalle();
                compra.CompraDetalle.Add(nuevoDetalle);
            }
        }

        public void RemoverCompraDetalle(Compra compra, CompraDetalle compraDetalle)
        {
            if(compra != null && compraDetalle != null)
            {
                compra.CompraDetalle.Remove(compraDetalle);
            }
        }


        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = System.Data.Entity.EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultado GuardarCompra(Compra compras)
        {
            var resultado = Validar(compras);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            CalcularExistencia(compras);


            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }

        private void CalcularExistencia(Compra compras)
        {
            foreach (var detalle in compras.CompraDetalle)
            {
                var producto = _contexto.Productos.Find(detalle.ProductoId);
                if (producto != null)
                {
                    if (compras.Activo == true)
                    {
                        producto.Existencia = producto.Existencia + detalle.Cantidad;
                    }
                    else
                    {
                        producto.Existencia = producto.Existencia - detalle.Cantidad;
                    }
                }
            }
        }

        private Resultado Validar(Compra compras)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            

            if (compras == null)
            {
                resultado.Mensaje = "Agregue una compra";
                resultado.Exitoso = false;

                return resultado;
            }
            if (compras.Id !=0 && compras.Activo == true)
            {
                resultado.Mensaje = "La Compra ya esta emitida y no se puede realizar cambios";
                resultado.Exitoso = false;



            }

            if (compras.Activo == false)
            {
                resultado.Mensaje = "La Compra esta anulada";
                resultado.Exitoso = false;



            }
            if (compras.CienteId == 0)
            {
                resultado.Mensaje = "Seleccione un cliente";
                resultado.Exitoso = false;
            }

            if (compras.CompraDetalle.Count == 0)
            {
                resultado.Mensaje = "Agregue procuto a la compra";
                resultado.Exitoso = false;
            }

            foreach (var detalle in compras.CompraDetalle)
            {
                if (detalle.ProductoId == 0)
                {
                    resultado.Mensaje = "Seleccione productos";
                    resultado.Exitoso = false;
                }
            }

            return resultado;
        }

        public void CalcularCompra (Compra compra)
        {
            if (compra != null)
            {
                double subtotal=0;

                foreach (var detalle in compra.CompraDetalle)
                {
                    var producto = _contexto.Productos.Find(detalle.ProductoId);
                    if (producto != null)
                    {
                        detalle.Precio = producto.Precio;
                        detalle.Total = detalle.Cantidad * producto.Precio;

                        subtotal += detalle.Total;
                    }
                }

                compra.Subtotal = subtotal;
                compra.Impuesto = subtotal * 0.15;
                compra.Total = subtotal + compra.Impuesto;
            }
        }

        public bool AnularCompra(int id)
        {
            foreach (var compras in ListaCompras)
            {
                if (compras.Id == id)
                {
                    compras.Activo = false;
                    CalcularExistencia(compras);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }

    public class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int CienteId { get; set; }
        public Cliente Cliente { get; set; }
        public BindingList<CompraDetalle> CompraDetalle { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public double Impuesto { get; set; }
        public bool Activo { get; set; }

        public Compra()
        {
            Fecha = DateTime.Now;
            CompraDetalle = new BindingList<CompraDetalle>();
            Activo = true;
        }
    }

    public class CompraDetalle
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public CompraDetalle()
        {
            Cantidad = 1;
        }
    }

}

