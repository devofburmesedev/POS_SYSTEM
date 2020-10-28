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
    public partial class SaleLists : Form
    {

        
            public SaleLists()
        {
            InitializeComponent();
            categoryComobox();
            unitComobox();
            productComobox();
            storeComobox();
        }
            double qty = 0;
        double totalprice=0;
        double s_id = 0;
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
        private int getStoresId(String p)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            int data = 0;
            con.Open();
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT S_id FROM Stores Where Name=@name";
                cmd.Parameters.AddWithValue("@name", p);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    data = Convert.ToInt32(reader["S_id"].ToString());

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

        private void categoryComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                comboBoxCategory1.Items.Clear();
              
                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT C_id,C_Name FROM Category";
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxCategory1.Items.Add(reader["C_Name"].ToString());
                    
                }
                comboBoxCategory1.SelectedIndex = 0;
               
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
                cmdProduct.CommandText = "SELECT P_Name FROM Product where C_id=@c_id";
                cmdProduct.Parameters.AddWithValue("@c_id",getCategoryId(comboBoxCategory1.SelectedItem.ToString()));
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
        int i=1;
        
      
       
        private void InsertProduct(int id,String product,String price,String qtys,String stores)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            
             String []datetimes=qtys.Split(' ');
                                String amount, unit;
                                amount = datetimes[0];
                                unit = datetimes[1];
          
            
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO VoucherProduct(P_id,Price,Amount,U_id,V_id,Total,S_id) VALUES(@p_id,@price,@amount,@u_id,@v_id,@total,@s_id)";
                cmd.Parameters.AddWithValue("@p_id", getProductId(product));
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@u_id", getUnitId(unit));
                cmd.Parameters.AddWithValue("@v_id", id);
                cmd.Parameters.AddWithValue("@total", ((Convert.ToDouble(price)) * (Convert.ToDouble(amount))).ToString());
                cmd.Parameters.AddWithValue("@s_id", getStoresId(stores));
                cmd.ExecuteNonQuery();


            }
            catch
            {
            }

            finally
            {
                con.Close();
            }
           
        }

        private int getVId(String credit,String p_amount,String total,DateTime date,String name)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            con.Open();
           
           int id=0;
            try
            {
                cmd= con.CreateCommand();

                cmd.CommandText = "SELECT V_id FROM Voucher Where  isCredit=@credit and CustomerName=@name and Paid_Amount=@p_amount and Total_Amount=@totalamount and DateAndTime=@datetime";
                cmd.Parameters.AddWithValue("@credit", credit);
                cmd.Parameters.AddWithValue("@p_amount", p_amount);



                cmd.Parameters.AddWithValue("@totalamount", total);
                cmd.Parameters.AddWithValue("@datetime",date );
                cmd.Parameters.AddWithValue("@name", name);
                
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["V_id"].ToString());
                   
                }
              
            }

            catch
            {


            }
            finally
            {
                con.Close();
            }
            
            return id;
        }

        private void comboBoxCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                cmd.Parameters.AddWithValue("@name", comboBoxStores.SelectedItem.ToString());
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


    

        private void btnAdd_Click(object sender, EventArgs e)
        {

            

                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dataGridView1);
                                newRow.Cells[0].Value = i;

                                newRow.Cells[1].Value = comboBoxProduct.SelectedItem.ToString();
                                newRow.Cells[2].Value = txtPrice.Text.ToString();
                                newRow.Cells[3].Value = txtAmount.Text.ToString() + " " + comboBoxUnit.SelectedItem.ToString();
                                newRow.Cells[4].Value = Convert.ToDouble(txtPrice.Text.ToString()) * Convert.ToDouble(txtAmount.Text.ToString());
                                newRow.Cells[5].Value = comboBoxStores.SelectedItem.ToString();
                                i++;
                                totalprice += Convert.ToDouble(txtPrice.Text.ToString()) * Convert.ToDouble(txtAmount.Text.ToString());
                                dataGridView1.Rows.Add(newRow);


                                txtTotalTwo.Text = totalprice.ToString();
                               
                               
                               if (txtTotalTwo.Text.ToString() != "" && txtDiscout.Text.ToString() != "")
                                   txtPaidAmount.Text = (Convert.ToDouble(txtTotalTwo.Text.ToString()) - Convert.ToDouble(txtDiscout.Text.ToString())).ToString();


                               comboBoxUnit.SelectedIndex = 0;
                               comboBoxCategory1.SelectedIndex = 0;
                               txtTotalTwo.Text = totalprice.ToString();
                               comboBoxProduct.SelectedIndex = 0;
                               txtPrice.Text = "";
                               txtTotal.Text = "";
                               txtAmount.Text = "";
        }

      

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtAmount.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) && ch!=8 && ch!=46)
                e.Handled = true;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text.ToString() != "")
                    Convert.ToDouble(txtPrice.Text.ToString());
                if (txtPrice.Text.ToString() != "" && txtAmount.Text.ToString() != "")
                    txtTotal.Text = (Convert.ToDouble(txtPrice.Text.ToString()) * Convert.ToDouble(txtAmount.Text.ToString())).ToString();
            }
            catch
            {
                txtPrice.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
        }

        private void txtDiscout_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtDiscout.Text.ToString() != "")
                     Convert.ToDouble(txtDiscout.Text.ToString()).ToString();
                if (txtTotalTwo.Text.ToString() != "" && txtDiscout.Text.ToString() != "" && !creditCheckBox.Checked)
                    txtPaidAmount.Text = (Convert.ToDouble(txtTotalTwo.Text.ToString()) - Convert.ToDouble(txtDiscout.Text.ToString())).ToString();
            }
            catch
            {
                txtDiscout.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
        }
        String name = null;
        DateTime date; int id = 0;
        String discount = null, totals = null, paidamount = null;
        private void btnAmountTwo_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd, cmdCate;
            con.Open();
            bool condition = false;
            
             name = txtCustomer.Text.ToString();
            String credit = null;
            String p_amount = null;
            String total = txtTotalTwo.Text.ToString();
            totals = txtTotalTwo.Text.ToString();
            discount = txtDiscout.Text.ToString();
            date = DateTime.Now.Date;
            try
            {
                cmdCate = con.CreateCommand();
                string[] dateTime = date.ToString().Split('/');
                int day, month, years;
                int.TryParse(dateTime[1], out day);
                int.TryParse(dateTime[0], out month);
                int.TryParse(dateTime[2], out years);
                cmdCate.CommandText = "SELECT * FROM Voucher WHERE  Total_Amount=@totalamount and CustomerName=@name and Paid_Amount=@pamount And Discount=@discount and Day(DateAndTime)=@day and Year(DateAndTime)=@year";
                cmdCate.Parameters.AddWithValue("@name", name);
                cmdCate.Parameters.AddWithValue("@pamount", txtPaidAmount.Text.ToString());
                cmdCate.Parameters.AddWithValue("@discount", txtDiscout.Text.ToString());
                
                cmdCate.Parameters.AddWithValue("@totalamount", txtTotalTwo.Text.ToString());
                cmdCate.Parameters.AddWithValue("@year", years);
                cmdCate.Parameters.AddWithValue("@month", month);
                cmdCate.Parameters.AddWithValue("@day", day);
               
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
                if (condition == true && txtTotalTwo.Text.ToString() == "")
                    MessageBoxShowing.showIncomplementMessage();
                else if (condition == true && txtTotalTwo.Text.ToString() != "")
                {

                    if (creditCheckBox.Checked)
                    {
                        credit = "true";
                        paidamount=p_amount = txtPaidAmount.Text.ToString();
                    }
                    else
                    {
                        credit = "false";
                        paidamount=p_amount = txtPaidAmount.Text.ToString();
                    }
                    try
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "INSERT INTO Voucher(CustomerName,isCredit,Paid_Amount,Total_Amount,DateAndTime,Discount) VALUES(@name,@credit,@p_amount,@totalamount,@datetime,@discount)";

                        cmd.Parameters.AddWithValue("@credit", credit);
                        cmd.Parameters.AddWithValue("@p_amount", p_amount);



                        cmd.Parameters.AddWithValue("@totalamount", total);
                        cmd.Parameters.AddWithValue("@datetime", date);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@discount", txtDiscout.Text.ToString());
                       
                        cmd.ExecuteNonQuery();

                        id = getVId(credit, p_amount, total, date, name);



                        for (int rows = 0; rows < dataGridView1.Rows.Count; rows++)
                        {
                            String product = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                            String price = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                            String qty = dataGridView1.Rows[rows].Cells[3].Value.ToString();
                            String amount = dataGridView1.Rows[rows].Cells[4].Value.ToString();
                            String stores = dataGridView1.Rows[rows].Cells[5].Value.ToString();
                           // MessageBox.Show(qty + " " + stores+" "+product+" "+price);
                            InsertProduct(id, product, price, qty, stores);
                            UpdateStores(id, product, price, qty,stores);
                            
                        }
                        BindGrid();
                        txtTotalTwo.Text = "";
                        txtDiscout.Text = "0";
                        creditCheckBox.Checked = false;
                        txtPaidAmount.Text = "";
                        //dataGridView1.Columns.Clear();
                        txtCustomer.Text = "";
                       
                        i = 1;

                    }
                    catch
                    {
                    }

                }
                MessageBoxShowing.showSuccessfulMessage();
               
            }
            catch
            {

            }
                
            finally
            {
                
                con.Close();
            }
         
           
           
        }

        private void UpdateStores(int id, string product, string price, string qtys, string stores)
        {
            double amount = 0;
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String[] datetimes = qtys.Split(' ');
            String amounts, unit;
            amounts= datetimes[0];
            unit = datetimes[1];
            
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT ProductInStores.Amount FROM ProductInStores,Stores Where ProductInStores.P_id=@p_id and ProductInStores.U_id=@u_id and ProductInStores.S_id=Stores.S_id and Stores.Name=@name";
                cmd.Parameters.AddWithValue("@p_id", getProductId(product));
                cmd.Parameters.AddWithValue("@u_id", getUnitId(unit));
                cmd.Parameters.AddWithValue("@name", stores);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    amount = Convert.ToDouble(reader["Amount"].ToString());


                }
            }

            catch
            {

                MessageBox.Show("erroe1");
            }
            finally
            {
                comboBoBoxName();
                con.Close();
            }
            double leftqty = amount- Convert.ToDouble(amounts);

         
            try
            {
                //  MessageBox.Show(getProductId(comboBoxProduct.SelectedItem.ToString())+" "+getUnitId(comboBoxUnit.SelectedItem.ToString())+" "+s_id);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "Update ProductInStores Set Amount=@amount Where P_id=@p_id and U_id=@u_id and S_id=@s_id";
                cmd.Parameters.AddWithValue("@p_id", getProductId(product));
                cmd.Parameters.AddWithValue("@u_id", getUnitId(unit));
                cmd.Parameters.AddWithValue("@s_id", getStoresId(stores));
                cmd.Parameters.AddWithValue("@amount", leftqty);
                cmd.ExecuteNonQuery();
                //MessageBox.Show(qty+" "+amount);
            }
            catch
            {

                MessageBox.Show("erroe2");
            }
            finally
            {
                con.Close();

            }
        }

        private void SaleLists_Load(object sender, EventArgs e)
        {
            txtDiscout.Text = "0";
            txtPaidAmount.Visible = true;
            paidAmountLabel.Visible = true;
            txtTotalTwo.ReadOnly = true;
            BindGrid();
            storeComobox();
        }
        private void storeComobox()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdUnit;
            con.Open();
            try
            {
                
                comboBoxStores.Items.Clear();
                cmdUnit = con.CreateCommand();
                comboBoxStores.Items.Add("None");
                cmdUnit.CommandText = "SELECT Name FROM Stores";
                var reader = cmdUnit.ExecuteReader();
                while (reader.Read())
                {
                    
                    comboBoxStores.Items.Add(reader["Name"].ToString());

                }
                
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
        private void BindGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();
            txtTotal.ReadOnly = true;
            DataGridViewCellStyle style = dataGridView1.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Green;
            style.ForeColor = Color.Green;
            style.Font = new Font("Times New Roman", 14, FontStyle.Bold);


            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
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
            product.Width = 150;
            dataGridView1.Columns.Insert(1, product);
            DataGridViewColumn price = new DataGridViewTextBoxColumn();
            price.Name = "price";
            price.HeaderText = "တန်ဖိုးငွေကျပ်";
            price.DataPropertyName = "price";
            price.Width = 150;
            dataGridView1.Columns.Insert(2, price);
            DataGridViewColumn qty = new DataGridViewTextBoxColumn();
            qty.Name = "qty";
            qty.HeaderText = "အရေအတွက်";
            qty.DataPropertyName = "qty";
            qty.Width = 150;
            dataGridView1.Columns.Insert(3, qty);

            DataGridViewColumn amount = new DataGridViewTextBoxColumn();
            amount.Name = "amount";
            amount.HeaderText = "သင့်ငွေ";
            amount.DataPropertyName = "amount";
            amount.Width = 100;
            dataGridView1.Columns.Insert(4, amount);
            DataGridViewColumn store = new DataGridViewTextBoxColumn();
            store.Name = "Product";
            store.HeaderText = "သိုလှောင်ရုံ";
            store.DataPropertyName = "Product";
            store.Width = 150;
            dataGridView1.Columns.Insert(5, store);
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();

            btnDelete.Text = "ဖြတ်မည်";

            btnDelete.DataPropertyName = "delete";
            btnDelete.Width = 100;
            btnDelete.CellTemplate.Style.BackColor = Color.Aqua;

            btnDelete.FlatStyle = FlatStyle.Standard;
            btnDelete.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Insert(6, btnDelete);
            dataGridView1.DataSource = null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (this.dataGridView1.Rows.Count > 0)
                {
                    DialogResult result = MessageBoxShowing.showDeleteYesNo();
                    if (result == DialogResult.Yes)
                    {

                        dataGridView1.Rows.Remove(this.dataGridView1.Rows[e.RowIndex]);
                    }
                }
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text.ToString() != "")
                Convert.ToDouble(txtAmount.Text.ToString()).ToString();
                if (txtPrice.Text.ToString() != "" && txtAmount.Text.ToString() != "")
                {
                    txtTotal.Text = (Convert.ToDouble(txtPrice.Text.ToString()) * Convert.ToDouble(txtAmount.Text.ToString())).ToString();
                }
            }
            catch
            {
                txtAmount.Text = "";
                MessageBoxShowing.showNumberErrorMessage();
            }
            if (  txtAmount.Text.ToString() != "" && qty < Convert.ToDouble(txtAmount.Text.ToString()) && comboBoxStores.SelectedItem.ToString()!= "None")
            {
                MessageBox.Show("No enough...");
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }

        private void creditCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (creditCheckBox.Checked)
                txtPaidAmount.Text = "0";
            else
                if (txtTotalTwo.Text.ToString() != "" && txtDiscout.Text.ToString() != "" && !creditCheckBox.Checked)
                    txtPaidAmount.Text = (Convert.ToDouble(txtTotalTwo.Text.ToString()) - Convert.ToDouble(txtDiscout.Text.ToString())).ToString();
        }

        private void comboBoxCategory1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            productComobox();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.Print print = new Report.Print(id,name,date.ToString("dd/MM/yyyy"),totals,paidamount,discount);
            print.Show();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalTwo_TextChanged(object sender, EventArgs e)
        {

        }

      
        String u_id = null, p_id = null;
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
            //productComobox();
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
                          
                            comboBoxUPPrice();

                        }

                    }
                    else
                    {
                      
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
        private void comboBoxUPPrice()
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            try
            {
                //comboBoxUpdate.DataSource = null;

                cmdCate = con.CreateCommand();
                cmdCate.CommandText = "SELECT Price FROM Price Where P_id=@p_id and U_id=@u_id";
                cmdCate.Parameters.AddWithValue("@p_id", p_id);
                cmdCate.Parameters.AddWithValue("@u_id", u_id);
                SqlDataReader reader = cmdCate.ExecuteReader();
                while (reader.Read())
                {

                    txtPrice.Text = reader["Price"].ToString();


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

        private void txtDiscout_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (txtPaidAmount.Text.ToString() != "")
                    Convert.ToDouble(txtPaidAmount.Text.ToString());
                }
                catch
            {
                txtPaidAmount.Text = "";
                    MessageBoxShowing.showNumberErrorMessage();

                }
        }

        private void comboBoxStores_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmdCate;
            con.Open();
            
            string name = comboBoxStores.SelectedItem.ToString();
            try
            {
                //comboBoxUpdate.DataSource = null;
               
                if (name != "None")
                {
                    cmdCate = con.CreateCommand();
                    cmdCate.CommandText = "SELECT ProductInStores.Amount FROM ProductInStores,Stores Where ProductInStores.P_id=@p_id and ProductInStores.U_id=@u_id and ProductInStores.S_id=Stores.S_id and Stores.Name=@name";
                    cmdCate.Parameters.AddWithValue("@p_id", getProductId(comboBoxProduct.SelectedItem.ToString()));
                    cmdCate.Parameters.AddWithValue("@u_id", getUnitId(comboBoxUnit.SelectedItem.ToString()));
                    cmdCate.Parameters.AddWithValue("@name", comboBoxStores.SelectedItem.ToString());
                    SqlDataReader reader = cmdCate.ExecuteReader();
                    while (reader.Read())
                    {

                        qty = Convert.ToDouble(reader["Amount"].ToString());


                    }
                }
            }
            catch
            {


            }
            finally
            {
                comboBoBoxName();
                con.Close();
            }
           // MessageBox.Show(qty+" "+getProductId(comboBoxProduct.SelectedItem.ToString())+" "+getUnitId(comboBoxUnit.SelectedItem.ToString()));
            if (txtAmount.Text.ToString()!="" && qty < Convert.ToDouble(txtAmount.Text.ToString()) && name != "None" )
            {
                MessageBox.Show("No enough...");
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }

      
        }
        }
    

