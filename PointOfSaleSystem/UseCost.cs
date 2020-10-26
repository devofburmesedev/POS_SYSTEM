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
    public partial class UseCost : Form
    {
        public UseCost()
        {
            InitializeComponent();
        }
        String date, method;
        private void UseCost_Load(object sender, EventArgs e)
        {
           
            BindGrid(dateTimePicker1.Text.ToString(), "dailyreport");
            btnMonthly.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Aqua;
            date = dateTimePicker1.Text.ToString();
            method = "dailyreport";
           
        }

        
        private void BindGrid(String dates,String method)
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
                product.HeaderText = "အမည်";
                product.DataPropertyName = "Product";
                product.Width = 150;
                dataGridView1.Columns.Insert(1, product);
                

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "ကုန်ကျငွေ";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView1.Columns.Insert(2, price);
                DataGridViewColumn desc = new DataGridViewTextBoxColumn();
                desc.Name = "ဖော်ပြချက်";
                desc.HeaderText = "ထုတ်ယူသူအမည်";
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
              
                btnDelete.Text = "ဖြတ်မည်";
             
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
                
                SqlDataReader reader=null;
               
                try
                {
                    if (dates != null)
                    {
                       
                        if (method.Equals("default"))
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "SELECT *  FROM UseCost Where Year(DateAndTime)=@year";
                            cmd.Parameters.AddWithValue("@year", dates);
                            reader = cmd.ExecuteReader();
                            
                        }
                        else if (method.Equals("monthlyreport"))
                        {
                            string[] dateTime = dates.Split('/');
                            int year, month;
                            int.TryParse(dateTime[0],out month);
                            int.TryParse(dateTime[1], out year);
                            cmd = con.CreateCommand();
                            cmd.CommandText = "SELECT * FROM UseCost Where Year(DateAndTime)=@year and Month(DateAndTime)=@month";
                            cmd.Parameters.AddWithValue("@year",year);
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
                            cmd.CommandText = "SELECT *  FROM UseCost Where Year(DateAndTime)=@year and Month(DateAndTime)=@month and Day(DateAndTime)=@day";
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
                               
                                newRow.Cells[1].Value = reader["UC_Name"].ToString();
                                newRow.Cells[2].Value = reader["Amount"].ToString() ;
                                newRow.Cells[3].Value = reader["Description"].ToString();
                                newRow.Cells[4].Value = reader["DateAndTime"].ToString();
                                sum += Convert.ToInt32(reader["Amount"].ToString());
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
                if ( txtName.Text.ToString().Trim() != "" && txtAmount.Text.ToString().Trim() != "" && txtDes.Text.ToString().Trim()!="")
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select * From usecost Where UC_Name=@name and Amount=@amount and Description=@des";
                  
                    cmd.Parameters.AddWithValue("@name", txtName.Text.ToString().Trim());

                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@des", txtDes.Text.ToString().Trim());
                    
                    var reader = cmd.ExecuteReader();
                  
                    
                    if (!reader.HasRows)
                    {
                       
                        con.Close();
                      
                        try
                        {
                           
                            con.Open();
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Insert Into usecost(DateAndTime,UC_Name,Amount,Description) Values(@date,@name,@amount,@des)";
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@name", txtName.Text.ToString().Trim());

                            cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@des", txtDes.Text.ToString().Trim());
                            cmd.ExecuteNonQuery();

                            MessageBoxShowing.showSuccessfulMessage();
                            txtName.Text = "";
                            txtAmount.Text = "";
                            txtDes.Text = "";
                            BindGrid(date,method);
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
                        txtName.Text = "";
                        txtAmount.Text = "";
                        txtDes.Text = "";
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
            BindGrid(dateTimePicker2.Text.ToString().Trim(),"monthlyreport");
            btnDaily.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Aqua;
            btnMonthly.BackColor = Color.Lime;
            date = dateTimePicker2.Text.ToString();
            method = "monthlyreport";
            
        }

        private void btnYearly_Click(object sender, EventArgs e)
        {
            BindGrid(dateTimePicker3.Text.ToString().Trim(),"default");
            btnDaily.BackColor = Color.Aqua;
            btnMonthly.BackColor = Color.Aqua;
            btnYearly.BackColor = Color.Lime;
            date = dateTimePicker3.Text.ToString();
            method = "default";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if (e.ColumnIndex == 5)
            {DialogResult result = MessageBoxShowing.showDeleteYesNo();
            if (result == DialogResult.Yes)
            {

                String name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String amount = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                String des = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                String date = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Delete  From usecost  Where UC_Name=@name and Amount=@amount and Description=@des";
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@des", des);
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

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
       
    }
}
