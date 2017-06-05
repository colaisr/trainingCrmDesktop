using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Model;

namespace Training_CRM_Application
{
    public partial class MainForm : Form
    {
        public DB dataBase = new DB(true);

        //holds the value for edit
        int CurrentAccount = 0;
        int CurrentContact = 0;
        int CurrentActivitie = 0;

        public MainForm()
        {
            InitializeComponent();
            if (File.Exists(@"c:\Training CRM DataBase\DataBase.xml"))
            {
                LoadFromFile();
            }
            else
            {
                SetSources();
            }
            SetValuesInPaneAccount(CurrentAccount);
            SetValuesInPaneContacts(CurrentContact);
            SetValuesInPaneActivitie(CurrentActivitie);        
        }
        //connects the data to the greeds
        private void SetSources()
        {
            AccountsSource = new BindingSource(dataBase.Accounts, null);
            dgvAccounts.DataSource = AccountsSource;

            ContactsSource = new BindingSource(dataBase.Contacts, null);
            dgvContacts.DataSource = ContactsSource;

            activitieSource = new BindingSource(dataBase.Activities, null);
            dgvActivities.DataSource = activitieSource;           
        }
        //saves to file on exit
        private void SaveToFile()
        {
            if (!Directory.Exists(@"c:\Training CRM DataBase"))
            {
                Directory.CreateDirectory(@"c:\Training CRM DataBase");
            }
            if (File.Exists(@"c:\Training CRM DataBase\DataBase.xml"))
            {
                File.Delete(@"c:\Training CRM DataBase\DataBase.xml");
            }
            using (TextWriter writer = new StreamWriter(@"c:\Training CRM DataBase\DataBase.xml", false))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DB));
                serializer.Serialize(writer, dataBase);
                writer.Close(); 
            }
        }
        //loads from file on launch
        private void LoadFromFile()
        {
            using (FileStream fs = new FileStream(@"c:\Training CRM DataBase\DataBase.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DB));                
                dataBase = (DB)serializer.Deserialize(fs); 
            }
            SetSources();
        }

        #region Account Tab

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dataBase.Accounts.RemoveAt(CurrentAccount);
            if (dataBase.Accounts.Count == 0)
            {
                CurrentAccount = -1;
                SetValuesInPaneAccount(CurrentAccount);

            }
            else
            {
                CurrentAccount = 0;
                SetValuesInPaneAccount(CurrentAccount);
                dgvAccounts.Rows[0].Selected = true;
            }
        }

        //controls enability of Remove
        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex!=-1)
            {
                var index = e.RowIndex;
                var row = dgvAccounts.Rows[index];
                row.Selected = true;
                CurrentAccount = index;
                SetValuesInPaneAccount(index);
               
            }
        }

        //sets edited Account
        private void SetValuesInPaneAccount(int index)
        {   //props

            if (dataBase.Accounts.Count != 0)
            {
                txtName.Enabled = true;
                cmbSource.Enabled = true;
                chkIsActive.Enabled = true;
                txtCity.Enabled = true;
                txtWebPage.Enabled = true;
                txtStreet.Enabled = true;
                cmbState.Enabled = true;
                txtZip.Enabled = true;
                rbnClosed.Enabled = true;
                rbnNew.Enabled = true;
                rbnPending.Enabled = true;

                txtName.Text = dataBase.Accounts[index].Name;
                cmbSource.Text = dataBase.Accounts[index].Source;
                chkIsActive.Checked = dataBase.Accounts[index].Active;
                txtWebPage.Text = dataBase.Accounts[index].WebPage;
                switch (dataBase.Accounts[index].Status)
                {
                    case Model.Status.New:
                        rbnNew.Checked = true;
                        break;
                    case Model.Status.Pending:
                        rbnPending.Checked = true;
                        break;
                    case Model.Status.Closed:
                        rbnClosed.Checked = true;
                        break;
                    default:
                        break;
                }
                //address
                txtCity.Text = dataBase.Accounts[index].City;
                txtStreet.Text = dataBase.Accounts[index].Street;
                cmbState.Text = dataBase.Accounts[index].State;
                txtZip.Text = dataBase.Accounts[index].Zip;

                btnRemoveAcount.Enabled = true;
            }
            else
            {
                txtName.Enabled = false;
                cmbSource.Enabled = false;
                chkIsActive.Enabled = false;
                txtCity.Enabled = false;
                txtWebPage.Enabled = false;
                txtStreet.Enabled = false;
                cmbState.Enabled = false;
                txtZip.Enabled = false;
                rbnClosed.Enabled = false;
                rbnNew.Enabled = false;
                rbnPending.Enabled = false;

                btnRemoveAcount.Enabled = false;
            }
        }

        #region Editing values

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Accounts[CurrentAccount].Name = t.Text;
            dgvAccounts.Refresh();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Accounts[CurrentAccount].Source = t.Text;
            dgvAccounts.Refresh();
        }

        private void rbnNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnNew.Checked)
            {
                dataBase.Accounts[CurrentAccount].Status = Model.Status.New;
                dgvAccounts.Refresh();
            }
            else
            {
                if (rbnPending.Checked)
                {
                    dataBase.Accounts[CurrentAccount].Status = Model.Status.Pending;
                    dgvAccounts.Refresh();
                }
                else
                {
                    dataBase.Accounts[CurrentAccount].Status = Model.Status.Closed;
                    dgvAccounts.Refresh();
                }
            }

        }

        private void txtWebPage_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Accounts[CurrentAccount].WebPage = t.Text;
            dgvAccounts.Refresh();
        }

        private void txtStreet_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Accounts[CurrentAccount].Street = t.Text;
            dgvAccounts.Refresh();
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Accounts[CurrentAccount].City = t.Text;
            dgvAccounts.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Accounts[CurrentAccount].Zip = t.Text;
            dgvAccounts.Refresh();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Accounts[CurrentAccount].State = t.Text;
            dgvAccounts.Refresh();
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            var t = sender as CheckBox;
            dataBase.Accounts[CurrentAccount].Active = t.Checked;
            dgvAccounts.Refresh();
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            var n = new NewAccount(dataBase.Accounts, dgvAccounts);

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


        #endregion 
        
        #endregion

        #region Contact tab

        private void btnRemoveContact_Click(object sender, EventArgs e)
        {
            dataBase.Contacts.RemoveAt(CurrentContact);
            if (dataBase.Contacts.Count == 0)
            {
                CurrentContact = -1;
                SetValuesInPaneContacts(CurrentContact);

            }
            else
            {
                CurrentContact = 0;
                SetValuesInPaneContacts(CurrentContact);
                dgvContacts.Rows[0].Selected = true;
            }
        }

        //controls enability of Remove
        private void dgvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex!=-1)
            {
                var index = e.RowIndex;
                var row = dgvContacts.Rows[index];
                row.Selected = true;

                CurrentAccount = index;
                SetValuesInPaneContacts(index);

                btnRemoveContact.Enabled = true; 
            }
        }

        //sets edited Contact
        private void SetValuesInPaneContacts(int index)
        {
            if (dataBase.Contacts.Count!=0)
            {
                cmbAccount.DataSource = dataBase.GetAllAccounts();
                //props
                cmbSalutation.Text = dataBase.Contacts[index].Salutation;
                cmbSalutation.Enabled = true;
                txtFirstName.Text = dataBase.Contacts[index].FirstName;
                txtFirstName.Enabled = true;
                txtLastName.Text = dataBase.Contacts[index].LastName;
                txtLastName.Enabled = true;
                cmbAccount.Text = dataBase.Contacts[index].Account;
                cmbAccount.Enabled = true;
                txtPhone.Text = dataBase.Contacts[index].Phone;
                txtPhone.Enabled = true;
                txtEmail.Text = dataBase.Contacts[index].Email;
                txtEmail.Enabled = true;



                //address
                txtHomeCity.Text = dataBase.Contacts[index].City;
                txtHomeCity.Enabled = true;
                txtHomeStreet.Text = dataBase.Contacts[index].Street;
                txtHomeStreet.Enabled = true;
                cmbHomeState.Text = dataBase.Contacts[index].State;
                cmbHomeState.Enabled = true;
                txtHomeZip.Text = dataBase.Contacts[index].Zip;
                txtHomeZip.Enabled = true;

                btnRemoveContact.Enabled = true;
            }
            else
            {
                cmbSalutation.Enabled = false;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                cmbAccount.Enabled = false;
                txtPhone.Enabled = false;
                txtEmail.Enabled = false;
                txtHomeCity.Enabled = false;
                txtHomeStreet.Enabled = false;
                cmbHomeState.Enabled = false;
                txtHomeZip.Enabled = false;
                btnRemoveContact.Enabled = false;
            }
        }

        #region Editing Values        

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].FirstName = t.Text;
            dgvContacts.Refresh();

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].LastName = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbSalutation_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Contacts[CurrentContact].Salutation = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Contacts[CurrentContact].Account = t.SelectedValue.ToString();
            dgvContacts.Refresh();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].Phone = t.Text;
            dgvContacts.Refresh();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].Email = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeStreet_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].Street = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeCity_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].City = t.Text;
            dgvContacts.Refresh();
        }

        private void cmbHomeState_TextChanged(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Contacts[CurrentContact].State = t.Text;
            dgvContacts.Refresh();
        }

        private void txtHomeZip_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Contacts[CurrentContact].Zip = t.Text;
            dgvContacts.Refresh();
        }

        #endregion

        //private void dgvContacts_Leave(object sender, EventArgs e)
        //{
        //    btnRemoveContact.Enabled = false;
        //}

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            var n = new NewContact(dataBase.Contacts, dgvContacts, dataBase.GetAllAccounts());
            n.Show();
            CurrentContact = dataBase.Contacts.Count-1;            
        }

        #endregion

        #region Activitie tab

        private void btnRemoveActivitie_Click(object sender, EventArgs e)
        {
            dataBase.Activities.RemoveAt(CurrentActivitie);
            if (dataBase.Activities.Count==0)
            {
                CurrentActivitie = -1;
                SetValuesInPaneActivitie(CurrentActivitie);
                
            }
            else
            {
                CurrentActivitie = 0;
                SetValuesInPaneActivitie(CurrentActivitie);
                dgvActivities.Rows[0].Selected = true;
            }
        }
        private void btnNewActivitie_Click(object sender, EventArgs e)
        {
            dataBase.Activities.AddNew();
            CurrentActivitie = dataBase.Activities.Count - 1;
            SetValuesInPaneActivitie(CurrentActivitie);
        }
        private void dgvActivities_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var index = e.RowIndex;
                var row = dgvActivities.Rows[index];
                row.Selected = true;
                CurrentActivitie = index;
                SetValuesInPaneActivitie(index);
                btnRemoveActivitie.Enabled = true;
            }
        }

        private void SetValuesInPaneActivitie(int index)
        {
            if (dataBase.Activities.Count!=0)//will be empty on first run
            {
                cmbContacts.DataSource = dataBase.GetAllContacts();
                cmbContacts.Text = dataBase.Activities[index].Contact;
                cmbContacts.Enabled = true;

                if (dataBase.Activities[index].Date.Year!=1)
                {
                    dtpActivitieDate.Value = dataBase.Activities[index].Date; 
                }
                dtpActivitieDate.Enabled = true;

                txtDescription.Text = dataBase.Activities[index].Description;
                txtDescription.Enabled = true;
                txtNotes.Text = dataBase.Activities[index].Notes;
                txtNotes.Enabled = true;
                btnRemoveActivitie.Enabled = true;
            }
            else
            {                
                cmbContacts.Enabled = false;

                dtpActivitieDate.Enabled = false;
                txtDescription.Enabled = false;
                txtNotes.Enabled = false;
                btnRemoveActivitie.Enabled = false;
            }
        }

        #region Editing Values        
        private void cmbContacts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var t = sender as ComboBox;
            dataBase.Activities[CurrentActivitie].Contact = t.SelectedValue.ToString();
            dgvActivities.Refresh();
        }

        private void dtpActivitieDate_ValueChanged(object sender, EventArgs e)
        {
            var t = sender as DateTimePicker;
            dataBase.Activities[CurrentActivitie].Date = t.Value;
            dgvActivities.Refresh();
        }
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Activities[CurrentActivitie].Description = t.Text;
            dgvActivities.Refresh();
        }
        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            var t = sender as TextBox;
            dataBase.Activities[CurrentActivitie].Notes = t.Text;
            dgvActivities.Refresh();
        }
        //to do : disable all-> create new..
        #endregion



        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToFile();
        }


    }
}
