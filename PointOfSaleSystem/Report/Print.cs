using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace PointOfSaleSystem.Report
{
    public partial class Print : Form
    {
        String name, datetime, total, discount, paidamount;
        int id;
        public Print(int id,String name, String date, String total, String paidamount, String discount)
        {
            InitializeComponent();
            this.name = name;
            this.datetime = date;
            this.total = total;
            this.discount = discount;
            this.paidamount = paidamount;
            this.id = id;
        }

        private void Print_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            string[] dateTime = datetime.Split('/');
            int day, month, years;
            int.TryParse(dateTime[0], out day);
            int.TryParse(dateTime[1], out month);
            int.TryParse(dateTime[2], out years);
            SqlConnection con = new MyConnection().GetConnection();

            Microsoft.Reporting.WinForms.ReportParameter [] p =new Microsoft.Reporting.WinForms.ReportParameter[]
            
            {
            new Microsoft.Reporting.WinForms.ReportParameter("name",name),
            new Microsoft.Reporting.WinForms.ReportParameter("date",datetime),
            new Microsoft.Reporting.WinForms.ReportParameter("total",total),
            new Microsoft.Reporting.WinForms.ReportParameter("discount",discount),
            new Microsoft.Reporting.WinForms.ReportParameter("paidamount",paidamount)
            
        };
            this.reportViewer1.LocalReport.SetParameters(p);
            this.reportViewer1.RefreshReport();

            con.Open();
            try
            {
               
                SqlDataAdapter da = new SqlDataAdapter("SELECT * From Voucher,VoucherProduct,Product Where Day(Voucher.DateAndTime)=@day and Voucher.V_id=VoucherProduct.V_id and Year(Voucher.DateAndTime)=@year and Month(Voucher.DateAndTime)=@month and Voucher.CustomerName=@name and Product.P_id=VoucherProduct.P_id and Voucher.Total_Amount=@amount and Voucher.Paid_Amount=@pamount and Voucher.Discount=@discount and Voucher.V_id=@id", con);
                da.SelectCommand.Parameters.AddWithValue("@day", day);
                da.SelectCommand.Parameters.AddWithValue("@month", month);
                da.SelectCommand.Parameters.AddWithValue("@year", years);
                da.SelectCommand.Parameters.AddWithValue("@name", name);
                da.SelectCommand.Parameters.AddWithValue("@amount",total);
                da.SelectCommand.Parameters.AddWithValue("@discount", discount);
                da.SelectCommand.Parameters.AddWithValue("@pamount", paidamount);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                DataSet1 ds = new DataSet1();
                da.Fill(ds, "DataTable1");


                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(datasource);
                this.reportViewer1.RefreshReport();


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
