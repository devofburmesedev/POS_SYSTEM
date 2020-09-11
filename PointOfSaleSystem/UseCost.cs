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
namespace PointOfSaleSystem
{
    public partial class UseCost : Form
    {
        public UseCost()
        {
            InitializeComponent();
        }

        private void UseCost_Load(object sender, EventArgs e)
        {
            BindGrid("2020","default");
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
                style.Font = new Font("Times New Roman", 20, FontStyle.Bold);


                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 16);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 93;
                dataGridView1.Columns.Insert(0, id);
                
                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "အမည်";
                product.DataPropertyName = "Product";
                product.Width = 260;
                dataGridView1.Columns.Insert(1, product);
                

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "ကုန်ကျငွေ";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(2, price);
                DataGridViewColumn desc = new DataGridViewTextBoxColumn();
                desc.Name = "ဖော်ပြချက်";
                desc.HeaderText = "ဖော်ပြချက်";
                desc.DataPropertyName = "ဖော်ပြချက်";
                desc.Width = 240;
                dataGridView1.Columns.Insert(3, desc);
               

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
               // btnDelete.Name = "Delete";
                btnDelete.Text = "ဖြတ်မည်";
                //btnDelete.HeaderText = "ဖြတ်မည်";
                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 90;
                btnDelete.CellTemplate.Style.BackColor = Color.Black;

                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(4, btnDelete);
                dataGridView1.DataSource = null;
                MySqlConnection con = new MyConnection().GetConnection();
                MySqlCommand cmd;
                con.Open();
                
                MySqlDataReader reader=null;
               
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
                            ;
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
                        
                        if (reader.HasRows)
                        {   
                            int i = 1,sum=0;
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;
                               
                                newRow.Cells[1].Value = reader["UC_Name"].ToString();
                                newRow.Cells[2].Value = reader["Amount"].ToString() ;
                                newRow.Cells[3].Value = reader["Description"].ToString();
                                sum += Convert.ToInt32(reader["Amount"].ToString());
                                i++;
                                dataGridView1.Rows.Add(newRow);


                            }
                            txtTotalAmount.ReadOnly = true;
                            txtTotalAmount.Text = sum.ToString();
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

        private void btnDaily_Click(object sender, EventArgs e)
        {
            BindGrid(txtDaily.Text.ToString().Trim(),"dailyreport");
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {
                if ( txtName.Text.ToString().Trim() != "" && txtAmount.Text.ToString().Trim() != "" && txtDes.Text.ToString().Trim()!="")
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select * From UseCost Where UC_Name=@name and Amount=@amount and Description=@des";
                  
                    cmd.Parameters.AddWithValue("@name", txtName.Text.ToString().Trim());

                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@des", txtDes.Text.ToString().Trim());
                    
                    var reader = cmd.ExecuteReader();
                    con.Close();
                    if (!reader.HasRows)
                    {
                        try
                        {
                            con.Open();
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Insert Into UseCost(DateAndTime,UC_Name,Amount,Description) Values(@date,@name,@amount,@des)";
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                            cmd.Parameters.AddWithValue("@name", txtName.Text.ToString().Trim());

                            cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@des", txtDes.Text.ToString().Trim());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                            txtName.Text = "";
                            txtAmount.Text = "";
                            txtDes.Text = "";
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
                        MessageBox.Show("သင်ထည့်သောဒေတာမာထည့်ပြီးသားဖြစ်ပါသည်", "သတိပေးချက်");
                        txtName.Text = "";
                        txtAmount.Text = "";
                        txtDes.Text = "";
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

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            BindGrid(txtMonthly.Text.ToString().Trim(),"monthlyreport");
            
        }

        private void btnYearly_Click(object sender, EventArgs e)
        {
            BindGrid(txtYearly.Text.ToString().Trim(),"default");
        }

       
       
    }
}
