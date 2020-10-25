using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointOfSaleSystem
{
    class MessageBoxShowing
    {
        public static void showSuccessfulMessage()
        {
            MessageBox.Show("ဒေတာထည့်သွင်းမှုအောင်မြင်ပါသည်", "သတိပေးချက်",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }
        public static void showSuccessfulDeleteMessage()
        {
            MessageBox.Show("ဒေတာဖြတ်မှုအောင်မြင်ပါသည်", "သတိပေးချက်", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static void showSuccessfulUpdateMessage()
        {
            MessageBox.Show("ဒေတာပြင်ဆင်မှုအောင်မြင်ပါသည်", "သတိပေးချက်", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static void showWarningMessage()
        {
            MessageBox.Show("သင်ထည့်သောဒေတာမာထည့်ပြီးသားဖြစ်ပါသည်", "သတိပေးချက်", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void showIncomplementMessage()
        {
            MessageBox.Show("ကျေးဇူးပြု၍ဒေတာထည့်သွင်းပါ","သတိပေးချက်", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void showNumberErrorMessage()
        {
            MessageBox.Show("Please enter correcr number", "သတိပေးချက်", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public static DialogResult showDeleteYesNo()
        {
           return MessageBox.Show("Are you sure to delete", "သတိပေးချက်", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
        }
    }
}
