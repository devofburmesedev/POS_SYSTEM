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
    public partial class RestoreInStores : Form
    {
        public RestoreInStores()
        {
            InitializeComponent();
        }

        private void RestoreInStores_Load(object sender, EventArgs e)
        {

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


                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 60;
                dataGridView1.Columns.Insert(0, id);
                DataGridViewColumn voucher = new DataGridViewTextBoxColumn();
                voucher.Name = "Id";
                voucher.HeaderText = "ဘောင်ချာနံပါတ်";
                voucher.DataPropertyName = "id";
                voucher.Width = 160;
                dataGridView1.Columns.Insert(1, voucher);

                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 150;
                dataGridView1.Columns.Insert(2, product);

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "အရေအတွက်";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView1.Columns.Insert(3, price);

                DataGridViewColumn units = new DataGridViewTextBoxColumn();
                units.Name = "unit";
                units.HeaderText = "ယူနစ်";
                units.DataPropertyName = "unit";
                units.Width = 130;
                dataGridView1.Columns.Insert(4, units);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 160;
                dataGridView1.Columns.Insert(5, date);
                
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                //btnDelete.Name = "Delete";
                btnDelete.Text = "Restore";
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
                        string[] dateTime = datetime.ToString().Split('/');
                        int day,month, year;
                        int.TryParse(dateTime[1], out day);
                        int.TryParse(dateTime[0], out month);
                        int.TryParse(dateTime[2], out year);
                       
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT * From VoucherProduct,Voucher where Voucher.V_id=@v_id and  Month(Voucher.DateAndTime)=@month and Year(Voucher.DateAndTime)=@year and Day(Voucher.DateAndTime)=@day and Voucher.V_id=VoucherProduct.V_id";
                        cmd.Parameters.AddWithValue("@v_id", data);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@day", day);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int i = 1;
                         
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;
                                newRow.Cells[1].Value = reader["V_id"].ToString();
                                newRow.Cells[2].Value = getProduct(reader["P_id"].ToString());
                                newRow.Cells[3].Value = reader["Amount"].ToString();
                                newRow.Cells[4].Value = getUnit(reader["U_id"].ToString());
                                newRow.Cells[5].Value = reader["DateAndTime"].ToString();
                                i++;
                                dataGridView1.Rows.Add(newRow);


                            }
                        }
                    }



                }
                catch
                {

                    MessageBox.Show("Error");
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

        private void txtVId1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtVId1.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && ch != 8 && ch != 46)
                e.Handled = true;
        }

        private void txtVId1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtVId1.Text.ToString() != "")
                    Convert.ToDouble(txtVId1.Text.ToString());
            }
            catch
            {
                txtVId1.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
            if (txtVId1.Text.ToString().Trim() != "")
                BindGridHistory(txtVId1.Text.ToString().Trim(),dateTimePicker1.Text);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if(e.ColumnIndex==6)
            {
                double amount = 0;
               
               

                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Amount FROM ProductInStores Where ProductInStores.P_id=@p_id and ProductInStores.U_id=@u_id and ProductInStores.S_id=@s_id ";
                    cmd.Parameters.AddWithValue("@p_id", getProductId(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    cmd.Parameters.AddWithValue("@u_id", getUnitId(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                    cmd.Parameters.AddWithValue("@s_id", getStoresId(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        amount = Convert.ToDouble(reader["Amount"].ToString());


                    }
                }

                catch
                {

                    MessageBox.Show("erroe1");
                }
                finally
                {
                    
                    con.Close();
                }
                try
                {
                    //  MessageBox.Show(getProductId(comboBoxProduct.SelectedItem.ToString())+" "+getUnitId(comboBoxUnit.SelectedItem.ToString())+" "+s_id);
                   
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Update ProductInStores Set Amount=@amount Where P_id=@p_id and U_id=@u_id and S_id=@s_id;Delete From VoucherProduct Where P_id=@p_id and U_id=@u_id and S_id=@s_id and V_id=@v_id";
                    cmd.Parameters.AddWithValue("@p_id", getProductId( dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    cmd.Parameters.AddWithValue("@u_id", getUnitId( dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                    cmd.Parameters.AddWithValue("@s_id", getStoresId( dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    cmd.Parameters.AddWithValue("@amount", Convert.ToDouble( dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())+Convert.ToDouble(amount));
                    cmd.Parameters.AddWithValue("@v_id", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(qty+" "+amount);
                    MessageBoxShowing.showSuccessfulUpdateMessage();
                }
                catch
                {


                }
                finally
                {
                    con.Close();
                    
                }
                
                double total=0;
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select Total From VoucherProduct Where V_id=@v_id";
                    cmd.Parameters.AddWithValue("@v_id", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        total += Convert.ToInt32(reader["Total"].ToString());

                    }
                }
                catch
                {
                }
                finally
                {
                    con.Close();
                }
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Update Voucher Set Total_Amount=@amount Where V_id=@v_id";
                    cmd.Parameters.AddWithValue("@v_id", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@amount", total);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                finally
                {
                    con.Close();
                }
               
            }
            if (txtVId1.Text != "")
                BindGridHistory(txtVId1.Text.ToString(), dateTimePicker1.Text);
        }
             private int getStoresId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data = 0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT S_id FROM VoucherProduct Where V_id=@name";
                cmd.Parameters.AddWithValue("@name", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = Convert.ToInt32(reader["S_id"].ToString());

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
