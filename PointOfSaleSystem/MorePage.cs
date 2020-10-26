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
    public partial class MorePage : Form
    {
        private string datetime=null;
        public MorePage(String date)
        {
            InitializeComponent();
            this.datetime = date;
            //BindGridData();
           
        }
        
        private void MorePage_Load(object sender, EventArgs e)
        {
            BindGridData();
            BindGrid();
        }
        private void BindGridData()
        {
            try
            {
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.Columns.Clear();
                DataGridViewCellStyle style = dataGridView2.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.Green;
                style.Font = new Font("Times New Roman", 14, FontStyle.Bold);


                dataGridView2.DefaultCellStyle.Font = new Font("Times New Roman", 12);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 50;
                dataGridView2.Columns.Insert(0, id);

                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 150;
                dataGridView2.Columns.Insert(1, product);
                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "တန်ဖိုးငွေကျပ်";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView2.Columns.Insert(2, price);
                DataGridViewColumn qty = new DataGridViewTextBoxColumn();
                qty.Name = "qty";
                qty.HeaderText = "အရေအတွက်";
                qty.DataPropertyName = "qty";
                qty.Width = 150;
                dataGridView2.Columns.Insert(3, qty);

                DataGridViewColumn amount = new DataGridViewTextBoxColumn();
                amount.Name = "amount";
                amount.HeaderText = "ပမာဏ";
                amount.DataPropertyName = "amount";
                amount.Width = 100;
                dataGridView2.Columns.Insert(4, amount);
                /*DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();

                btnDelete.Text = "ဖြတ်မည်";

                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 100;
                btnDelete.CellTemplate.Style.BackColor = Color.Aqua;

                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(5, btnDelete);*/
                dataGridView2.DataSource = null;
                string[] dateTime = datetime.Split('/');
                int day, month, year;
                int.TryParse(dateTime[0], out day);
                int.TryParse(dateTime[1], out month);
                int.TryParse(dateTime[2], out year);
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                
                
                con.Open();
                try
                {
                   
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * From VoucherProduct,Voucher Where  Day(Voucher.DateAndTime)=@day and Month(Voucher.DateAndTime)=@month and Year(Voucher.DateAndTime)=@year and Voucher.V_id=VoucherProduct.V_id";
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        
                        int i = 1;
                        while (reader.Read())
                        {
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(dataGridView2);
                            newRow.Cells[0].Value = i;

                            newRow.Cells[1].Value = getProduct(reader["P_id"].ToString());

                            newRow.Cells[2].Value = reader["Price"].ToString();
                            newRow.Cells[3].Value = reader["Amount"].ToString() + " " + getUnit(reader["U_id"].ToString());
                            newRow.Cells[4].Value = reader["Total_Amount"].ToString();
                            i++;
                            dataGridView2.Rows.Add(newRow);

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
        private void BindGrid( )
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.White;

                style.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 60;
                dataGridView1.Columns.Insert(0, id);
                DataGridViewColumn voucher = new DataGridViewTextBoxColumn();
                voucher.Name = "Id";
                voucher.HeaderText = "ဘောင်ချာနံပါတ်";
                voucher.Width = 150;
                dataGridView1.Columns.Insert(1, voucher);
                DataGridViewColumn cname = new DataGridViewTextBoxColumn();
                cname.Name = "CName";
                cname.HeaderText = "ဝယ်ယူသူအမည်";
                cname.DataPropertyName = "CName";
                cname.Width = 150;
                dataGridView1.Columns.Insert(2, cname);

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "ပေးချေငွေ";
                price.DataPropertyName = "price";
                price.Width = 150;
                dataGridView1.Columns.Insert(3, price);
                DataGridViewColumn Tprice = new DataGridViewTextBoxColumn();
                Tprice.Name = "price";
                Tprice.HeaderText = "စုစုပေါင်းငွေ";
                Tprice.DataPropertyName = "price";
                Tprice.Width = 150;
                dataGridView1.Columns.Insert(4, Tprice);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 160;
                dataGridView1.Columns.Insert(5, date);
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                string[] dateTime = datetime.Split('/');
                int day, month, year;
                int.TryParse(dateTime[0], out day);
                int.TryParse(dateTime[1], out month);
                int.TryParse(dateTime[2], out year);
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * From Voucher Where Day(DateAndTime)=@day and Month(DateAndTime)=@month and Year(DateAndTime)=@year";
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 1;

                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {



                            DataGridViewRow newRows = new DataGridViewRow();
                            newRows.CreateCells(dataGridView1);
                            newRows.Cells[0].Value = i;
                            newRows.Cells[1].Value = reader["V_id"].ToString();
                            newRows.Cells[2].Value = reader["CustomerName"].ToString();
                            newRows.Cells[3].Value = reader["Paid_Amount"].ToString();
                            newRows.Cells[4].Value = reader["Total_Amount"].ToString();
                            newRows.Cells[5].Value = reader["DateAndTime"].ToString();
                            i++;
                            dataGridView1.Rows.Add(newRows);
                          
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
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
