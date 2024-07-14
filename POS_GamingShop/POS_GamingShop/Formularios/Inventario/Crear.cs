using POS_GamingShop.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_GamingShop.Formularios.Inventario
{
    public partial class Crear : Form
    {
        OleDbConnection con = new OleDbConnection();
        public Crear()
        {
            InitializeComponent();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + ".\\GamingShop_DB.mdb;Persist Security Info=false;";
        }
        public void cbbInitialize()
        {
            con.Open();
            OleDbCommand c1 = new OleDbCommand("SELECT CATEG_ID, DESCRP FROM CATEGORIAS", con);
            OleDbDataReader lector1 = c1.ExecuteReader();

            cbbCatego.Items.Add(new Categoria { Id = 0, Descripcion = "Seleccione una categoría" }); 

            while (lector1.Read())
            {
                cbbCatego.Items.Add(new Categoria
                {
                    Id = Convert.ToInt32(lector1["CATEG_ID"]),
                    Descripcion = lector1["DESCRP"].ToString()
                });
            }
            lector1.Close();
            cbbProveedor.Items.Add(new Proveedor { Id = 0, Descripcion = "Seleccione un proveedor" });
            OleDbCommand c2 = new OleDbCommand("SELECT PROVE_ID, NOMBRE FROM PROVEEDORES", con);
            OleDbDataReader lector2 = c2.ExecuteReader();
            while (lector2.Read())
            {
                cbbProveedor.Items.Add(new Proveedor
                {
                    Id = Convert.ToInt32(lector2["PROVE_ID"]),
                    Descripcion = lector2["NOMBRE"].ToString()
                });
            }
            lector2.Close();
            con.Close();
        }
        public void EnabledSave()
        {
            if (txtDescripcion.Text == "" || cbbCatego.SelectedIndex == 0 || cbbProveedor.SelectedIndex == 0 ||  txtPrecio.Value==0)
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
            }
        }
        public void Clean() {   
                   txtDescripcion.Text = "";
                   cbbCatego.SelectedIndex = 0;
                   cbbProveedor.SelectedIndex = 0;
                   txtStock.Value = 1;
                   txtPrecio.Value = 1;
                   dtFecven.Value = DateTime.Now;
                   txtImagen.Text = "";
        }

        private void Crear_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            cbbInitialize();
            Clean();
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbCatego.Focus();
            }
        }

        private void cbbCatego_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
               cbbProveedor.Focus();
            }
        }

        private void cbbProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtStock.Focus();
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPrecio.Focus();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtFecven.Focus();
            }
        }

        private void txtImagen_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            try
            {
                //OBTENER EL NUEVO ID
                con.Open();
                OleDbCommand c1 = new OleDbCommand("SELECT TOP 1 PROD_ID FROM PRODUCTOS ORDER BY PROD_ID DESC", con);
                 OleDbDataReader lector = c1.ExecuteReader();

                 string codigo = "B0001";

                 if (lector.Read() && lector["PROD_ID"] != DBNull.Value)
                 {
                     string ultimoCodigo = lector["PROD_ID"].ToString();
                     int numero = int.Parse(ultimoCodigo.Substring(1));
                     numero++;
                     codigo = $"B{numero:0000}";
                 }
                 lector.Close();
                 int categoria = 1;
                 if (cbbCatego.SelectedItem != null)
                 {
                     categoria = ((Categoria)cbbCatego.SelectedItem).Id;  
                 }
                 int proveedor = 0;
                 if (cbbProveedor.SelectedItem != null)
                 {
                     proveedor = ((Proveedor)cbbProveedor.SelectedItem).Id;
                 }
                 OleDbCommand c2 = new OleDbCommand();
                 c2.Connection = con;
                 c2.CommandText = "INSERT INTO PRODUCTOS (PROD_ID, DESCRP, CATEG_ID, PROVE_ID, STOCK, PRECIO, FECVEN, ICONO) VALUES ('" + codigo + "', '" + txtDescripcion.Text + "', " + categoria + ", " + proveedor + ", " + txtStock.Value + ", " + txtPrecio.Value + ", '" + dtFecven.Value.ToString("dd/MM/yyyy") + "', '" + txtImagen.Text + "')";
                 c2.ExecuteNonQuery();
                 con.Close();
                 Inventario.Instance.LoadData();

                MessageBox.Show("Registro guardado correctamente", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void cbbCatego_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void cbbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void txtStock_ValueChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void txtPrecio_ValueChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void dtFecven_ValueChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }

        private void txtImagen_TextChanged(object sender, EventArgs e)
        {
            EnabledSave();
        }
    }
}
