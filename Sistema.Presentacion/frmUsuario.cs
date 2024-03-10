using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Negocio;
using System.Data.SqlClient;

namespace Sistema.Presentacion
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
            prepareCbUsuarios();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {

            this.Listar();
            this.Formato();
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListado.Columns["Column1"].Index)
            {
                DataGridViewCheckBoxCell chkeliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["Column1"];
                chkeliminar.Value = !Convert.ToBoolean(chkeliminar.Value);
            }
        }
        public void Formato()
        {
            dgvListado.Columns[0].Visible = true;
            dgvListado.Columns[1].Visible = true;
            dgvListado.Columns[2].Width = 150;
            dgvListado.Columns[3].Width = 300;
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[7].Width = 250;
            dgvListado.Columns[8].Width = 100;
            dgvListado.Columns[9].Width = 100;
        }

        private void prepareCbUsuarios()
        {
            string[] documentos = { "Cedula de Identidad", "Licencia de conducir", "Pasaporte", "Acta de nacimiento", "Seguro de vida" };

            foreach (string documento in documentos)
            {
                CbUsuarios.Items.Add(documento);
            }
            CbUsuarios.SelectedIndex = 0;
        }

        private void CbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string documentoSeleccionado = CbUsuarios.SelectedItem.ToString();
        }
        private void MensajeError(string msg)
        {
            MessageBox.Show(msg, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOK(string msg)
        {
            MessageBox.Show(msg, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(TxtNombre, "Ingrese el nombre");
                    errorProvider1.SetError(TxtEmail, "Ingrese el correo electronico");
                    errorProvider1.SetError(TxtId, "Ingrese el rol del usuario");
                }
                else
                {
                    respuesta = NUsuarios.Insertar(Convert.ToInt32(TxtId.Text), TxtNombre.Text, CbUsuarios.Text, TxtNumDocumento.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text);
                    if (respuesta == "OK")
                    {
                        this.MensajeOK("El registro se insertó de manera correcta");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Buscar()
        {
            dgvListado.DataSource = NUsuarios.Buscar(TxtBuscar.Text);
            this.Formato();
            LblRegistros.Text = $"Total de Registros: {dgvListado.RowCount}";
        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NUsuarios.Listar();
                LblRegistros.Text = $"Total de Registros: {dgvListado.RowCount}";
                this.Limpiar();
                this.Formato();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Limpiar()
        {
            TxtId.Clear();
            TxtNombre.Clear();
            TxtNumDocumento.Clear();
            TxtEmail.Clear();
            TxtClave.Clear();
            TxtBuscar.Clear();
            TxtTelefono.Clear();
            CbUsuarios.Items.Clear();
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = true;
                TxtId.Text = dgvListado.CurrentRow.Cells["idusuario"].Value.ToString();
                TxtNombre.Text = dgvListado.CurrentRow.Cells["nombre"].Value.ToString();
                CbUsuarios.Text = dgvListado.CurrentRow.Cells["tipo documento"].Value.ToString();
                TxtNumDocumento.Text = dgvListado.CurrentRow.Cells["num documento"].Value.ToString();
                TxtDireccion.Text = dgvListado.CurrentRow.Cells["direccion"].Value.ToString();
                TxtTelefono.Text = dgvListado.CurrentRow.Cells["telefono"].Value.ToString();
                TxtEmail.Text = dgvListado.CurrentRow.Cells["email"].Value.ToString();
                TxtClave.Text = dgvListado.CurrentRow.Cells["Clave"].Value.ToString();
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione una celda a partir del nombre");
            }
        }


    }
}
