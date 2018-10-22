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
    public partial class FormClientes : Form
    {

      ClientesBL _clientes;

        public FormClientes()
        {
            InitializeComponent();

            _clientes = new ClientesBL();
            listaClientesBindingSource.DataSource = _clientes.ObtenerClientes();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            deshabilitarhabilitarbotones(true);
            eliminar(0);
        }

        private void listaClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaClientesBindingSource.EndEdit();
            var cliente =(Cliente) listaClientesBindingSource.Current;

            var resultado = _clientes.GuardarCliente(cliente);

            if(resultado.exitoso==true)
            {
                listaClientesBindingSource.ResetBindings(false);
                deshabilitarhabilitarbotones(true);
            }
            else
            {
                MessageBox.Show(resultado.mensaje);

            }
       
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clientes.agregarcliente();
            listaClientesBindingSource.MoveLast();

            deshabilitarhabilitarbotones(false);
                }
               
        private void deshabilitarhabilitarbotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButton1.Visible = !valor;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
          
            
            if (idTextBox.Text != "")
            {
              var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {

                    var id = Convert.ToInt32(idTextBox.Text);
                    eliminar(id);

                }
            }
        }

        private void eliminar(int id)
        {
           
                var resultado = _clientes.eliminarcliente(id);

                if (resultado == true)
                {
                    listaClientesBindingSource.ResetBindings(false);
                }
                else
                {
                    MessageBox.Show("No se puede elimina cliente");
                }
        }
    }
}
