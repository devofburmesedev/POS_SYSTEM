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
using System.Data.SqlClient;
namespace PointOfSaleSystem
{
    public partial class CreditList : Form
    {
        public CreditList()
        {
            InitializeComponent();
            categoryComobox();
        }
        int p_id;
        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory1.Items.Clear();
              
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxCategory1.Items.Add(reader["C_Name"].ToString());
                    
                }
                comboBoxCategory1.SelectedIndex = 0;
               
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }
        String date, method;
        private void CreditList_Load(object sender, EventArgs e)
        {
            BindGrid(dateTimePicker1.Text.ToString(), "dailyreport");
            btnMonthly.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Aqua;
            date = dateTimePicker1.Text.ToString();
            method = "dailyreport";
            
        }
        private int getCategoryId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data = 0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT C_id FROM Category Where C_Name=@name";
                cmd.Parameters.AddWithValue("@name", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = Convert.ToInt32(reader["C_id"].ToString());

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
        private void productComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdProduct;
            con.Open();
            try
            {
                comboBoxProduct.Items.Clear();
                cmdProduct = con.CreateCommand();
                cmdProduct.CommandText = "SELECT P_Name FROM Product where C_id=@c_id";
                cmdProduct.Parameters.AddWithValue("@c_id", getCategoryId(comboBoxCategory1.SelectedItem.ToString()));
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
       
        private void BindGrid(String dates, String method)
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
                id.Width = 50;
                dataGridView1.Columns.Insert(0, id);

                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 150;
                dataGridView1.Columns.Insert(1, product);


                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "စုစုပေါင်းငွေ";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView1.Columns.Insert(2, price);
                DataGridViewColumn desc = new DataGridViewTextBoxColumn();
                desc.Name = "ဖော်ပြချက်";
                desc.HeaderText = "လက်ခံသူ";
                desc.DataPropertyName = "ဖော်ပြချက်";
                desc.Width = 150;
                dataGridView1.Columns.Insert(3, desc);
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.Name = "date";
                time.HeaderText = "ရက်စွဲ";
                time.DataPropertyName = "date";
                time.Width = 160;
                dataGridView1.Columns.Insert(4, time);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                // btnDelete.Name = "Delete";
                btnDelete.Text = "ဖြတ်မည်";
                //btnDelete.HeaderText = "ဖြတ်မည်";
                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 70;
                btnDelete.CellTemplate.Style.BackColor = Color.Aqua;

                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(5, btnDelete);
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                con.Open();

                SqlDataReader reader = null;

                try
                {
                    if (dates != null)
                    {

                        if (method.Equals("default"))
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "SELECT *  FROM creditlist Where Year(DateAndTime)=@year";
                            cmd.Parameters.AddWithValue("@year", dates);
                            reader = cmd.ExecuteReader();

                        }
                        else if (method.Equals("monthlyreport"))
                        {
                            string[] dateTime = dates.Split('/');
                            int year, month;
                            int.TryParse(dateTime[0], out month);
                            int.TryParse(dateTime[1], out year);
                            cmd = con.CreateCommand();
                            cmd.CommandText = "SELECT * FROM creditlist Where Year(DateAndTime)=@year and Month(DateAndTime)=@month";
                            cmd.Parameters.AddWithValue("@year", year);
                            cmd.Parameters.AddWithValue("@month", month);

                            reader = cmd.ExecuteReader();
                         
                        }
                        else if (method.Equals("dailyreport"))
                        {
                            string[] dateTime = dates.Split('/');
                            int year, month, day;
                            int.TryParse(dateTime[0], out day);
                            int.TryParse(dateTime[1], out month);
                            int.TryParse(dateTime[2], out year);
                            cmd = con.CreateCommand();
                            cmd.CommandText = "SELECT *  FROM creditlist Where Year(DateAndTime)=@year and Month(DateAndTime)=@month and Day(DateAndTime)=@day";
                            cmd.Parameters.AddWithValue("@year", year);
                            cmd.Parameters.AddWithValue("@month", month);
                            cmd.Parameters.AddWithValue("@day", day);
                            reader = cmd.ExecuteReader();
                           // MessageBox.Show(day+" "+month+" "+year+" "+reader.HasRows);

                        }
                        double sum = 0.0;
                        if (reader.HasRows)

                        {
                            int i = 1;
                            //MessageBox.Show(reader.HasRows+" ");
                            
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;

                                newRow.Cells[1].Value =getProduct( reader["C_id"].ToString());
                                newRow.Cells[2].Value = reader["Amount"].ToString();
                                newRow.Cells[3].Value = reader["Receiver"].ToString();
                                
                                newRow.Cells[4].Value = reader["DateAndTime"].ToString();
                                //MessageBox.Show(reader["C_id"].ToString() + " " + reader["Amount"].ToString() + " " + reader["Receiver"].ToString() + " " + reader["DateAndTime"].ToString());
                                //MessageBox.Show(reader["C_id"].ToString() + " " + getProduct(reader["C_id"].ToString()));
                                sum += Convert.ToDouble(reader["Amount"].ToString() );
                                i++;
                                dataGridView1.Rows.Add(newRow);
                            }

                        }
                        txtTotalAmount.ReadOnly = true;
                        txtTotalAmount.Text = sum.ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {
                if (comboBoxCategory1.SelectedItem.ToString()!=null && txtAmount.Text.ToString().Trim() != "" && txtRec.Text.ToString().Trim() != "")
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select * From CreditList Where C_id=@c_id and Amount=@amount and Receiver=@rec";

                    cmd.Parameters.AddWithValue("@c_id", p_id);

                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@rec", txtRec.Text.ToString().Trim());

                    var reader = cmd.ExecuteReader();
                    
                    if (!reader.HasRows)
                    {
                        con.Close();
                        try
                        {
                            con.Open();
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Insert Into CreditList(C_id,Receiver,DateAndTime,Amount) Values(@c_id,@receiver,@date,@amount)";
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                            cmd.Parameters.AddWithValue("@c_id",p_id);

                            cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@receiver", txtRec.Text.ToString().Trim());
                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulMessage();
                            comboBoxCategory1.SelectedIndex = 0;
                            txtAmount.Text = "";
                            txtRec.Text = "";
                            BindGrid(date, method);
                        }
                        catch
                        {

                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                    else
                    {
                        MessageBoxShowing.showWarningMessage();
                        comboBoxCategory1.SelectedIndex = 0;
                        txtAmount.Text = "";
                        txtRec.Text = "";
                    }
                }
                else
                {
                    MessageBoxShowing.showIncomplementMessage();
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

       
        private void btnDaily_Click(object sender, EventArgs e)
        {
            BindGrid(dateTimePicker1.Text.ToString(), "dailyreport");
            btnMonthly.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Aqua;
            btnDaily.BackColor = Color.Lime;
            date = dateTimePicker1.Text.ToString();
            method = "dailyreport";
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            BindGrid(dateTimePicker2.Text.ToString().Trim(), "monthlyreport");
            btnDaily.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Aqua;
            btnMonthly.BackColor = Color.Lime;
            date = dateTimePicker2.Text.ToString();
            method = "monthlyreport";
        }

        private void btnYearly_Click(object sender, EventArgs e)
        {
            BindGrid(dateTimePicker3.Text.ToString().Trim(), "default");
            btnDaily.BackColor = Color.Aqua;
            btnMonthly.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Lime;
            date = dateTimePicker3.Text.ToString();
            method = "default";
        }

        private void comboBoxCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            productComobox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if (e.ColumnIndex == 5)
            {
               DialogResult result = MessageBoxShowing.showDeleteYesNo();
               if (result == DialogResult.Yes)
               {

                   String name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                   String amount = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                   String rec = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                   String date = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                   con.Open();
                   try
                   {
                       cmd = con.CreateCommand();
                       cmd.CommandText = "Delete  From creditlist Where C_id=@c_id and Amount=@amount and Receiver=@rec and DateAndTime=@date";
                       cmd.Parameters.AddWithValue("@c_id", p_id);

                       cmd.Parameters.AddWithValue("@amount", amount);
                       cmd.Parameters.AddWithValue("@rec", rec);
                       cmd.Parameters.AddWithValue("@date", date);
                       cmd.ExecuteNonQuery();
                       BindGrid(date, method);

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

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text.ToString() != "")
                    Convert.ToDouble(txtAmount.Text.ToString());
            }
            catch
            {
                txtAmount.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtAmount.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && ch != 8 && ch != 46)
                e.Handled = true;
        }


        private string getProduct(string ps)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String data = null;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_Name FROM Product WHERE P_id=@id";
                cmd.Parameters.AddWithValue("@id",ps);
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

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_id FROM Product WHERE P_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxProduct.SelectedItem.ToString());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    p_id = Convert.ToInt32(reader["P_id"].ToString());

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
