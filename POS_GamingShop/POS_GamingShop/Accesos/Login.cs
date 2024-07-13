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

namespace POS_GamingShop.Accesos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
            txtPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*try
            {
                using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\GamingShop_DB.mdb;Persist Security Info=false;"))
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM USUARIOS WHERE USUARIO = ? AND CONTRA = ?", con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@contra", txtPassword.Text);

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtError.Text = "";
                                switch (reader["ROL_ID"].ToString())
                                {
                                    case "1":
                                        this.Hide();
                                        new Accesos.AdminForm().Show();
                                        break;
                                    case "2":
                                        //IR DIRECTAMENTE AL INVENTARIO
                                        break;
                                    case "3":
                                        //IR DIRECTAMENTE A VENTAS
                                        break;
                                }
                            }
                            else
                            {
                                txtError.Text = "Usuario y/o contraseña son incorrectos.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }*/
            this.Hide();
            new Accesos.AdminForm().Show();
        }
    }
}
