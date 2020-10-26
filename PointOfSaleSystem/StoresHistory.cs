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
    public partial class StoresHistory : Form
    {
        public StoresHistory()
        {
            InitializeComponent();
        }

        private void StoresHistory_Load(object sender, EventArgs e)
        {
            storeComobox();
            if(comboBoxStores.DataSource!=null)
                BindGridHistory(comboBoxStores.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
        }
        private void BindGridHistory(String data, String datetime)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.Green;
                style.Font = new Font("Times New Roman", 14, FontStyle.Bold);


                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 14);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 60;
                dataGridView1.Columns.Insert(0, id);


                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 150;
                dataGridView1.Columns.Insert(1, product);

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "အရေအတွက်";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView1.Columns.Insert(2, price);

                DataGridViewColumn units = new DataGridViewTextBoxColumn();
                units.Name = "unit";
                units.HeaderText = "ယူနစ်";
                units.DataPropertyName = "unit";
                units.Width = 130;
                dataGridView1.Columns.Insert(3, units);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 120;
                dataGridView1.Columns.Insert(4, date);
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.Name = "date";
                time.HeaderText = "ေန့စွဲ";
                time.DataPropertyName = "date";
                time.Width = 120;
                dataGridView1.Columns.Insert(5, time);
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                //btnDelete.Name = "Delete";
                btnDelete.Text = "ဖြတ်မည်";
                // btnDelete.HeaderText = "ဖြတ်မည်";
                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 100;
                btnDelete.CellTemplate.Style.BackColor = Color.Aqua;

                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(6, btnDelete);
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                con.Open();
                try
                {
                    {
                        string[] dateTime = dateTimePicker2.Text.ToString().Split('/');
                        int month, year;
                        int.TryParse(dateTime[0], out month);
                        int.TryParse(dateTime[1], out year);
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT * From HistoryProductInStores,Stores where Stores.Name=@name and  Month(HistoryProductInStores.DateAndTime)=@month and Year(HistoryProductInStores.DateAndTime)=@year and Stores.S_id=HistoryProductInStores.S_id";
                        cmd.Parameters.AddWithValue("@name", data);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;

                                newRow.Cells[1].Value = getProduct(reader["P_id"].ToString());
                                newRow.Cells[3].Value = getUnit(reader["U_id"].ToString());
                                newRow.Cells[2].Value = reader["Amount"].ToString();
                                String[] datetimes = (reader["DateAndTime"].ToString()).Split(' ');
                                String dates, times;
                                dates = datetimes[0];
                                times = datetimes[1];
                                newRow.Cells[4].Value = dates;
                                newRow.Cells[5].Value = times;
                                i++;
                                dataGridView1.Rows.Add(newRow);


                            }
                        }
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
            catch
            {

            }

        }
        private void storeComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                
                comboBoxStores.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT Name FROM Stores";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                   
                    comboBoxStores.Items.Add(reader["Name"].ToString());

                }
                
                comboBoxStores.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }
        private string getUnit(string p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String data = null;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_Name FROM Unit Where U_id=@u_id ";
                cmd.Parameters.AddWithValue("@u_id", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = reader["U_Name"].ToString();

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

        private string getProduct(string p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String data = null;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_Name FROM Product Where P_id=@p_id ";
                cmd.Parameters.AddWithValue("@p_id", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = reader["P_Name"].ToString();

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

        private void comboBoxStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridHistory(comboBoxStores.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            BindGridHistory(comboBoxStores.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;


            if (e.ColumnIndex == 6)
            {
                DialogResult result = MessageBoxShowing.showDeleteYesNo();
                if (result == DialogResult.Yes)
                {

                    int product = getProductId(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    int unit = getUnitId(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    String amount = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    String date = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    String time = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    String datetime = date + " " + time;
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Delete  From HistoryProductInStores  Where P_id=@p_id and U_id=@u_id and Amount=@amount and DateAndTime=@date";
                        cmd.Parameters.AddWithValue("@p_id", product);

                        cmd.Parameters.AddWithValue("@u_id", unit);

                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@date", datetime);
                        cmd.ExecuteNonQuery();

                        BindGridHistory(comboBoxStores.SelectedItem.ToString(), dateTimePicker2.Text);
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
        private int getUnitId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data = 0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_id FROM Unit Where U_Name=@name";
                cmd.Parameters.AddWithValue("@name", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = Convert.ToInt32(reader["U_id"].ToString());

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

        }

    }

