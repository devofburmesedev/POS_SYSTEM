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
    public partial class UpdateProductCategory : Form
    {
        public UpdateProductCategory()
        {
            InitializeComponent();
            comoBoxUpdateCategory();
        }

        private void tetCategory_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            tetCategory.Text = comboBoxUpdate.SelectedItem.ToString();
        }

        private void UpdateProductCategory_Load(object sender, EventArgs e)
        {
            comoBoxUpdateCategory();
        }
        private void comoBoxUpdateCategory()
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxUpdate.Items.Add(reader["C_Name"].ToString());


                }
                comboBoxUpdate.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
             if ( tetCategory.Text.ToString().Trim() != null && tetCategory.Text.ToString().Trim() != "" && (btnCategory.Text.ToString().Equals("ပြင်မည်")))
                {

                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update Category Set C_Name=@name Where C_Name=@nameUpdate";
                        cmd.Parameters.AddWithValue("@name", tetCategory.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@nameUpdate", comboBoxUpdate.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBoxShowing.showSuccessfulUpdateMessage();

                        tetCategory.Text = " ";
                    }
                    catch
                    {
                    }
                    finally
                    {
                        comoBoxUpdateCategory();
                        con.Close();
                    }
                }
        }
    }
}
