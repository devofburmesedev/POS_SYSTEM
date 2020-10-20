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
    public partial class UpdatePrice : Form
    {
        public UpdatePrice()
        {
            InitializeComponent();
            productComobox();
            unitComobox();
            comboBoxUPPrice();
        }
        String u_id=null;
        String p_id=null;
        String previous_price = null;
        private void UpdatePrice_Load(object sender, EventArgs e)
        {
            productComobox();
            unitComobox();
            comboBoxUPPrice();
        }
        private void productComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdProduct;
            con.Open();
            try
            {
                comboBoxProduct.Items.Clear();
                cmdProduct = con.CreateCommand();
                cmdProduct.CommandText = "SELECT P_Name FROM Product";
                var reader = cmdProduct.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxProduct.Items.Add(reader["P_Name"].ToString());

                }
                comboBoxProduct.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void unitComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                comboBoxUnit.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT U_Name FROM Unit";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxUnit.Items.Add(reader["U_Name"].ToString());

                }
                comboBoxUnit.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }
        private void comboBoxUPPrice()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                //comboBoxUpdate.DataSource = null;
                comboBoxPriceUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT Price FROM Price Where P_id=@p_id and U_id=@u_id";
                cmdCate.Parameters.AddWithValue("@p_id", p_id);
                cmdCate.Parameters.AddWithValue("@u_id", u_id);
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxPriceUD.Items.Add(reader["Price"].ToString());


                }
                comboBoxPriceUD.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_id FROM Product WHERE P_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxProduct.SelectedItem);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    p_id = reader["P_id"].ToString();

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

        private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_id FROM Unit WHERE U_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxUnit.SelectedItem);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    u_id = reader["U_id"].ToString();

                }
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
            con.Open();
            try
            {
                if (p_id != null && u_id != null)
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Price FROM Price WHERE P_id=@p_id and U_id=@u_id";
                    cmd.Parameters.AddWithValue("@p_id", p_id);
                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            previous_price = reader["Price"].ToString();
                            comboBoxUPPrice();

                        }

                    }
                    else
                    {
                       
                    }
                }
                else
                {
                    
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

        private void btnPrice_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            if (p_id != null && u_id != null && btnPrice.Text.Equals("ပြင်မည်"))
                    {
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Update Price Set Price=@price,P_id=@p_id,U_id=@u_id  Where P_id=@p_id and U_id=@u_id and Price=@prices";
                            cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                            cmd.Parameters.AddWithValue("@p_id", p_id);
                            cmd.Parameters.AddWithValue("@u_id", u_id);
                            cmd.Parameters.AddWithValue("@prices", previous_price);
                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulUpdateMessage();
                            tetAmount.Text = "";
                            comboBoxProduct.SelectedIndex = 0;
                            comboBoxUnit.SelectedIndex = 0;

                        }
                        catch
                        {
                        }
                        finally
                        {
                            //Stocks.BindGrid();
                            comboBoxUPPrice();
                            con.Close();
                        }
                    }
        }

        private void comboBoxPriceUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            tetAmount.Text = comboBoxPriceUD.SelectedItem.ToString();
        }
    }
}
