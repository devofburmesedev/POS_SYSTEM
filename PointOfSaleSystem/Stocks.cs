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
        String product, price;
        bool load = false;
        private void Stocks_Load(object sender, EventArgs e)
        {
            load = true;
            categoryComobox();
            unitComobox();
            productComobox();
            updateLabel.Visible = true;
            productUpdateLabel.Visible = true;
            productDeleteLabel.Visible = true;
            comboBoxProductUD.Visible = false;
            comboBoxUpdate.Visible = false;
            unitUpdateLabel.Visible = true;
            comboBoxUnitUpdate.Visible = false;
            priceUpdateLabel.Visible = true;
            priceDeleteLabel.Visible = true;
            comboBoxPriceUD.Visible = false;
            comoBoxUpdateCategory();
            comboBoxUpdateUnit();
            comboBoxUDProduct();
            comboBoxUPPrice();
            if(comboBoxCategory1.DataSource!=null)
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
           
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
                else if (condition == true && tetCategory.Text.ToString().Trim() != null && tetCategory.Text.ToString().Trim() != "" && (btnCategory.Text.ToString().Equals("ပြင်မည်")))
                {

                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update Category Set C_Name=@name Where C_Name=@nameUpdate";
                        cmd.Parameters.AddWithValue("@name", tetCategory.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@nameUpdate", comboBoxUpdate.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBoxShowing.showSuccessfulUpdateMessage();
                        btnCategory.Text = "ထည့်မည်";
                        tetCategory.Text = " ";
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
                comoBoxUpdateCategory();
                con.Close();
            }

        }
        private void comboBoxUPPrice()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                //comboBoxUpdate.DataSource = null;
                comboBoxPriceUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT Price FROM Price Where P_id=@p_id and U_id=@u_id";
                cmdCate.Parameters.AddWithValue("@p_id", p_id);
                cmdCate.Parameters.AddWithValue("@u_id", u_id);
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxPriceUD.Items.Add(reader["Price"].ToString());


                }
                comboBoxPriceUD.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
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

                else if (condition == true && tetUnit.Text.ToString().Trim() != null && tetUnit.Text.ToString().Trim() != "" && (addUnit.Text.ToString().Equals("ပြင်မည်")))
                {
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update Unit Set U_Name=@name Where U_Name=@nameUpdate";
                        cmd.Parameters.AddWithValue("@name", tetUnit.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@nameUpdate", comboBoxUnitUpdate.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBoxShowing.showSuccessfulUpdateMessage();
                        addUnit.Text = "ထည့်မည်";
                        tetUnit.Text = "";
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
            catch
            {
                }
            finally
            {
                unitComobox();
                comboBoxUpdateUnit();
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
                    else if (condition == true && textBox3.Text.ToString().Trim() != null && textBox3.Text.ToString().Trim() != "" &&(addProduct.Text.ToString().Equals("ပြင်မည်")))
                    {
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Update Product Set C_id=c_id,P_Name=@name Where P_Name=@nameUpdate";
                            cmd.Parameters.AddWithValue("@c_id", c_id);
                            cmd.Parameters.AddWithValue("@name", textBox3.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@nameUpdate", comboBoxProductUD.SelectedItem.ToString());

                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulUpdateMessage();
                            addProduct.Text = "ထည့်မည်";
                            textBox3.Text = "";
                            comboBoxCategory2.SelectedIndex = 0;
                        }
                        catch
                        {
                        }
                    }
                    else if (textBox3.Text.ToString().Trim() != null && addProduct.Text.ToString().Equals("ဖြတ်မည်") && textBox3.Text.ToString().Trim() != "")
                    {
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Delete  From Product Where P_Name=@nameUpdate";
                            cmd.Parameters.AddWithValue("@nameUpdate", comboBoxProductUD.SelectedItem.ToString());

                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulDeleteMessage();
                            textBox3.Text = "";
                            comboBoxCategory2.SelectedIndex = 0;
                            addProduct.Text = "ထည့်မည်";
                        }
                        catch
                        {
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
                    comboBoxUDProduct();
                    con.Close();
                }

            
        }

       
       
        private void comboBoxUDProduct()
        {

            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxProductUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT P_Name FROM Product Where C_id=@c_id";
                cmdCate.Parameters.AddWithValue("@c_id",category_id );
                
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxProductUD.Items.Add(reader["P_Name"].ToString());


                }
                comboBoxProductUD.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxUpdateUnit()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUnitUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT U_Name FROM Unit";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    comboBoxUnitUpdate.Items.Add(reader["U_Name"].ToString());


                }
                comboBoxUnitUpdate.SelectedIndex = 0;
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
                style.ForeColor = Color.White;

                style.Font = new Font("Times New Roman", 18, FontStyle.Bold); 
                dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman",20,FontStyle.Bold);
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

        }

        private void updateLabel_Click_1(object sender, EventArgs e)
        {
            comboBoxUpdate.Visible = true;
            updateLabel.Visible = false;
        }
        private void comoBoxUpdateCategory()
        {
            
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                       
                        comboBoxUpdate.Items.Add(reader["C_Name"].ToString());
                    
                    
                }
                comboBoxUpdate.SelectedIndex = 0;
            }
            catch
            {


            }
            finally
            {
                con.Close();
            }
        }

        private void comboBoxUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLabel.Visible = true;

            if (!comboBoxUpdate.Visible == false)
            {
                tetCategory.Text = comboBoxUpdate.SelectedItem.ToString();
                btnCategory.Text = "ပြင်မည်";
            }
            comboBoxUpdate.Visible = false;
        }

        
        

        private void comboBoxUnitUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            unitUpdateLabel.Visible = true;

            if (!comboBoxUnitUpdate.Visible==false)
            {
                tetUnit.Text = comboBoxUnitUpdate.SelectedItem.ToString();
                addUnit.Text = "ပြင်မည်";
            }
              comboBoxUnitUpdate.Visible = false;
        }

        
        private void unitUpdateLabel_Click_1(object sender, EventArgs e)
        {
            unitUpdateLabel.Visible = false;
            comboBoxUnitUpdate.Visible = true;
        }

       
       
      
        private void productUpdateLabel_Click_1(object sender, EventArgs e)
        {
            productUpdateLabel.Visible = false;
            productDeleteLabel.Visible = false;
            comboBoxProductUD.Visible = true;
            product = productUpdateLabel.Text.ToString().Trim();

        }

        private void comboBoxProductUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            productUpdateLabel.Visible = true;
            productDeleteLabel.Visible = true;
            

            if (!comboBoxProductUD.Visible == false && load==false)
            {
               
                textBox3.Text = comboBoxProductUD.SelectedItem.ToString();
                addProduct.Text = product;
            }
            load = false;
            comboBoxProductUD.Visible = false;
        }

        private void productDeleteLabel_Click(object sender, EventArgs e)
        {
            productUpdateLabel.Visible = false;
            productDeleteLabel.Visible = false;
            comboBoxProductUD.Visible = true;
            product = productDeleteLabel.Text.ToString().Trim();

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
                        comboBoxUDProduct();
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

        
        private void priceUpdateLabel_Click_1(object sender, EventArgs e)
        {
            priceUpdateLabel.Visible = false;
            priceDeleteLabel.Visible = false;
            comboBoxPriceUD.Visible = true;
            price = priceUpdateLabel.Text.ToString().Trim();
        }

       

       
        private void priceDeleteLabel_Click(object sender, EventArgs e)
        {
            priceUpdateLabel.Visible = false;
            priceDeleteLabel.Visible = false;
            comboBoxPriceUD.Visible = true;
            price = priceDeleteLabel.Text.ToString().Trim();
        }

        private void comboBoxPriceUD_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceUpdateLabel.Visible = true;
            priceDeleteLabel.Visible = true;


            if (!comboBoxPriceUD.Visible == false)
            {
                tetAmount.Text = comboBoxPriceUD.SelectedItem.ToString();
                btnPrice.Text =price;
            }
            previous_price = comboBoxPriceUD.SelectedItem.ToString();
            comboBoxPriceUD.Visible = false;
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
                    else if (p_id != null && u_id != null && btnPrice.Text.Equals("ပြင်မည်"))
                    {
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Update Price Set Price=@price,P_id=@p_id,U_id=@u_id  Where P_id=@p_id and U_id=@u_id and Price=@prices";
                            cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                            cmd.Parameters.AddWithValue("@p_id", p_id);
                            cmd.Parameters.AddWithValue("@u_id", u_id);
                            cmd.Parameters.AddWithValue("@prices",previous_price );
                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulUpdateMessage();
                            tetAmount.Text = "";
                            comboBoxProduct.SelectedIndex = 0;
                            comboBoxUnit.SelectedIndex = 0;
                            btnPrice.Text = "ထည့်မည်";
                        }
                        catch
                        {
                        }

                    }
                    else if (btnPrice.Text.ToString().Equals("ဖြတ်မည်") && p_id != null && u_id != null)
                    {

                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "Delete  From Price  Where Price=@prices and U_id=@u_id and P_id=@p_id";
                            cmd.Parameters.AddWithValue("@u_id", u_id);
                            cmd.Parameters.AddWithValue("@p_id", p_id);
                            cmd.Parameters.AddWithValue("@prices", tetAmount.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBoxShowing.showSuccessfulDeleteMessage();
                            tetAmount.Text = "";
                            comboBoxProduct.SelectedIndex = 0;
                            comboBoxUnit.SelectedIndex = 0;
                            btnPrice.Text = "ထည့်မည်";
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
                comboBoxUPPrice();
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
                            comboBoxUPPrice();

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

      
      
        }

        
        }
  
