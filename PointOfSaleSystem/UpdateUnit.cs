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
    public partial class UpdateUnit : Form
    {
        public UpdateUnit()
        {
            InitializeComponent();
            comboBoxUpdateUnit();
        }

        private void comboBoxUnitUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            tetUnit.Text = comboBoxUnitUpdate.SelectedItem.ToString();
        }

        private void UpdateUnit_Load(object sender, EventArgs e)
        {
            comboBoxUpdateUnit();
        }
        private void comboBoxUpdateUnit()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUnitUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT U_Name FROM Unit";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxUnitUpdate.Items.Add(reader["U_Name"].ToString());


                }
                comboBoxUnitUpdate.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void addUnit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            if ( tetUnit.Text.ToString().Trim() != null && tetUnit.Text.ToString().Trim() != "" && (addUnit.Text.ToString().Equals("ပြင်မည်")))
                {
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update Unit Set U_Name=@name Where U_Name=@nameUpdate";
                        cmd.Parameters.AddWithValue("@name", tetUnit.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@nameUpdate", comboBoxUnitUpdate.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBoxShowing.showSuccessfulUpdateMessage();
                       
                        tetUnit.Text = "";
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
}
