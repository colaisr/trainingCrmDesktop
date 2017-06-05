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
    public partial class NewContact : Form
    {

        private DataGridView dgvContacts;
        private BindingList<Model.Contact> listOfContacts;

        private bool addClicked = false;


        public NewContact()
        {
            InitializeComponent();
        }

        public NewContact(BindingList<Model.Contact> listContacts, DataGridView dgvontacts,BindingList<string> GetAllAccounts)
        {
            this.listOfContacts = listContacts;
            this.listOfContacts.Add(new Model.Contact());
            this.dgvContacts = dgvontacts;
          
            
            InitializeComponent();
            cmbAccount.DataSource = GetAllAccounts;
        }

        private void NewContact_LocationChanged(object sender, EventArgs e)
        {
            if ((this.Left + this.Width) > Screen.AllScreens[0].Bounds.Width)
                this.Left = Screen.AllScreens[0].Bounds.Width - this.Width;

            if (this.Left < Screen.AllScreens[0].Bounds.Left)
                this.Left = Screen.AllScreens[0].Bounds.Left;

            if ((this.Top + this.Height) > Screen.AllScreens[0].Bounds.Height)
                this.Top = Screen.AllScreens[0].Bounds.Height - this.Height;

            if (this.Top < Screen.AllScreens[0].Bounds.Top)
                this.Top = Screen.AllScreens[0].Bounds.Top;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddContact();
            this.DialogResult = DialogResult.OK;
        }

        private void AddContact()
        {
            listOfContacts.Last().Salutation = cmbSalutation.Text;
            listOfContacts.Last().FirstName = txtFirstName.Text;
            listOfContacts.Last().LastName = txtLastName.Text;
            listOfContacts.Last().Account = cmbAccount.Text;
            listOfContacts.Last().Phone = txtPhone.Text;
            listOfContacts.Last().Email = txtEmail.Text;

            listOfContacts.Last().State = cmbHomeState.Text;
            listOfContacts.Last().City = txtHomeCity.Text;
            listOfContacts.Last().Zip = txtHomeZip.Text;
            listOfContacts.Last().Street = txtHomeStreet.Text;
            dgvContacts.Refresh();
            addClicked = true;
            this.Close();
        }
    }
}
