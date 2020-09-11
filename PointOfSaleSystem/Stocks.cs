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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
        }
        
        String p_id=null,u_id=null,previous_price=null;
        String product, price;
        private void Stocks_Load(object sender, EventArgs e)
        {
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
            BindGrid(comboBoxCategory1.SelectedItem.ToString());
           
         }

            
        private void btnCategory_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd, cmdCate;
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
                    MessageBox.Show("သင်ထည့်သောဒေတာမာထည့်ပြီးသားဖြစ်ပါသည်", "သတိပေးချက်");
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
                if (tetCategory.Text.ToString().Trim()=="")
                    MessageBox.Show("ကျေးဇူးပြု၍ဒေတာထည့်သွင်းပါ");
                else if (condition == true && tetCategory.Text.ToString().Trim() != null && tetCategory.Text.ToString().Trim()!="" && btnCategory.Text.ToString().Equals("ထည့်မည်"))
                {
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Category(C_Name) VALUES(@name)";
                        cmd.Parameters.AddWithValue("@name", tetCategory.Text.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        tetCategory.Text = "";
                        MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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
                        MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                        btnCategory.Text="ထည့်မည်";
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
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdCate;
            con.Open();
            try
            {
                //comboBoxUpdate.DataSource = null;
                comboBoxPriceUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT Price FROM Price";
                MySqlDataReader reader = cmdCate.ExecuteReader();
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
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd, cmdUnit;
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
                    tetUnit.Text = "";
                    MessageBox.Show("သင်ထည့်သောဒေတာမာထည့်ပြီးသားဖြစ်ပါသည်", "သတိပေးချက်");
                    
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
                if (tetUnit.Text.ToString().Trim() == "")
                    MessageBox.Show("ကျေးဇူးပြု၍ဒေတာထည့်သွင်းပါ");
                else if (condition == true && tetUnit.Text.ToString().Trim() != null && tetUnit.Text.ToString().Trim() !="" && addUnit.Text.ToString().Equals("ထည့်မည်"))
                {
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Unit(U_Name) VALUES(@name)";
                    cmd.Parameters.AddWithValue("@name", tetUnit.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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
                    MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd, cmdProduct;
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
                    MessageBox.Show("သင်ထည့်သောဒေတာမာထည့်ပြီးသားဖြစ်ပါသည်", "သတိပေးချက်");
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
            
                if (comboBoxCategory2.SelectedItem != null)
                {
                    con.Open();
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT C_id FROM Category WHERE C_Name=@name";
                        cmd.Parameters.AddWithValue("@name", comboBoxCategory2.SelectedItem);
                        MySqlDataReader reads = cmd.ExecuteReader();
                        while (reads.Read())
                        {

                            c_id = reads.GetString("C_id");
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
                    if (condition && c_id != null && textBox3.Text.ToString().Trim() != null && addProduct.Text.ToString().Equals("ထည့်မည်"))
                    {
                    if (textBox3.Text.ToString().Trim() == "")
                        MessageBox.Show("ကျေးဇူးပြု၍ဒေတာထည့်သွင်းပါ", "သတိပေးချက်");
                        try
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandText = "INSERT INTO Product(C_id,P_Name) VALUES(@c_id,@name)";
                            cmd.Parameters.AddWithValue("@c_id", c_id);
                            cmd.Parameters.AddWithValue("@name", textBox3.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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
                            MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                            addProduct.Text = "ထည့်မည်";
                            addProduct.Text = "";
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
                            cmd.CommandText = "Delete Price.*,Product.* From Product,Price Where Product.P_Name=@nameUpdate";
                            cmd.Parameters.AddWithValue("@nameUpdate", comboBoxProductUD.SelectedItem.ToString());

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ဒေတာဖြတ်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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

            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxProductUD.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT P_Name FROM Product";
                MySqlDataReader reader = cmdCate.ExecuteReader();
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
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUnitUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT U_Name FROM Unit";
                MySqlDataReader reader = cmdCate.ExecuteReader();
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
               
                style.Font = new Font("Times New Roman", 20); 
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
                MySqlConnection con = new MyConnection().GetConnection();
                MySqlCommand cmd;
                con.Open();
                try
                {
                    cmd = con.CreateCommand();
                   cmd.CommandText= "SELECT Product.P_Name,Unit.U_Name,Price.Price FROM Price,Product,Unit,Category Where Category.C_id=Product.C_id and Category.C_Name=@c_name and Unit.U_id=Price.U_id and Product.P_id=Price.P_id";
                    cmd.Parameters.AddWithValue("@c_name", data);
                    MySqlDataReader reader = cmd.ExecuteReader();
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

       

        private void comboBoxCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String data = comboBoxCategory1.SelectedItem.ToString();
            BindGrid(data);
            
        }

     

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {   

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT P_id FROM Product WHERE P_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxProduct.SelectedItem);
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
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;
            con.Open();
            try
            {
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT U_id FROM Unit WHERE U_Name=@name";
                cmd.Parameters.AddWithValue("@name", comboBoxUnit.SelectedItem);
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
                            previous_price=tetAmount.Text = reader.GetString("Price");
                           // btnPrice.Enabled = false;
                            
                        }

                    }
                    else
                    {
                        btnPrice.Text = "ထည့်မည်";
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

        private void tetAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (previous_price.Equals(tetAmount.Text.ToString()))
                {
                    if (btnPrice.Text.ToString().Equals("ဖြတ်မည်"))
                        btnPrice.Enabled = true;
                    else
                    {
                        
                        btnPrice.Enabled = false;
                    }
                    btnPrice.Text = "ထည့်မည်";
                }
                else
                {
                    btnPrice.Text = "ပြင်မည်";
                    btnPrice.Enabled = true;

                }
            }
            catch
            {
            }
            
        }

       

        private void updateLabel_Click(object sender, EventArgs e)
        {
            comboBoxUpdate.Visible = true;
            updateLabel.Visible = false;
            
        }

        private void comoBoxUpdateCategory()
        {
            
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxUpdate.Items.Clear();
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_Name FROM Category";
                MySqlDataReader reader = cmdCate.ExecuteReader();
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
            

            if (!comboBoxProductUD.Visible == false)
            {
                textBox3.Text = comboBoxProductUD.SelectedItem.ToString();
                addProduct.Text = product;
            }
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
            comboBoxPriceUD.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPrice_Click_1(object sender, EventArgs e)
        {
            MySqlConnection con = new MyConnection().GetConnection();
            MySqlCommand cmd;

            con.Open();
            try
            {
                if (tetAmount.Text.Trim() == "" && p_id == null && u_id == null)
                    MessageBox.Show("ကျေးဇူးပြု၍ဒေတာထည့်သွင်းပါ", "သတိပေးချက်");
                else
                if (p_id != null && u_id != null && btnPrice.Text.Equals("ထည့်မည်"))
                {
                    
                    
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Price(Price,P_id,U_id) VALUES(@price,@p_id,@u_id)";
                        cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_id", p_id);
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်");
                        tetAmount.Text = "";
                        comboBoxProduct.SelectedIndex = 0;
                        comboBoxUnit.SelectedIndex = 0;
                    }
                    catch
                    {
                    }
                }
                else if (p_id != null && u_id != null && btnPrice.Text.Equals("ပြင်မည်"))
                {
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "Update Price Set Price=@price,U_id=@p_id,U_id=@u_id Where P_id=@p_id and U_id=@u_id and Price=@prices";
                        cmd.Parameters.AddWithValue("@price", tetAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_id", p_id);
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        cmd.Parameters.AddWithValue("@prices", previous_price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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

                        cmd.Parameters.AddWithValue("@prices", comboBoxPriceUD.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        cmd.Parameters.AddWithValue("@p_id", p_id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ဒေတာဖြတ်မှုအောင်မြင်ပါသည်", "သတိပေးချက်");
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
            catch (Exception)
            {

                throw;

            }
            finally
            {
                comboBoxUPPrice();
                BindGrid(comboBoxCategory1.SelectedItem.ToString());
                con.Close();
            }
        }

       
        }
    
    }

