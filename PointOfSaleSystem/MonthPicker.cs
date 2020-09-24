using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PointOfSaleSystem
{
    class MonthPicker:DateTimePicker
    {
        public MonthPicker()
        {
            Format = DateTimePickerFormat.Custom;
            CustomFormat = "MMMM/yyyy";
        }
        public new  DateTimePickerFormat Format
        {
             get
             {
                 return base.Format;
             }
         
           set
           {
                base.Format=value;
           }
        }
        public new string CustomFormat
        {
            get
            {
                return base.CustomFormat;
            }
            set
            {
                base.CustomFormat = value;
            }
        }
  protected override void  WndProc(ref Message m)
{
      if(m.Msg==WM_NOFITY)
      {
          var nmhdr=(NMHDR)Marshal.PtrToStructure(m.LParam,typeof(NMHDR));
          switch(nmhdr.code)
          {
              case -950:
                  var cal=SendMessage(Handle,DTM_GETMONTHCAL,IntPtr.Zero,IntPtr.Zero);
                  SendMessage(cal,MCM_SETCURRENTVIEW,IntPtr.Zero,(IntPtr)1);
                  break;
              case MCN_VIEWCHANGE:
                  var nmviewchange=(NMVIEWCHANGE)Marshal.PtrToStructure(m.LParam,typeof(NMVIEWCHANGE));
                  if(nmviewchange.dwOldView==1 && nmviewchange.dwNewView==0)
                      SendMessage(Handle,DTM_CLOSEMONTHCAL,IntPtr.Zero,IntPtr.Zero);
                  break;
          }
      }
 	 base.WndProc(ref m);
}
        private const int WM_NOFITY=0x004e;
        private const int DTM_CLOSEMONTHCAL=0x1000;
        private const int DTM_GETMONTHCAL=0x1000+8;
        private const int MCM_SETCURRENTVIEW=0x1000+32;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private const int MCN_VIEWCHANGE=-750;
        public static extern IntPtr SendMessage(IntPtr hWnd,int wMsg,IntPtr wParm,IntPtr lParm);
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idForm;
            public int code;
        }
         struct NMVIEWCHANGE
        {   
            public NMHDR nmhdr;
            public int dwOldView;
            public int dwNewView;

        }

         private void InitializeComponent()
         {
             this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
             this.SuspendLayout();
             // 
             // dateTimePicker2
             // 
             this.dateTimePicker2.Location = new System.Drawing.Point(0, 0);
             this.dateTimePicker2.Name = "dateTimePicker2";
             this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
             this.dateTimePicker2.TabIndex = 0;
             this.ResumeLayout(false);

         }

        
}
    }

