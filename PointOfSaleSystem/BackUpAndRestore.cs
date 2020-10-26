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
    public partial class BackUpAndRestore : Form
    {
        public BackUpAndRestore()
        {
            InitializeComponent();
        }
       
        private void BackUpAndRestore_Load(object sender, EventArgs e)
        {

        }
        private void btnBrowseOne_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.SelectedPath;
                btnBackUp.Enabled = true;
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            SqlCommand cmd;
            String database = con.Database.ToString();
            try
            {
               
               
                   // String sql = String.Format("BACKUP DATANASE[{0}] TO DISK='{1}'",database,textBox1.Text.ToString().Trim());
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "BACKUP DATABASE [" + database + "] TO DISK='" + textBox1.Text.ToString().Trim() + "\\"+"Database"+"-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")+ ".bak'" ;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success.......");
                    btnBackUp.Enabled = false;

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fail......."+database+ex.GetBaseException());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnBrowseTwo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database Restore";
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                textBox2.Text = dlg.FileName;
                btnRestore.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            SqlConnection con = new MyConnection().GetConnection();
            
            String database = con.Database.ToString();
            try
            {
                con.Open();
               String sql2 = String.Format("ALTER DATABASE ["+database+"] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                SqlCommand bu2 = new SqlCommand(sql2, con);
                bu2.ExecuteNonQuery();
                String sql3 = String.Format("USE MASTER RESTORE DATABASE ["+database+"] FROM DISK='"+textBox2.Text.ToString().Trim()+"' WITH REPLACE;");

                SqlCommand bu3 = new SqlCommand(sql3, con);
                bu3.ExecuteNonQuery();
                String sql4 = String.Format("ALTER DATABASE [" + database + "] SET MULTI_USER ");

                SqlCommand bu4 = new SqlCommand(sql4, con);
                bu4.ExecuteNonQuery();
                MessageBox.Show("Success.......");
                btnRestore.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail......." + database + ex.GetBaseException());
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
