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
        int c_id;
        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory1.Items.Clear();
              
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
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
        private void BindGrid(String dates, String method)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.Green;
                style.Font = new Font("Times New Roman", 18, FontStyle.Bold);


                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 16);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 93;
                dataGridView1.Columns.Insert(0, id);

                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းအမျိုးအစား";
                product.DataPropertyName = "Product";
                product.Width = 260;
                dataGridView1.Columns.Insert(1, product);


                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "ပမာဏ";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(2, price);
                DataGridViewColumn desc = new DataGridViewTextBoxColumn();
                desc.Name = "ဖော်ပြချက်";
                desc.HeaderText = "လက်ခံသူ";
                desc.DataPropertyName = "ဖော်ပြချက်";
                desc.Width = 240;
                dataGridView1.Columns.Insert(3, desc);
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.Name = "date";
                time.HeaderText = "ရက်စွဲ";
                time.DataPropertyName = "date";
                time.Width = 100;
                dataGridView1.Columns.Insert(4, time);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                // btnDelete.Name = "Delete";
                btnDelete.Text = "ဖြတ်မည်";
                //btnDelete.HeaderText = "ဖြတ်မည်";
                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 90;
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
                            cmd.CommandText = "SELECT *  FROM CreditList Where Year(DateAndTime)=@year";
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
                            cmd.CommandText = "SELECT * FROM CreditList Where Year(DateAndTime)=@year and Month(DateAndTime)=@month";
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
                            cmd.CommandText = "SELECT *  FROM CreditList Where Year(DateAndTime)=@year and Month(DateAndTime)=@month and Day(DateAndTime)=@day";
                            cmd.Parameters.AddWithValue("@year", year);
                            cmd.Parameters.AddWithValue("@month", month);
                            cmd.Parameters.AddWithValue("@day", day);
                            reader = cmd.ExecuteReader();

                        }
                        int i = 1;
                        double sum = 0.0;
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;

                                newRow.Cells[1].Value = reader["C_id"].ToString();
                                newRow.Cells[2].Value = reader["Amount"].ToString();
                                newRow.Cells[3].Value = reader["Receiver"].ToString();
                                sum += Convert.ToInt32(reader["Amount"].ToString());
                                newRow.Cells[4].Value = reader["DateAndTime"].ToString();
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

                    cmd.Parameters.AddWithValue("@c_id", c_id);

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
                            cmd.Parameters.AddWithValue("@c_id",c_id);

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
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxCategory1.SelectedItem.ToString());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    c_id =Convert.ToInt32( reader["C_id"].ToString());

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if (e.ColumnIndex == 5)
            {
                String name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String amount = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                String rec = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                String date= dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Delete  From creditlist Where C_id=@c_id and Amount=@amount and Receiver=@rec and DateAndTime=@date";
                    cmd.Parameters.AddWithValue("@c_id", c_id);

                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@rec", rec);
                    cmd.Parameters.AddWithValue("@date",date );
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
}
