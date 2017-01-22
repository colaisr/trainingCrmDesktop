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
    public partial class NewAccount : Form
    {
        private BindingList<Account> listOfAccounts;
        public string CreditCard;
        public DateTime Expires;
        public string CCV="vb";
        private bool addClicked = false;        
        private DataGridView dgvAccounts;

        public NewAccount()
        {
            InitializeComponent();
        }

        public NewAccount(BindingList<Account> listOfAccounts, DataGridView dgvAccounts)
        {
            this.listOfAccounts = listOfAccounts;
            this.listOfAccounts.Add(new Account());
            InitializeComponent();
            this.dgvAccounts = dgvAccounts;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddAccount();
            this.DialogResult = DialogResult.OK;
        }

        private void AddAccount()
        {
            Status status ;
            if (rbnNew.Checked)
            {
                status = Status.New;
            }
            else
            {
                if (rbnPending.Checked)
                {
                    status = Status.Pending;
                }
                else
                {
                    status = Status.Closed;
                }
            }
            listOfAccounts.Last().Name = txtName.Text;
            listOfAccounts.Last().WebPage = txtWebPage.Text;
            listOfAccounts.Last().Active = chkIsActive.Checked;
            listOfAccounts.Last().Source = cmbSource.Text;
            listOfAccounts.Last().Status = status;

            listOfAccounts.Last().State = cmbState.Text;
            listOfAccounts.Last().City = txtCity.Text;
            listOfAccounts.Last().Zip = txtZip.Text;
            listOfAccounts.Last().Street = txtStreet.Text;
            dgvAccounts.Refresh();
            addClicked = true;
            this.Close();           
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
           this.Close();
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            var CRInfo = new CreditCardInfo(listOfAccounts);
            CRInfo.Show();
        }

        private void NewAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void NewAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (addClicked!=true)
            {
                listOfAccounts.Remove(listOfAccounts.Last());
            }
        }
    }
}
