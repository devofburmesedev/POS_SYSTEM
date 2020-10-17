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
    public partial class SaleLiatMainForm : Form
    {
        public SaleLiatMainForm()
        {
            InitializeComponent();
            BindGrid();
            
        }
        public static string datetimepicker = null;
       

        private void btnViewCreditList_Click(object sender, EventArgs e)
        {
            CustomerCreditLists creditList = new CustomerCreditLists();
            creditList.ShowDialog();
        }

        private void SaleLiatMainForm_Load(object sender, EventArgs e)
        {
            BindGrid();
            txtTotal.Enabled = false;
        }


        private void BindGrid()
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.White;

                style.Font = new Font("Times New Roman", 18, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 20, FontStyle.Bold);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 60;
                dataGridView1.Columns.Insert(0, id);

                DataGridViewColumn cname = new DataGridViewTextBoxColumn();
                cname.Name = "CName";
                cname.HeaderText = "ဝယ်ယူသူအမည်";
                cname.DataPropertyName = "CName";
                cname.Width = 200;
                dataGridView1.Columns.Insert(1, cname);
                
                DataGridViewColumn voucher = new DataGridViewTextBoxColumn();
                voucher.Name = "Id";
                voucher.HeaderText = "ဘောင်ချာနံပါတ်";
                voucher.DataPropertyName = "id";
                voucher.Width = 160;
                dataGridView1.Columns.Insert(2, voucher);
                DataGridViewColumn Tprice = new DataGridViewTextBoxColumn();
                Tprice.Name = "price";
                Tprice.HeaderText = "စုစုပေါင်းငွေ";
                Tprice.DataPropertyName = "price";
                Tprice.Width = 200;
                dataGridView1.Columns.Insert(3, Tprice);
                //DataGridViewColumn date = new DataGridViewTextBoxColumn();
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();

                btnDelete.Text = "ဖြတ်မည်";

                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 100;
                btnDelete.CellTemplate.Style.BackColor = Color.Aqua;

                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(4, btnDelete);
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                con.Open();
                string[] dateTime = dateTimePicker1.Text.ToString().Split('/');
                int day,month, year;
                int.TryParse(dateTime[0],out day);
                int.TryParse(dateTime[1], out month);
                int.TryParse(dateTime[2], out year);
                
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * From Voucher Where Day(DateAndTime)=@day and Month(DateAndTime)=@month and Year(DateAndTime)=@year";
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int i = 1;
                        double total = 0;
                        while (reader.Read())
                        {
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataGridView1);
                            newRow.Cells[0].Value = i;
                            
                            newRow.Cells[1].Value = reader["CustomerName"].ToString();
                            newRow.Cells[2].Value = reader["V_id"].ToString();
                            newRow.Cells[3].Value = reader["Total_Amount"].ToString();
                            total += Convert.ToInt32(reader["Total_Amount"].ToString());
                            i++;
                            dataGridView1.Rows.Add(newRow);

                        }
                        txtTotal.Text = total.ToString();

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
        
        private void btnMore_Click(object sender, EventArgs e)
        {
            datetimepicker = dateTimePicker1.Text;
            MorePage page = new MorePage(datetimepicker);
            page.Show();
        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;


            
                if (e.ColumnIndex == 4)
                {

                    String v_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                    //String product = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //String unit = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                   
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Delete  From Voucher  Where V_id=@v_id;Delete From VoucherProduct Where V_id=@V_id";
                        cmd.Parameters.AddWithValue("@v_id", v_id);

                        
                        cmd.ExecuteNonQuery();
                        BindGrid();
                        

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

             private void btnAddUser_Click(object sender, EventArgs e)
        {
            SaleLists saleList = new SaleLists();

            saleList.Show();

        }

             private void btnPayment_Click(object sender, EventArgs e)
             {
                 HistoryPayment payment = new HistoryPayment();
                 payment.Show();
             }
       

        
    }
}
