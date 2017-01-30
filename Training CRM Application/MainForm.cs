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
    public partial class MainForm : Form
    {
        public List<Account> Accounts { get; set; }
         BindingList<Account> ListOfAccounts;
         BindingList<Contact> ListOfContacts;
        int CurrentAccount = 0;
        int CurrentContact = 0;

        public MainForm()
        {
            InitializeComponent();
            fillInitialAccounts();
            SetValuesInPaneAccount(CurrentAccount);
            SetValuesInPaneContacts(CurrentContact);

        }


        private void fillInitialAccounts()
        {
            ListOfAccounts = new BindingList<Account>() {
                new Account() {
                    Name = "Nice",
                    Source ="WEB",
                    Status =Status.New,
                    WebPage ="nice.com",
                    Street ="Zarhin 12",
                    City ="Raanana",
                    State ="Arizona",
                    Zip ="45689"},
                new Account() {
                    Name ="Google",
                    Source ="hot Net",
                    Status =Status.Closed,
                    WebPage ="google.com",
                    Street ="Smolensin 14",
                    City ="Holon",
                    State ="Cansas",
                    Zip ="654987" } };

            AccountsSource = new BindingSource(ListOfAccounts, null);
            dgvAccounts.DataSource = AccountsSource;


            //contacts default Values
            ListOfContacts = new BindingList<Contact>();
            ListOfContacts.Add(new Contact()
            {
                FirstName = "Gal",
                LastName = "Tesler",
                Salutation = "Mr",
                Account = "Eaglue",
                City = "Tel Aviv",
                Email = "gal.tesler@eaglue.com",
                Phone = "0542334569",
                State = "Nebraska",
                Street = "Rotshild 12",
                Zip = "321654"
            });
            ListOfContacts.Add(new Contact()
            {
                FirstName = "Gaby",
                LastName = "Maor",
                Salutation = "Mr",
                Account = "Nice",
                City = "London",
                Email = "Gaby.maor@somwhere.com",
                Phone = "0542334987",
                State = "Nebraska",
                Street = "Usishkin 12",
                Zip = "654654"
            });

            ContactsSource = new BindingSource(ListOfContacts, null);
            dgvContacts.DataSource = ContactsSource;

        }
        #region Account Tab

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = null;

            if (dgvAccounts.SelectedRows != null)
            {
                selectedRows = dgvAccounts.SelectedRows;
            }
            if (selectedRows != null)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    int i = row.Index;

                    ListOfAccounts.RemoveAt(i);
                }
            }
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var row = dgvAccounts.Rows[index];
            row.Selected = true;

            CurrentAccount = index;
            SetValuesInPaneAccount(index);

            btnRemoveAcount.Enabled = true;
        }

        private void SetValuesInPaneAccount(int index)
        {   //props
            txtName.Text = ListOfAccounts[index].Name;
            cmbSource.Text = ListOfAccounts[index].Source;
            chkIsActive.Checked = ListOfAccounts[index].Active;
            txtWebPage.Text = ListOfAccounts[index].WebPage;
            switch (ListOfAccounts[index].Status)
            {
                case Status.New:
                    rbnNew.Checked = true;
                    break;
                case Status.Pending:
                    rbnPending.Checked = true;
                    break;
                case Status.Closed:
                    rbnClosed.Checked = true;
                    break;
                default:
                    break;
            }
            //address
            txtCity.Text = ListOfAccounts[index].City;
            txtStreet.Text = ListOfAccounts[index].Street;
            cmbState.Text = ListOfAccounts[index].State;
            txtZip.Text = ListOfAccounts[index].Zip;
            
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfAccounts[CurrentAccount].Name = t.Text;
            dgvAccounts.Refresh();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            ListOfAccounts[CurrentAccount].Source = t.Text;
            dgvAccounts.Refresh();
        }

        private void rbnNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnNew.Checked)
            {
                ListOfAccounts[CurrentAccount].Status = Status.New;
                dgvAccounts.Refresh();
            }
            else
            {
                if (rbnPending.Checked)
                {
                    ListOfAccounts[CurrentAccount].Status = Status.Pending;
                    dgvAccounts.Refresh();
                }
                else
                {
                    ListOfAccounts[CurrentAccount].Status = Status.Closed;
                    dgvAccounts.Refresh();
                }
            }

        }

        private void txtWebPage_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfAccounts[CurrentAccount].WebPage = t.Text;
            dgvAccounts.Refresh();
        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfAccounts[CurrentAccount].Street = t.Text;
            dgvAccounts.Refresh();
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfAccounts[CurrentAccount].City = t.Text;
            dgvAccounts.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfAccounts[CurrentAccount].Zip = t.Text;
            dgvAccounts.Refresh();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            ListOfAccounts[CurrentAccount].State = t.Text;
            dgvAccounts.Refresh();
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            var t = sender as CheckBox;
            ListOfAccounts[CurrentAccount].Active = t.Checked;
            dgvAccounts.Refresh();
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            var n = new NewAccount(ListOfAccounts, dgvAccounts);

            n.Show();


            //markedOut- RT Does not catches event of click- if in show Dialog

            //DialogResult result;
            //result = n.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    dgvAccounts.Refresh();
            //}
            //else
            //{
            //    if (result == DialogResult.Cancel)
            //    {
            //        ListOfAccounts.Remove(ListOfAccounts.Last());
            //    }
            //}
        }

        private void dgvAccounts_Leave(object sender, EventArgs e)
        {
            btnRemoveAcount.Enabled = false;
        }
        
        #endregion

        #region Contact tab

        private void btnRemoveContact_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = null;

            if (dgvContacts.SelectedRows != null)
            {
                selectedRows = dgvContacts.SelectedRows;
            }
            if (selectedRows != null)
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    int i = row.Index;

                    ListOfContacts.RemoveAt(i);
                }
            }
        }

        private void dgvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var row = dgvContacts.Rows[index];
            row.Selected = true;

            CurrentAccount = index;
            SetValuesInPaneContacts(index);

            btnRemoveContact.Enabled = true;
        }

        private void SetValuesInPaneContacts(int index)
        {
            //props
            cmbSalutation.Text = ListOfContacts[index].Salutation;
            txtFirstName.Text= ListOfContacts[index].FirstName;
            txtLastName.Text= ListOfContacts[index].LastName;
            cmbAccount.Text= ListOfContacts[index].Account;
            txtPhone.Text= ListOfContacts[index].Phone;
            txtEmail.Text= ListOfContacts[index].Email;

            //address
            txtHomeCity.Text = ListOfContacts[index].City;
            txtHomeStreet.Text = ListOfContacts[index].Street;
            cmbHomeState.Text = ListOfContacts[index].State;
            txtHomeZip.Text = ListOfContacts[index].Zip;
        }


        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].FirstName = t.Text;
            dgvContacts.Refresh();

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].LastName = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbSalutation_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            ListOfContacts[CurrentAccount].Salutation = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbAccount_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            ListOfContacts[CurrentAccount].Account = t.Text;
            dgvContacts.Refresh();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].Phone = t.Text;
            dgvContacts.Refresh();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].Email = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeStreet_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].Street = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeCity_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].City = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbHomeState_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            ListOfContacts[CurrentAccount].State = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeZip_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            ListOfContacts[CurrentAccount].Zip = t.Text;
            dgvContacts.Refresh();
        }      

        private void dgvContacts_Leave(object sender, EventArgs e)
        {
            btnRemoveContact.Enabled = false;
        }      

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            var n = new NewContact(ListOfContacts, dgvContacts);

            n.Show();
        }
        #endregion

        
    }
}
