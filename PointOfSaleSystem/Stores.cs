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
    public partial class Stores : Form
    {
        public Stores()
        {
            InitializeComponent();
            productComobox();
            unitComobox();
            storeComobox();
            
           
        }
        int p_id, u_id, s_id;
        bool conditions = false;
        private void Stores_Load(object sender, EventArgs e)
        {
            storeComobox();
           if(comboBoxStores2.DataSource!=null)
            BindGrid(comboBoxStores2.SelectedItem.ToString());
            dateTimePicker2.Visible = false;
            comboBoxName.Visible = false;
            comboBoxLocation.Visible = false;
            conditions = false;
            btnView.BackColor = Color.Aqua;
           
        }
        private void storeComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                comboBoxStores2.Items.Clear();
                comboBoxStores.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT Name FROM Stores";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxStores2.Items.Add(reader["Name"].ToString());
                    comboBoxStores.Items.Add(reader["Name"].ToString());

                }
                comboBoxStores2.SelectedIndex = 0;
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
        private void storeComobox2()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
               
                
                comboBoxName.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT Name FROM Stores";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    
                    
                    comboBoxName.Items.Add(reader["Name"].ToString());

                }
               
               
                comboBoxName.SelectedIndex = 0;
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
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                comboBoxUnit.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT U_Name FROM Unit";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxUnit.Items.Add(reader["U_Name"].ToString());

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
        private void productComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdProduct;
            con.Open();
            try
            {
                comboBoxProduct.Items.Clear();
                cmdProduct = con.CreateCommand();
                cmdProduct.CommandText = "SELECT P_Name FROM Product";
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
        String datas = null;
        private void btnStores_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd,cmdCate;
           
            con.Open();
             bool condition = false;

            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM Stores WHERE Name=@name and Location=@location";
                cmdCate.Parameters.AddWithValue("@name", txtStores.Text.ToString().Trim());
                cmdCate.Parameters.AddWithValue("@location", txtLocation.Text.ToString().Trim());
                var reader = cmdCate.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;

                }
                else
                {

                    MessageBoxShowing.showWarningMessage();
                    txtLocation.Text="";
                    txtStores.Text="";
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
                
                if ((txtStores.Text.ToString().Trim() == ""  || txtLocation.Text.ToString() == "") &&  condition == true)
                    MessageBoxShowing.showIncomplementMessage();
                else if (condition == true && txtStores.Text.ToString().Trim() != "" && txtLocation.Text.ToString().Trim() != "")
                {
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Stores(Name,Location) VALUES(@name,@location)";
                        cmd.Parameters.AddWithValue("@name", txtStores.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@location", txtLocation.Text.ToString().Trim());
                        cmd.ExecuteNonQuery();

                        MessageBoxShowing.showSuccessfulMessage();
                        txtLocation.Text = "";
                        txtStores.Text = "";
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
                storeComobox();
                storeComobox2();
                comboBoBoxLocation();
                con.Close();
            }
        }

        private void btnStoreProduct_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd, cmdCate;
            con.Open();
            bool condition = false;

            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM ProductInStores WHERE P_id=@p_id and U_id=@u_id and S_id=@s_id and Amount=@amount";
                cmdCate.Parameters.AddWithValue("@p_id",p_id );
                cmdCate.Parameters.AddWithValue("@u_id", u_id);
                cmdCate.Parameters.AddWithValue("@s_id",s_id );
                cmdCate.Parameters.AddWithValue("@amount",txtAmount.Text.ToString() );
                var reader = cmdCate.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;

                }
                else
                {

                    MessageBoxShowing.showWarningMessage();
                    comboBoxStores.SelectedIndex = 0;
                    comboBoxProduct.SelectedIndex = 0;
                    comboBoxUnit.SelectedIndex = 0;
                    txtAmount.Text = "";
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
                if ((comboBoxProduct.SelectedItem.ToString() == null || comboBoxUnit.SelectedItem.ToString() == null || comboBoxStores.SelectedItem.ToString() == null || txtAmount.Text.ToString() == "") && condition == true)
                    MessageBoxShowing.showIncomplementMessage();
                else if (condition == true)
                {
                    try
                    {
                          cmd = con.CreateCommand();
                          cmd.CommandText = "INSERT INTO ProductInStores(P_id,U_id,S_id,Amount) VALUES(@p_id,@u_id,@s_id,@amount)";
                          cmd.Parameters.AddWithValue("@p_id", p_id);
                          cmd.Parameters.AddWithValue("@u_id",u_id );
                          cmd.Parameters.AddWithValue("@s_id",s_id );
                          cmd.Parameters.AddWithValue("@amount",txtAmount.Text.ToString() );
                          cmd.ExecuteNonQuery();
                          con.Close();
                          con.Open();
                          cmd = con.CreateCommand();
                          cmd.CommandText = "INSERT INTO HistoryProductInStores(P_id,U_id,S_id,Amount,DateAndTime) VALUES(@p_id,@u_id,@s_id,@amount,@date)";
                          cmd.Parameters.AddWithValue("date", DateTime.Now.Date);
                          cmd.Parameters.AddWithValue("@p_id", p_id);
                          cmd.Parameters.AddWithValue("@u_id", u_id);
                          cmd.Parameters.AddWithValue("@s_id", s_id);
                          cmd.Parameters.AddWithValue("@amount", txtAmount.Text.ToString());
                          cmd.ExecuteNonQuery();
                          MessageBoxShowing.showSuccessfulMessage();
                          comboBoxStores.SelectedIndex = 0;
                          comboBoxProduct.SelectedIndex = 0;
                          comboBoxUnit.SelectedIndex = 0;
                          txtAmount.Text = "";
                          if (!conditions)
                              BindGrid(comboBoxStores2.SelectedItem.ToString());
                          else
                              BindGridHistory(comboBoxStores2.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
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
                cmd.Parameters.AddWithValue("@name", comboBoxProduct.SelectedItem);
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

        private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_id FROM Unit WHERE U_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxUnit.SelectedItem);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    u_id = Convert.ToInt32(reader["U_id"].ToString());

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

        private void comboBoxStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoBoxName();
        }

        private void comboBoBoxName()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT S_id FROM Stores WHERE Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxStores.SelectedItem);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    s_id = Convert.ToInt32(reader["S_id"].ToString());

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
                style.Font = new Font("Times New Roman", 18, FontStyle.Bold);


                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 14);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 60;
                dataGridView1.Columns.Insert(0, id);
                DataGridViewColumn stores = new DataGridViewTextBoxColumn();
                stores.Name = "date";
                stores.HeaderText = "သိုလှောင်ရုံ";
                stores.DataPropertyName = "date";
                stores.Width = 200;
                dataGridView1.Columns.Insert(1, stores);
                DataGridViewColumn location = new DataGridViewTextBoxColumn();
                location.Name = "Product";
                location.HeaderText = "နေရာ";
                location.DataPropertyName = "Product";
                location.Width = 180;
                dataGridView1.Columns.Insert(2, location);
                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 180;
                dataGridView1.Columns.Insert(3, product);
                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "အရေအတွက်";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(4, price);
                
                DataGridViewColumn units = new DataGridViewTextBoxColumn();
                units.Name = "unit";
                units.HeaderText = "ယူနစ်";
                units.DataPropertyName = "unit";
                units.Width = 200;
                dataGridView1.Columns.Insert(5, units);
              
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
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT * From Stores,ProductInStores where Stores.Name=@name and Stores.S_id=ProductInStores.S_id";
                        cmd.Parameters.AddWithValue("@name", data);
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;
                                newRow.Cells[1].Value = reader["Name"].ToString();
                                newRow.Cells[2].Value = reader["Location"].ToString();


                                newRow.Cells[3].Value = getProduct(reader["P_id"].ToString());
                                newRow.Cells[5].Value =getUnit( reader["U_id"].ToString());
                                newRow.Cells[4].Value = reader["Amount"].ToString();
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
        private void BindGridHistory(String data,String datetime)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.Green;
                style.Font = new Font("Times New Roman", 18, FontStyle.Bold);


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
                product.Width = 180;
                dataGridView1.Columns.Insert(1, product);
                
                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "အရေအတွက်";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(2, price);
               
                DataGridViewColumn units = new DataGridViewTextBoxColumn();
                units.Name = "unit";
                units.HeaderText = "ယူနစ်";
                units.DataPropertyName = "unit";
                units.Width = 200;
                dataGridView1.Columns.Insert(3, units);
                DataGridViewColumn date = new DataGridViewTextBoxColumn();
                date.Name = "date";
                date.HeaderText = "ရက်စွဲ";
                date.DataPropertyName = "date";
                date.Width = 100;
                dataGridView1.Columns.Insert(4, date);
                DataGridViewColumn time = new DataGridViewTextBoxColumn();
                time.Name = "date";
                time.HeaderText = "ရက်စွဲ";
                time.DataPropertyName = "date";
                time.Width = 100;
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
                                String []datetimes=(reader["DateAndTime"].ToString()).Split(' ');
                                String dates, times;
                                dates = datetimes[0];
                                times = datetimes[1];
                                newRow.Cells[4].Value = dates; 
                                newRow.Cells[5].Value =times;
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

        private int getStoreProductId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data=0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT S_id FROM Stores Where Name=@name";
                cmd.Parameters.AddWithValue("@name", p);
               
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data =Convert.ToInt32( reader["S_id"].ToString());

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
            String data=null;
            con.Open();
            try
            {
               
                cmd= con.CreateCommand();
                cmd.CommandText = "SELECT P_Name FROM Product Where P_id=@p_id ";
                cmd.Parameters.AddWithValue("@p_id", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                  data=reader["P_Name"].ToString();

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

        private void comboBoxStores2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(comboBoxStores2.SelectedItem.ToString());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;

           
                if (!conditions)
                {
                    if (e.ColumnIndex == 6)
                    {

                        int store = getStoreProductId(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                      
                        int product = getProductId(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                        int unit = getUnitId(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                        String amount = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                     
                        con.Open();
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Delete  From ProductInStores  Where P_id=@p_id and U_id=@u_id and S_id=@s_id and Amount=@amount";
                            cmd.Parameters.AddWithValue("@p_id", product);

                            cmd.Parameters.AddWithValue("@u_id", unit);
                            cmd.Parameters.AddWithValue("@s_id", store);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.ExecuteNonQuery();
                            BindGrid(comboBoxStores2.SelectedItem.ToString());

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
                    else
                    {
                       
                        if (e.ColumnIndex == 6)
                        {
                            
                            try
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

                                    BindGridHistory(comboBoxStores2.SelectedItem.ToString(),dateTimePicker2.Text);
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


             }
                
            }
        
    
        private void btnViewHistory_Click(object sender, EventArgs e)
        {
           dateTimePicker2.Visible = true;
           btnView.BackColor = Color.Lime;
           btnViewHistory.BackColor = Color.Aqua;
           conditions = true;
           BindGridHistory(comboBoxStores2.SelectedItem.ToString(),dateTimePicker2.Text.ToString());
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Visible = false;
            btnView.BackColor = Color.Aqua;
            btnViewHistory.BackColor = Color.Lime;
            conditions = false;
            BindGrid(comboBoxStores2.SelectedItem.ToString());
        }

        private void comboBoxStores2_SelectedIndexChanged_1(object sender, EventArgs e)
             
        {
            if (!conditions)
                BindGrid(comboBoxStores2.SelectedItem.ToString());
            else
                BindGridHistory(comboBoxStores2.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            BindGridHistory(comboBoxStores2.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
        }
        bool change = false; string names, locations;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
           
            storeComobox2();
            comboBoBoxLocation();
            if (btnUpdate.Text == "ပြင်မည်")
            {
                
                comboBoxName.Visible = true;
                comboBoxLocation.Visible = true;
                change = true;
            }
            else if(btnUpdate.Text == "သိမ်းမည်")
            {
                try
                {

                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "Update Stores Set Name=@name,Location=@location Where Name=@names and Location=@locations";
                    cmd.Parameters.AddWithValue("@name", txtStores.Text.ToString());
                    cmd.Parameters.AddWithValue("@location", txtLocation.Text.ToString());
                    cmd.Parameters.AddWithValue("@names", names);
                    cmd.Parameters.AddWithValue("@locations", locations);

                    cmd.ExecuteNonQuery();
                    MessageBoxShowing.showSuccessfulUpdateMessage();
                    if (!conditions)
                        BindGrid(comboBoxStores2.SelectedItem.ToString());
                    else
                        BindGridHistory(comboBoxStores2.SelectedItem.ToString(), dateTimePicker2.Text.ToString());
                    txtStores.Text = "";
                    txtLocation.Text = "";
                    btnUpdate.Text = "ပြင်မည်";
                }
                catch
                {

                }
                finally
                {
                    storeComobox2();
                    comboBoBoxLocation();
                    con.Close();
                }

            }
            
        }
        bool isChange = false;
        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {

            datas = comboBoxName.SelectedItem.ToString();
           
            if (change)
            {
              
                names=txtStores.Text = comboBoxName.SelectedItem.ToString();
                if (txtStores.Text.ToString()!="" && txtLocation.Text.ToString()!="")
                {   
                    comboBoxName.Visible = false;
                    comboBoxLocation.Visible = false;
                    btnUpdate.Text = "သိမ်းမည်";
                    
                    change = false;
                }
                else if (txtLocation.Text.ToString() == "")
                {
                    comboBoBoxLocation();
                    isChange = true;
                }
            }
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
            if (change && isChange)
            {
                
               locations= txtLocation.Text = comboBoxLocation.SelectedItem.ToString();
               if (txtStores.Text.ToString() != "" && txtLocation.Text.ToString() != "")
                {
                    comboBoxName.Visible = false;
                    comboBoxLocation.Visible = false;
                    btnUpdate.Text = "သိမ်းမည်";
                  
                    change = false;
                    isChange = false;
                }
            }
        }

        private void comboBoBoxLocation()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                comboBoxLocation.Items.Clear();
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT Location FROM Stores where Name=@name";
                cmdUnit.Parameters.AddWithValue("@name", datas);
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    
                    comboBoxLocation.Items.Add(reader["Location"].ToString());

                }
                
                comboBoxLocation.SelectedIndex = 0;
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
