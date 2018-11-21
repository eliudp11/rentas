using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Rentas
{
    public partial class FormCompra : Form
    {
        ComprasBL _comprasBL;
        ClientesBL _clientesBL;
        ProductosBL _productosBL;



        public FormCompra()
        {
            InitializeComponent();

            _comprasBL = new ComprasBL();
            listaComprasBindingSource.DataSource = _comprasBL.ObtenerCompras();

            _clientesBL = new ClientesBL();
            listaClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();

            _productosBL = new ProductosBL();
            listaProductosBindingSource.DataSource = _productosBL.ObtenerProductos();


        }

       private void FormCompra_Load(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _comprasBL.AgregarCompra();
            listaComprasBindingSource.MoveLast();
            DeshabilitarHabilitarBotones(false);
                }

             private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;
        }

        private void listaComprasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaComprasBindingSource.EndEdit();

            var compra = (Compra)listaComprasBindingSource.Current;
            var resultado = _comprasBL.GuardarCompra(compra);

            if (resultado.Exitoso == true)
            {
                listaComprasBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Compra Guardada");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            _comprasBL.CancelarCambios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var compra = (Compra)listaComprasBindingSource.Current;
            _comprasBL.AgregarCompraDetalle(compra);

            DeshabilitarHabilitarBotones(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var compra = (Compra)listaComprasBindingSource.Current;
            var compraDetalle = (CompraDetalle)compraDetalleBindingSource.Current;

            _comprasBL.RemoverCompraDetalle(compra, compraDetalle);

            DeshabilitarHabilitarBotones(false);
        }

        private void compraDetalleDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void compraDetalleDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var compra = (Compra)listaComprasBindingSource.Current;
            _comprasBL.CalcularCompra(compra);

            listaComprasBindingSource.ResetBindings(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea anular esta compra?", "Anular", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Anular(id);
                }
            }
        }

        private void Anular(int id)
        {
            var resultado = _comprasBL.AnularCompra(id);

            if (resultado == true)
            {
                listaComprasBindingSource.ResetBindings(false);

            }
            else
            {
                MessageBox.Show("Ocurrio un error al anular compra");
            }
        }

        private void listaComprasBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var compra = (Compra)listaComprasBindingSource.Current;

            if(compra != null && compra.Id != 0 && compra.Activo == false)
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }

        }
    }
    }

