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
    public partial class Inventario : Form
    {
        OleDbConnection con = new OleDbConnection();
        private static Random random = new Random();
        private static Inventario instance;
        public Inventario()
        {
            InitializeComponent();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + ".\\GamingShop_DB.mdb;Persist Security Info=false;";
            instance = this;
        }
        public static Inventario Instance
        {
            get { return instance; }
        }
        public void LoadData()
        {
            try
            {
                dtInventarios.Rows.Clear();
                con.Open();
                OleDbCommand comando = new OleDbCommand("SELECT T1.PROD_ID AS ID,T1.DESCRP AS PROD, T1.CATEG_ID, T2.DESCRP AS CATEGO, T1.PROVE_ID, T3.NOMBRE AS PROVEE, T1.STOCK, T1.PRECIO, T1.FECVEN FROM  (PRODUCTOS AS T1 INNER JOIN CATEGORIAS AS T2 ON T1.CATEG_ID = T2.CATEG_ID) LEFT JOIN PROVEEDORES AS T3 ON T1.PROVE_ID = T3.PROVE_ID", con);
                OleDbDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    string proveedor = lector["PROVEE"] == DBNull.Value ? "NINGUNO" : lector["PROVEE"].ToString();
                    dtInventarios.Rows.Add(lector["ID"].ToString(), lector["PROD"].ToString(), lector["CATEGO"].ToString(), lector["CATEG_ID"].ToString(), proveedor, lector["PROVE_ID"].ToString(), lector["STOCK"].ToString(), lector["PRECIO"].ToString(), lector["FECVEN"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Inventario_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dtInventarios.CurrentRow != null)
            {
                var id = dtInventarios.CurrentRow.Cells["ID"].Value;

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Crear crear = new Crear();
           crear.ShowDialog();
        }
    }
}
