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
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
namespace PointOfSaleSystem
{
    public partial class BuyList : Form
    {
        public BuyList()
        {
            InitializeComponent();
            productComobox();
            unitComobox();
            categoryComobox();
        }
        String p_id= null, c_id = null, u_id = null;
        String months, years;
        private void BuyList_Load(object sender, EventArgs e)
        {
            productComobox();
            unitComobox();
            categoryComobox();
           
                
               
                comboBoxCategory2.SelectedItem = "ကုန်ပစ္စည်းအမျိုးအစား";
                //dateTime();
                months ="9";
                years= "2020";
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
         
        }
        private void productComobox()
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdProduct;
            con.Open();
            try
            {
                comboBoxProduct.Items.Clear();
                cmdProduct = con.CreateCommand();
                cmdProduct.CommandText = "SELECT P_Name FROM Product";
                var reader = cmdProduct.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxProduct.Items.Add(reader.GetString("P_Name"));

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

        private void unitComobox()
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdUnit;
            con.Open();
            try
            {
                comboBoxUnit.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT U_Name FROM Unit";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxUnit.Items.Add(reader.GetString("U_Name"));

                }
                comboBoxUnit.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void categoryComobox()
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory1.Items.Clear();
                comboBoxCategory2.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                MySqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxCategory1.Items.Add(reader["C_Name"].ToString());
                    comboBoxCategory2.Items.Add(reader["C_Name"].ToString());
                }
                comboBoxCategory1.SelectedIndex = 0;
                comboBoxCategory2.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }


        private void BindGrid(String data)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.Green;
                style.Font = new Font("Times New Roman", 20,FontStyle.Bold);
                
                
                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 14);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 50;
                dataGridView1.Columns.Insert(0, id);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 100;
                dataGridView1.Columns.Insert(1, date);
                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 180;
                dataGridView1.Columns.Insert(2, product);
                DataGridViewColumn amount = new DataGridViewTextBoxColumn();
                amount.Name = "amount";
                amount.HeaderText = "အရေအတွက်";
                amount.DataPropertyName = "u";
                amount.Width = 150;
                dataGridView1.Columns.Insert(3, amount);

                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "စုစုပေါင်းကုန်ကျငွေ";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(4, price);
                DataGridViewColumn desc= new DataGridViewTextBoxColumn();
                desc.Name = "ဖော်ပြချက်";
                desc.HeaderText = "ဖော်ပြချက်";
                desc.DataPropertyName = "ဖော်ပြချက်";
                desc.Width = 130;
                dataGridView1.Columns.Insert(5, desc);
                DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn();
               // btnUpdate.Name = "Updates";
                btnUpdate.Text = "ပြင်မည်";
                //btnUpdate.HeaderText = "ပြင်မည်";
                btnUpdate.DataPropertyName = "update";
                btnUpdate.Width = 100;
                btnUpdate.CellTemplate.Style.BackColor = Color.Black;
                btnUpdate.FlatStyle = FlatStyle.Standard;
                btnUpdate.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(6,btnUpdate);
                
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                //btnDelete.Name = "Delete";
                btnDelete.Text = "ဖြတ်မည်";
               // btnDelete.HeaderText = "ဖြတ်မည်";
                btnDelete.DataPropertyName = "delete";
                btnDelete.Width = 100;
                btnDelete.CellTemplate.Style.BackColor = Color.Black;
               
                btnDelete.FlatStyle = FlatStyle.Standard;
                btnDelete.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Insert(7, btnDelete);
                dataGridView1.DataSource = null;
                MySqlConnection con = new MyConnection().GetConnection();
                MySqlCommand cmd;
                con.Open();
                try
                {
                    if (date != null)
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT Product.P_Name,Unit.U_Name,BuyList.DateAndTime,BuyList.TotalPrice,BuyList.U_amt,BuyList.Description FROM Product,Unit,Category,BuyList Where Category.C_Name=@c_name And Category.C_id=BuyList.C_id And Month(BuyList.DateAndTime)=@month and Year(BuyList.DateAndTime)=@year and Product.P_id=BuyList.P_id and BuyList.U_id=Unit.U_id";
                        cmd.Parameters.AddWithValue("@c_name", data);
                        cmd.Parameters.AddWithValue("@year", years);
                        cmd.Parameters.AddWithValue("@month", months);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;
                                newRow.Cells[1].Value = reader["DateAndTime"].ToString();
                                newRow.Cells[2].Value = reader["P_Name"].ToString();
                                newRow.Cells[3].Value = reader["U_amt"].ToString() +" "+ reader["U_Name"].ToString();
                                newRow.Cells[4].Value = reader["TotalPrice"].ToString();
                                newRow.Cells[5].Value = reader["Description"].ToString();
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

        private void comboBoxCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCategoryMethod(comboBoxCategory2.SelectedItem.ToString());
        }

        private void comboBoxCategoryMethod(String data)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                cmd.Parameters.AddWithValue("@name", data);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    c_id = reader.GetString("C_id");

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

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProductMethod(comboBoxProduct.SelectedItem.ToString());
        }

        private void comboBoxProductMethod(String data)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_id FROM Product WHERE P_Name=@name";
                cmd.Parameters.AddWithValue("@name", data);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    p_id = reader.GetString("P_id");

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

        private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxUnitMethod(comboBoxUnit.SelectedItem.ToString());
        }

        private void comboBoxUnitMethod(String data)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_id FROM Unit WHERE U_Name=@name";
                cmd.Parameters.AddWithValue("@name", data);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    u_id = reader.GetString("U_id");

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {
                if (c_id != null && p_id != null && u_id != null && txtTotalPrice.Text.ToString().Trim() != "" && txtQty.Text.ToString().Trim() != "")
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Select * From BuyList Where C_id=@c_id and P_id=@p_id and U_id=@u_id and U_amt=@u_amt and TotalPrice=@totalamount";
                    //cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@p_id", p_id);
                    //cmd.Parameters.AddWithValue("@c_id", c_id);
                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    cmd.Parameters.AddWithValue("@c_id", c_id);
                    cmd.Parameters.AddWithValue("@u_amt", txtQty.Text.ToString());
                    cmd.Parameters.AddWithValue("@totalamount", txtTotalPrice.Text.ToString());
                    cmd.Parameters.AddWithValue("@des", txtTotalPrice.Text.ToString());
                    var reader=cmd.ExecuteReader();
                    con.Close();
                    if (!reader.HasRows)
                    {
                        try
                        {
                            con.Open();
                            cmd = con.CreateCommand();
                            cmd.CommandText = "INSERT INTO BuyList(DateAndTime,C_id,P_id,U_id,U_amt,TotalPrice,Description) VALUES(@date,@c_id,@p_id,@u_id,@u_amt,@totalamount,@desc)";
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
                            cmd.Parameters.AddWithValue("@p_id", p_id);
                            cmd.Parameters.AddWithValue("@u_id", u_id);
                            cmd.Parameters.AddWithValue("@c_id", c_id);
                            cmd.Parameters.AddWithValue("@u_amt", txtQty.Text.ToString());
                            cmd.Parameters.AddWithValue("@totalamount", txtTotalPrice.Text.ToString());
                            cmd.Parameters.AddWithValue("@desc", txtDesc.Text.ToString());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                            txtQty.Text = "";
                            txtTotalPrice.Text = "";
                            txtDesc.Text = "";
                            comboBoxProduct.SelectedIndex = 0;
                            comboBoxUnit.SelectedIndex = 0;
                            comboBoxCategory2.SelectedIndex = 0;
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
                        txtQty.Text = "";
                        txtTotalPrice.Text = "";
                        comboBoxProduct.SelectedIndex = 0;
                        comboBoxUnit.SelectedIndex = 0;
                        comboBoxCategory2.SelectedIndex = 0;
                        txtDesc.Text = "";
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
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
            }
        }

        private void comboBoxCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            String c_name = null;
            //dataGridView1.Columns["DateAndTime"].ReadOnly=true;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Rows[0].ReadOnly = false;
            String date = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            String product = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String[] qty = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Split(' ');
            String totalprice = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            String desc = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            String number =null, str = "";
            foreach (String data in qty)
            {
                if (Regex.IsMatch(data, @"^\d+$"))
                {
                    number = data;
                   
                }
                else
                    str = data;
            }
           
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Category.C_Name FROM Category,Product WHERE Category.C_id=Product.C_id and Product.P_Name=@name";
                cmd.Parameters.AddWithValue("@name", product);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    c_name = reader.GetString("C_Name");

                }
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
            comboBoxCategoryMethod(c_name);
            comboBoxProductMethod(product);
            comboBoxUnitMethod(str);
            if (e.ColumnIndex == 6  )
            {

                dataGridView1.ReadOnly = false;
                DataGridViewButtonCell btn = null;
                btn = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[6];
                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {

                    if (dc.Index.Equals(3) || dc.Index.Equals(4) || dc.Index.Equals(6) || dc.Index.Equals(5))
                    {

                        dc.ReadOnly = false;


                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }

                }
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (dr.Index.Equals(e.RowIndex))
                    {

                        dr.ReadOnly = false;

                    }
                    else
                    {
                        dr.ReadOnly = true;
                    }

                }
                
                 try
                 {   
                if (btn.Value.ToString() == "ပြင်မည်")
                {
                    
                    btn.UseColumnTextForButtonValue = false;
                    dataGridView1.Rows[e.RowIndex].Cells[6].Value = "သိမ်းမည်";
                   
                    
                }
                else if (btn.Value.ToString() == "သိမ်းမည်")
                {
                    try
                    {
                       
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update BuyList Set U_amt=@u_amt,TotalPrice=@price,Description=@desc Where C_id=@c_id and P_id=@p_id and U_id=@u_id";
                        cmd.Parameters.AddWithValue("@c_id", c_id);
                        cmd.Parameters.AddWithValue("@p_id", p_id);
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        cmd.Parameters.AddWithValue("@u_amt", number);
                        cmd.Parameters.AddWithValue("@price",totalprice);
                        cmd.Parameters.AddWithValue("@desc", desc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                        BindGrid(comboBoxCategory1.SelectedItem.ToString());
                    }
                    catch
                    {
                        
                    }

                    dataGridView1.ReadOnly = true;
                    dataGridView1.Rows[e.RowIndex].Cells[6].Value = "ပြင်မည်";
                    
                }
                 }
                catch
                 {
                }
                   
            }
            
            else if (e.ColumnIndex == 7)
            {



                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Delete  From BuyList  Where C_id=@c_id and P_id=@p_id and U_amt=@u_amt and U_id=@u_id and TotalPrice=@price";
                    cmd.Parameters.AddWithValue("@p_id", p_id);
                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    cmd.Parameters.AddWithValue("@u_amt", number);
                    cmd.Parameters.AddWithValue("@price", totalprice);
                    cmd.Parameters.AddWithValue("@c_id", c_id);
                    cmd.ExecuteNonQuery();
                    BindGrid(comboBoxCategory1.SelectedItem.ToString());

                }
                catch
                {

                }

            }
                
               
        }
        

        
    
       
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) )
                 e.Handled = true;
           
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool con = false;
            if (Regex.IsMatch(textBox2.Text.ToString(), @"^\d+$"))
            {
                con = true;
                
            }
            else
                con = false;
            try
            {
                if (con)
                    if (Convert.ToInt32(textBox2.Text.ToString()) > 12)
                    {
                        MessageBox.Show("1လ မှ 12 လအတွင်းဝင်ပါ", "သတိပေးချက်");
                        textBox2.Text = null;
                    }
                    else 
                    {
                        months = textBox2.Text.ToString();
                        if(textBox3.Text.ToString()!="")
                        BindGrid(comboBoxCategory1.SelectedItem.ToString());
                    }
                
            }
            catch
            {
                
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            years = textBox3.Text.ToString();
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

      
    }
}
