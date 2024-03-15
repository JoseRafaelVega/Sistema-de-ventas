﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public int IdUsuario { get; set; }
        public int IdRol {  get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
        
        
        private int childFormNumber = 0;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //crear un ob jeto de frmCategoria
            frmCategoria frm = new frmCategoria();
            //Se añade a un padre MDI
            frm.MdiParent = this;
            //se muestra la instancia del formulario
            frm.Show();
        }

        private void artículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulo frm = new frmArticulo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRol frm = new frmRol();
            frm.MdiParent = this;
            frm.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuario frm = new frmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = $"Usuario: "{ this.Nombre}
            {DateTime.Now};
            MessageBox.Show("Bienvenido al sistema: " + this.Nombre);


            if (this.Rol.Equals('Administrador'))
            {
                categoríasToolStripMenuItem.Enabled = true;
                ingresosToolStripMenuItem.Enabled = true;
                ventasToolStripMenuItem.Enabled = true;
                accesosToolStripMenuItem.Enabled = true;
                consultasToolStripMenuItem.Enabled = true;
                comprasToolStripMenuItem.Enabled = true;
                almacénToolStripMenuItem.Enabled = true;
            }
            else if (this.Rol.Equals('Vendedor'))
            {
                categoríasToolStripMenuItem.Enabled = false;
                ingresosToolStripMenuItem.Enabled = false;
                ventasToolStripMenuItem.Enabled = true;
                accesosToolStripMenuItem.Enabled = false;
                consultasToolStripMenuItem.Enabled = true;
                comprasToolStripMenuItem.Enabled = true;
                almacénToolStripMenuItem.Enabled = false;
            }
            else if (this.Rol.Equals('almacen'))
            {

            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Desea salir de la aplicación", MessageBoxButtons.OKCancel)

        }
    }
}
