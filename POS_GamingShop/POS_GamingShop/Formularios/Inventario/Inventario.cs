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
        public Inventario()
        {
            InitializeComponent();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + ".\\GamingShop_DB.mdb;Persist Security Info=false;";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Inventario_Load(object sender, EventArgs e)
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
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dtInventarios.CurrentRow != null)
            {
                var id = dtInventarios.CurrentRow.Cells["ID"].Value;

               /* con.Open();
                OleDbCommand comando = new OleDbCommand("SELECT TOP 1 PROD_ID FROM PRODUCTOS ORDER BY PROD_ID DESC", con);
                OleDbDataReader lector = comando.ExecuteReader();

                string nuevoCodigo = "B0001";

                if (lector.Read() && lector["PROD_ID"] != DBNull.Value)
                {
                    string ultimoCodigo = lector["PROD_ID"].ToString();
                    int numero = int.Parse(ultimoCodigo.Substring(1)); 
                    numero++;
                    nuevoCodigo = $"B{numero:0000}"; 
                }
                lector.Close();
                con.Close();*/

            }
        }
    }
}
