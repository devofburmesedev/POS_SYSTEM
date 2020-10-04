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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            //categoryComobox();
            //pileChartData();
        }
        
        private void Dashboard_Load(object sender, EventArgs e)
        {
           

            chart1.Titles.Add("Sale List");
            
            categoryComobox();
        }

        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory.Items.Clear();
               
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxCategory.Items.Add(reader["C_Name"].ToString());
                  
                }
                comboBoxCategory.SelectedIndex = 0;
               
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int category_id = 0;
            if (comboBoxCategory.SelectedItem != null)
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                    cmd.Parameters.AddWithValue("@name", comboBoxCategory.SelectedItem.ToString());
                    SqlDataReader reads = cmd.ExecuteReader();
                    while (reads.Read())
                    {

                        category_id = Convert.ToInt32(reads["C_id"].ToString());

                        pileChartData(category_id,dateTimePicker1.Text.ToString());
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

        private void pileChartData(int category_id,String date)
        {

            
           
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String data = null;
            chart1.DataSource = null;
         
            foreach (var series in chart1.Series)
               series.Points.Clear();
            con.Open();
            try
            {

                
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_Name FROM Product Where C_id=@c_id";
                cmd.Parameters.AddWithValue("@c_id",category_id);
               
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    

                    data = reader["P_Name"].ToString();
                    double sum=getTotalValue(data,date);
                    chart1.Series["s1"].IsValueShownAsLabel = true;
                    if(sum!=0)
                    chart1.Series["s1"].Points.AddXY(data,sum);
                    chart1.DataBind();
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

        private double getTotalValue(String datas,String date)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            double data = 0.0;
            con.Open();
            try
            {
                string[] dateTime = dateTimePicker1.Text.ToString().Split('/');
                int day, month, year;
                int.TryParse(dateTime[0], out day);
                int.TryParse(dateTime[1], out month);
                int.TryParse(dateTime[2], out year);
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT VoucherProduct.Amount FROM VoucherProduct,Voucher Where VoucherProduct.P_id=@id and Day(Voucher.DateAndTime)=@day and Month(Voucher.DateAndTime)=@month and Year(Voucher.DateAndTime)=@year and VoucherProduct.V_id=Voucher.V_id";
                cmd.Parameters.AddWithValue("@day", day);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@id", getProductId(datas));
                SqlDataReader reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {

                    data += Convert.ToDouble(reader["Amount"].ToString());
                    
                }

            }
            catch
            {


            }

               
            finally
            {
                con.Close();
            }
            return data;
        }
        private int getProductId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data = 0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_id FROM Product Where P_Name=@name";
                cmd.Parameters.AddWithValue("@name", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = Convert.ToInt32(reader["P_id"].ToString());

                }

            }
            catch
            {


            }

            finally
            {
                con.Close();
            }
            return data;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int category_id = 0;
            if (comboBoxCategory.SelectedItem != null)
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                    cmd.Parameters.AddWithValue("@name", comboBoxCategory.SelectedItem.ToString());
                    SqlDataReader reads = cmd.ExecuteReader();
                    while (reads.Read())
                    {

                        category_id = Convert.ToInt32(reads["C_id"].ToString());

                        pileChartData(category_id, dateTimePicker1.Text.ToString());
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

        
        }

    }

