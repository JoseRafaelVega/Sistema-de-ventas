using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Negocio;
using BarcodeLib;


namespace Sistema.Presentacion
{
    public partial class frmArticulo : Form
    {
        public frmArticulo()
        {
            InitializeComponent();


        }

        public void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = true;
            dgvListado.Columns[2].Width = 150;
            dgvListado.Columns[3].Width = 300;
            dgvListado.Columns[3].Width = 100;
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[8].Width = 250;
            dgvListado.Columns[8].HeaderText = "Descripción";
            dgvListado.Columns[9].Width = 100;
            
            
        }
        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkeliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkeliminar.Value = !Convert.ToBoolean(chkeliminar.Value);
            }
        }
        private void Buscar()
        {
            dgvListado.DataSource = NArticulos.Buscar(TxtBuscar.Text);
            this.Formato();
            label1.Text = $"Total de Registros: {dgvListado.RowCount}";
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }
        private void MensajeError(string msg)
        {
            MessageBox.Show(msg, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOK(string msg)
        {
            MessageBox.Show(msg, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Limpiar()
        {
            TxtBuscar.Clear();
            txtDescripcion.Clear();
            txtId.Clear();
            BtnInsertar.Visible = true;
            errorProvider1.Clear();
            txtNombre.Clear();
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            ChkSeleccionar.Checked = false;
            TxtCodigo.Clear();
            txtDescripcion.Clear();
            TxtPrecio.Clear();
            TxtStock.Clear();
            CmbCategoria.Items.Clear();
            txtImagen.Clear();
        }
        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NArticulos.Listar();
                label1.Text = $"Total de Registros: {dgvListado.RowCount}";
                this.Limpiar();
                this.Formato();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtId.Clear();
                DataTable dt = new DataTable();
                dt = NArticulos.categorialistar();
                foreach (DataRow dr in dt.Rows)
                {
                    var id = dr["idcategoria"];
                    var name = dr["nombre"].ToString();
                    if (name == CmbCategoria.SelectedItem.ToString())
                    {
                        txtId.Text = id.ToString();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CmbCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                CmbCategoria.Items.Clear();
                DataTable dt = new DataTable();
                dt = NArticulos.categorialistar();
                foreach (DataRow dr in dt.Rows)
                {
                    var name = dr["nombre"].ToString();
                    CmbCategoria.Items.Add(name);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            Barcode codigobarras = new Barcode();
            codigobarras.IncludeLabel = true;
            panel1.BackgroundImage = codigobarras.Encode(
                BarcodeLib.TYPE.CODE128,
                TxtCodigo.Text,
                Color.Black,
                Color.White
                );

        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(txtNombre, "Ingrese el nombre");
                    errorProvider1.SetError(CmbCategoria, "Ingrese una categoria");
                }
                else
                {
                    respuesta = NArticulos.Insertar(Convert.ToInt32(txtId.Text), TxtCodigo.Text, txtNombre.Text, Convert.ToDecimal(TxtPrecio.Text), Convert.ToInt32(TxtStock.Text), txtDescripcion.Text, txtImagen.Text);
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
            tabControl1.SelectedIndex = 0;
        }

        private void  button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog safi = new OpenFileDialog();

            safi.Filter = "image files (.jpg, .jpge, .png) | *.jpg; *.jpge; *.png";
            if (safi.ShowDialog() == DialogResult.OK)
            {
                picbox.Image = Image.FromFile(safi.FileName);
                txtImagen.Text = safi.FileName;
                txtImagen.Text = safi.FileName.Substring(safi.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Image imgbarras = (Image)panel1.BackgroundImage.Clone();
            OpenFileDialog abrirarch = new OpenFileDialog();

            abrirarch.AddExtension = true;
            abrirarch.Filter = "png image(.png) | *.png*";
            abrirarch.ShowDialog();

            if (!string.IsNullOrEmpty(abrirarch.FileName))
            {
                imgbarras.Save(abrirarch.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            imgbarras.Dispose();
        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            
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

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea activar esta categoria?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Activar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se activó la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();
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
                DialogResult opcion = MessageBox.Show("Desea desactivar esta categoria?", "Sistema de ventas-desactivar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Desactivar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se desactivó la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();
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
                DialogResult opcion = MessageBox.Show("Desea eliminar esta categoria?", "Sistema de ventas-eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Eliminar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"Se eliminó la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtNombre.Text == string.Empty || txtId.Text == String.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(txtNombre, "Ingrese el nombre");
                    errorProvider1.SetError(CmbCategoria, "Ingrese una categoria");
                }
                else
                {
                    respuesta = NArticulos.Actualizar(Convert.ToInt32(TxtIdArticulo.Text), int.Parse(txtId.Text), TxtCodigo.Text, txtNombre.Text, decimal.Parse(TxtPrecio.Text), int.Parse(TxtStock.Text), txtDescripcion.Text, txtImagen.Text);
                    if (respuesta == "OK")
                    {
                        this.MensajeOK("El registro se actualizó de manera correcta");
                        this.Listar();
                        this.Limpiar();
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

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgvListado_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                TxtIdArticulo.Text = dgvListado.CurrentRow.Cells["ID"].Value.ToString();
                txtId.Text = dgvListado.CurrentRow.Cells["idcategoria"].Value.ToString();
                TxtCodigo.Text = dgvListado.CurrentRow.Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                TxtPrecio.Text = dgvListado.CurrentRow.Cells["Precio_Venta"].Value.ToString();
                TxtStock.Text = dgvListado.CurrentRow.Cells["Stock"].Value.ToString();
                txtDescripcion.Text = dgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtImagen.Text = dgvListado.CurrentRow.Cells["Imagen"].Value.ToString();
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione una celda a partir del nombre");
            }
        }
    }
}
