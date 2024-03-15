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
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = true;
            dgvListado.Columns[2].Visible = false;
            dgvListado.Columns[3].HeaderText = "Rol";
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
                    errorProvider1.SetError(CbRol, "Ingrese el rol del usuario");
                }
                else
                {
                    respuesta = NUsuarios.Insertar(Convert.ToInt32(CbRol.Text), TxtNombre.Text, CbUsuarios.Text, TxtNumDocumento.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text);
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
            TxtDireccion.Clear();
            TxtEmail.Clear();
            TxtClave.Clear();
            TxtBuscar.Clear();
            TxtTelefono.Clear();
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = true;
                CbRol.Text = dgvListado.CurrentRow.Cells["idrol"].Value.ToString();
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                dgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                dgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (TxtNombre.Text == string.Empty || CbRol.Text == string.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(TxtNombre, "Ingrese el nombre");
                    errorProvider1.SetError(TxtEmail, "Ingrese el correo electronico");
                    errorProvider1.SetError(TxtId, "Ingrese el rol del usuario");
                }
                else
                {
                    respuesta = NUsuarios.Actualizar(Convert.ToInt32(TxtId.Text), Convert.ToInt32(CbRol.Text), TxtNombre.Text, CbUsuarios.Text, TxtNumDocumento.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text);
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea activar este usuario?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuarios.Activar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se activó el usuario: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }
                    }
                    this.Listar();
                    ChkSeleccionar.Checked = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
    
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea desactivar este usuario?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuarios.Desactivar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se desactivó el usuario: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();
                    ChkSeleccionar.Checked = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea eliminar este usuario?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuarios.Eliminar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se eliminó el usuario: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();
                    ChkSeleccionar.Checked = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void CbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CbRol.Items.Clear();
            try
            {
                DataTable dt = NUsuarios.RolListar();
                
                foreach (DataRow fila in dt.Rows)
                {
                    var name = fila["nombre"].ToString();
                    var id = fila["idrol"].ToString();

                    if (name == CbRol.SelectedItem.ToString())
                    {
                        IdRol.Text = id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void CbRol_Click(object sender, EventArgs e)
        {
            try
            {
                CbRol.Items.Clear();

                DataTable roles = NUsuarios.RolListar();

                foreach (DataRow fila in roles.Rows)
                {
                    var name = fila["nombre"].ToString();
                     CbRol.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
