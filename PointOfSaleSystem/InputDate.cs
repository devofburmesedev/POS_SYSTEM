using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointOfSaleSystem
{
    public partial class InputDate : Form
    {
        public InputDate()
        {
            InitializeComponent();
        }
        public static String month, year;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
             month = textBox1.Text.ToString();
            year = textBox2.Text.ToString();
            this.Close();
        }

        private void textBox1_keyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox2_keyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void InputDate_Load(object sender, EventArgs e)
        {
            month = "";
            year = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
