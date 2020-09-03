using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace PointOfSaleSystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private Dashboard dForm = null;
        private Stocks sForm = null;
        private BuyList bForm = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.TopLevel = true;
            this.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Arrow;
            viewForm();
            
        }

        private void viewForm()
        {
            if (this.dForm == null)
            {
                dForm = new Dashboard();
                dForm.TopLevel = false;
                panel1.Controls.Add(dForm);
                dForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                dForm.Dock = DockStyle.Fill;
                dForm.Show();
            }
            else
            {
                dForm.BringToFront();
            }
        }

     

        
        private void button1_Click(object sender, EventArgs e)
        {
            viewForm();
        }
      

       
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (sForm == null)
            {
                sForm = new Stocks();
                sForm.TopLevel = false;
                panel1.Controls.Add(sForm);
                sForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                sForm.Dock = DockStyle.Fill;
                sForm.Show();
            }
            else
            {
                sForm.BringToFront();
            }
            //viewForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bForm == null)
            {
                bForm = new BuyList();
                bForm.TopLevel = false;
                panel1.Controls.Add(bForm);
                bForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                bForm.Dock = DockStyle.Fill;
                bForm.Show();
            }
            else
            {
                bForm.BringToFront();
            }

        }
    }
}
