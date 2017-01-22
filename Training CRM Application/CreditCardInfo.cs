using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Training_CRM_Application
{
    public partial class CreditCardInfo : Form
    {
        private BindingList<Account> bL;  

        public CreditCardInfo()
        {
            InitializeComponent();
        }

        public CreditCardInfo(BindingList<Account> bL)
        {
            this.bL = bL;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bL.Last().CCV = txtCCV.Text;
            bL.Last().CardNumber = txtCreditCard.Text;
            bL.Last().CardExpires = datExpiration.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
