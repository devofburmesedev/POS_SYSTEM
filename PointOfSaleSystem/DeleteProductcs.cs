using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PointOfSaleSystem
{
    public partial class DeleteProductcs : Form
    {
        public DeleteProductcs()
        {
            InitializeComponent();
            comboBoxUDProduct();
            categoryComobox();
        }
        String category_id = null;
        private void DeleteProductcs_Load(object sender, EventArgs e)
        {
            comboBoxUDProduct();
            categoryComobox();
        }

        private void comboBoxCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if (comboBoxCategory2.SelectedItem != null)
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                    cmd.Parameters.AddWithValue("@name", comboBoxCategory2.SelectedItem.ToString());
                    SqlDataReader reads = cmd.ExecuteReader();
                    while (reads.Read())
                    {

                        category_id = reads["C_id"].ToString();
                        comboBoxUDProduct();
                    }
                }
                catch
                {


                }
                finally
                {

                    con.Close();
                }
            }
        }

        private void comboBoxProductUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = comboBoxProductUD.SelectedItem.ToString();

        }

        private void addProduct_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;

            String c_id = null;
            if (comboBoxCategory2.SelectedItem != null)
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                    cmd.Parameters.AddWithValue("@name", comboBoxCategory2.SelectedItem);
                    SqlDataReader reads = cmd.ExecuteReader();
                    while (reads.Read())
                    {

                        c_id = reads["C_id"].ToString();
                    }
                }
                catch
                {


                }
                finally
                {
                   
                    con.Close();
                }
            }
             if (textBox3.Text.ToString().Trim() != null && addProduct.Text.ToString().Equals("ဖြတ်မည်") && textBox3.Text.ToString().Trim() != "")
            {

                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Delete  From Product Where P_Name=@nameUpdate";
                    cmd.Parameters.AddWithValue("@nameUpdate", comboBoxProductUD.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBoxShowing.showSuccessfulDeleteMessage();
                    textBox3.Text = "";
                    comboBoxCategory2.SelectedIndex = 0;

                }
                catch
                {
                }
                finally
                {
                    comboBoxUDProduct();
                    con.Close();
                }
            }
        }
        private void comboBoxUDProduct()
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxProductUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT P_Name FROM Product Where C_id=@c_id";
                cmdCate.Parameters.AddWithValue("@c_id", category_id);

                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxProductUD.Items.Add(reader["P_Name"].ToString());


                }
                comboBoxProductUD.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }
        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {

                comboBoxCategory2.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxCategory2.Items.Add(reader["C_Name"].ToString());
                }

                comboBoxCategory2.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }
    }
}
