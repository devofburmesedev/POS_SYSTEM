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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
        }
        
        String p_id=null,u_id=null,category_id=null,previous_price=null;
       
        
        private void Stocks_Load(object sender, EventArgs e)
        {
           
            categoryComobox();
            unitComobox();
            productComobox();
            updateLabel.Visible = true;
            productUpdateLabel.Visible = true;
            productDeleteLabel.Visible = true;
            categoryComoboxs();
            unitUpdateLabel.Visible = true;
           
            priceUpdateLabel.Visible = true;
            priceDeleteLabel.Visible = true;
            
            
           
            
           
            if(comboBoxCategory1.DataSource!=null)
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
           
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
        private void categoryComoboxs()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory.Items.Clear();

                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxCategory.Items.Add(reader["C_Name"].ToString());

                }
                comboBoxCategory.SelectedIndex = 0;

            }
            catch
            {


            }
            finally
            {
                
                con.Close();
            }
        }

       
        
     
        private void btnCategory_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd, cmdCate;
            con.Open();
            bool condition = false;

            try
            {
                cmdCate = con.CreateCommand();

                cmdCate.CommandText = "SELECT * FROM Category WHERE C_Name=@name";
                cmdCate.Parameters.AddWithValue("@name", tetCategory.Text.ToString().Trim());
                var reader = cmdCate.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;

                }
                else
                {
                    tetCategory.Text = " ";
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
                if (tetCategory.Text.ToString().Trim() == "" && condition == true)
                    MessageBoxShowing.showIncomplementMessage();
                else if (condition == true && tetCategory.Text.ToString().Trim() != null && tetCategory.Text.ToString().Trim() != "" && btnCategory.Text.ToString().Equals("ထည့်မည်"))
                {
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Category(C_Name) VALUES(@name)";
                        cmd.Parameters.AddWithValue("@name", tetCategory.Text.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        tetCategory.Text = "";
                        MessageBoxShowing.showSuccessfulMessage();
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
                categoryComobox();
               
                con.Close();
            }

        }
       
        private void addUnit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd, cmdUnit;
            bool condition = false;
            con.Open();
            try
            {
                cmdUnit = con.CreateCommand();
                cmdUnit.CommandText = "SELECT * FROM Unit WHERE U_Name=@name";
                cmdUnit.Parameters.AddWithValue("@name", tetUnit.Text.ToString().Trim());
                var reader = cmdUnit.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;
                }
                else
                {

                    MessageBoxShowing.showWarningMessage();
                    tetUnit.Text = "";
                    
                }

            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
            try{
                if (tetUnit.Text.ToString().Trim() == "" && condition == true)
                    MessageBoxShowing.showIncomplementMessage();
                else if (condition == true && tetUnit.Text.ToString().Trim() != null && tetUnit.Text.ToString().Trim() != "" && addUnit.Text.ToString().Equals("ထည့်မည်"))
                {
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Unit(U_Name) VALUES(@name)";
                        cmd.Parameters.AddWithValue("@name", tetUnit.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBoxShowing.showSuccessfulMessage();
                        tetUnit.Text = "";
                    }
                    catch (Exception)
                    {

                        throw;

                    }
                    finally
                    {

                    }
                }

                

            }
            catch
            {
                }
            finally
            {
                unitComobox();
               
                con.Close();
            }

        }
        private void addProduct_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd, cmdProduct;
            bool condition = false;
            String c_id = null;
            con.Open();
            try
            {
                cmdProduct = con.CreateCommand();
                cmdProduct.CommandText = "SELECT * FROM Product WHERE P_Name=@name";
                cmdProduct.Parameters.AddWithValue("@name",textBox3.Text.ToString().Trim());
                var reader = cmdProduct.ExecuteReader();
                if (!reader.HasRows)
                {
                    condition = true;
                    
                }
                else if (!addProduct.Text.ToString().Equals("ဖြတ်မည်") && !addProduct.Text.ToString().Equals("ပြင်မည်"))
                {
                    MessageBoxShowing.showWarningMessage();
                    textBox3.Text = "";
                    comboBoxCategory2.SelectedIndex = 0;
                }
            }
            catch
            {

            }
            finally
            {

                con.Close();
            }
            
                if (comboBoxCategory2.SelectedItem != null )
                {
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                        cmd.Parameters.AddWithValue("@name", comboBoxCategory2.SelectedItem);
                        SqlDataReader reads = cmd.ExecuteReader();
                        while (reads.Read())
                        {

                            c_id = reads["C_id"].ToString();
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

                con.Open();
                try
                {
                    if (textBox3.Text.ToString().Trim() == "" && condition)
                        MessageBoxShowing.showIncomplementMessage();
                    if (condition && c_id != null && textBox3.Text.ToString().Trim() != "" && addProduct.Text.ToString().Equals("ထည့်မည်"))
                    {
                        
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "INSERT INTO Product(C_id,P_Name) VALUES(@c_id,@name)";
                            cmd.Parameters.AddWithValue("@c_id", c_id);
                            cmd.Parameters.AddWithValue("@name", textBox3.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulMessage();
                            textBox3.Text = "";
                            comboBoxCategory2.SelectedIndex = 0;
                        }
                        catch{
                        }
                    }
                    
                   
                }

                catch (Exception)
                {

                    throw;

                }
                finally
                {
                    productComobox();
                    
                    con.Close();
                }

            
        }

       
       
        

        

        public void BindGrid(String data)
        {
            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns.Clear();
                DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Green;
                style.ForeColor = Color.White;

                style.Font = new Font("Times New Roman", 14, FontStyle.Bold); 
                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman",12,FontStyle.Bold);
                DataGridViewColumn id = new DataGridViewTextBoxColumn();
                id.Name = "id";
                id.HeaderText = "စဉ်";
                id.DataPropertyName = "No.";
                id.Width = 200;
                dataGridView1.Columns.Insert(0, id);
                DataGridViewColumn product = new DataGridViewTextBoxColumn();
                product.Name = "Product";
                product.HeaderText = "ကုန်ပစ္စည်းများ";
                product.DataPropertyName = "Product";
                product.Width = 200;
                dataGridView1.Columns.Insert(1, product);
                DataGridViewColumn units = new DataGridViewTextBoxColumn();
                units.Name = "unit";
                units.HeaderText = "ယူနစ်";
                units.DataPropertyName = "unit";
                units.Width = 200;
                dataGridView1.Columns.Insert(2, units);
               
                DataGridViewColumn price = new DataGridViewTextBoxColumn();
                price.Name = "price";
                price.HeaderText = "စျေးနှုန်း";
                price.DataPropertyName = "price";
                price.Width = 200;
                dataGridView1.Columns.Insert(3, price);
                
                dataGridView1.DataSource = null;
                SqlConnection con = new MyConnection().GetConnection();
                SqlCommand cmd;
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                   cmd.CommandText= "SELECT Product.P_Name,Unit.U_Name,Price.Price FROM Price,Product,Unit,Category Where Category.C_id=Product.C_id and Category.C_Name=@c_name and Unit.U_id=Price.U_id and Product.P_id=Price.P_id";
                    cmd.Parameters.AddWithValue("@c_name", data);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            DataGridViewRow newRow =new DataGridViewRow();
                            newRow.CreateCells(dataGridView1);
                            newRow.Cells[0].Value=i;
                            newRow.Cells[1].Value = reader["P_Name"].ToString(); 
                            newRow.Cells[2].Value = reader["U_Name"].ToString();
                            newRow.Cells[3].Value = reader["Price"].ToString();
                            i++;
                            dataGridView1.Rows.Add(newRow);
                            //MessageBox.Show(reader.Read()+"?"+reader.FieldCount);
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
                cmdProduct.Parameters.AddWithValue("@c_id", getCategoryId(comboBoxCategory.SelectedItem.ToString()));
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

        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory1.Items.Clear();
                comboBoxCategory2.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
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

       

        private void comboBoxCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String data = comboBoxCategory1.SelectedItem.ToString();
            BindGrid(data);
            
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

                    p_id = reader["P_id"].ToString();
                   
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
        
        private void tetAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
              if(tetAmount.Text.ToString()!="")
                  Convert.ToDouble(tetAmount.Text.ToString());
            }
            catch
            {
                tetAmount.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
        }

        private void updateLabel_Click_1(object sender, EventArgs e)
        {
            UpdateProductCategory pCate = new UpdateProductCategory();
            pCate.Show();
           
        }
       

        
        
        

        private void comboBoxUnitUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        private void unitUpdateLabel_Click_1(object sender, EventArgs e)
        {
            UpdateUnit uUnit = new UpdateUnit();
            uUnit.Show();
        }

       
       
      
        
        

        private void comboBoxCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            if (comboBoxCategory2.SelectedItem != null)
            {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                    cmd.Parameters.AddWithValue("@name", comboBoxCategory2.SelectedItem.ToString());
                    SqlDataReader reads = cmd.ExecuteReader();
                    while (reads.Read())
                    {

                        category_id = reads["C_id"].ToString();
                        
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

        
      
       

       
       
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPrice_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;

            con.Open();
            try
            {
                if (tetAmount.Text.Trim() == "" || p_id == null || u_id == null)
                    MessageBoxShowing.showIncomplementMessage();
                else
                    if (p_id != null && u_id != null && btnPrice.Text.Equals("ထည့်မည်"))
                    {

                        cmd = con.CreateCommand();
                        cmd.CommandText = "Select * From Price Where Price=@price and P_id=@p_id and U_id=@u_id";
                        cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_id", p_id);
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        var reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                        {

                            con.Close();
                            try
                            {
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "INSERT INTO Price(Price,P_id,U_id) VALUES(@price,@p_id,@u_id)";
                                cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                                cmd.Parameters.AddWithValue("@p_id", p_id);
                                cmd.Parameters.AddWithValue("@u_id", u_id);
                                cmd.ExecuteNonQuery();
                                MessageBoxShowing.showSuccessfulMessage();
                                tetAmount.Text = "";
                                comboBoxProduct.SelectedIndex = 0;
                                comboBoxUnit.SelectedIndex = 0;
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            MessageBoxShowing.showWarningMessage();
                            tetAmount.Text = "";
                            comboBoxProduct.SelectedIndex = 0;
                            comboBoxUnit.SelectedIndex = 0;
                        }
                    }
                   
                   

                    }
            
            catch 
            {
            }
            finally
            {
               
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
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

                    u_id = reader["U_id"].ToString();

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
                if (p_id != null && u_id != null)
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Price FROM Price WHERE P_id=@p_id and U_id=@u_id";
                    cmd.Parameters.AddWithValue("@p_id", p_id);
                    cmd.Parameters.AddWithValue("@u_id", u_id);
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            previous_price = reader["Price"].ToString();
                           
                        }

                    }
                    else
                    {
                        btnPrice.Text = "ထည့်မည်";
                    }
                }
                else
                {
                    tetAmount.Text = "";
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

        private void tetAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            
                char ch = e.KeyChar;
                if (ch == 46 && tetAmount.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }
                if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && ch != 8 && ch != 46)
                    e.Handled = true;
            
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void productUpdateLabel_Click(object sender, EventArgs e)
        {
            UpdateProduct uProduct = new UpdateProduct();
            uProduct.Show();
        }

        private void productDeleteLabel_Click(object sender, EventArgs e)
        {
            DeleteProductcs dProduct = new DeleteProductcs();
            dProduct.Show();

        }

        private void priceUpdateLabel_Click(object sender, EventArgs e)
        {
            UpdatePrice uPrice = new UpdatePrice();
            uPrice.Show();
        }

        private void priceDeleteLabel_Click(object sender, EventArgs e)
        {
            DeletePricecs dPrice = new DeletePricecs();
            dPrice.Show();
        }

        private void Stocks_MouseEnter(object sender, EventArgs e)
        {
            if (comboBoxCategory1.DataSource != null)
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void Stocks_Activated(object sender, EventArgs e)
        {
            if (comboBoxCategory1.DataSource != null)
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
           // if (comboBoxCategory1.DataSource != null)
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void Stocks_Enter(object sender, EventArgs e)
        {
            productComobox();
            
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void UpdateProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            productComobox();
        }

        private void UpdatePrice_FormClosed(object sender, FormClosedEventArgs e)
        {
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            productComobox();
        }

      
        }

        
        }
  
