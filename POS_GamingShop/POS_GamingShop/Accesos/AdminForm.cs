﻿using POS_GamingShop.Formularios.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_GamingShop.Accesos
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void gestiónDeInventariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
            menuStrip1.Enabled = false;
            Inventario Inv = new Inventario();
            Inv.FormClosing += new FormClosingEventHandler(AdminForm_FormClosing);
            Inv.MdiParent = this;
            Inv.Show();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            menuStrip1.Enabled = true;
        }
    }
}
