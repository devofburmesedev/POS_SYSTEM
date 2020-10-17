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
    public partial class HistoryPayment : Form
    {
        public HistoryPayment()
        {
            
            
           
           InitializeComponent();
           VoucherId();

           //CustomerName(comboBoxHistoryPayment.SelectedItem.ToString());
           if (comboBoxSelection.DataSource != null)
               BindGrid(Convert.ToInt32(comboBoxSelection.SelectedItem.ToString()));
        }
        double totalPaidAmount = 0,realPaidAmount=0;
        double total = 0;
        private void btnPayment_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd,cmdCate;
            DateTime date=DateTime.Now.Date;
            String v_id=comboBoxHistoryPayment.SelectedItem.ToString();
            con.Open();
            bool condition = false;
            
            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM HistoryPayment WHERE V_id=@v_id and Amount=@p_amount and DateAndTime=@datetime";
                cmdCate.Parameters.AddWithValue("@p_amount", txtPaidAmount.Text);

                cmdCate.Parameters.AddWithValue("@datetime", date);
                cmdCate.Parameters.AddWithValue("@v_id", v_id);
                var reader = cmdCate.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;

                }
                else
                {

                    MessageBoxShowing.showWarningMessage();
                    
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

                if ((txtPaidAmount.Text.ToString().Trim() == "" || comboBoxHistoryPayment.SelectedItem==null) && condition )
                    MessageBoxShowing.showIncomplementMessage();
                else if (txtPaidAmount.Text.ToString().Trim() != "" && comboBoxHistoryPayment.SelectedItem.ToString().Trim() != null && condition)
                {
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO HistoryPayment(V_id,Amount,DateAndTime) VALUES(@v_id,@p_amount,@datetime)";
                        
                        cmd.Parameters.AddWithValue("@p_amount", txtPaidAmount.Text);

                        cmd.Parameters.AddWithValue("@datetime",date);
                        cmd.Parameters.AddWithValue("@v_id",v_id );
                        cmd.ExecuteNonQuery();
                        BindGrid(Convert.ToInt32(comboBoxSelection.SelectedItem.ToString()));
                        MessageBoxShowing.showSuccessfulMessage();
                        txtPaidAmount.Text = "";
                        comboBoxHistoryPayment.SelectedIndex = 0;
                        comboBoxCustomerName.SelectedIndex=0;
                    }
                    catch
                    {
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
            
            if (realPaidAmount == total)

            {
                con.Open();

                try
                {

                    
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Update Voucher Set isCredit=@credit,Paid_Amount=@p_amount,DateAndTime=@date Where V_id=@v_id;Delete From HistoryPayment Where V_id=@v_id";
                    cmd.Parameters.AddWithValue("@credit", "false");
                    cmd.Parameters.AddWithValue("@p_amount", totalPaidAmount);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@v_id",v_id );
                  
                    cmd.ExecuteNonQuery();
                    
                }
                catch
                {

                }
                VoucherId();
            }
        }

        
        private void VoucherId()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();

           
            try
            {
                comboBoxHistoryPayment.Items.Clear();
                comboBoxSelection.Items.Clear();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT * FROM Voucher";
                //cmd.Parameters.AddWithValue("@credit", "true");
                
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                     comboBoxHistoryPayment.Items.Add( reader["V_id"].ToString());
                     comboBoxSelection.Items.Add(reader["V_id"].ToString());

                }
                comboBoxHistoryPayment.SelectedIndex = 0;
                comboBoxSelection.SelectedIndex = 0;
            }

            catch
            {


            }
            finally
            {
                con.Close();
            }

            
        }
        private void CustomerName(string id)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();


            try
            {
                comboBoxCustomerName.Items.Clear();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT CustomerName FROM Voucher Where  V_id=@v_id";
                cmd.Parameters.AddWithValue("@v_id",id);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBoxCustomerName.Items.Add(reader["CustomerName"].ToString());

                }
                comboBoxCustomerName.SelectedIndex = 0;
            }

            catch
            {


            }
            finally
            {
                con.Close();
            }


        }

        private void comboBoxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        double overvalue = 0;
        private void comboBoxHistoryPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerName(comboBoxHistoryPayment.SelectedItem.ToString());
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //BindGrid(Convert.ToInt32(txtSearch.Text.ToString()));
        }
        private void BindGrid(int v_id)
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
                DataGridViewColumn voucher = new DataGridViewTextBoxColumn();
                voucher.Name = "Id";
                voucher.HeaderText = "ဘောင်ချာနံပါတ်";
                voucher.DataPropertyName = "id";
                voucher.Width = 200;
                dataGridView1.Columns.Insert(1, voucher);
                DataGridViewColumn cname = new DataGridViewTextBoxColumn();
                cname.Name = "CName";
                cname.HeaderText = "ဝယ်ယူသူအမည်";
                cname.DataPropertyName = "CName";
                cname.Width = 200;
                dataGridView1.Columns.Insert(2, cname);

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "ပေးချေငွေ";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(3, price);
                DataGridViewColumn Tprice = new DataGridViewTextBoxColumn();
                Tprice.Name = "price";
                Tprice.HeaderText = "စုစုပေါင်းငွေ";
                Tprice.DataPropertyName = "price";
                Tprice.Width = 200;
                dataGridView1.Columns.Insert(4, Tprice);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 100;
                dataGridView1.Columns.Insert(5, date);
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * From Voucher Where V_id=@v_id";
                    cmd.Parameters.AddWithValue("@v_id", v_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 1;

                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {

                            if (i == 1)
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;
                                newRow.Cells[1].Value = reader["V_id"].ToString();
                                newRow.Cells[2].Value = reader["CustomerName"].ToString();
                                newRow.Cells[3].Value = reader["Paid_Amount"].ToString();
                                newRow.Cells[4].Value = reader["Total_Amount"].ToString();
                                newRow.Cells[5].Value = reader["DateAndTime"].ToString();
                                realPaidAmount = Convert.ToInt32(reader["Paid_Amount"].ToString());
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
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT HistoryPayment.V_id,HistoryPayment.Amount,HistoryPayment.DateAndTime,Voucher.CustomerName,Voucher.Total_Amount,Voucher.Paid_Amount,Voucher.DateAndTime From HistoryPayment,Voucher Where Voucher.V_id=@v_id and  HistoryPayment.V_id=Voucher.V_id";
                    cmd.Parameters.AddWithValue("@v_id",v_id );
                    SqlDataReader reader = cmd.ExecuteReader();
                    int i = 1;
                    
                    if (reader.HasRows)
                    {

                        
                        while (reader.Read())
                        {

                            i++;
                            
                           DataGridViewRow newRows = new DataGridViewRow();
                            newRows.CreateCells(dataGridView1);
                            newRows.Cells[0].Value = i;
                            newRows.Cells[1].Value = reader["V_id"].ToString();
                            newRows.Cells[2].Value = reader["CustomerName"].ToString();
                            newRows.Cells[3].Value = reader["Amount"].ToString();
                            newRows.Cells[4].Value = reader["Total_Amount"].ToString();
                            newRows.Cells[5].Value = reader["DateAndTime"].ToString();
                            realPaidAmount += Convert.ToInt32(reader["Amount"].ToString());
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
        

        private void HistoryPayment_Load(object sender, EventArgs e)
        {
            if (comboBoxHistoryPayment.DataSource != null)
                BindGrid(Convert.ToInt32(comboBoxSelection.SelectedItem.ToString()));
            VoucherId();
            if(comboBoxHistoryPayment.DataSource!=null)
            CustomerName(comboBoxHistoryPayment.SelectedItem.ToString());
            btnPayment.Enabled = true;
            
        }
        double flag = 0;
        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;

            con.Open();

            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM Voucher WHERE V_id=@v_id";
                //cmdCate.Parameters.AddWithValue("@p_amount", txtPaidAmount.Text);

                //cmdCate.Parameters.AddWithValue("@datetime", );
                cmdCate.Parameters.AddWithValue("@v_id", comboBoxHistoryPayment.SelectedItem.ToString());
                var reader = cmdCate.ExecuteReader();



                while (reader.Read())
                {

                    totalPaidAmount = Convert.ToDouble(reader["Paid_Amount"].ToString());

                    total = Convert.ToDouble(reader["Total_Amount"].ToString()) - Convert.ToDouble(reader["Discount"].ToString());
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
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM HistoryPayment WHERE V_id=@v_id";
                //cmdCate.Parameters.AddWithValue("@p_amount", txtPaidAmount.Text);

                //cmdCate.Parameters.AddWithValue("@datetime", );
                cmdCate.Parameters.AddWithValue("@v_id", comboBoxHistoryPayment.SelectedItem.ToString());
                var reader = cmdCate.ExecuteReader();



                while (reader.Read())
                {

                    totalPaidAmount += Convert.ToDouble(reader["Amount"].ToString());


                }
            }
            catch
            {
            }
            overvalue = total - totalPaidAmount;
            if (txtPaidAmount.Text.ToString().Trim() != "")
            {

                flag = Convert.ToDouble(txtPaidAmount.Text.ToString());

                
                if (overvalue < flag)
                {

                    btnPayment.Enabled = false;

                    
                }
                else
                {

                    btnPayment.Enabled = true;


                  

                }
            }

            
        }
           

       
        private void txtPaidAmount_CursorChanged(object sender, EventArgs e)
        {
             /*SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
           
            con.Open();
          
            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM HistoryPayment WHERE V_id=@v_id";
                //cmdCate.Parameters.AddWithValue("@p_amount", txtPaidAmount.Text);

                //cmdCate.Parameters.AddWithValue("@datetime", );
                cmdCate.Parameters.AddWithValue("@v_id", comboBoxHistoryPayment.SelectedItem.ToString());
                var reader = cmdCate.ExecuteReader();
                totalPaidAmount += Convert.ToDouble(txtPaidAmount.Text.ToString());
               
               while(reader.Read())
                {
                   
                   totalPaidAmount+=Convert.ToDouble(reader["Amount"].ToString());
                  
                   
                }
              
               if (totalPaidAmount > total)
               {
                   btnPayment.Enabled = false;
                   totalPaidAmount -= Convert.ToDouble(txtPaidAmount.Text.ToString());
                   txtPaidAmount.Text =null;
               }
               else
               {
                   btnPayment.Enabled = true;
               }
            }

            catch
            {


            }
            finally
            {
                con.Close();
            }*/
        }

      
             private void comboBoxSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(comboBoxSelection.SelectedItem.ToString()));

        }

             private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
             {
                 char ch = e.KeyChar;
                 if (ch == 46 && txtPaidAmount.Text.IndexOf('.') != -1)
                 {
                     e.Handled = true;
                     return;
                 }
                 if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && ch != 8 && ch != 46)
                     e.Handled = true;
             }

            
       
    }
}
